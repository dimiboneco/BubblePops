using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBuilder : MonoBehaviour
{
    public ColorMapper colorMapper;
    public GameObject BubblePrefab;
    public Transform InitialBubblePosition;
    public Transform InitialPlayerPosition;
    public Transform NextPlayerPosition;
    public RuleHandler Rulehandler;
    private List<Bubble> BubbleList = new List<Bubble>();
    private Bubble playerBubble;
    private Bubble nextPlayerBubble;
    private bool isIndented;

    public void BuildPlayerBubbles()
    {
        playerBubble = SpawnBubble(InitialPlayerPosition.position, Rulehandler.GenerateOne());
        playerBubble.gameObject.layer = 8;
        nextPlayerBubble = SpawnBubble(NextPlayerPosition.position, Rulehandler.GenerateOne());
        nextPlayerBubble.gameObject.layer = 8;
    }

    public void BuildBubbles(int x, int y)
    {
        for (int i=0; i<y; i++)
        {
            var myNumbers= Rulehandler.GenerateRow(x);
            isIndented = !isIndented;
            for (int j=0; j<x; j++)
            {
                var myVector = new Vector3(j, -i, 0);
                if(isIndented)
                {
                    myVector.x += 0.5f;
                }

                var currentBubble = SpawnBubble(InitialBubblePosition.position + myVector, myNumbers[j]);
                BubbleList.Add(currentBubble);
                currentBubble.CheckForCeiling(currentBubble);
            }
        }
    }

    private Bubble SpawnBubble(Vector3 position, int number)
    {
        var myGameObject = Instantiate(BubblePrefab, position, Quaternion.identity);
        var currentBubble = myGameObject.GetComponent<Bubble>();
        currentBubble.Initialize(number, colorMapper.MatchNumberToColor(number));
        return currentBubble; 
    }

    public void PushRow()
    {
        foreach(var bubble in BubbleList)
        {             
                bubble.transform.position += new Vector3(0, -1, 0);
        }
    }

    private void GenerateNewRow()
    {
        PushRow();
        BuildBubbles(6, 1);
    }


    public void Shoot(Vector2 mouseposition)
    {
        var worldposition = Camera.main.ScreenToWorldPoint(mouseposition, Camera.MonoOrStereoscopicEye.Mono);
        var direction = worldposition - InitialPlayerPosition.position;
        playerBubble.Shoot(direction.normalized);
        BubbleList.Add(playerBubble);
        StartCoroutine(SwitchPlayersCoroutine());
    }

    private IEnumerator SwitchPlayersCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        nextPlayerBubble.transform.position = InitialPlayerPosition.transform.position;
        playerBubble = nextPlayerBubble;
        nextPlayerBubble = SpawnBubble(NextPlayerPosition.position, Rulehandler.GenerateOne());
        nextPlayerBubble.gameObject.layer = 8;
    }
}
