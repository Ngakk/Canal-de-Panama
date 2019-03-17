using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Water : MonoBehaviour
{
    [HideInInspector]
    public bool isUp = true;
    public Gate left, right;

    private bool isChanging;
    public bool canUp, canDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveUp()
    {
        isChanging = true;
        LeanTween.move(gameObject, transform.position + Vector3.up * 2.5f, 2.0f).setOnComplete(OnMoveUp);
    }

    public void OnMoveUp()
    {
        isChanging = false;
        isUp = true;
    }

    public void MoveDown()
    {
        isChanging = true;
        LeanTween.move(gameObject, transform.position + Vector3.down * 2.5f, 2.0f).setOnComplete(OnMoveDown);
    }

    public void OnMoveDown()
    {
        isChanging = false;
        isUp = false;
    }

    public void UpRequest()
    {
        CheckIfCanUp();
        if (!isChanging && canUp)
        {
            MoveUp();
        }
        else
        {
            left?.CloseRequest();
        }
        
    }

    public void DownRequest()
    {
        CheckIfCanDown();
        if (canDown && !isChanging)
        {
            MoveDown();
        }
        else
        {
            right?.CloseRequest();
        }
    }

    public void CheckIfCanUp()
    {
        bool leftOpen;

        if (left == null)
            leftOpen = false;
        else
            leftOpen = left.isOpen;

        canUp = !isUp && !leftOpen;
    }

    public void CheckIfCanDown()
    {
        bool rightOpen;

        if (right == null)
            rightOpen = false;
        else
            rightOpen = right.isOpen;

        canDown = isUp && !rightOpen;
    }
}
