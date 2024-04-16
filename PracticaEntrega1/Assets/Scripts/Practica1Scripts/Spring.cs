using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    LineRenderer line;
    public Transform StartPoint;
    public Transform EndPoint;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Setpoints();
    }
    private void Setpoints()
    {
        line.SetPosition(0,StartPoint.position);
        line.SetPosition(1,EndPoint.position);
    }
}
