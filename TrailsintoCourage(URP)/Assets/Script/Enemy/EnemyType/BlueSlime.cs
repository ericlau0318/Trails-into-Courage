using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueSlime : EnemyValue
{
    private readonly string blueSlime = "blueSlime";
    private Animator enemyAnimator;
    private Rigidbody rb;
    // enemy setting
    private float attackTime;
    [SerializeField]
    private bool isAttack, inAttackArea, startSwan, isSwan, cycle;
    private Vector3 swanTargetPosition;
    // UI hp
    private float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitialBlueSlime();
        InitialObjectCollect(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        currentHealth = enemyHealth;
        hurtTime     -= Time.deltaTime;
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        EnemyDied();
        //RandomCirclePoint();
    }
    // Blue Slime settin value / component
    private void InitialBlueSlime()
    {
        damage          =   2;
        enemyHealth     =   20;
        attackPeriod    =   0.8f;
        movingSpeed     =   2.5f;
        attackRadius    =   1.54f;
        senseRadius     =   4;
        rotateSpeed     =   125f;

        maxHealth       =   enemyHealth;
        currentHealth   =   maxHealth;
        rb              =   GetComponent<Rigidbody>();
        enemyAnimator   =   GetComponent<Animator>();

        isAttack        =   false;
        inAttackArea    =   false;
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
        // check the attack animation when finish
        /*if (!enemyAnimator.GetBool("Attack"))
        {
            isAttack = false;
        }*/
        // attack fector attack time/ attack area/ attacking?
        if (attackTime <= 0 && inAttackArea && !isAttack && enemyHealth> 0)
        {   // atual attack
            isAttack = true;
            // attack
            enemyAnimator.SetTrigger("isAttack");
            // reset attack period time
            attackTime = attackPeriod;
        }
        else if (attackTime > 0)
        {   // count attack period time
            attackTime -= Time.deltaTime;
            isAttack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurt(other, "Spell(Clone)", blueSlime, PlayerState.spellDamage);
        if (hurtTime <= 0)
        {
            EnemyHurt(other, "Sword(Clone)", blueSlime, PlayerState.attackDamage);
            hurtTime = 1;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (isAttack)
        {
            if (collision.collider.CompareTag("Player"))
            {
                playerState.TakeDamage(damage);
            }
        }
    }
    private void ChasingPlayer()
    {
        Rotation(playerCurrentPosition, this.gameObject, rb);
        if (spawner.grassLand && !isAttack && !inAttackArea && enemyHealth > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
        else if (spawner.desert || spawner.volcano && !isAttack && !inAttackArea && enemyHealth > 0)
        {   // check for swaning/ÓÎ×ß or not
            if (DetectCircleArea(senseRadius))
            {
                isSwan = false;
                startSwan = false;
                cycle = false;
            }
            if (!DetectCircleArea(senseRadius))
            {
                isSwan = true;
                /*if (isSwan)
                {
                    enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, swanTargetPosition, movingSpeed * Time.deltaTime);
                }
                if(!cycle)
                {
                    RandomCirclePoint();
                    cycle = true;
                }
                if(swanTargetPosition == enemy.transform.position)
                {
                    RandomCirclePoint();
                }*/
            }
            // chasing player
            if (!isSwan)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);

            }
        }
    }
    // find player position and desire moving direction
    private Vector3 ChasingPosition()
    {   // 4 direction movement checking
        Vector3 finalChasingPosition = Vector3.zero;
        if (enemyCurrentPositionX > playerCurrentPositionX && enemyCurrentPositionZ > playerCurrentPositionZ)
        {
            finalChasingPosition = new Vector3(enemyCurrentPositionX - 1, enemyCurrentPositionY, enemyCurrentPositionZ - 1);
        }

        else if (enemyCurrentPositionX < playerCurrentPositionX && enemyCurrentPositionZ > playerCurrentPositionZ)
        {
            finalChasingPosition = new Vector3(enemyCurrentPositionX + 1, enemyCurrentPositionY, enemyCurrentPositionZ - 1);
        }

        else if (enemyCurrentPositionX < playerCurrentPositionX && enemyCurrentPositionZ < playerCurrentPositionZ)
        {
            finalChasingPosition = new Vector3(enemyCurrentPositionX + 1, enemyCurrentPositionY, enemyCurrentPositionZ + 1);
        }

        else if (enemyCurrentPositionX > playerCurrentPositionX && enemyCurrentPositionZ < playerCurrentPositionZ)
        {
            finalChasingPosition = new Vector3(enemyCurrentPositionX - 1, enemyCurrentPositionY, enemyCurrentPositionZ + 1);
        }
        return finalChasingPosition;
    }
}
