using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGeneration : MonoBehaviour
{
  
    public GameObject mountainPrefab;
    public CameraController cameraController;
    public float Mheight = 10;
    public float need2Instantiate;
    float GertPos;
    float EmilyPos;
    bool hasInstantiated = false;
    int currentLevel = 1;
    void Start()
    {
        Instantiate(mountainPrefab, new Vector3(0, 0, 3), Quaternion.identity);
        need2Instantiate = Mheight;
        GertPos = cameraController.target1.transform.position.y;
        EmilyPos = cameraController.target2.transform.position.y;
    }

    void Update()
    {
        GertPos = cameraController.target1.transform.position.y;
        EmilyPos = cameraController.target2.transform.position.y;
        float highestPlayerPos = Mathf.Max(GertPos, EmilyPos);

        if (highestPlayerPos >= (currentLevel * Mheight) - 2f)
        {
            Instantiate(mountainPrefab, new Vector3(0, currentLevel * Mheight, 3), Quaternion.identity);
            currentLevel++;
        }
    }
}

