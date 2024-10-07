using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public CameraController cam;
    public CharacterController character;
    public GameObject enemyPrefab;
    public float respawnSpeed;
    Vector3 gertPos;
    Vector3 EmilyPos;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
        character = GameObject.Find("Gert").GetComponent<CharacterController>();
        respawnSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Normalspawn()
    {
        // Instantiate(enemyPrefab, NEED TO GET LOCATION SOMEHOW, Quaternion.identity);
        yield return new WaitForSeconds(respawnSpeed);
    }

    Vector3 getPossibleSpawnLocationOpposite()
    {
        return Vector3.up;
    }
    Vector3 getPossibleSpawnLocationBelow()
    {
        gertPos = cam.target1.transform.position;
        EmilyPos = cam.target2.transform.position;
        return Vector3.up;
    }
}
