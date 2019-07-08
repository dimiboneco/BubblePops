using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour
{
    public GameBuilder gameBuilder;

    public void BeginDrag(BaseEventData baseEventData)
    {
        var data = baseEventData as PointerEventData;
    }

    public void OnDrag(BaseEventData baseEventData)
    {
        var data = baseEventData as PointerEventData;
    }

    public void EndDrag(BaseEventData baseEventData)
    {
        var data = baseEventData as PointerEventData;
        gameBuilder.Shoot(data.position);
    }
}
