using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AkQ : MonoBehaviour
{
    // Start is called before the first frame update
    bool isUp;
    void Update()
    {
        if (!isUp && transform.position.y < -4)
        {

            isUp = true;
            RigidUp();
        }
    }
    void RigidUp()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300);
    }
}
