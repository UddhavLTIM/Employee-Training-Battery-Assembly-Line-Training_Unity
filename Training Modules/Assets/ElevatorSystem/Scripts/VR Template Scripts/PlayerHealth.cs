/**
=====================
// license at:
Name:			PlayerHealth.cs
Version:		0.1
Update Date:	3, May, 2023
Author:			Uddhav Labde
Description:	This class check for collisions and triggers fail state
=====================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // PUBLIC
    // PROTECTED
    [SerializeField]
    protected Rigidbody m_player;
    [SerializeField]
    protected float m_impactForceThreshold;
    // PRIVATE
    // ACCESS

    // Start is called before the first frame update
    void Start()
    {
        m_player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > m_impactForceThreshold)
        {
            Debug.Log("DAMAGE " + collision.impulse.magnitude);
        }
    }
}
