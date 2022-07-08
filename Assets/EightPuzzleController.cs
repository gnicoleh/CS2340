using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightPuzzleController : MonoBehaviour
{
    [SerializeField] private Transform emptySpace = null;
    private Camera _camera;
    [SerializeField] private TileScript[] tiles;
    private int emptySpaceIndex = 8;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (Vector2.Distance(emptySpace.position, hit.transform.position) < 3)
                {
                    Vector2 lastEmptySpacePosition = emptySpace.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>();
                    emptySpace.position = thisTile.targetPostition;
                    thisTile.targetPostition = lastEmptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
    }

    public void Shuffle()
    {
        if (emptySpaceIndex != 8)
        {
            var tileOn8LastPos = tiles[8].targetPostition;
            tiles[8].targetPostition = emptySpace.position;
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
                var lastPos = tiles[i].targetPostition;
                int randomIndex = Random.Range(0,8);
                tiles[i].targetPostition = tiles[randomIndex].targetPostition;
                tiles[randomIndex].targetPostition = lastPos;
                var tile = tiles[i];
                tiles[i] = tiles[randomIndex];
                tiles[randomIndex] = tile;
            }
            inversion = GetInversions();
            Debug.Log("");
        } while (inversion % 2 == 0);
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

    int GetInversions()
    {
        int inversionSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = 0; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionSum += thisTileInvertion;
        }
        return inversionSum;
    }
}
