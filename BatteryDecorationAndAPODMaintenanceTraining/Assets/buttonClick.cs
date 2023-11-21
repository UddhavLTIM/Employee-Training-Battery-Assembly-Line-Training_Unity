using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonClick : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(TaskonClick);
    }

    private void TaskonClick()
    {
        animator.SetBool("isConveyorOpen", true);
    }

}
