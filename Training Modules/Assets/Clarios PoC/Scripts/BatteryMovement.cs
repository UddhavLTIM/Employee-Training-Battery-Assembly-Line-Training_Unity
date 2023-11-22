using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryMovement : MonoBehaviour
{
    [SerializeField] private GameObject endposition;
    [SerializeField] private GameObject spawnPosition;
    [SerializeField] private float speed = 0.1f;

    private void Start()
    {
        spawnPosition = GameObject.Find("/Interactable_Objects/T2SpawnBatteryManager");
        endposition = GameObject.Find("/Interactable_Objects/T2EndPostion");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,endposition.transform.position)>0)
        {
            transform.position = Vector3.MoveTowards(spawnPosition.transform.position, endposition.transform.position, speed * Time.deltaTime);
        }
    }
}
