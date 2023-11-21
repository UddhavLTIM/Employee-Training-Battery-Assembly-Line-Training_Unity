using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBatterySpawnner : MonoBehaviour
{
    [SerializeField]
    public GameObject batteryPrefab;
    public Transform SpawnPosition;
    //public int batteryno;
    //int i = 0;
    //public float delay = 4.19f;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPosition = this.transform;
    }

    /* Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (i >= batteryno && delay == 4.19f * timer)
        {
            this.gameObject.SetActive(false);
            Debug.Log("GameObject off!");
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        //i += 1;
        GameObject Bat = Instantiate(batteryPrefab, SpawnPosition.position, Quaternion.Euler(new Vector3(0, -90, 0)));
        Debug.Log("First Battery Spawned");
        GetComponent<Collider>().isTrigger = false;
        other.isTrigger = false;
        GetComponent<SpawnBatteries>().enabled = true;
    }
}
