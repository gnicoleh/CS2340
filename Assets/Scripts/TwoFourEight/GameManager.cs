using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DG.Tweening;
using Random = UnityEngine.Random;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _width = 4;
    [SerializeField] private int _heigth = 4;
    [SerializeField] private Node _nodePrefab;
    [SerializeField] private Block _blockPrefab;
    [SerializeField] private SpriteRenderer _boardPrefab;
    [SerializeField] private List<BlockType> _types;
    [SerializeField] private float _travelTime = 0.3f;
    [SerializeField] private int _winCondition = 2048;

    [SerializeField] private GameObject _winScreen, _loseScreen;
 
    private List<Node> _nodes;
    private List<Block> _blocks;
    private GameState _state;
    private int _round;
 
    private BlockType GetBlockTypeByValue(int value) => _types.First(t => t.Value == value);
 
    void Start()
    {
        ChangeState(GameState.GenerateLevel);
    }
 
    private void ChangeState(GameState newState)
    {
        _state = newState;
        switch (newState)
        {
            case GameState.GenerateLevel:
                SetUpGame();
                break;
            case GameState.SpawningBlocks:
                SpawnBlocks(_round++ == 0 ? 2 : 1);
                break;
            case GameState.WaitingInput:
                break;
            case GameState.Moving:
                break;
            case GameState.Win:
                _winScreen.SetActive(true);
                break;
            case GameState.Lose:
                _loseScreen.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
 
    void Update()
    {
        if (_state != GameState.WaitingInput) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) Shift(Vector2.up);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) Shift(Vector2.down);
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) Shift(Vector2.left);
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) Shift(Vector2.right);
    }
 
    void SetUpGame()
    {
        /*
        Spawns Grid. The grid is made by a list of nodes.
        A node can contain at maximum one block at a given time.
        Blocks gets added to the grid each time the user makes a move.
        */
        _round = 0;
        _nodes = new List<Node>();
        _blocks = new List<Block>();
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _heigth; y++)
            {
                var node = Instantiate(_nodePrefab, new Vector2(x, y), Quaternion.identity);
                _nodes.Add(node);
            }
        }
    
        // Centers board
        var center = new Vector2((float) _width / 2 - 0.5f, (float) _heigth / 2 - 0.5f);
        
        var board = Instantiate(_boardPrefab, center, Quaternion.identity);
        board.size = new Vector2(_width, _heigth);
    
        // Centers camera
        Camera.main.transform.position = new Vector3(center.x, center.y, -10);
    
        ChangeState(GameState.SpawningBlocks);
    }
 
     // Instantiates the blocks for each round
    void SpawnBlocks(int amount)
    {
        // Take all the free nodes in the grid, and choose a random one
        var freeNodes = _nodes.Where(n => n.OccupiedBlock == null).OrderBy(b => Random.value).ToList();
     
        foreach (var node in freeNodes.Take(amount))
        {
            SpawnBlock(node, Random.value > 0.8f ? 4 : 2);
        }
    
        // If no empty nodes are left to instantiate a new block, we lose
        // However, need to check if moving up, down, left, or right would merge and free up space
        // Alse need to check that when all blocks are on one side, and no blocks where SHIFTED, a new block should NOT be instantiated (ln 110)
        if (freeNodes.Count() == 0)
        {
            ChangeState(GameState.Lose);
            return;
        }

        ChangeState(_blocks.Any(b => b.Value == _winCondition) ? GameState.Win : GameState.WaitingInput);
    }

    // Helper method to instantiate blocks
    void SpawnBlock(Node node, int value)
    {
        var block = Instantiate(_blockPrefab, node.Pos, Quaternion.identity);
        block.Init(GetBlockTypeByValue(value));
        block.SetBlock(node);
        _blocks.Add(block);
    }

    /*
    Handles the proper order to shift the blocks depending on user input.
    - up arrow: shift from top to bottom
    - down arrow: shift from bottom to top
    - left arrow: shfit from left to right
    - right arrow: shfit from right to left
    */
    void Shift(Vector2 dir)
    {
        ChangeState(GameState.Moving);    // Prevents diagonal movement of the blocks

        var orderedBlocks = _blocks.OrderBy(b => b.Pos.x).ThenBy(b => b.Pos.y);
        if (dir == Vector2.right || dir == Vector2.up) orderedBlocks.Reverse();

        foreach (var block in orderedBlocks)
        {
            // Start with current node
            var next = block.Node;
            // Sets new node
            do
            {
                // Set curr to itself
                block.SetBlock(next);

                // Curr: "Is there another node next to me?"   NOTE: node != block (node is the container, block is the number)
                var possibleNode = GetNodeAtPosition(next.Pos + dir);
                if (possibleNode != null)
                {
                    // We know a node is present. Is it available and can our new block merge with this other block?
                    // If it's possible to merrge, set merge
                    if (possibleNode.OccupiedBlock != null && possibleNode.OccupiedBlock.CanMerge(block.Value))
                    {
                        block.MergeBlock(possibleNode.OccupiedBlock);
                    }
                    // Otherwise, can we move to this spot?
                    else if (possibleNode.OccupiedBlock == null) next = possibleNode;

                    // No new node available? End do-while loop
                }

            } while (next != block.Node); // If next != than itself, we've successfully set the block (ln 138), continue loop

        }

        // Updates each block's position to the appropriate available node previously found/set in the do-while loop
        var sequence = DOTween.Sequence();

        foreach (var block in orderedBlocks)
        {
            var movePoint = block.MergingBlock != null ? block.MergingBlock.Node.Pos : block.Node.Pos;

            sequence.Insert(0, block.transform.DOMove(movePoint, _travelTime));    // DOMove() comes from DOTween asset package
        }

        // After updating position, merge the ones that moved and can/should be merged
        sequence.OnComplete(() =>
        {
            foreach (var block in orderedBlocks.Where(b => b.MergingBlock != null))
            {
                MergeBlocks(block.MergingBlock, block);
            }

            ChangeState(GameState.SpawningBlocks);
        });

    }

    // Spawns new block of the merged blocks, with its appropriate value after the merge.
    // Destroys the two block objects that were merged
    void MergeBlocks(Block baseBlock, Block mergingBlock)
    {
        SpawnBlock(baseBlock.Node, baseBlock.Value * 2);

        RemoveBlock(baseBlock);
        RemoveBlock(mergingBlock);
    }

    void RemoveBlock(Block block)
    {
        _blocks.Remove(block);
        Destroy(block.gameObject);
    }

    Node GetNodeAtPosition(Vector2 pos)
    {
        return _nodes.FirstOrDefault(n => n.Pos == pos);
    }
}

[Serializable]
public struct BlockType
{
    public int Value;
    public Color Color;
}

public enum GameState
{
    GenerateLevel,
    SpawningBlocks,
    WaitingInput,
    Moving,
    Win,
    Lose
}
