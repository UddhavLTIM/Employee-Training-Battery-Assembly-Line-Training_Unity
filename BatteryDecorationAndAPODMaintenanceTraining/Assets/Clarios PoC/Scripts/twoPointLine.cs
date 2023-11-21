using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class twoPointLine : MonoBehaviour
{
    public Transform m_pointA;
    public Transform m_pointB;
    private LineRenderer m_line;

    // Start is called before the first frame update
    void Start()
    {
        m_line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_line.positionCount = 2;
        m_line.SetPosition(0, m_pointA.position);
        m_line.SetPosition(1, m_pointB.position);
    }
}
