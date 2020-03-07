using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContentList : MonoBehaviour
{
    public AppManager manager;
    public GenericFrame frame;
    private Information information;
    public Text titulo;

    public Information Information
    {
        get
        {
            return (information);
        }
        set
        {
            information = value;
        }
    }

    public void GetFrame()
    {
        frame.Information = information;
        manager.NextFrame(frame.GetComponent<RectTransform>());
        manager.CleanBarra();
    }
}
