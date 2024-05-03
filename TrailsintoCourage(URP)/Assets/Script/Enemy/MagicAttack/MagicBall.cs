using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : EnemyMagic
{
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        InitialCollect();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isBossMagic)
            {
                player.TakeDamage(magician.damage);
                DestroyMagic(hitEffect);
            }
            else
            {
                player.TakeDamage(boss.longDamage);
                DestroyMagic(hitEffect);
            }
        }
        else if (other.CompareTag("Ground") || other.gameObject.name == "Fireball 1(Clone)" || other.gameObject.name == "Sword(Clone)")
        {
            DestroyMagic(hitEffect);
        }
    }
}
