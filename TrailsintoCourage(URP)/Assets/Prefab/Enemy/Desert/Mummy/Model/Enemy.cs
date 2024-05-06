using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    //public PlayerState player;
    public Collider modelCollider;
    public Transform point1;
    public Transform point2;
    public float movingSpeed = 5f;
    /*private Transform target;
    private bool movingToPoint1;*/
    // Start is called before the first frame update
    private void Start()
    {
        //target = point1;
        //movingToPoint1 = true;
        //player = FindObjectOfType<PlayerState>();
    }
    // Update is called once per frame
    private void Update()
    {

        // Move towards the current target
        /*transform.position = Vector3.MoveTowards(transform.position, target.position, movingSpeed * Time.deltaTime);

        // Check if the target is reached
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // Toggle the target
            if (movingToPoint1)
            {
                target = point2;
                movingToPoint1 = false;
            }
            else
            {
                target = point1;
                movingToPoint1 = true;
            }
        }*/
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("IsAttack");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the model's collider");
            //PlayerState.TakeDamage(archer.damage);
        }
    }
}
