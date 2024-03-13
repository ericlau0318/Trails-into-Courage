using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallTripCS : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float destroyDelay;
    public float speed;
    public float coolDown;
    private void Start()
    {
        InvokeRepeating("ShootFireball", 0f, coolDown);
    }
    public void ShootFireball()
    {

        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        Rigidbody fireballRigidbody = fireball.GetComponent<Rigidbody>();
        fireballRigidbody.velocity = transform.forward * speed; 

        Destroy(fireball, destroyDelay); 
    }
}
