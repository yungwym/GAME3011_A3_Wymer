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

    public SpriteRenderer spriteIcon;

    public Sprite icon1;
    public Sprite icon2;
    public Sprite icon3;
    public Sprite icon4;
    public Sprite icon5;

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
                spriteIcon.sprite = icon1;
                break;

            case ItemType.RED:
                Debug.Log("Red");
                currentItemType = ItemType.RED;
                spriteRenderer.color = red;
                spriteIcon.sprite = icon2;
                break;

            case ItemType.GREEN:
                Debug.Log("Green");
                currentItemType = ItemType.GREEN;
                spriteRenderer.color = green;
                spriteIcon.sprite = icon3;
                break;

            case ItemType.YELLOW:
                Debug.Log("Yellow");
                currentItemType = ItemType.YELLOW;
                spriteRenderer.color = yellow;
                spriteIcon.sprite = icon4;
                break;

            case ItemType.ORANGE:
                Debug.Log("Orange");
                currentItemType = ItemType.ORANGE;
                spriteRenderer.color = orange;
                spriteIcon.sprite = icon5;
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
