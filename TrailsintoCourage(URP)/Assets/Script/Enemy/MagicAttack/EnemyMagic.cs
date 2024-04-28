using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    public float magicFlyingSpeed;
    private Vector3 shootPosition;
    private Boss boss;
    private Magician magician;
    public GameObject hitEffect; 
    private PlayerState player;
    private bool isBossMagic;
    // Start is called before the first frame update
    void Start()
    {
        player          = FindObjectOfType<PlayerState>();    
        if (this.name == "WaterBall(Clone)")
        {
            magician = FindObjectOfType<Magician>();
            shootPosition = (magician.playerCurrentPosition - transform.position).normalized;
            isBossMagic = false;
        }
        else
        {
            boss = FindObjectOfType<Boss>();
            shootPosition = (boss.playerCurrentPosition - transform.position).normalized;
            isBossMagic = true;
        }        
        Destroy(gameObject, 5f);
    }
    // Update is called once per frame
    private void Update()
    {
        Vector3 newPosition     =   transform.position + magicFlyingSpeed * Time.deltaTime * shootPosition;
        newPosition.y           =   transform.position.y;
        transform.position      =   newPosition;

    }
    private void DestroyMagic()
    {        
        Instantiate(hitEffect, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        hitEffect.SetActive(true);
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            if(!isBossMagic)
            {
                player.TakeDamage(magician.damage);
                DestroyMagic();
            }
            else
            {
                player.TakeDamage(boss.longDamage);
                DestroyMagic();
            }
        }
        else if(other.CompareTag("Ground"))
        {
            DestroyMagic();
        }
    }

}
