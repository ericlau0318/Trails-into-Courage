using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : EnemyValue
{
    private readonly string demon = "Demon";
    private Rigidbody rb;

    public Transform point2;
    public Transform point1;
    private Transform target;
    // enemy setting
    private float attackTime;
    [SerializeField]
    private bool isAttack, inAttackArea, movingToPoint1;
    // UI hp
    private float maxHealth;
    private float currentHealth;
    public GameObject rightArm, leftArm;
    private BoxCollider lefttArmCollider, rightArmCollider;
    public float rotateAngle;

    // Start is called before the first frame update
    void Start()
    {
        InitialDemon();
        InitialObjectCollect(this.gameObject);
        target = point1;
        movingToPoint1 = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Level3GameManager.IsLevel3Pass)
        {
            currentHealth = enemyHealth;
            if(hurtTime > 0)
            {
                hurtTime -= Time.deltaTime;
            }
            DrawLineArea();
            UpdateEnemyUI(currentHealth, maxHealth);
            UpdateCurrentPosition(this.gameObject);
            CheckAttack();
            if (this.name == ("DemonStand"))
            {
                senseRadius = 9;
                if (DetectCircleArea(senseRadius) && !isAttack && !inAttackArea)
                {
                    Rotation(playerCurrentPosition, this.gameObject, rb, rotateAngle);
                    transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
                }
            }
            else
            {
                ChasingPlayer();
            }
            EnemyDied(exp);
        }
    }
    // Archer setting / component
    private void InitialDemon()
    {
        damage                  =       8;
        enemyHealth             =       100;
        exp                     =       10;
        attackPeriod            =       1.6f;
        movingSpeed             =       2.3f;
        attackRadius            =       3;
        senseRadius             =       6f;
        rotateSpeed             =       125f;

        maxHealth               =       enemyHealth;
        currentHealth           =       maxHealth;
        rb                      =       GetComponent<Rigidbody>();
        rightArmCollider        =       rightArm.GetComponent<BoxCollider>();
        lefttArmCollider        =       leftArm.GetComponent<BoxCollider>();


        isAttack                =       false;
        inAttackArea            =       false;
    }
    private void CheckAttack()
    {   // check inside or outside the attack area
        if (DetectCircleArea(attackRadius))
        {
            inAttackArea = true;
        }
        else if (!DetectCircleArea(attackRadius))
        {
            inAttackArea = false;
        }
        // attack fector attack time/ attack area/ attacking?
        if (attackTime <= 0 && inAttackArea && !isAttack && enemyHealth > 0)
        {   // atual attack
            isAttack = true;
            // attack
            enemyAnimator.SetTrigger("isAttack");
        }
        if (attackTime > 0)
        {   // count attack period time
            attackTime -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurtByMagic(other, demon);
        EnemyHurtBySword(other, demon);
        if (other.CompareTag("Player"))
        {
            playerState.TakeDamage(damage);
        }
    }
    private void ChasingPlayer()
    {
        if (enemyHealth > 00 && !DetectCircleArea(senseRadius))
        {   // check for swaning/??��? or not
            // check sence area if false swan to walk point to point
            transform.position = Vector3.MoveTowards(transform.position, target.position, movingSpeed * Time.deltaTime);

            // Check if the target is reached
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Toggle the target
                if (movingToPoint1)
                {
                    target = point2;
                    Rotation(target.transform.position, this.gameObject, rb, rotateAngle);
                    movingToPoint1 = false;
                }
                else
                {
                    target = point1;
                    Rotation(target.transform.position, this.gameObject, rb, rotateAngle);
                    movingToPoint1 = true;
                }
            }
        }
        else if (DetectCircleArea(senseRadius) && !isAttack && !inAttackArea)
        {
            Rotation(playerCurrentPosition, this.gameObject, rb, rotateAngle);
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
    }
    private void StartAttack()
    {
        lefttArmCollider.enabled    =       true;
        rightArmCollider.enabled    =       true;
    }
    private void EndAttack() 
    {
        lefttArmCollider.enabled    =       false;
        rightArmCollider.enabled    =       false;
        attackTime                  =       attackPeriod;
        isAttack                    =       false;
    }
}
