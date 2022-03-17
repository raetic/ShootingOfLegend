using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class towerAttack : MonoBehaviour
{
    public int towerNumber;
    [SerializeField] GameObject Atk;
    Vector3 v;
    void Start()
    {
        GetComponent<Enemy>().Hp = towerNumber * (20 + GetComponent<Enemy>().BM.stage * 5);
        StartCoroutine("Attack");
        v = new Vector3(transform.position.x, transform.position.y + 0.1f);
    }
    IEnumerator Attack()
    {
        int c = 0;
        yield return new WaitForSeconds(2);
        while (true)
        {
            c++;
            if (c % 2 == 0)
            {
                v = new Vector3(transform.position.x, transform.position.y + 0.1f);
            }
            else
            {
                v = new Vector3(transform.position.x, transform.position.y - 0.1f);
            }
            GameObject atk = Instantiate(Atk, transform.position,Quaternion.Euler(new Vector3(0,0,270)));
            atk.GetComponent<Rigidbody2D>().AddForce(Vector2.down * (150 + 50 * towerNumber));
            yield return new WaitForSeconds(1);
        }

    }
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, v, Time.deltaTime*0.2f);
    }
   
}
