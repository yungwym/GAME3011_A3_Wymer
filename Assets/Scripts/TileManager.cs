using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{ 
    public static TileManager tileManagerInstance;

    public GameController gameController;

    //Grid Variables 
    public Tile[,] grid;

    private float tileSize = 0.60f;

    public Transform startPosition;

    public Tile emptyTilePrefab;

    private int GridRowSize;
    private int GridColumnSize;

    private Tile selectedTileOne;
    private Tile selectedTileTwo;

    public bool b_TileOneSelected;
    public bool b_TileTwoSelected;

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

        grid = new Tile[rows, columns];

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

        SetNeighbouringTiles();
        SetItemsInGrid();
    }

    private void SetItemsInGrid()
    {
       // Tile initialTile = grid[0, 0];
        //initialTile.GetComponent<Tile>().RandomizeInitialTile();

        for (int row = 0; row < GridRowSize; row++)
        {
            for (int col = 0; col < GridColumnSize; col++)
            {
                Tile nextTile = grid[row, col];
                nextTile.GetComponent<Tile>().SetRowAndColumn(row, col);
                nextTile.GetComponent<Tile>().RandomizeStartingItem();
            }
        }
    }

    public Tile SpawnEmptyTile(int row, int col, float x, float y)
    {
        Tile newTile = Instantiate(emptyTilePrefab, new Vector2(x, y), Quaternion.identity);
        newTile.transform.parent = gameObject.transform;
        return newTile;
    }

    private void SetNeighbouringTiles()
    {
        for (int row = 0; row < GridRowSize; row++)
        {
            for (int col = 0; col < GridColumnSize; col++)
            {
                Tile currentTile = grid[row,col].GetComponent<Tile>();

                //Top -row, col
                if (GetTileAtRowAndColumn(row - 1, col))
                {
                    currentTile.topNeighbour = GetTileAtRowAndColumn(row - 1, col);
                }

                //Right row, +col
                if (GetTileAtRowAndColumn(row, col + 1))
                {
                    currentTile.rightNeighbour = GetTileAtRowAndColumn(row, col + 1);
                }

                //Bottom +row, col
                if (GetTileAtRowAndColumn(row + 1, col))
                {
                    currentTile.bottomNeighbour = GetTileAtRowAndColumn(row + 1, col);
                }

                //Right row, -col
                if (GetTileAtRowAndColumn(row, col - 1))
                {
                    currentTile.leftNeighbour = GetTileAtRowAndColumn(row, col - 1);
                }
            }
        }
    }

    private Tile GetTileAtRowAndColumn(int row, int col)
    {
        //Check if Row and Column are valid 
        if (row >= 0 && row < GridRowSize)
        {
            if (col >= 0 && col < GridColumnSize)
            {
                return grid[row, col];
            }
        }
        return null;
    }

    //Functions to Check if Tiles are matching 

    //Select Tile Function
    public void SelectTileOne(int row, int column)
    {
        Debug.Log("Tile One Selected at: Row: " + row + " Column: " + column);
        b_TileOneSelected = true;
        selectedTileOne = grid[row, column];
    }

    public void SelectTileTwo(int row, int column)
    {
        Debug.Log("Tile Two Selected at: Row: " + row + " Column: " + column);
        b_TileTwoSelected = true;
        selectedTileTwo = grid[row, column];
    }

    private void SwapTile()
    {
        
    }


}
