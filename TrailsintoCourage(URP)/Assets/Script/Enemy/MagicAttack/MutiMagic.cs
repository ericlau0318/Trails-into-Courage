using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutiMagic : EnemyMagic
{
    private Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        player              =   FindObjectOfType<PlayerState>();
        boss                =   FindObjectOfType<Boss>();
        magicFlyingSpeed    =   10;
        Destroy(gameObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition     =   transform.position + magicFlyingSpeed * Time.deltaTime * transform.forward;
        newPosition.y           =   transform.position.y;
        transform.position      =   newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(boss.longDamage);
            DestroyMagic();
        }
        else if (other.CompareTag("Ground"))
        {
            DestroyMagic();
        }
    }
}
