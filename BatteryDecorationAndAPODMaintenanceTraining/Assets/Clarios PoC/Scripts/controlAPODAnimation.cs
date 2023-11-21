using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class controlAPODAnimation : MonoBehaviour
{
    [SerializeField]
    protected Animator m_Animator;
    [SerializeField]
    protected Button m_Button;
    [SerializeField]
    protected string m_animParameter;
    [SerializeField]
    protected bool m_isOn;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = this.gameObject.GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickTask(m_animParameter, m_isOn));
    }

    private UnityAction OnClickTask(string animParameter, bool isOn)
    {
        m_Animator.SetBool(animParameter, isOn);
        throw new NotImplementedException();
    }
}
