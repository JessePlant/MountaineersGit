using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall1Generator : MonoBehaviour
{
    public GameObject cubePrefab;
    public int length = 15;
    public int height = 10;
    public float spacing = 1.0f;
    void Start()
    {
        GenWalls();
    }
    void GenWalls()
    {
        // Generate the first wall (Z = 0)
        GenWall(new Vector3(0.5f, 0.5f, 0.5f), Vector3.right, length);

        // Generate the second wall (Z = 15.5), parallel to the first
        GenWall(new Vector3(0.5f, 0.5f, 14.5f), Vector3.right, length);

        // Generate the third wall (X = 0), perpendicular to the first
        GenWall(new Vector3(0.5f, 0.5f, 1.5f), Vector3.forward, length-2);

        // Generate the fourth wall (X = 15.5), parallel to the third
        GenWall(new Vector3(14.5f, 0.5f, 1.5f), Vector3.forward, length-2);
    }
    void GenWall(Vector3 startPosition, Vector3 direction, int length)
    {
        for(int y =0; y<height; y++)
        {
            for (int i = 0; i < length; i++)
            {
                // Calculate the position of each block along the wall
                Vector3 position = startPosition + direction * i * spacing + Vector3.up * y * spacing;

                // Instantiate a new block at the calculated position
                Instantiate(cubePrefab, position, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
