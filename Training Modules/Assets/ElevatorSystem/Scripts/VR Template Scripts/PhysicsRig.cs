using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    [SerializeField]
    protected Transform m_playerHead;
    /*[SerializeField]
    protected Transform m_leftController;
    [SerializeField]
    protected Transform m_rightController;

    [SerializeField]
    protected ConfigurableJoint m_headJoint;
    [SerializeField]
    protected ConfigurableJoint m_leftHandJoint;
    [SerializeField]
    protected ConfigurableJoint m_rightHandJoint;*/

    [SerializeField]
    protected CapsuleCollider m_bodyCollider;

    [SerializeField]
    protected float m_bodyHieghtMin = 0.5f;
    [SerializeField]
    protected float m_bodyHieghtMax = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // body and its collider - collider scales as per player height
        m_bodyCollider.height = Mathf.Clamp(m_playerHead.localPosition.y, m_bodyHieghtMin, m_bodyHieghtMax);
        m_bodyCollider.center = new Vector3(m_playerHead.localPosition.x, m_bodyCollider.height / 2, m_playerHead.localPosition.z);

        /*//physics joints for controllers and head joint to the main body above
        m_leftHandJoint.targetPosition = m_leftController.localPosition;
        m_leftHandJoint.targetRotation = m_leftController.localRotation;

        m_rightHandJoint.targetPosition = m_rightController.localPosition;
        m_rightHandJoint.targetRotation = m_rightController.localRotation;

        m_headJoint.targetPosition = m_playerHead.localPosition;*/
    }
}
