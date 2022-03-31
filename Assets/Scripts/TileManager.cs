using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TILEPOSITION
{ 
    NONE,
    TOP,
    RIGHT,
    BOTTOM,
    LEFT
}

public class TileManager : MonoBehaviour
{
    public static TileManager tileManagerInstance;

    public GameController gameController;
    public AudioManager audioManager;

    //Grid Variables 
    public Tile[,] grid;
    private float tileSize = 0.60f;

    public Transform startPosition;
    public Tile emptyTilePrefab;

    private int GridRowSize;
    private int GridColumnSize;

    //Swapping Variables 

    private Tile selectedTileOne;
    private Tile selectedTileTwo;

    private List<Tile> validTilesToSwap;

    public bool b_TileOneSelected;
    public bool b_TileTwoSelected;

    public List<Tile> matchedTilesOne;
    public List<Tile> matchedTilesTwo;

    public float timeRemaining;
    public float timeBonus;

    public int numberOfMoves = 1;

    public int score = 0;

    public bool gameOver;

    public List<ItemType> validItemTypes;


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
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.b_GameStarted)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else if (timeRemaining < 0)
            {
                StartCoroutine(GameOver());
            }

            if (numberOfMoves == 0)
            {
                StartCoroutine(GameOver());
            }

            if (b_TileOneSelected && b_TileTwoSelected)
            {
                Debug.Log("Swap");
                SwapTile(selectedTileOne, selectedTileTwo);
            }
        }
        
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
        for (int row = 0; row < GridRowSize; row++)
        {
            for (int col = 0; col < GridColumnSize; col++)
            {
                Tile nextTile = grid[row, col];
                nextTile.GetComponent<Tile>().SetRowAndColumn(row, col);
                //  nextTile.GetComponent<Tile>().RandomizeStartingItem();
                  nextTile.GetComponent<Tile>().RandomizeInitialTile();
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
                Tile currentTile = grid[row, col].GetComponent<Tile>();

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
                //Left row, -col
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
        validTilesToSwap = selectedTileOne.GetNeighbouringTiles();
        audioManager.Play("Select");
    }

    public void SelectTileTwo(int row, int column)
    {
        Debug.Log("Tile Two Selected at: Row: " + row + " Column: " + column);

        selectedTileTwo = grid[row, column];

        if (validTilesToSwap.Contains(selectedTileTwo))
        {
            Debug.Log("Valid Tile Two Selection");
            b_TileTwoSelected = true;

            audioManager.Play("Select");
        }
        else
        {
            Debug.Log("Invalid Tile Two Selection");
        }
    }

    private void SwapTile(Tile tileOne, Tile tileTwo)
    {
        GameObject itemOne = tileOne.GetCurrentItem();
        GameObject itemTwo = tileTwo.GetCurrentItem();

        tileOne.SetCurrentItem(itemTwo);
        tileTwo.SetCurrentItem(itemOne);

        selectedTileOne = null;
        selectedTileTwo = null;

        b_TileOneSelected = false;
        b_TileTwoSelected = false;

        Debug.Log("Swapped");

        numberOfMoves -= 1;

        CheckNeighbouringTiles(tileTwo);
        CheckNeighbouringTiles(tileOne);
    }

    private void CheckNeighbouringTiles(Tile tile)
    {
        List<Tile> matchedTiles = new List<Tile>();

        matchedTiles.Add(tile);

        ItemType targetItemType = tile.GetCurrentItemType();

        Tile topN = tile.GetSpecificNeighbourTile(TILEPOSITION.TOP);
        Tile rightN = tile.GetSpecificNeighbourTile(TILEPOSITION.RIGHT);
        Tile bottomN = tile.GetSpecificNeighbourTile(TILEPOSITION.BOTTOM);
        Tile leftN = tile.GetSpecificNeighbourTile(TILEPOSITION.LEFT);

        if (topN != null)
        {
            if (topN.GetCurrentItemType() == targetItemType)
            {
                Debug.Log("Match with T");
                matchedTiles.Add(topN);

                Tile topNN = topN.GetSpecificNeighbourTile(TILEPOSITION.TOP);
                if (topNN != null)
                {
                    if (topNN.GetCurrentItemType() == targetItemType)
                    {
                        Debug.Log("Match with TT");
                        matchedTiles.Add(topNN);
                    }
                }
            }
        }
       
        if (rightN != null)
        {
            if (rightN.GetCurrentItemType() == targetItemType)
            {
                Debug.Log("Match with R");
                matchedTiles.Add(rightN);

                Tile rightNN = rightN.GetSpecificNeighbourTile(TILEPOSITION.RIGHT);

                if (rightNN != null)
                {
                    if (rightNN.GetCurrentItemType() == targetItemType)
                    {
                        Debug.Log("Match with RR");
                        matchedTiles.Add(rightNN);
                    }
                }
            }
        }
        
        if (bottomN != null)
        {
            if (bottomN.GetCurrentItemType() == targetItemType)
            {
                Debug.Log("Match with B");
                matchedTiles.Add(bottomN);

                Tile bottomNN = bottomN.GetSpecificNeighbourTile(TILEPOSITION.BOTTOM);

                if (bottomNN != null)
                {
                    if (bottomNN.GetCurrentItemType() == targetItemType)
                    {
                        Debug.Log("Match with BB");
                        matchedTiles.Add(bottomNN);
                    }
                }
            }
        }
      
        if (leftN != null)
        {
            if (leftN.GetCurrentItemType() == targetItemType)
            {
                Debug.Log("Match with L");
                matchedTiles.Add(leftN);

                Tile leftNN = leftN.GetSpecificNeighbourTile(TILEPOSITION.LEFT);

                if (leftNN != null)
                {
                    if (leftNN.GetCurrentItemType() == targetItemType)
                    {
                        Debug.Log("Match with LL");
                        matchedTiles.Add(leftNN);
                    }
                }
            }
        }

        //Check for number of Matches
        if (matchedTiles.Count >= 3)
        {
            AddToScore(matchedTiles.Count * 100);
            RemoveMatchedItems(matchedTiles);

            //Add time Bonus
            timeRemaining += timeBonus;
           
        }
        else
        {
            matchedTiles.Clear();
        }
    }
    

    public void RemoveMatchedItems(List<Tile> matchedTiles)
    {
        foreach (Tile tile in matchedTiles)
        {
            tile.RemoveCurrentItem();
            Debug.Log("Deleted");
            StartCoroutine(RepopulateEmptyTile(tile));
        }
        matchedTiles.Clear();
    }

    IEnumerator RepopulateEmptyTile(Tile tile)
    {
       yield return new WaitForSeconds(0.5f);

       tile.RandomizeInitialTile();
    }

    //Set Difficulty Functions 

    public void SetDifficultyToEasy()
    {
        Debug.Log("Easy");
        SetRemainingTime(30.0f);
        SetTimeBonus(8.0f);
        SetNumberOfMoves(20);
    }

    public void SetDifficultyToMedium()
    {
        Debug.Log("Medium");
        SetRemainingTime(20.0f);
        SetTimeBonus(5.0f);
        SetNumberOfMoves(15);
    }

    public void SetDifficultyToHard()
    {
        Debug.Log("Hard");
        SetRemainingTime(10.0f);
        SetTimeBonus(2.0f);
        SetNumberOfMoves(10);
    }

    public void SetRemainingTime(float time)
    {
        timeRemaining = time;
    }

    public float GetRemainingTime()
    {
        return timeRemaining;
    }

    public void SetTimeBonus(float time)
    {
        timeBonus = time;
    }

    public void AddToScore(int scoreToAdd)
    {
        Debug.Log(score);
        score += scoreToAdd;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetNumberOfMoves(int num)
    {
        numberOfMoves = num;
    }

    public int GetNumberOfMoves()
    {
        return numberOfMoves;
    }


    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Game Over");
        gameOver = true;

        gameController.b_GameStarted = false;
    }

}
