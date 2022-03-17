using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DVR : MonoBehaviour
{

    bool isTurn;
    GameObject player;
    private void Start()
    {
      player=GameObject.FindWithTag("Player");
    
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 7&&!isTurn)
        {
            isTurn = true;
            transform.localScale = new Vector2(1, -1);
         GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        }
        if (isTurn)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 10);
            if (transform.position == player.transform.position)
                Destroy(gameObject);
        }
    }
}
