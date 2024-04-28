using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBallTripCS : MonoBehaviour
{
    //public GameObject fireballPrefab;
    public float destroyDelay;
    public float speed;
    public float coolDown;

    private void Start()
    {
        InvokeRepeating("ShootFireball", 0f, coolDown);
    }
    public void ShootFireball()
    {

        //GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        GameObject b = FireballPool.instance.GetFireball();
        b.SetActive(true);
        b.transform.position = transform.position;
        //Rigidbody fireballRigidbody = fireball.GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * speed; 
        Rigidbody rb = b.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        //Destroy(b, destroyDelay); 
    }
}
