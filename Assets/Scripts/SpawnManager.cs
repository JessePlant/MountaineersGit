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
    public GameObject enemyPrefab, EnemyVariant1;
    public float respawnSpeed;
    Transform gertPos;
    Transform EmilyPos;
    public bool ReadyToSpawn = true;

    public List<float> respawnspeeds;

    

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        respawnSpeed = 5f;
        gertPos = cam.target1.transform;
        EmilyPos = cam.target2.transform;
        EnemyVariant1.transform.rotation = Quaternion.Euler(new Vector3(4.001f,-0.107f,-2.003f));
    }

    // Update is called once per frame
    void Update()
    {
      //print("SpawnManager "+gertPos.position.y);
      if(cam.target1.transform.position.y > 5 && ReadyToSpawn)
      {
        Vector3 spawnlocation = getPossibleSpawnLocationBelow(); 
        print("Spawning at" + spawnlocation);
        Instantiate(EnemyVariant1,spawnlocation,Quaternion.identity);
        StartCoroutine(SpawnMore());
      }

      if(GameObject.Find("FinishedZombie") == null){
        return;
      }	  
      else{
        EnemyVariant1 = GameObject.Find("FinishedZombie");
        EnemyVariant1.GetComponentInChildren<Transform>().LookAt(cam.target1.transform.position);
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
        if(UpOrDown == 0)
        {
            if(GorE == 0)
            {
               return new Vector3(gertPos.position.x, gertPos.position.y - 4f, gertPos.position.z);
            }
            else
            {

                return new Vector3(EmilyPos.position.x, EmilyPos.position.y - 4f, EmilyPos.position.z);
            }
        }
        else
        {
            if(GorE == 0)
            {

                return new Vector3(gertPos.position.x, gertPos.position.y + 4f, gertPos.position.z);
            }
            else
            {
                return new Vector3(EmilyPos.position.x, EmilyPos.position.y + 4f, EmilyPos.position.z);
            }
        }
    }

    IEnumerator SpawnMore()
    {
        ReadyToSpawn = false;
        lastPos = gertPos.position.y;
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
