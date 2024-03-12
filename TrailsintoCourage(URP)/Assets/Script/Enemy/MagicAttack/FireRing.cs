using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    private Boss boss;
    private PlayerState player;
    private float cycleDamgeTime;
    private float cycleDamgePeriod;

    void Start()
    {
        //boss        = FindObjectOfType<Boss>();
        player      = FindObjectOfType<PlayerState>();
        cycleDamgeTime = 0.8f;
        cycleDamgePeriod = -1;
        Destroy(gameObject, 4f);

    }

    // Update is called once per frame
    void Update()
    {
        if (cycleDamgePeriod > 0)
        {
            cycleDamgePeriod -= Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && cycleDamgePeriod < 0)
        {
            player.TakeDamage(boss.longDamage);
            cycleDamgePeriod = cycleDamgeTime;

        }
    }
}
