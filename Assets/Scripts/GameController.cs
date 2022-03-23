using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Tile Manager
    public TileManager tileManager;

    bool b_GameStarted;

    // Start is called before the first frame update
    void Start()
    {
        b_GameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && !b_GameStarted)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Debug.Log("Game Started");
        b_GameStarted = true;
        tileManager.GenerateEmptyGrid(15, 11);
    }
}
