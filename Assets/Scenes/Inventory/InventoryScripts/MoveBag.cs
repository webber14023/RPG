using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveBag : MonoBehaviour,IDragHandler
{
    RectTransform currentRect;
    public RectTransform canvasTranform;
    public float size;

    public void OnDrag(PointerEventData eventData)
    {
        //currentRect.anchoredPosition = eventData.position;
        currentRect.anchoredPosition += eventData.delta * size;
    }
    
    void Awake() {
        //canvasTranform = transform.parent.GetComponent<RectTransform>();
        currentRect = GetComponent<RectTransform>();
        Debug.Log(canvasTranform.lossyScale);
        Debug.Log(canvasTranform.localScale);
        size = 1080f / Screen.width;
    }
}
