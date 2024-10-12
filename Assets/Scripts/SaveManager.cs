using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public bool assaultRifle; // In the Sherpa, call save manager to save the assault rifle being bought
    public bool LazerRifle;
    public float maxHeight;
    public int money;
    public static SaveManager Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        Load();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveData.dat")){
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);
            if(saveData.assaultRifle == null){
                saveData.assaultRifle = false;
            }
            else{
                assaultRifle = saveData.assaultRifle;
            }
            if(saveData.LazerRifle == null){
                saveData.LazerRifle = false;
            }
            else{
                LazerRifle = saveData.LazerRifle;
            }


            maxHeight = saveData.maxHeight;
            money = saveData.money;
            file.Close();
        }
    }
    public void Save()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");
        SaveData saveData  = new SaveData();
        saveData.assaultRifle = assaultRifle;
        saveData.LazerRifle = LazerRifle;
        saveData.maxHeight = maxHeight;
        saveData.money = money;
        binaryFormatter.Serialize(file, saveData);
        file.Close();

    }
}
[Serializable]
class SaveData
{
   public bool assaultRifle;
   public bool LazerRifle;
   public float maxHeight;
   public int money;

}
