using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandsOnInput : MonoBehaviour
{
    [SerializeField]
    protected InputActionProperty m_pinchAnimationAction;
    [SerializeField]
    protected InputActionProperty m_flexAnimationAction;
    [SerializeField]
    protected Animator m_handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float pinchValue = m_pinchAnimationAction.action.ReadValue<float>();
        float flexValue = m_flexAnimationAction.action.ReadValue<float>();
        m_handAnimator.SetFloat("Pinch", pinchValue);
        m_handAnimator.SetFloat("Flex", flexValue);
    }
}
