using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Searcher;
using UnityEngine;

public class Boss : EnemyValue
{
    private readonly string boss = "BOSS";
    private Rigidbody rb;
    // attack mood/state
    [SerializeField]
    private int shortDamage;
    public int longDamage, longSpecialDamage;
    private float longAttackRadius, shortAttackRadius;
    public GameObject fireRing, magicBall, mutiMagicBall;
    public Vector3 magicPosition01, magicPosition02, magicPosition03;

    // enemy setting
    [SerializeField]
    private float longAttackTime, longSpecialAttackTime, shortAttackTime;
    private float longAttackPeriod, shortAttackPeriod, longSpecialAttackPeriod;
    [SerializeField]
    private bool isAttack, inLongAttackArea, switchLongMod;
    // UI hp
    public float maxHealth;
    public float currentHealth;
    public float rotate;
    public float recoverHealth;
    // Start is called before the first frame update
    void Start()
    {
        InitialBoss();
        InitialObjectCollect(this.gameObject);
        switchLongMod = true;
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = enemyHealth;
        hurtTime -= Time.deltaTime;
        DrawLine();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        EnemyDied(exp);
    }
    // Boss setting / component
    private void InitialBoss()
    {
        enemyHealth                 =       100;
        recoverHealth               =       3;
        exp                         =       20;
        movingSpeed                 =       4;
        shortDamage                 =       8;
        longSpecialDamage           =       7;
        longDamage                  =       5;
      
        longAttackPeriod            =       2;
        longSpecialAttackPeriod     =       4;
        shortAttackPeriod           =       2;       
        longAttackRadius            =       14;
        shortAttackRadius           =       1.5f;

        rotateSpeed                 =       125f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

        isAttack            =   false;
        inLongAttackArea    =   false;
        switchLongMod       =   true;
    }
    private void CheckAttack()
    {   
        //check distance between player to switch attack mod
        if(Vector3.Distance(transform.position, playerCurrentPosition) > 9.5f)
        {
            switchLongMod = true;
            // check inside or outside the attack area
            if (DetectCircleArea(longAttackRadius))
            {
                inLongAttackArea = true;
            }
            else
            {
                inLongAttackArea = false;
            }
        }
        else
        {
            switchLongMod = false;          
        }
        
        // attack fector attack time/ attack area/ attacking?
        if (switchLongMod && inLongAttackArea && !isAttack && enemyHealth > 0 )
        {   // atual attack          
            if(longAttackTime <= 0)
            {
                if(enemyHealth > maxHealth/2 )
                {
                    isAttack = true;
                    Instantiate(magicBall, magicPosition01, Quaternion.identity);
                    Debug.Log("longSimple");
                    // reset attack period time
                    longAttackTime = longAttackPeriod;
                }
                else 
                {
                    isAttack = true;
                    Instantiate(magicBall,     magicPosition01, Quaternion.identity);
                    Instantiate(mutiMagicBall, magicPosition02, Quaternion.identity);
                    Instantiate(mutiMagicBall, magicPosition03, Quaternion.identity);
                    Debug.Log("longSimple");
                    // reset attack period time
                    longAttackTime = longAttackPeriod;
                }                
            }
            else if (enemyHealth <= maxHealth / 2 && longSpecialAttackTime <= 0)
            {
                isAttack = true;
                Instantiate(fireRing, playerCurrentPosition, Quaternion.identity);
                Debug.Log("longSpecial");
                // reset attack period time
                longSpecialAttackTime = longSpecialAttackPeriod;
            }
        }
        // count attack period time
        if (longAttackTime > 0)
        {
            longAttackTime -= Time.deltaTime;
            isAttack = false;
        }
        if(longSpecialAttackTime > 0)
        {
            longSpecialAttackTime -= Time.deltaTime;
            isAttack = false;
        }

        // attack fector attack time/ attack area/ attacking?
        if (shortAttackTime <= 0 && DetectCircleArea(shortAttackRadius) && !isAttack && enemyHealth > 0)
        {   // atual attack
            isAttack = true;
            //enemyAnimator.SetTrigger("isAttack");
            Debug.Log("short");
            // reset attack period time
            shortAttackTime = shortAttackPeriod;
        }
        // count attack period time
        if (shortAttackTime > 0)
        {
            shortAttackTime -= Time.deltaTime;
            isAttack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurtBySpell(other, boss);
        EnemyHurtBySword(other, boss);
    }
    private void ChasingPlayer()
    {
        Rotation(playerCurrentPosition, this.gameObject, rb, rotate);
        RotateMagicPoint(playerCurrentPosition, magicPosition02, 1);
        RotateMagicPoint(playerCurrentPosition, magicPosition03, 1);
        if (switchLongMod)
        {
            senseRadius = longAttackRadius + 12;
            // check sence area for action
            if (enemyHealth > 0 && DetectCircleArea(senseRadius) && !isAttack && !inLongAttackArea)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
        else
        {
            senseRadius = shortAttackRadius + 6;
            // check sence area for action  // check inside or outside the attack area
            if (enemyHealth > 0 && DetectCircleArea(senseRadius) && !isAttack && !DetectCircleArea(shortAttackRadius))
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
    }

    private void RotateMagicPoint(Vector3 targetPosition, Vector3 position, float angle)
    {
        Vector3 directionToPlayer = targetPosition - position;
        // Zero out the y component to keep the slime upright
        directionToPlayer.y = 0;

        // Create a new rotation that looks in the direction of the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

        // Adjust for the slime's actual forward direction if it's not the global Z-axis
        // For example, if the slime's forward is the positive X-axis, we rotate the quaternion

        // inital setting is - 90 angle, but someone moodle 0 angle is different rotation state so need to try different change
        Quaternion correctedRotation = Quaternion.Euler(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y - (angle), lookRotation.eulerAngles.z);
        // Smoothly rotate the slime towards the player
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, correctedRotation, rotateSpeed * Time.deltaTime));
    }
    private void DrawLine()
    {   // sense area point
        Vector3 senseRightward = new(enemyCurrentPositionX + senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseLeftward = new(enemyCurrentPositionX - senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + senseRadius);
        Vector3 senseBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - senseRadius);
        // sense area Ê®×Ö
        Debug.DrawLine(senseBackward, senseForward, Color.blue);
        Debug.DrawLine(senseLeftward, senseRightward, Color.blue);
        // long attack area point
        Vector3 attackRightward = new(enemyCurrentPositionX + longAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackLeftward = new(enemyCurrentPositionX - longAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + longAttackRadius);
        Vector3 attackBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - longAttackRadius);
        // attack area Ê®×Ö
        Debug.DrawLine(attackForward, attackBackward, Color.red);
        Debug.DrawLine(attackLeftward, attackRightward, Color.red);
        // short attack area point
        Vector3 shortattackRightward    = new(enemyCurrentPositionX  + shortAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 shortattackLeftward     = new(enemyCurrentPositionX  - shortAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 shortattackForward      = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ  + shortAttackRadius);
        Vector3 shortattackBackward     = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ  - shortAttackRadius);
        // attack area Ê®×Ö
        Debug.DrawLine(shortattackForward , shortattackBackward,  Color.green);
        Debug.DrawLine(shortattackLeftward, shortattackRightward, Color.green);


    }
}
