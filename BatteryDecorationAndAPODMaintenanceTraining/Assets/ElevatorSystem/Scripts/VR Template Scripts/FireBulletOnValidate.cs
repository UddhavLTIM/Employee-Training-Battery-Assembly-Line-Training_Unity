using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnValidate : MonoBehaviour
{
    [SerializeField] 
    protected GameObject m_bullet;
    [SerializeField]
    protected Transform m_spawnPoint;
    [SerializeField]
    protected float m_fireSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FireBullet(ActivateEventArgs args)
    {
        GameObject spawnBullet = Instantiate(m_bullet);
        spawnBullet.transform.position = m_spawnPoint.position;
        spawnBullet.GetComponent<Rigidbody>().velocity = m_spawnPoint.forward * m_fireSpeed;
        Destroy(spawnBullet, 5f);
    }
}
