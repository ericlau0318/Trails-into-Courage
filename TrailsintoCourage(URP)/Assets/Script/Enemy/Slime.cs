using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed ;
    private Rigidbody rb;
    public Animator animator;
    public GameObject player;
    //private float rotatespeed = 100f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        /*Vector3 directionToPlayer = player.transform.position - transform.position;

        // Zero out the y component to keep the slime upright
        directionToPlayer.y = 0;

        // Create a new rotation that looks in the direction of the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust for the slime's actual forward direction if it's not the global Z-axis
        // For example, if the slime's forward is the positive X-axis, we rotate the quaternion
        Quaternion correctedRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y - 90, lookRotation.eulerAngles.z);

        // Smoothly rotate the slime towards the player
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, correctedRotation, rotatespeed * Time.deltaTime));*/
    }
    public void IdleJump()
    {
        rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
    }
}
