using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{   // spawner setting
    public float radius;
    public float spawnerX, spawnerY, spawnerZ;
    //place chasing
    public bool grassLand, desert, volcano;
    // enemy type & count
    public GameObject[] enemy;
    public int monsterCount;
    public int maxMonsterCount;
    public float spawnTime;
    private bool isMax;
    private string currentSceneName;

    // Start is called before the first frame update
    void Start()
    {
        VariableSetting();
        InvokeRepeating(nameof(SpawnMonster), 0f, spawnTime);
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == ("Level"))
        {
            grassLand   =   true;
            desert      =   false;
            volcano     =   false;
        }
        else if (currentSceneName == ("Level2"))
        {
            //grassLand   =   false;
            //desert      =   true;
            //volcano     =   false;
        }
        else
        {
            //grassLand   =  false;
            //desert      =   false;
            //volcano     =   true;
        }
    }

    private void VariableSetting()
    {
        spawnerX = this.transform.position.x;
        spawnerY = this.transform.position.y;
        spawnerZ = this.transform.position.z;
        radius = 18;
    }
    private void SpawnMonster()
    {
        if (monsterCount == maxMonsterCount)
        {
            isMax = true;
        }
        else
        {
            isMax = false;
        }
        if (monsterCount <= maxMonsterCount && !isMax)
        {
            float randomX = Random.Range(spawnerX + radius, spawnerX - radius);
            float randomZ = Random.Range(spawnerZ + radius, spawnerZ - radius);
            Instantiate(enemy[Random.Range(0, enemy.Length)], new Vector3(randomX, spawnerY, randomZ), Quaternion.identity);
            monsterCount++;
        }
    }
    // set spawner for the center of circle to calculate random point
    public Vector3 RandomPoint()
    {
        Vector3 finalSwanPosition = Vector3.zero;
        // radius
        float randomRadius = Random.Range(-radius, radius);
        // 2d interface: a center of circle x, b center of circle y, but this is 3d so y is z 
        float centerX = spawnerX;
        float centerY = spawnerZ;
        // 2d interface: target position x, y, but this is 3d so y is z
        float randomX = Random.Range(-radius, radius);
        float randomY = Random.Range(-radius, radius);

        //circle formula ( x - a )2 + ( y - b )2 = r2 | 2 is power
        float circleRadius = Mathf.Pow(randomRadius, 2);
        float circle = Mathf.Pow(randomX - centerX, 2) + Mathf.Pow(randomY - centerY, 2);
        // if = r2 on circle, < r2 inside circle , >r2 outside circle
        if (circleRadius <= circle)
        {
            finalSwanPosition = new Vector3(randomX, spawnerY, randomY);
        }
        return finalSwanPosition;
    }
}
