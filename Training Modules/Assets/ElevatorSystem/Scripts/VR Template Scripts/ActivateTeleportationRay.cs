using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField]
    protected XRRayInteractor m_LeftRay;

    [SerializeField]
    protected XRRayInteractor m_RightRay;

    [SerializeField]
    protected GameObject m_leftTeleportation;
    [SerializeField]
    protected GameObject m_rightTeleportation;

    [SerializeField]
    protected InputActionProperty m_leftActivate;
    [SerializeField]
    protected InputActionProperty m_rightActivate;

    [SerializeField]
    protected InputActionProperty m_leftCancel;
    [SerializeField]
    protected InputActionProperty m_rightCancel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //m_leftTeleportation.SetActive(m_leftCancel.action.ReadValue<float>() == 0 && m_leftActivate.action.ReadValue<float>() > 0.1f);
        //m_rightTeleportation.SetActive(m_rightCancel.action.ReadValue<float>() == 0 && m_rightActivate.action.ReadValue<float>() > 0.1f);
        bool isLeftRayHovering = m_LeftRay.TryGetHitInfo(out Vector3 leftPosition, out Vector3 leftNormal, out int leftNo, out bool leftValid);
        bool isRightRayHovering = m_RightRay.TryGetHitInfo(out Vector3 rightPosition, out Vector3 rightNormal, out int rightNo, out bool rightValid);

        m_leftTeleportation.SetActive(!isLeftRayHovering && m_leftActivate.action.ReadValue<float>() > 0.1f);
        m_rightTeleportation.SetActive(!isRightRayHovering && m_rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
