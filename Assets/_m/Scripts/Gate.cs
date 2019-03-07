using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    bool isOpen;

    public void Open()
    {
        if (!isOpen)
        {
            LeanTween.moveLocal(gameObject, new Vector3(0, 0, transform.localScale.z), 0.5f).setOnComplete(OnOpen);
        }
    }

    public void OnOpen()
    {

    }

    public void Close()
    {
        if (isOpen)
        {
            LeanTween.moveLocal(gameObject, new Vector3(0, 0, -transform.localScale.z), 0.5f).setOnComplete(OnClose);
        }
    }

    public void OnClose()
    {

    }
}
