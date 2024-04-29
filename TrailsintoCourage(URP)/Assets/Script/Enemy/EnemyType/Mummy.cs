using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mummy : EnemyValue
{
    private readonly string mummy = "Mummy";
    private Rigidbody rb;

    public Transform point2;
    public Transform point1;
    private Transform target;
    // enemy setting
    private float attackTime;
    [SerializeField]
    private bool isAttack, inAttackArea, movingToPoint1, isSwan;
    // UI hp
    private float maxHealth;
    private float currentHealth;
    private float damageTime;
    public float r;
    // Start is called before the first frame update
    void Start()
    {
        InitialMummy();
        InitialObjectCollect(this.gameObject);
        target = point1;
        movingToPoint1 = true;
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = enemyHealth;
        hurtTime -= Time.deltaTime;
        damageTime -= Time.deltaTime;
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        EnemyDied(exp);
    }
    // Archer setting / component
    private void InitialMummy()
    {
        damage                   =     4;
        enemyHealth              =     30;
        exp                      =     6;
        attackPeriod             =     1.5f;
        movingSpeed              =     3f;
        attackRadius             =     1.8f;
        senseRadius              =     10;
        rotateSpeed              =     125f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        damageTime = 0.5f;

        isAttack = false;
        inAttackArea = false;

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
            // reset attack period time
            attackTime = attackPeriod;
        }
        // count attack period time
        else if (attackTime > 0)
        {
            attackTime -= Time.deltaTime;
            isAttack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurtBySpell(other, mummy);
        EnemyHurtBySword(other, mummy);

        if (other.CompareTag("Player") && damageTime <0)
        {
            Debug.Log("Player has collided with the model's collider");
            playerState.TakeDamage(damage);
            damageTime = 1f;
        }
    }
    private void ChasingPlayer()
    {
        if (spawner.desert && enemyHealth > 00)
        {   // check for swaning/??¡Á? or not

            // check sence area if false swan to walk point to point
            if (!DetectCircleArea(senseRadius))
            {
                isSwan          =   true;
                movingSpeed     =   3f;
                Rotation(target.transform.position, this.gameObject, rb, r);
                transform.position = Vector3.MoveTowards(transform.position, target.position, movingSpeed * Time.deltaTime);

                // Check if the target is reached
                if (Vector3.Distance(transform.position, target.position) < 0.1f && isSwan)
                {
                    // Toggle the target
                    if (movingToPoint1)
                    {
                        target = point2;
                        movingToPoint1 = false;
                    }
                    else
                    {
                        target = point1;
                        movingToPoint1 = true;
                    }
                }
            }

            else if (DetectCircleArea(senseRadius))
            {
                isSwan          =   false;
                movingSpeed     =   4f;
                Rotation(playerCurrentPosition, this.gameObject, rb, r);
                if (!isAttack && !inAttackArea)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
                }               
            }
        }
    }
}
