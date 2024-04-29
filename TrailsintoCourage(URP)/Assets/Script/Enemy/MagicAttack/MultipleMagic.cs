using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleMagic : EnemyMagic
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
        Vector3 newPosition             =   transform.position + magicFlyingSpeed * Time.deltaTime * transform.forward;
        newPosition.y                   =   transform.position.y;
        transform.position              =   newPosition;
        Vector3 flatShootPosition       =   new Vector3(shootPosition.x, 0, shootPosition.z).normalized;
        Quaternion targetRotation       =   Quaternion.LookRotation(flatShootPosition);
        transform.rotation              =   targetRotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(boss.longDamage);
            DestroyMagic(hitEffect);
        }
        else if (other.CompareTag("Ground"))
        {
            DestroyMagic(hitEffect);
        }
        else if(other.gameObject.name == "FireBall 1(Clone)")
        {
            DestroyMagic(hitEffect);
        }
    }
}
