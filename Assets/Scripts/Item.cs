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

    public void SetTile(int itemInt)
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        switch (itemInt)
        {
            case 1:
                Debug.Log("Blue");
                itemType = ItemType.BLUE;
                spriteRenderer.color = Color.blue;
                break;

            case 2:
                Debug.Log("Red");
                itemType = ItemType.RED;
                spriteRenderer.color = Color.red;
                break;

            case 3:
                Debug.Log("Green");
                itemType = ItemType.GREEN;
                spriteRenderer.color = Color.green;
                break;

            case 4:
                Debug.Log("Yellow");
                itemType = ItemType.YELLOW;
                spriteRenderer.color = Color.yellow;
                break;

            case 5:
                Debug.Log("Orange");
                itemType = ItemType.ORANGE;
                spriteRenderer.color = orange;
                break;

            default:
                break;
        }

    }

}
