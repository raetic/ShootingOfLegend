using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dmg : MonoBehaviour
{
    Color color;
    public TextMeshProUGUI t;
    float time;
    Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<TextMeshProUGUI>();
       if(GetComponentInChildren<Image>())
        image = GetComponentInChildren<Image>();
        Invoke("Disappear", 2f);
    }
    private void Update()
    {
       time += Time.deltaTime;
     
        if (image != null)
        {
            t.color = new Color(1, 1, 0, 1 - time / 2);
            image.color = new Color(1, 1, 1, 1 - time / 2); }
        else
        {
            t.color = new Color(1, 1, 1, 1 - time / 2);
        }
        transform.position = new Vector2(transform.position.x, transform.position.y + time/150);
    }
    void Disappear() {

        Destroy(gameObject);
    }
  
}
