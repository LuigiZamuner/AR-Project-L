using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCode : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Transform startPos;
    [SerializeField]
    private Transform destinationPos;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, startPos.position);
        lineRenderer.SetPosition(1, destinationPos.position);
    }
}
