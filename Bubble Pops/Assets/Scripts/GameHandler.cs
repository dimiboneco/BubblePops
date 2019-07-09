using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameBuilder Gamebuilder;
    public UIHandler uiHandler;


    private void Start()
    {
        uiHandler.score = 0;
        uiHandler.perfectText.gameObject.SetActive(false);
        Gamebuilder.BuildBubbles(6, 8);
        Gamebuilder.BuildPlayerBubbles();
    }

    private void Update()
    {
        Gamebuilder.CheckForEmptyBoard();
    }
}
