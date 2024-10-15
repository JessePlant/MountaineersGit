using System.Collections;
using UnityEngine;

public class ChainLineRenderer : MonoBehaviour
{
    public Transform Emily;
    public Transform Gert;
    public LineRenderer lineRenderer;

    void Update()
    {
        Vector3 direction = Gert.position - Emily.position;
        Vector3 midpoint = (Emily.position + Gert.position) / 2;

        float offset = 0f;
        float negValue = -15f / 2f - 0.5f;
        float posValue = 15f / 2f - 0.5f;
            ;
        Vector3[] corners = new Vector3[] {
            new(posValue, midpoint.y, posValue),
            new(posValue, midpoint.y, negValue),
            new(negValue, midpoint.y, posValue),
            new(negValue, midpoint.y, negValue),
        };

        float [] distances = new [] {
            Vector3.Distance(midpoint, corners[0]),
            Vector3.Distance(midpoint, corners[1]),
            Vector3.Distance(midpoint, corners[2]),
            Vector3.Distance(midpoint, corners[3]),
        };

        int closest = 0;
        for (int i = 1, n = distances.Length; i < n; i++) 
        { 
            if (distances[i] < distances[closest])
            {
                closest = i;
            }
        }

        Vector3 mountMid1 = new(0, Gert.position.y,0);
        Vector3 mountMid2 = new(0, Emily.position.y, 0);
        
        if (Physics.Raycast(mountMid1, (mountMid1 - Gert.position), out var hit1) && Physics.Raycast(mountMid2, (mountMid2 - Emily.position), out var hit2))

        {
            Vector2 emily = new(Emily.position.x, Emily.position.z);
            Vector2 gert = new(Gert.position.x, Gert.position.z);
            Vector2 p = new(gert.x, emily.y);

            if (hit1.normal == hit2.normal && (Vector2.Distance(emily, p) == 0 || Vector2.Distance(gert, p) == 0))
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, Emily.position);
                lineRenderer.SetPosition(1, Gert.position);
                print("=========== Not Curved ================");
            }
            else
            {
                // Add three points to the line renderer: start, offset (bend), and end
                lineRenderer.positionCount = 3;
                lineRenderer.SetPosition(0, Emily.position);
                lineRenderer.SetPosition(1, corners[closest]);
                lineRenderer.SetPosition(2, Gert.position);
                print("=========== Curved ================");

            }

        } 
    }
}
