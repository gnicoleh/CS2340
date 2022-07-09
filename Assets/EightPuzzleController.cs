using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
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
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 0.8)
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
            foreach(var a in tiles)
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
                Debug.Log("you won");
                for (int i = 0; i < tiles.Length; i++)
                {
                    if (tiles[i] != null)
                    {
                        tiles[i].GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
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
            Debug.Log("Shuffled.");
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
            int thisTileInversion = 0;
            for (int j = 0; j < tiles.Length; j++)
            {
                if (tiles[i] != null && tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInversion += 1;
                    }
                }
            }
            inversionsSum += thisTileInversion;
        }
        return inversionsSum;
    }

    public void showGameOverPopout() {
        gameOverTitle.text = "You Won!";
        gameOverPopout.SetActive(true);
    }

    public void restartScene()
    {
        Start();
    }

    public void sceneSwitcher()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
