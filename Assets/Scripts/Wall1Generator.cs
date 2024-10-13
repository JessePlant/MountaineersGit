using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
public class Wall1Generator : MonoBehaviour
{
    public GameObject cubePrefab, Gert;
    public Interface Location;
    public int length = 15;
    public int height = 10;
    public float spacing = 1.0f;
    private float StartHeight = 0.5f;
    public NavMeshSurface Up;
    public NavMeshSurface Left;
    public NavMeshSurface Right;
    public NavMeshSurface Back;
    public NavMeshSurface ThroughScreen;
    void Start()
    {
        Gert= GameObject.Find("Gert");

    }
    void GenWalls(float height)
    {
        // Generate the first wall (Z = 0)
        GenWall(new Vector3(0.5f, height, 0.5f), Vector3.right, length);

        // Generate the second wall (Z = 15.5), parallel to the first
        GenWall(new Vector3(0.5f, height, 14.5f), Vector3.right, length);

        // Generate the third wall (X = 0), perpendicular to the first
        GenWall(new Vector3(0.5f, height, 1.5f), Vector3.forward, length-2);

        // Generate the fourth wall (X = 15.5), parallel to the third
        GenWall(new Vector3(14.5f, height, 1.5f), Vector3.forward, length-2);
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
        if(Location.Gert.transform.position.y > 5.5f)
        {
            GenWalls(StartHeight);
            StartCoroutine(BakeNavMeshAfterGeneration());
        }
    }

        // Coroutine to wait until next frame before baking the NavMesh
    IEnumerator BakeNavMeshAfterGeneration()
    {
        // Wait until the end of the frame to ensure all cubes are instantiated
        yield return new WaitForEndOfFrame();

        // Bake the NavMesh after all objects are instantiated
        
        Debug.Log("Baking South NavMesh...");
        Left.BuildNavMesh();
        
        Debug.Log("Baking East NavMesh...");
        Right.BuildNavMesh();
        
        Debug.Log("Baking West NavMesh...");
        Back.BuildNavMesh();
        
        ThroughScreen.BuildNavMesh();
        Debug.Log("NavMeshes baked in all four directions.");
    }

    IEnumerator GenWallsCoRoutine(float height){
        GenWall(new Vector3(0.5f, height, 0.5f), Vector3.right, length);
        yield return new WaitForEndOfFrame();
        GenWall(new Vector3(0.5f, height, 14.5f), Vector3.right, length);
        yield return new WaitForEndOfFrame();
        GenWall(new Vector3(0.5f, height, 1.5f), Vector3.forward, length-2);
        yield return new WaitForEndOfFrame();
        GenWall(new Vector3(14.5f, height, 1.5f), Vector3.forward, length-2);


    }
}
