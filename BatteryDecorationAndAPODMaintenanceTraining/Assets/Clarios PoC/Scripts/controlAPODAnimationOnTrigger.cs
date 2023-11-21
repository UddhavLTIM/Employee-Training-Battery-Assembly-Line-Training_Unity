using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Content.Interaction;

public class controlAPODAnimationOnTrigger : MonoBehaviour
{
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected OnTrigger m_trigger;
    [SerializeField]
    protected string m_animParameter;
    [SerializeField]
    protected bool m_isOn;

    // Start is called before the first frame update
    void Start()
    {
        m_trigger = this.gameObject.GetComponent<OnTrigger>();
        m_trigger.onEnter.AddListener(OnClickTask(m_animParameter, m_isOn));
    }

    private UnityAction OnClickTask(string animParameter, bool isOn)
    {
        m_animator.SetBool(animParameter, isOn);
        throw new NotImplementedException();
    }
}
