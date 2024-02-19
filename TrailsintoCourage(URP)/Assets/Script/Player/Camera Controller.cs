using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private GameObject player;
    private PlayerController playerController;
    private Transform FPP_FP;
    private Transform TPP_FP;
    public bool firstPersonPerspective;
    public float rotationSpeed = 10f;
    private float mouseX;
    private float mouseY;
    public float maxMouseY = 60f;
    public float minMouseY = -60f;

    private float distance;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        FPP_FP = playerController.FPPfixationPoint;
        TPP_FP = playerController.TPPfixationPoint;
        if (FPP_FP == true)
        {
            distance = Vector3.Distance(transform.position, FPP_FP.position);
        }
        else
        {
            distance = Vector3.Distance(transform.position, TPP_FP.position);
        }
    }

    private void Update()
    {
        if (PlayerController.isPlayerTalking == false && PlayerState.StatePanelOpen==false)
        {
            if (player == null)
            {
                return;
            }
            mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
            mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            mouseY = Mathf.Clamp(mouseY, minMouseY, maxMouseY);
        }
            Quaternion rotation = Quaternion.Euler(mouseY, mouseX, 0f);
            transform.rotation = rotation;
            if (firstPersonPerspective == true)
            {
                transform.position = FPP_FP.position;
                float cameraAngleY = transform.eulerAngles.y;
                player.transform.rotation = Quaternion.Euler(player.transform.eulerAngles.x, cameraAngleY, player.transform.eulerAngles.z);
            }

            if (firstPersonPerspective == false)
            {
                Vector3 negDistance = new Vector3(0f, 0f, -distance);
                Vector3 position = rotation * negDistance + TPP_FP.position;
                transform.position = position;
            }
        
    }
}
