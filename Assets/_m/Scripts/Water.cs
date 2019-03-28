using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaterState
{
    UP,
    DOWN,
    CHANGING
}

public class Water : MonoBehaviour
{
    //[HideInInspector]
    public WaterState state = WaterState.DOWN;
    public Gate left, right;
    
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
        state = WaterState.CHANGING;
        LeanTween.move(gameObject, transform.position + Vector3.up * 2.5f, 2.0f).setOnComplete(OnMoveUp);
    }

    public void OnMoveUp()
    {
        state = WaterState.UP;
    }

    public void MoveDown()
    {
        state = WaterState.CHANGING;
        LeanTween.move(gameObject, transform.position + Vector3.down * 2.5f, 2.0f).setOnComplete(OnMoveDown);
    }

    public void OnMoveDown()
    {
        state = WaterState.DOWN;
    }

    public void UpRequest()
    {
        CheckIfCanUp();
        if (canUp)
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
        if (canDown)
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
        bool leftClosed;

        if (left == null)
            leftClosed = false;
        else
            leftClosed = left.state == GateState.CLOSED;

        canUp = state == WaterState.DOWN && leftClosed;
    }

    public void CheckIfCanDown()
    {
        bool rightClosed;

        if (right == null)
            rightClosed = true;
        else
            rightClosed = right.state == GateState.CLOSED;

        canDown = state == WaterState.UP && rightClosed;
    }
}
