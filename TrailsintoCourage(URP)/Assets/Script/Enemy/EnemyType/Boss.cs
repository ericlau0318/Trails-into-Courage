using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyValue
{
    private readonly string boss = "BOSS";
    private Animator enemyAnimator;
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
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        EnemyDied();
    }
    // Archer setting / component
    private void InitialMummy()
    {
        damage = 4;
        enemyHealth = 20;
        attackPeriod = 1.5f;
        movingSpeed = 2f;
        attackRadius = 3f;
        senseRadius = 6;
        rotateSpeed = 125f;

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
        if (attackTime <= 0 && inAttackArea && !isAttack && enemyHealth > 0 )
        {   // atual attack
            isAttack = true;
            // attack
            //enemyAnimator.SetTrigger("isAttack");
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
        EnemyHurt(other, "Spell(Clone)", boss, PlayerState.spellDamage);
        if (hurtTime <= 0)
        {
            EnemyHurt(other, "Sword(Clone)", boss, PlayerState.attackDamage);
            hurtTime = 0.5f;
        }
    }
    private void ChasingPlayer()
    {
        if (enemyHealth > 00)
        {   // check for swaning/??��? or not

            // check sence area if false swan to walk point to point
            if (!DetectCircleArea(senseRadius))
            {

            }

            else if (DetectCircleArea(senseRadius) && !isAttack && !inAttackArea)
            {
                Rotation(playerCurrentPosition, this.gameObject, rb);
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
    }
}
