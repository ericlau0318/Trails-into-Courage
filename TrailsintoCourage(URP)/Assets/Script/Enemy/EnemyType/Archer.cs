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
    private bool isAttack, inAttackArea;
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
        hurtTime -= Time.deltaTime;
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayerGrassLand(this.gameObject, rb, isAttack, inAttackArea, movingSpeed);
        EnemyDied();
        //RandomCirclePoint();
    }
    // Archer setting / component
    private void InitialArcher()
    {
        damage = 3;
        enemyHealth = 12;
        attackPeriod = 3f;
        movingSpeed = 1.5f;
        attackRadius = 5f;
        senseRadius = 7;
        rotateSpeed = 125f;

        hurtTime = 0.5f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();

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
        if (hurtTime <= 0)
        {
            EnemyHurt(other, "Sword(Clone)", archer, PlayerState.attackDamage);
            hurtTime = 0.5f;
        }
    }
    // find player position and desire moving direction
    /*private Vector3 ChasingPosition()
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
    }*/
}