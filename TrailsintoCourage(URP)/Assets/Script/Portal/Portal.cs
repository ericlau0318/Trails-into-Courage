using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private float timeToTeleport = 1f;

    private float teleportTimer;
    private GameObject player;
    public bool isTeleporting;
    // Start is called before the first frame update
    void Start()
    {
        teleportTimer = timeToTeleport;
    }

    // OnTriggerEnter is called when the Collider other enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            teleportTimer = timeToTeleport; // Reset the timer
        }
    }

    // OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (teleportTimer > 0)
            {
                teleportTimer -= Time.deltaTime;
            }
            else if (!isTeleporting)
            {
                isTeleporting = true;
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isTeleporting = false;
    }
}
