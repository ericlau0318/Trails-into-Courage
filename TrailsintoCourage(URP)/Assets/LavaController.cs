using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LavaController : MonoBehaviour
{
    public  Transform lastSavePoint;
    public PlayerState playerState;
    public GameObject EndScene;
    public float lavadamage;
    void Start()
    {
        lastSavePoint.position = new Vector3(87f,15f,174f);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag==("Player"))
        {
            Debug.Log("Player Lava");
            playerState.TakeDamage(lavadamage);
            if (playerState.currentHealth <= 0)
            {
                EndScene.SetActive(true);
            }
            other.gameObject.transform.position = lastSavePoint.position;
        }
    }

    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        DataManager.Instance.AutoSave();
    }
}
