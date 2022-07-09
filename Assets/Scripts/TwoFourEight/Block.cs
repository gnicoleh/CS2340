using TMPro;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Value;
    public Node Node;
    public Block MergingBlock;    // non-null block that is in the process of merging, no need to reset since it gets destroyed after merging
    public bool Merging;
    public Vector2 Pos => transform.position;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private TextMeshPro _text;
    public void Init(BlockType type)
    {
        Value = type.Value;
        _renderer.color = type.Color;
        _text.text = type.Value.ToString();
    }

    public void SetBlock(Node node)
    {
        if (Node != null) Node.OccupiedBlock = null;
        Node = node;
        Node.OccupiedBlock = this;
    }

    public void MergeBlock(Block blockToMergeWith)
    {
        // Set the block we are merging with
        MergingBlock = blockToMergeWith;

        // Set current node as unoccupied to allow other blocks to use it
        Node.OccupiedBlock = null;

        // Set the base block as merging, so it does not get used more than once    e.i.: [2][2][2] == [4][2] != [6]
        MergingBlock.Merging = true;
    }

    public bool CanMerge(int value) => value == Value && !Merging && MergingBlock == null;
}
