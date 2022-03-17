using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    int counter = 0;
    public void Shoot(GameObject p,Vector3 v)
    {
        GameObject bul = Instantiate(p, transform.position, transform.rotation);
      
        bul.GetComponent<Rigidbody2D>().AddForce(v *300);
        counter++;
        if (counter == 12) Destroy(gameObject);
    }
}
