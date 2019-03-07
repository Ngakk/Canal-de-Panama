using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canal : MonoBehaviour
{
    public GameObject gatePrefab;
    public GameObject waterPrefab;
    public GameObject boatPrefab;

    public int NumeroDeCompuertas = 3;
    public float wOffset = 10;
    public float hOffset = 3;

    private List<Gate> gates;
    private List<Water> waters;
    private Vector3 start;
    private Vector3 end;


    // Start is called before the first frame update
    void Start()
    {
        //Creating start water
        GameObject go = Instantiate(waterPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
