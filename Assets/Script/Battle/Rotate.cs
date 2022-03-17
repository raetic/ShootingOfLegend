using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed;
    Transform tra;
    float z;
    public bool x;
    public bool y;
    void Start()
    {
        tra = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        z += Time.deltaTime * speed;
        if(x) tra.transform.rotation = Quaternion.Euler(new Vector3(z, 0, 0));
        else if(y) tra.transform.rotation = Quaternion.Euler(new Vector3(0, z, 0));
        else
        tra.transform.rotation = Quaternion.Euler(new Vector3(0, 0,z));
    }
}
