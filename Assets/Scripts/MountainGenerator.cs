using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainGenerator : MonoBehaviour
{
    [SerializeField] private GameObject climbablePrefab; // Prefab for climbable cubes
    [SerializeField] private GameObject unclimbablePrefab; // Prefab for unclimbable cubes
    [SerializeField] private int mountainHeight = 10; // Number of cubes along height
    [SerializeField] private int mountainWidth = 10; // Number of cubes along width
    [SerializeField] private int mountainDepth = 10; // Number of cubes along depth
    [SerializeField] private float cubeSize = 1f; // Size of each cube
    [SerializeField] private float unclimbableProbability = 0.3f; // Probability of making a square unclimbable (30%)
    [SerializeField] private string unclimbableLayerName = "Unclimbable"; // Layer name for unclimbable squares

    private void Start()
    {
        BuildMountain();
    }

    void BuildMountain()
    {
        // Check if the unclimbable layer exists
        int unclimbableLayer = LayerMask.NameToLayer(unclimbableLayerName);
        if (unclimbableLayer == -1)
        {
            Debug.LogError("Unclimbable layer not found. Make sure to add it in Project Settings > Tags and Layers.");
            return;
        }

        // Create the parent object for the mountain
        GameObject mountainParent = new GameObject("Mountain");

        // Calculate the starting position to center the mountain at (0, 0, 0)
        Vector3 startPosition = new Vector3(
            -mountainWidth * cubeSize / 2f,
            0, // Keep the y position at 0 for the base
            -mountainDepth * cubeSize / 2f
        );

        // Build only the outer layer of cubes
        for (int x = 0; x < mountainWidth; x++)
        {
            for (int y = 0; y < mountainHeight; y++)
            {
                for (int z = 0; z < mountainDepth; z++)
                {
                    // Check if the cube is on the outer layer
                    if (IsOuterCube(x, y, z))
                    {
                        // Determine if the cube is climbable or unclimbable
                        bool isClimbable;

                        // Always make the top and bottom layers climbable
                        if (y == 0 || y == mountainHeight - 1)
                        {
                            isClimbable = true; // Bottom and top layers are always climbable
                        }
                        else
                        {
                            // Randomly determine climbability for other layers
                            isClimbable = Random.value > unclimbableProbability;
                        }

                        // Create a new cube based on climbability
                        GameObject cube;
                        if (isClimbable)
                        {
                            cube = Instantiate(climbablePrefab, startPosition + new Vector3(x * cubeSize, y * cubeSize, z * cubeSize), Quaternion.identity);
                        }
                        else
                        {
                            cube = Instantiate(unclimbablePrefab, startPosition + new Vector3(x * cubeSize, y * cubeSize, z * cubeSize), Quaternion.identity);
                            cube.layer = unclimbableLayer; // Set the layer for unclimbable cubes
                        }

                        // Set the cube's scale
                        cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

                        // Parent the cube to the mountain object
                        cube.transform.parent = mountainParent.transform;
                    }
                }
            }
        }
    }

    // Function to determine if the cube is on the outer layer
    private bool IsOuterCube(int x, int y, int z)
    {
        return x == 0 || x == mountainWidth - 1 ||
               y == 0 || y == mountainHeight - 1 ||
               z == 0 || z == mountainDepth - 1;
    }
}
