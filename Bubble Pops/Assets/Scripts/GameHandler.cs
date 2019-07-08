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
        Gamebuilder.BuildBubbles(6, 8);
        Gamebuilder.BuildPlayerBubbles();
    }
}
