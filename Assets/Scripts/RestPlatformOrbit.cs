using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestPlatformOrbit : MonoBehaviour
{
    public float orbitDistance = 15f; // Distance from the origin
    public float orbitSpeed = 20f; // Speed of orbiting
    private float angle; // Current angle for the orbit

    private void Start()
    {
        // Initialize the angle based on the prefab's starting position
        angle = Random.Range(0f, 2 * Mathf.PI);
    }

    private void Update()
    {
        // Update the angle based on the orbit speed and time
        angle += orbitSpeed * Time.deltaTime;

        // Calculate the new position
        float x = orbitDistance * Mathf.Cos(angle);
        float z = orbitDistance * Mathf.Sin(angle);

        // Update the position of the prefab
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
