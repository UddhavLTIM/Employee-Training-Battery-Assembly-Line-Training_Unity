using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBatteries : MonoBehaviour
{
    [SerializeField]
    private GameObject batteryPrefab;
    public Transform spawnPosition;
    public float spawnInterval = 10f;
    //[SerializeField]
    //private Transform endposition;
    [SerializeField]
    //private float speed = 0.1f;
    public int batteries_Number = 10;
    //public Animator animator;

    private void Start()
    {
        if (spawnPosition == null)
        {
            spawnPosition = this.transform;
        }

        StartCoroutine(spawnBateries());
    }

    private void Update()
    {
        /*var step = speed * Time.deltaTime;
        gameObject.transform.position = Vector3.MoveTowards(spawnPosition.position, endposition.position, step);*/
        
    }

    private IEnumerator spawnBateries()
    {
        for (int i = 0; i <= batteries_Number; i++)
        {
            if (i == batteries_Number)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GameObject Bat = Instantiate(batteryPrefab, spawnPosition.position, Quaternion.Euler(new Vector3(0, -90, 0)));
                Debug.Log("Battery Spawned");
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
