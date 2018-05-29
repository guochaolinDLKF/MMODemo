using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class UIDragView : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Action OnDragBegin;
    public Action<Vector2> OnDragging;
    public Action OnDragEnd;


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (OnDragBegin != null)
        {
            OnDragBegin();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragging != null)
        {
            OnDragging(eventData.delta);
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnDragEnd != null)
        {
            OnDragEnd();
        }
    }

}
