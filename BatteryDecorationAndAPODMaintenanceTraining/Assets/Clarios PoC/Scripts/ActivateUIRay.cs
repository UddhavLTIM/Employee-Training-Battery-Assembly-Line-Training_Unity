using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateUIRay : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_leftUIRay;
    [SerializeField]
    protected GameObject m_rightUIRay;

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
        m_leftUIRay.SetActive(m_leftActivate.action.ReadValue<float>() > 0.1f);
        m_rightUIRay.SetActive(m_rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
