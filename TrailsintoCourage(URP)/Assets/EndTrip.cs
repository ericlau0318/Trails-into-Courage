using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndTrip : MonoBehaviour
{
    public GameObject EndScene;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        DataManager.Instance.AutoSave();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("Player"))
        {
            StartCoroutine(GameEndScene());
        }
    }

    public IEnumerator GameEndScene()
    {
        yield return new WaitForSeconds(5f);
        EndScene.SetActive(true);
    }
}
