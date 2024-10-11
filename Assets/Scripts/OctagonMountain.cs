using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctagonMountain : MonoBehaviour
{
    public GameObject cubePrefab; // Assign the cube prefab in the Inspector
    public float radius = 4f; // Distance from the center to each cube
    private int numberOfSides = 8;
    private int cubesPerSide = 3;
    public float cubeSize = 2f;
    Vector3 placement = new Vector3(0, 0, 1);
    private int sideLength = 1;
    private int height;
    // Start is called before the first frame update
    void Start()
    {
        float angleIncrement = 360f / numberOfSides;  // Angle between each side of the octagon
        Vector3 placement = Vector3.zero;             // Initial position
        float currentAngle = 0f;

        for (int j = 0; j < numberOfSides; j++)
        {
            // Calculate the direction for the current side
            Vector3 direction = new Vector3(Mathf.Cos(currentAngle * Mathf.Deg2Rad), 0, Mathf.Sin(currentAngle * Mathf.Deg2Rad));

            // Calculate rotation to align cubes along the current side
            Quaternion cubeRotation = Quaternion.LookRotation(direction);

            for (int i = 0; i < cubesPerSide; i++)
            {
                // Calculate the position of the cube along the side
                Vector3 offset = direction * (i * sideLength);
                Instantiate(cubePrefab, placement + offset, cubeRotation);  // Rotate each cube to align with the side
            }

            // Move to the start of the next side
            placement += direction * (cubesPerSide * sideLength);
            currentAngle += angleIncrement;  // Rotate to the next side
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void BuildOctagon()
    {
        // Starting position at the center
        Vector3 position = Vector3.zero;

        // Initial direction pointing right (positive X-axis)
        Vector3 direction = Vector3.right;

        // Angle between sides in radians (45 degrees)
        float angle = 45f;

        // Total number of sides in an octagon
        int sides = 8;

        // Store the positions for seamless connection between sides

        // Loop through each side of the octagon
        for (int i = 0; i < sides; i++)
        {
            // Place cubes along the current side
            for (int j = 0; j < cubesPerSide; j++)
            {
                // Instantiate a cube at the current position
                Quaternion rotation = Quaternion.LookRotation(direction);
                Instantiate(cubePrefab, position, rotation);

                // Move to the next position along the current direction
                position += direction * cubeSize;
            }

            // Update the last position for the next side

            // Rotate the direction vector by 45 degrees for the next side
            direction = Quaternion.Euler(0, angle, 0) * direction;

            // Reset position to the last cube to start the next side
        }
    }
}
