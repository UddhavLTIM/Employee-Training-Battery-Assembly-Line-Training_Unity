using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnStickers : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabGameObject;

    [SerializeField]
    private Transform SpawnLocation;

    private XRSocketInteractor SocketInteractor;

    // Start is called before the first frame update
    void Start()
    {
        SocketInteractor = GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (SocketInteractor.selectTarget == null)
        {
           Instantiate(prefabGameObject,SpawnLocation.position,SpawnLocation.rotation,null);
        }
    }
}
