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
    private ItemType currentItemType;

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
                currentItemType = ItemType.BLUE;
                spriteRenderer.color = blue;
                break;

            case ItemType.RED:
                Debug.Log("Red");
                currentItemType = ItemType.RED;
                spriteRenderer.color = red;
                break;

            case ItemType.GREEN:
                Debug.Log("Green");
                currentItemType = ItemType.GREEN;
                spriteRenderer.color = green;
                break;

            case ItemType.YELLOW:
                Debug.Log("Yellow");
                currentItemType = ItemType.YELLOW;
                spriteRenderer.color = yellow;
                break;

            case ItemType.ORANGE:
                Debug.Log("Orange");
                currentItemType = ItemType.ORANGE;
                spriteRenderer.color = orange;
                break;

            default:
                Debug.Log("Default");
                break;
        }
    }

    public ItemType GetItemType()
    {
        return currentItemType;
    }

}
