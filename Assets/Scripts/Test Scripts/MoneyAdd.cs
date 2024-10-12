using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAdd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            print("Adding Money");
            SaveManager.Instance.money += 100;
            SaveManager.Instance.Save();
        }
        if(Input.GetKeyDown(KeyCode.N)){
            SaveManager.Instance.money -= 100;
            SaveManager.Instance.Save();
        }
    }
}
