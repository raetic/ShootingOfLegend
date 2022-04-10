using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainManager : MonoBehaviour
{
    Data data=new Data();
    public void GoBattle()
    {
        Debug.Log("a");


        SceneManager.LoadScene(1);
    }
    void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "Data.json");
        if (File.Exists(path))
        {
            string battleData = File.ReadAllText(path);
            data = JsonUtility.FromJson<Data>(battleData);
            
        }
    }


   public void SelectCharacter(int i)
    {


        data.curCharacter = i;
      
        string battleData = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "Data.json");
        File.WriteAllText(path, battleData);
         
    }
}
public class Data
{
    public int blue;
    public int curCharacter;
}
