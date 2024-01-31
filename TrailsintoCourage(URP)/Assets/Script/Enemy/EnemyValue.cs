using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class EnemyValue : MonoBehaviour
{
    // enemy state value
    public int   damage;
    public float enemyHealth;
    public float attackPeriod;
    public float attackRadius;
    public float senseRadius;

    public float movingSpeed;
    public float rotateSpeed;
    public float hurtTime;
    private bool isdead;
    // collect enemy position
    public float enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ;
    public Spawner spawner;
    // collect player position / state
    private GameObject player;
    public float playerCurrentPositionX, playerCurrentPositionY, playerCurrentPositionZ;
    public Vector3 playerCurrentPosition;
    private StateController stateController;
    public PlayerState playerState;
    // UI
    [SerializeField]
    private Transform parentTransform;
    private Transform canvas;
    private GameObject healthBar;
    [SerializeField]
    private Slider healthSlider;
    private Level1GameManager level1GameManager;

    // player attack demage
    public void EnemyHurt(Collider other, string damageType, string enemyType, float damage)
    {
        if (other.gameObject.name == damageType)
        {
            enemyHealth -= damage;
            Debug.Log(enemyType + "_HP: " + enemyHealth);
        }
    }
    public void EnemyOneHurt(Collider other, string damageType, string enemyType, float damage)
    {
        if (other.gameObject.name == damageType)
        {
            enemyHealth -= damage;
            Debug.Log(enemyType + "_HP: " + enemyHealth);
        }
    }
    public void EnemyDied()
    {
        if (enemyHealth <= 0 && !isdead)
        {
            isdead = true;
            level1GameManager.AddKilledCount();
            stateController.GainExp(4);
            spawner.monsterCount--;
            Destroy(gameObject, 0.5f);

        }
    }
    // collect UI object / state controller 
    public void InitialObjectCollect(GameObject enemy)
    {
        stateController     =   FindObjectOfType<StateController>();
        parentTransform     =   enemy.transform;
        canvas              =   parentTransform.Find("Canvas");
        healthBar           =   canvas.transform.Find("HPSlider").gameObject;
        healthSlider        =   healthBar.GetComponent<Slider>();
        level1GameManager   =   FindObjectOfType<Level1GameManager>();
        playerState         =   FindObjectOfType<PlayerState>();
        spawner             =   FindObjectOfType<Spawner>();
        hurtTime            =   1;
        isdead              =   false;
    }
    // update hp UI
    public void UpdateEnemyUI(float currentValue, float max)
    {
        float current       = currentValue;
        healthSlider.value  = current / max;
    }
    // collect enemy & player position
    public void UpdateCurrentPosition(GameObject enemy)
    {   // enemy position
        enemy = this.gameObject;
        enemyCurrentPositionX = enemy.transform.position.x;
        enemyCurrentPositionY = enemy.transform.position.y;
        enemyCurrentPositionZ = enemy.transform.position.z;

        // player position
        player = GameObject.FindGameObjectWithTag("Player");
        playerCurrentPositionX = player.transform.position.x;
        playerCurrentPositionY = player.transform.position.y;
        playerCurrentPositionZ = player.transform.position.z;
        playerCurrentPosition = new Vector3(playerCurrentPositionX, playerCurrentPositionY, playerCurrentPositionZ);
    }
    // rotate to face to player
    public void Rotation(Vector3 targetPosition, GameObject enemy,Rigidbody rb)
    {
        Vector3 directionToPlayer = targetPosition - enemy.transform.position;
        // Zero out the y component to keep the slime upright
        directionToPlayer.y = 0;

        // Create a new rotation that looks in the direction of the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust for the slime's actual forward direction if it's not the global Z-axis
        // For example, if the slime's forward is the positive X-axis, we rotate the quaternion
        Quaternion correctedRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y - 90, lookRotation.eulerAngles.z);
        // Smoothly rotate the slime towards the player
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, correctedRotation, rotateSpeed * Time.deltaTime));
    }
    // checking someing inside the circle or not 
    public bool DetectCircleArea(float radius)
    {
        bool inside = false;
        // radius
        float r = radius;
        // 2d interface: a center of circle x, but this is 3d so y is z
        float centerX = enemyCurrentPositionX;
        float centerY = enemyCurrentPositionZ;
        // 2d interface: player x, b center of circle y, but this is 3d so y is z 
        float x = playerCurrentPositionX;
        float y = playerCurrentPositionZ;

        //circle formula ( x - a )2 + ( y - b )2 = r2 | 2 is power
        // if = r2 on circle, < r2 inside circle , >2 outside circle
        float circleRadius = Mathf.Pow(r, 2);
        float circle = Mathf.Pow(x - centerX, 2) + Mathf.Pow(y - centerY, 2);

        if (circle <= circleRadius)
        {
            inside = true;
        }
        else if (circle > circleRadius)
        {
            inside = false;
        }
        return inside;
    }
    public void RandomCirclePoint()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        float randomRadius = Random.Range(0f, spawner.radius);

        float x = Mathf.Cos(randomAngle) * randomRadius;
        float z = Mathf.Sin(randomAngle) * randomRadius;

        Vector3 swanTargetPosition = new Vector3(spawner.spawnerX, spawner.spawnerY, spawner.spawnerZ) + new Vector3(x, enemyCurrentPositionY, z);
        Debug.Log(swanTargetPosition);
    }
    // area SerializeField using debug draw line(��ʾʮ�ֵ�ʵ��Բ��)
    public void DrawLineArea()
    {   // sense area point
        Vector3 senseRightward = new(enemyCurrentPositionX + senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseLeftward = new(enemyCurrentPositionX - senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + senseRadius);
        Vector3 senseBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - senseRadius);
        // sense area ʮ��
        Debug.DrawLine(senseBackward, senseForward, Color.blue);
        Debug.DrawLine(senseLeftward, senseRightward, Color.blue);
        // attack area point
        Vector3 attackRightward = new(enemyCurrentPositionX + attackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackLeftward = new(enemyCurrentPositionX - attackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + attackRadius);
        Vector3 attackBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - attackRadius);
        // attack area ʮ��
        Debug.DrawLine(attackForward, attackBackward, Color.red);
        Debug.DrawLine(attackLeftward, attackRightward, Color.red);
    }
}