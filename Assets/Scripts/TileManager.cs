using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public static TileManager tileManagerInstance;

    public GameController gameController;

    //Grid Variables 
    public GameObject[,] grid;

    private float tileSize = 0.60f;

    public Transform startPosition;

    public GameObject emptyTilePrefab;

    private int GridRowSize;
    private int GridColumnSize;

    private void Awake()
    {
        if (tileManagerInstance != null)
        {
            return;
        }
        tileManagerInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateEmptyGrid(int rows, int columns)
    {
        GridRowSize = rows;
        GridColumnSize = columns;

        grid = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Debug.Log("Tile at Row: " + row + " , Column: " + col);

                float posY = (startPosition.position.y) - row * tileSize;
                float posX = (startPosition.position.x) + col * tileSize;

                grid[row, col] = SpawnEmptyTile(row, col, posX, posY);
            }
        }
    }

    public GameObject SpawnEmptyTile(int row, int col, float x, float y)
    {
        GameObject newTile = Instantiate(emptyTilePrefab, new Vector2(x, y), Quaternion.identity);
        newTile.transform.parent = gameObject.transform;
        return newTile;
    }

    //Functions to Check if Tiles are matching 
}
