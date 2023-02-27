using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public float initialHeight = 6; 
    private RectTransform rectTransform;

    public Transform startingAnchor;
    public Transform endingAnchor;
    
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetLength(float length)
    {
        rectTransform.sizeDelta = new Vector2(length, initialHeight);
    }

    public void SetRotation(float rotation)
    {
        rectTransform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void UpdateAnchoring(float scaleFactor)
    {
        var startingPos = startingAnchor.transform.position / scaleFactor;
        var endingPos = endingAnchor.transform.position / scaleFactor;

        var rotation = Mathf.Atan2(endingPos.y - startingPos.y, endingPos.x - startingPos.x) * Mathf.Rad2Deg;
        
        SetLength(Vector2.Distance(startingPos, endingPos));
        SetRotation(rotation);
    }
}
