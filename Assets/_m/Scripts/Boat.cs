using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Transform frontPoint;
    public float Speed = 2.0f;
    public float StopDistance = 2.5f;
    public int myInstanceId;

    private Rigidbody rigi;
    private Vector3 dir = Vector3.zero;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        dir = transform.right;
        myInstanceId = GetInstanceID();
    }

    // Update is called once per frame
    void Update()
    {
        ThrowRaycast();
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }*/
    }

    public void ThrowRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(frontPoint.position, dir, out hit, StopDistance))
        {
            if (isMoving) Stop();

            if (hit.collider.CompareTag("Gate"))
            {
                Gate gate = hit.collider.gameObject.GetComponent<Gate>();

                if (gate.state == GateState.OPEN && !isMoving)
                {
                    Move();
                    gate.CrossingRequest(myInstanceId);
                }
                else if (gate.state == GateState.CLOSED && isMoving) { Stop(); }

                if (!isMoving)
                    gate.OpenRequest();
            }

            Debug.DrawRay(frontPoint.position, dir * StopDistance, Color.red);

        }
        else
        {
            Move();
            Debug.DrawRay(frontPoint.position, dir * StopDistance, Color.green);
        }
        
    }

    public void Stop()
    {
        rigi.velocity = Vector3.Scale(rigi.velocity, Vector3.up);
        isMoving = false;
    }

    public void Move()
    {
        rigi.velocity = dir.normalized * Speed;
        isMoving = true;
    }
}

//TODO: que pueda ir para arriba y luego para abajo el barco