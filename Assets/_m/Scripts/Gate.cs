using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateState
{
    OPEN,
    CLOSED,
    CHANGING,
    CROSSING
}

public class Gate : MonoBehaviour
{
    //[HideInInspector]
    public GateState state = GateState.CLOSED;
    public GameObject model;
    public Water left, right;

    private bool canOpen = true;
    private List<int> boatsCrossing = new List<int>();

    public void Start()
    {
        CheckIfCanOpen();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.O)) Open();
        if (Input.GetKeyDown(KeyCode.C)) Close();*/

    }

    public void Open()
    {
        if (state == GateState.CLOSED)
        {
            state = GateState.CHANGING;
            LeanTween.moveLocal(model, new Vector3(0, 0, 10), 2.0f).setOnComplete(OnOpen);
        }
    }

    public void OnOpen()
    {
        state = GateState.OPEN;
    }

    public void Close()
    {
        if (state == GateState.OPEN)
        {
            state = GateState.CHANGING;
            LeanTween.moveLocal(model, new Vector3(0, 0, 0), 2.0f).setOnComplete(OnClose);
        }
    }

    public void OnClose()
    {
        state = GateState.CLOSED;
    }

    private void CheckIfCanOpen()
    {
        if (state == GateState.CHANGING)
        {
            canOpen = false;
            return;
        }

        bool leftUp, rightDown;

        if (left == null) leftUp = true;
        else leftUp = left.state == WaterState.UP;

        if (right == null) rightDown = true;
        else rightDown = right.state == WaterState.DOWN;

        canOpen = leftUp && rightDown;
    }

    public void OpenRequest()
    {
        CheckIfCanOpen();
        if (canOpen)
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
        if (state == GateState.OPEN && boatsCrossing.Count == 0)
        {
            Close();
        }
    }

    private void LevelWater()
    {
        left?.UpRequest();
        right?.DownRequest();
    }

    public void CrossingRequest(int _id)
    {
        if (!boatsCrossing.Contains(_id))
        {
            boatsCrossing.Add(_id);
            Debug.Log("Adding crossing request with id " + _id + " to " + gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            int otherId = other.GetComponentInParent<Boat>().myInstanceId;
            if (!boatsCrossing.Contains(otherId))
            {
                boatsCrossing.Add(otherId);
                Debug.Log("Adding crossing request with id " + otherId + " to " + gameObject.name);
            }

            Debug.Log("Boat entered from " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boat"))
        {
            int otherId = other.GetComponentInParent<Boat>().myInstanceId;
            if (boatsCrossing.Contains(otherId))
            {
                boatsCrossing.Remove(otherId);
                Debug.Log("Removing crossing request with id " + otherId + " from " + gameObject.name);
            }
            Debug.Log("Boat exited from " + gameObject.name);
        }
    }
}
