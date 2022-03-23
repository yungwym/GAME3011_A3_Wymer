using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeStartingItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomizeStartingItem()
    {
        int randomItemInt = Random.Range(1, 5);
        itemPrefab.GetComponent<Item>().SetTile(randomItemInt);
    }
}
