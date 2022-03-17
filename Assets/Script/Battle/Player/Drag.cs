using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour
{
    public GameObject P;
    private Vector3 m_Offset;
    private float m_ZCoord;
    Player pl;
    bool isDrag;
    Touch touch;
    Vector3 v3;

    private void Start()
    {
        P = GameObject.FindWithTag("Player");
        pl = P.GetComponent<Player>();
    }

    private void Update()
    {      
        if (Input.GetMouseButtonDown(0) && !isDrag && GetMouseWorldPosition().y > -3.6f|| Input.GetMouseButtonDown(0) && !isDrag && GetMouseWorldPosition().x >0)
        {
            if (Input.touchCount < 2)
            {

                if (Application.platform == RuntimePlatform.WindowsEditor)
                { v3 = GetMouseWorldPosition();
                    
                }
                if(Application.platform == RuntimePlatform.Android)
                {
             
                  touch = Input.GetTouch(0);
                  v3=  new Vector3((touch.position.x - 540) / 194, (touch.position.y - 960) / 194);
                }

              
                isDrag = true;
                m_ZCoord = Camera.main.WorldToScreenPoint(P.transform.position).z;
                m_Offset = P.transform.position - v3;

            }
        }
    

    
            if (isDrag&&!pl.isStun)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
                v3 = GetMouseWorldPosition();
            if (Application.platform == RuntimePlatform.Android)
            {
                touch = Input.GetTouch(0);
                v3 = new Vector3((touch.position.x - 540) / 194, (touch.position.y - 960) / 194);
            }
            P.transform.position = Vector3.MoveTowards(P.transform.position, new Vector3((v3 + m_Offset).x, (v3 + m_Offset).y,0), pl.speed * Time.deltaTime);
                if (P.transform.position.x < -2.4)
                    P.transform.position = new Vector3(-2.4f, P.transform.position.y, 0);
                if (P.transform.position.x > 2.4)
                    P.transform.position = new Vector3(2.4f, P.transform.position.y, 0);
                if (P.transform.position.y < -3)
                    P.transform.position = new Vector3(P.transform.position.x, -3f, 0);
                if (P.transform.position.y > 4.3)
                    P.transform.position = new Vector3(P.transform.position.x, 4.3f, 0);
            
        }
       if (Input.GetMouseButtonUp(0) )
        {
        isDrag=false;
        }
    }

  
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}



