using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public float damage;
    private PlayerState player;

    void Start()
    {
        player = FindObjectOfType<PlayerState>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
