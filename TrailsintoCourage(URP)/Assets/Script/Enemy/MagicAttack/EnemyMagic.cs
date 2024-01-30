using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    public float magicFlyingSpeed;
    private Vector3 shootPosition;
    private Archer archer;
    private PlayerState player;
    // Start is called before the first frame update
    void Start()
    {
        player          = FindObjectOfType<PlayerState>();
        archer          = FindObjectOfType<Archer>();
        shootPosition   = (archer.playerCurrentPosition - transform.position).normalized;
        Destroy(gameObject, 5f);
    }
    // Update is called once per frame
    private void Update()
    {
        Vector3 newPosition     =   transform.position + magicFlyingSpeed * Time.deltaTime * shootPosition;
        newPosition.y           =   transform.position.y;
        transform.position      =   newPosition;

    }
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            player.TakeDamage(archer.damage);
            Destroy(gameObject);
        }
        else
        Destroy(gameObject);
    }

}
