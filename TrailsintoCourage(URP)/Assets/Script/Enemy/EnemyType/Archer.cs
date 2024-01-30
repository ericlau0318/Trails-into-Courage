using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Archer : EnemyValue
{
    private readonly string archer = "Archer";
    private Animator enemyAnimator;
    private Rigidbody rb;

    public GameObject magic;
    public GameObject magicPosition;
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
        InitialArcher();
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
    // Archer setting / component
    private void InitialArcher()
    {
        damage          =   3;
        enemyHealth     =   12;
        attackPeriod    =   3f;
        movingSpeed     =   1.5f;
        attackRadius    =   5f;
        senseRadius     =   7;
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
        if (attackTime <= 0 && inAttackArea && !isAttack && enemyHealth > 0)
        {   // atual attack
            isAttack = true;
            // spawn magic to atatck
            Instantiate(magic, magicPosition.transform.position, Quaternion.identity);
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
        EnemyHurt(other, "Spell(Clone)", archer, PlayerState.spellDamage);
        if (hurtTime <=0)
        {
            EnemyHurt(other, "Sword(Clone)", archer, PlayerState.attackDamage);
            hurtTime = 1;
        }
    }
    private void ChasingPlayer()
    {
        Rotation(playerCurrentPosition, this.gameObject, rb);
        if (spawner.grassLand && !isAttack && !inAttackArea && enemyHealth > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
        else if (spawner.desert || spawner.volcano && !isAttack && !inAttackArea && enemyHealth > 00)
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
