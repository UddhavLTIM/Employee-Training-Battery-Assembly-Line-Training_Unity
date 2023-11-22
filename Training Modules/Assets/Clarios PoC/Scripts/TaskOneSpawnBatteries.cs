using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskOneSpawnBatteries : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_batteryPrefab;
    [SerializeField]
    protected Transform m_spawnPosition;
    [SerializeField]
    protected float m_spawnInterval;
    [SerializeField]
    protected int m_batteriesToSpawn;

    //[SerializeField]
    //private Transform endposition;
    //[SerializeField]
    //private float speed = 0.1f;
    //public Animator animator;

    private void Start()
    {
        if (m_spawnPosition == null)
        {
            m_spawnPosition = this.transform;
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
        for (int i = 0; i <= m_batteriesToSpawn; i++)
        {
            if (i == m_batteriesToSpawn)
            {
                gameObject.SetActive(false);
            }
            else
            {
                GameObject Battery = Instantiate(m_batteryPrefab, m_spawnPosition.position, Quaternion.Euler(new Vector3(0, -90, 0)));
                //Debug.Log("Battery Spawned");
                yield return new WaitForSeconds(m_spawnInterval);
            }
        }
    }
}
