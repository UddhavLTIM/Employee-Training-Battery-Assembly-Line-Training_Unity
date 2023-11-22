using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstConveyorAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnManager;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (spawnManager.gameObject.active == false)
        {
                anim.SetBool("isConveyorOpen", false);
        }
    }
}
