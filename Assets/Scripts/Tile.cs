using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    NONE,
    EMPTY,
    FULL
}

public class Tile : MonoBehaviour
{
    public GameObject itemPrefab;
    public BoxCollider2D boxCollider2D;
    private TileManager tileManager;
    public Transform centerPosition;

    //Tile Info
    public int RowNum;
    public int ColumnNum;
    public TileType tileType;

    //Neigbor Tiles 
    public Tile topNeighbour;
    public Tile rightNeighbour;
    public Tile bottomNeighbour;
    public Tile leftNeighbour;

    //Initial Item
    private GameObject currentItem;
    private ItemType currentItemType;

    public List<Tile> neighbourTiles;
    public List<ItemType> validItemTypes;

    public List<Tile> sameTypeNeighbours;

    public List<Tile> matches;
    public List<Tile> verticalMatches;

    public int numOfMatches = 0;

    // Start is called before the first frame update
    void Start()
    {
        tileManager = TileManager.tileManagerInstance;

        validItemTypes = new List<ItemType>();
}

    // Update is called once per frame
    void Update()
    {
        //Get Input 
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(mousePosition.x, mousePosition.y);

            if (boxCollider2D.bounds.Contains(mousePos))
            {
                if (tileManager.b_TileOneSelected != true)
                {
                    tileManager.SelectTileOne(GetRow(), GetColumn());
                }
                else if (tileManager.b_TileOneSelected == true)
                {
                    tileManager.SelectTileTwo(GetRow(), GetColumn());
                }
            }
        }
    }

    private void SetupValidItemTypes()
    {
        validItemTypes.Add(ItemType.BLUE);
        validItemTypes.Add(ItemType.GREEN);
        validItemTypes.Add(ItemType.ORANGE);
        validItemTypes.Add(ItemType.RED);
        validItemTypes.Add(ItemType.YELLOW);

        neighbourTiles.Add(topNeighbour);
        neighbourTiles.Add(leftNeighbour);
        neighbourTiles.Add(bottomNeighbour);
        neighbourTiles.Add(rightNeighbour);

        tileType = TileType.FULL;
    }

    public void RandomizeStartingItem()
    {
        SetupValidItemTypes();

        currentItem = Instantiate(itemPrefab, centerPosition.position, Quaternion.identity);

        currentItem.transform.SetParent(transform);

        int randomItemInt = Random.Range(0, validItemTypes.Count);
        ItemType randomItemType = validItemTypes[randomItemInt];

        currentItem.GetComponent<Item>().SetTile(randomItemType);

        currentItemType = currentItem.GetComponent<Item>().GetItemType();
    }
    
  public void RandomizeInitialTile()
  {
      SetupValidItemTypes();

        foreach (Tile tile in neighbourTiles)
        {       
            if (tile != null)
            {
                ItemType tempItem = tile.GetCurrentItemType();
                validItemTypes.Remove(tempItem);
            }
        }
        
        currentItem = Instantiate(itemPrefab, centerPosition.position, Quaternion.identity);
        currentItem.transform.SetParent(transform);

        int randomItemInt = Random.Range(0, validItemTypes.Count);
        ItemType randomItemType = validItemTypes[randomItemInt];

        currentItem.GetComponent<Item>().SetTile(randomItemType);
        currentItemType = currentItem.GetComponent<Item>().GetItemType();
    }
  
    public void RemoveCurrentItem()
    {
        Destroy(currentItem.gameObject);
        tileType = TileType.EMPTY;
    }


    //Getters and Setters

    public List<Tile> GetNeighbouringTiles()
    {
        return neighbourTiles;
    }

    public GameObject GetCurrentItem()
    {
        return currentItem;
    }

    public ItemType GetCurrentItemType()
    {
        return currentItemType;
    }

    public void SetCurrentItem(GameObject item)
    {
        currentItem = item;
        currentItem.transform.SetParent(transform);
        currentItem.transform.position = centerPosition.transform.position;

        currentItemType = currentItem.GetComponent<Item>().GetItemType();
    }

    public void SetRowAndColumn(int row, int column)
    {
        RowNum = row;
        ColumnNum = column;
    }

    public int GetRow()
    {
        return RowNum;
    }

    public int GetColumn()
    {
        return ColumnNum;
    }

    
    public Tile GetSpecificNeighbourTile(TILEPOSITION TILEPOS)
    {
        switch (TILEPOS)
        {
            case TILEPOSITION.NONE:

                return null;
            case TILEPOSITION.TOP:

                return topNeighbour;
            case TILEPOSITION.RIGHT:

                return rightNeighbour;
            case TILEPOSITION.BOTTOM:

                return bottomNeighbour;
            case TILEPOSITION.LEFT:

                return leftNeighbour;
            default:
                return null; 
        }
    }
}


