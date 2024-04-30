using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    public float magicFlyingSpeed;
    public Vector3 shootPosition;
    public Boss boss;
    public Magician magician;
    public PlayerState player;
    public bool isBossMagic;
    public void InitialCollect()
    {
        player = FindObjectOfType<PlayerState>();
        Destroy(gameObject, 5f);
        magicFlyingSpeed = 10;
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
    }
    public void DestroyMagic(GameObject hitEffect)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        this.gameObject.SetActive(false);
        hitEffect.SetActive(true);
        Destroy(gameObject, 0.5f);
    }
    public void Moving()
    {
        Vector3 newPosition = transform.position + magicFlyingSpeed * Time.deltaTime * shootPosition;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        Vector3 flatShootPosition = new Vector3(shootPosition.x, 0, shootPosition.z).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(flatShootPosition);
        transform.rotation = targetRotation;
    }
}
