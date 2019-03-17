using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [HideInInspector]
    public bool isOpen;
    public GameObject model;
    public Water left, right;

    private bool canOpen = true;
    private bool isChanging = false;

    public void Start()
    {
        CheckIfCanOpen();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) Open();
        if (Input.GetKeyDown(KeyCode.C)) Close();

    }

    public void Open()
    {
        if (!isOpen)
        {
            isChanging = true;
            LeanTween.moveLocal(model, new Vector3(0, 0, 10), 2.0f).setOnComplete(OnOpen);
        }
    }

    public void OnOpen()
    {
        isOpen = true;
        isChanging = false;
    }

    public void Close()
    {
        if (isOpen)
        {
            isChanging = true;
            LeanTween.moveLocal(model, new Vector3(0, 0, 0), 2.0f).setOnComplete(OnClose);
        }
    }

    public void OnClose()
    {
        isOpen = false;
        isChanging = false;
    }

    private void CheckIfCanOpen()
    {
        bool leftUp, rightUp;

        if (left == null) leftUp = true;
        else leftUp = left.isUp;

        if (right == null) rightUp = false;
        else rightUp = right.isUp;

        canOpen = leftUp && !rightUp;
    }

    public void OpenRequest()
    {
        CheckIfCanOpen();
        if (canOpen && !isChanging)
        {
            Open();
        }
        else
        {
            LevelWater();
        }
    }

    public void CloseRequest()
    {
        if (isOpen)
        {
            Close();
        }
    }

    private void LevelWater()
    {
        left?.UpRequest();
        right?.DownRequest();
    }
}
