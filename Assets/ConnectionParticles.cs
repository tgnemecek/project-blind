using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionParticles : MonoBehaviour
{
    public GameObject line;
    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = line.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3[] positions = new Vector3[99];
        //int positionCount = lineRenderer.positionCount;
        //lineRenderer.GetPositions(positions);

        //if (positions.Length < 3) return;

        //gameObject.transform.position = positions[0];

        //for (int i = 0; i < positions.Length; i++)
        //{
        //    if (positions[i].x == positions[i+1].x)
        //    {

        //    }
        //}
    }
}
