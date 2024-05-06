using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPortal : MonoBehaviour
{
    public GameObject bossportal;
    public GameObject door;
    public Transform closedoorpoint;
    public Transform opendoorpoint;
    void Start()
    {
        if(Level1GameManager.IsLevel1Pass==true && Level2GameManager.IsLevel2Pass==true && Level3GameManager.IsLevel3Pass == true)
        {
            door.SetActive(false);
            bossportal.SetActive(true);
        }
        else
        {
            door.transform.position = closedoorpoint.position;
            bossportal.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            door.SetActive(false);
            bossportal.SetActive(true);
        }
    }
}
