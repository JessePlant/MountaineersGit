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
    void Start()
    {
        Instantiate(mountainPrefab, new Vector3(0, 0, 2), Quaternion.identity);
        need2Instantiate = Mheight;
        GertPos = cameraController.target1.transform.position.y;
        EmilyPos = cameraController.target2.transform.position.y;
    }

    void Update()
    {
        GertPos = cameraController.target1.transform.position.y;
        EmilyPos = cameraController.target2.transform.position.y;

        if ((GertPos >= (need2Instantiate-2) || EmilyPos >= (need2Instantiate - 2)) && !hasInstantiated)
        {
            Instantiate(mountainPrefab, new Vector3(0, need2Instantiate, 2), Quaternion.identity);
            need2Instantiate += Mheight;
            hasInstantiated = true;
        }
        if (GertPos < (need2Instantiate - 2) && EmilyPos < (need2Instantiate - 2))
        {
            hasInstantiated = false;
        }
    }
}

