using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public void ToggleGameObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
