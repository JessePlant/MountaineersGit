using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float lastPos;
    float incrSpawnHeight = 30;
    public CameraController cam;
    public GameObject enemyPrefab;
    public float respawnSpeed;
    Vector3 gertPos;
    Vector3 EmilyPos;
    public bool ReadyToSpawn = true;

    

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        respawnSpeed = 5f;
        gertPos = cam.target1.transform.position;
        EmilyPos = cam.target2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      print("SpawnManager "+gertPos.y);
      if(cam.target1.transform.position.y > 5 && ReadyToSpawn)
      {
        print("Spawning");
        Vector3 spawnlocation = getPossibleSpawnLocationBelow();
        Instantiate(enemyPrefab,spawnlocation,Quaternion.identity);
        StartCoroutine(SpawnMore());
      }  
    }
    IEnumerator Normalspawn()
    {
        // Instantiate(enemyPrefab, NEED TO GET LOCATION SOMEHOW, Quaternion.identity);
        yield return new WaitForSeconds(respawnSpeed);
    }

    Vector3 getPossibleSpawnLocationOpposite(Transform Gert)
    {
       return Vector3.up;
    }
    
    Vector3 getPossibleSpawnLocationBelow()
    {

        int UpOrDown = Random.Range(0, 2);
        print("UpOrDown: "+UpOrDown);
        int GorE = Random.Range(0, 2);
        print("GorE: "+GorE);	
        Vector3 spawnLocation;
        if(UpOrDown == 0)
        {
            if(GorE == 0)
            {
                gertPos.y -= 4f;
                spawnLocation = gertPos;
                return spawnLocation;
            }
            else
            {
                EmilyPos.y -= 4f;
                spawnLocation = EmilyPos;
                return spawnLocation;
            }
        }
        else
        {
            if(GorE == 0)
            {
                gertPos.y += 5;
                spawnLocation = gertPos;
                return spawnLocation;
            }
            else
            {
                EmilyPos.y += 5;
                spawnLocation = EmilyPos;
                return spawnLocation;
            }
        }
    }

    IEnumerator SpawnMore()
    {
        ReadyToSpawn = false;
        lastPos = gertPos.y;
        yield return new WaitForSecondsRealtime(respawnSpeed);
        ReadyToSpawn = true;
        increaseRespawnSpeed();
    }
    void increaseRespawnSpeed()
    {
        if (lastPos >=incrSpawnHeight)
        {
            respawnSpeed -= 0.5f;
            incrSpawnHeight += 10;
        }
    }
}
