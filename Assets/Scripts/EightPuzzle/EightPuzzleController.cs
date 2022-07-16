using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EightPuzzleController : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera camera;
    [SerializeField] private TileScript[] tiles;
    private int emptySpaceIndex = 8;
    public int correctTiles = 0;
    public GameObject gameOverPopout;
    public TMP_Text gameOverTitle;
    private bool isFinished = false;


    [SerializeField] private Button restartBtn;
    [SerializeField] private Button howToBtn;
    [SerializeField] private Button mainMenuBtn;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        enableTile();
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 1.1)
                {
                    Vector2 lastEmptySapcePosition = emptySpace.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>();
                    emptySpace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySapcePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
        int correctTiles = 0;
        if (!isFinished)
        {
            foreach (var a in tiles)
            {
                if (a != null)
                {
                    if (a.inRightPlace)
                    {
                        correctTiles += 1;
                    }
                }
            }
            if (correctTiles == tiles.Length - 1)
            {
                disableTile();
                isFinished = true;
                if (isFinished == true)
                {
                    showGameOverPopout();
                }
            }
        }
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptySpace.position;
            emptySpace.position = tileOn8LastPos;
            tiles[emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            emptySpaceIndex = 8;
        }
        int inversion;
        do
        {
            for (int i = 0; i < 8; i++)
            {
                var lastPos = tiles[i].targetPosition;
                int randomIndex = Random.Range(0, 7);
                tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                tiles[randomIndex].targetPosition = lastPos;
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }
            inversion = GetInversions();
        } while (inversion % 2 != 0);
    }

    public int findIndex(TileScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                int thisTileInversion = 0;
                for (int j = i; j < tiles.Length; j++)
                {
                    if (tiles[j] != null)
                    {
                        if (tiles[i].number > tiles[j].number)
                        {
                            thisTileInversion += 1;
                        }
                    }
                }
                inversionsSum += thisTileInversion;
            }
        }
        return inversionsSum;
    }

    public void showGameOverPopout()
    {
        //gameOverTitle.text = "You Won!";
        gameOverPopout.SetActive(true);
    }

    public void enableTile()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                tiles[i].GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    public void disableTile()
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null)
            {
                tiles[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void disableForRestartBtn()
    {
        howToBtn.interactable = false;
        mainMenuBtn.interactable = false;
    }

    public void enableForRestartBtn()
    {
        howToBtn.interactable = true;
        mainMenuBtn.interactable = true;
    }

    public void disableForHowToBtn()
    {
        restartBtn.interactable = false;
        mainMenuBtn.interactable = false;
    }
    public void enableForHowToBtn()
    {
        restartBtn.interactable = true;
        mainMenuBtn.interactable = true;
    }

    public void disableForMainMenuBtn()
    {
        howToBtn.interactable = false;
        restartBtn.interactable = false;
    }
    public void enableForMainMenuBtn()
    {
        howToBtn.interactable = true;
        restartBtn.interactable = true;
    }


    public void restartScene()
    {
        Start();
    }
}
