using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        transform.position += magicFlyingSpeed * Time.deltaTime * transform.forward;
        Vector3 flatShootPosition = new Vector3(transform.position.x, 0, transform.position.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(flatShootPosition);
        transform.rotation = targetRotation;
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
    }
}
