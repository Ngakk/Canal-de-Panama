using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public float Speed = 2.0f;

    private Rigidbody rigi;
    private Vector3 dir = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody>();
        dir = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Stop();
        }
    }

    public void Stop()
    {
        Debug.Log("Stop");
        rigi.velocity = Vector3.zero;
    }

    public void Move()
    {
        Debug.Log("Start");
        rigi.velocity = dir.normalized * Speed;
    }
}
