using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
{
    public delegate void SwipeRightHandler();
    public delegate void SwipeLeftHandler();
    public delegate void SwipeDownHandler();
    public delegate void SwipeUpHandler();


    public static event SwipeRightHandler OnSwipeRight;
    public static event SwipeLeftHandler OnSwipeLeft;
    public static event SwipeUpHandler OnSwipeUp;
    public static event SwipeDownHandler OnSwipeDown;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var deltaSwipe = eventData.delta;
        if(Mathf.Abs(deltaSwipe.x)> Mathf.Abs(deltaSwipe.y))
        {
            if (deltaSwipe.x > 0f)
                OnSwipeRight();
            else
                OnSwipeLeft();
        }
        else
        {
            if (deltaSwipe.y > 0f)
                OnSwipeUp();
            else
                OnSwipeDown();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.StartGame();
    }
}
