using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskOneStartButtonClick : MonoBehaviour
{
    [SerializeField]
    protected Animator m_Animator;
    [SerializeField]
    protected Button m_Button;

    // Start is called before the first frame update
    void Start()
    {
        m_Button = this.gameObject.GetComponent<Button>();
        m_Button.onClick.AddListener(OnClickTask);
    }

    private void OnClickTask()
    {
        m_Animator.SetBool("isConveyorOn", true);
    }
}
