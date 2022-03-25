using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    NONE,
    BLUE,
    RED,
    GREEN,
    YELLOW,
    ORANGE
}


public class Item : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private ItemType itemType;

    public Color blue;
    public Color red;
    public Color green;
    public Color yellow;
    public Color orange;
   

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
               
    }

    public void SetTile(ItemType itemType)
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (itemType)
        {
            case ItemType.BLUE:
                Debug.Log("Blue");
                itemType = ItemType.BLUE;
                spriteRenderer.color = blue;
                break;

            case ItemType.RED:
                Debug.Log("Red");
                itemType = ItemType.RED;
                spriteRenderer.color = red;
                break;

            case ItemType.GREEN:
                Debug.Log("Green");
                itemType = ItemType.GREEN;
                spriteRenderer.color = green;
                break;

            case ItemType.YELLOW:
                Debug.Log("Yellow");
                itemType = ItemType.YELLOW;
                spriteRenderer.color = yellow;
                break;

            case ItemType.ORANGE:
                Debug.Log("Orange");
                itemType = ItemType.ORANGE;
                spriteRenderer.color = orange;
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

}
