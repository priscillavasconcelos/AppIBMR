using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    private RectTransform myRect;
    private Vector3 myInitialPosition;
    void Start()
    {
        myRect = GetComponent<RectTransform>();
        myInitialPosition = myRect.position;
    }

    public virtual void Active()
    {
        myRect.position = Vector3.zero;
    }

    public virtual void Deactive()
    {
        myRect.position = myInitialPosition;
    }
}
