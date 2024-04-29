using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class EnemyValue : MonoBehaviour
{
    public Animator enemyAnimator;
    // enemy state value
    public int damage;
    public float enemyHealth;
    public float attackPeriod;
    public float attackRadius;
    public float senseRadius;

    public float movingSpeed;
    public float rotateSpeed;
    public float hurtTime;
    // collect enemy position
    public float enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ;
    public Spawner spawner;
    // collect player position / state
    private GameObject player;
    public float playerCurrentPositionX, playerCurrentPositionY, playerCurrentPositionZ;
    public Vector3 playerCurrentPosition;
    public StateController stateController;
    public PlayerState playerState;
    // UI
    private Transform canvas;
    private GameObject healthBar;
    [SerializeField]
    private Slider healthSlider;

    // player attack demage
    public void EnemyHurtBySpell(Collider other, string enemyType)
    {
        if (other.gameObject.name == "Fireball 1(Clone)")
        {
            enemyHealth -= PlayerState.spellDamage;
            Debug.Log(enemyType + "_HP: " + enemyHealth);
        }
    }
    public void EnemyHurtBySword(Collider other, string enemyType)
    {
        if (other.gameObject.name == "Sword(Clone)" && hurtTime <= 0)
        {
            enemyHealth -= PlayerState.attackDamage;
            Debug.Log(enemyType + "_HP: " + enemyHealth); 
            hurtTime = 0.5f;
        }
    }
    public void EnemyDied()
    {
        if(enemyHealth <= 0)
        {
            stateController.GainExp(4);
            Destroy(gameObject, 0.5f);
        }
    }
    // collect UI object / state controller 
    public void InitialObjectCollect(GameObject enemy)
    {
        enemyAnimator = enemy.GetComponent<Animator>();
        stateController = FindObjectOfType<StateController>();
        canvas = enemy.transform.Find("Canvas");
        healthBar = canvas.Find("HPSlider").gameObject;
        healthSlider = healthBar.GetComponent<Slider>();
        playerState = FindObjectOfType<PlayerState>();
        spawner = FindObjectOfType<Spawner>();
        hurtTime = 0.5f;
    }
    // update hp UI
    public void UpdateEnemyUI(float currentValue, float max)
    {
        float current = currentValue;
        healthSlider.value = current / max;
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
        playerCurrentPosition  = new Vector3(playerCurrentPositionX, playerCurrentPositionY, playerCurrentPositionZ);
    }
    // rotate to face to player
    public void Rotation(Vector3 targetPosition, GameObject enemy, Rigidbody rb, float angle)
    {
        Vector3 directionToPlayer = targetPosition - enemy.transform.position;
        // Zero out the y component to keep the slime upright
        directionToPlayer.y = 0;

        // Create a new rotation that looks in the direction of the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust for the slime's actual forward direction if it's not the global Z-axis
        // For example, if the slime's forward is the positive X-axis, we rotate the quaternion

        // inital setting is - 90 angle, but someone moodle 0 angle is different rotation state so need to try different change
        Quaternion correctedRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y -(angle), lookRotation.eulerAngles.z);
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
    // area SerializeField using debug draw line(显示十字但实质圆形)
    public void DrawLineArea()
    {   // sense area point
        /*Vector3 senseRightward = new(enemyCurrentPositionX + senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseLeftward = new(enemyCurrentPositionX - senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + senseRadius);
        Vector3 senseBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - senseRadius);
        // sense area 十字
        Debug.DrawLine(senseBackward, senseForward, Color.blue);
        Debug.DrawLine(senseLeftward, senseRightward, Color.blue);*/
        // attack area point
        Vector3 attackRightward = new(enemyCurrentPositionX + attackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackLeftward = new(enemyCurrentPositionX - attackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + attackRadius);
        Vector3 attackBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - attackRadius);
        // attack area 十字
        Debug.DrawLine(attackForward, attackBackward, Color.red);
        Debug.DrawLine(attackLeftward, attackRightward, Color.red);
    }
}