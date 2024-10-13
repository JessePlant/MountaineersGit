using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MountainGenerator : MonoBehaviour
{
    [SerializeField] private GameObject climbablePrefab; // Prefab for climbable cubes
    [SerializeField] private GameObject unclimbablePrefab; // Prefab for unclimbable cubes
    [SerializeField] private GameObject restPrefab; // Prefab for rest platform cubes
    [SerializeField] private int mountainHeight = 10; // Number of cubes along height
    [SerializeField] private int mountainWidth = 10; // Number of cubes along width
    [SerializeField] private int mountainDepth = 10; // Number of cubes along depth
    [SerializeField] private float cubeSize = 1f; // Size of each cube
    [SerializeField] private float unclimbableProbability = 0.3f; // Probability of making a square unclimbable (30%)
    [SerializeField] private string unclimbableLayerName = "Unclimbable"; // Layer name for unclimbable squares
    [SerializeField] private float restRate = 0.1f; // Rest rate to calculate the number of rest platforms

    // BoxCollider settings for unclimbable cubes
    [SerializeField] private Vector3 colliderSizeMultiplier = new(1f, 1f, 1f); // Size multiplier for the BoxCollider
    [SerializeField] private Vector3 colliderCenterOffset = Vector3.zero; // Offset for the BoxCollider center

    public NavMeshSurface leftNavMesh;
    public NavMeshSurface rightNavMesh;
    public NavMeshSurface frontNavMesh;
    public NavMeshSurface backNavMesh;
    private void Start()
    {
        BuildMountain();
        CreateRestPlatforms();
        PlaceCharacters();
        Rebake();
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
        GameObject mountainParent = new("Mountain");

        // Calculate the starting position to center the mountain at (0, 0, 0)
        Vector3 startPosition = new(
            -mountainWidth * cubeSize / 2f,
            0.5f, // Keep the y position at 0 for the base
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

                            // Adjust the BoxCollider for unclimbable cubes
                            BoxCollider boxCollider = cube.GetComponent<BoxCollider>();
                            if (boxCollider == null)
                            {
                                boxCollider = cube.AddComponent<BoxCollider>();
                            }

                            // Increase the size of the BoxCollider
                            boxCollider.size = Vector3.Scale(boxCollider.size, colliderSizeMultiplier);

                            // Adjust the center of the BoxCollider to encapsulate the cube properly
                            boxCollider.center = colliderCenterOffset;
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

    void CreateRestPlatforms()
    {
        // Calculate the number of rest platforms for each side
        int restPlatformsPerSide = Mathf.FloorToInt(0.1f * mountainHeight);

        // Define the positions for the rest platforms on each side
        Vector3 startPosition = new Vector3(
            -mountainWidth * cubeSize / 2f,
            0.5f,
            -mountainDepth * cubeSize / 2f
        );

        // Left face (x = 0)
        for (int i = 0; i < restPlatformsPerSide; i++)
        {
            int randomHeight = restPlatformsPerSide * (i + 1);
            float randomDepth = Random.Range(-mountainDepth / 2f + 1.5f, mountainDepth / 2f - 1.5f);
            Vector3 position = new Vector3(startPosition.x - 0.5f, randomHeight, randomDepth);
            Instantiate(restPrefab, position, Quaternion.identity);
        }

        // Right face (x = mountainWidth - 1)
        for (int i = 0; i < restPlatformsPerSide; i++)
        {
            int randomHeight = restPlatformsPerSide * (i + 1);
            float randomDepth = Random.Range(-mountainDepth / 2f + 1.5f, mountainDepth / 2f - 1.5f);
            Vector3 position = new Vector3(startPosition.x + mountainWidth - 0.5f, randomHeight, randomDepth);
            Instantiate(restPrefab, position, Quaternion.identity);
        }

        // Front face (z = 0)
        for (int i = 0; i < restPlatformsPerSide; i++)
        {
            int randomHeight = restPlatformsPerSide * (i + 1);
            float randomWidth = Random.Range(-mountainWidth / 2f + 1.5f , mountainWidth / 2f - 1.5f);
            Vector3 position = new Vector3(randomWidth, randomHeight, startPosition.z - 0.5f);
            Instantiate(restPrefab, position, Quaternion.identity);
        }

        // Back face (z = mountainDepth - 1)
        for (int i = 0; i < restPlatformsPerSide; i++)
        {
            int randomHeight = restPlatformsPerSide * (i + 1);
            float randomWidth = Random.Range(-mountainWidth / 2f + 1.5f, mountainWidth / 2f - 1.5f); ;
            Vector3 position = new Vector3(randomWidth, randomHeight, startPosition.z + mountainDepth - 0.5f);
            Instantiate(restPrefab, position, Quaternion.identity);
        }
    }

    void PlaceCharacters()
    {

    }

    // Function to determine if the cube is on the outer layer
    private bool IsOuterCube(int x, int y, int z)
    {
        return x == 0 || x == mountainWidth - 1 ||
               y == 0 || y == mountainHeight - 1 ||
               z == 0 || z == mountainDepth - 1;
    }
    void Rebake()
    {
        leftNavMesh.BuildNavMesh();
        rightNavMesh.BuildNavMesh();
        frontNavMesh.BuildNavMesh();
        backNavMesh.BuildNavMesh();
    }
}
