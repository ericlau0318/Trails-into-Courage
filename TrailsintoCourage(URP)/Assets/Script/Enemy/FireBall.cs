using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : MonoBehaviour
{
    public float damage;
    private PlayerState player;
    private Rigidbody rb;
    public bool waitingForRemove;
    public float life;
    void Start()
    {
        player = FindObjectOfType<PlayerState>();
        rb = GetComponent<Rigidbody>();
        waitingForRemove = false;
    }
    public void resultDefault()
    {
        waitingForRemove = false;
        life = 2.0f;
    }
    private void Update()
    {
        life -= Time.deltaTime;
        if (life < 0)
        {
            if (!waitingForRemove)
            {
                waitingForRemove=true;
                Invoke("removeSelf", 0.5f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Player")
        {
            player.TakeDamage(damage);
            //Destroy(gameObject);
            if (!waitingForRemove)
            {
                waitingForRemove= true;
                removeSelf();
                Debug.Log("123");
            }
        }
    }

    public void removeSelf()
    {
        FireballPool.instance.ReleaseFireball(gameObject);
    }
}
