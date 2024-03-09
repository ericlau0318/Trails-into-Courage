using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyValue
{
    private readonly string boss = "BOSS";
    private Animator enemyAnimator;
    private Rigidbody rb;
    // attack mood/state
    [SerializeField]
    private int shortDamage;
    [SerializeField]
    private int longDamage;
    private float longAttactRadius, shortAttackRadius;

    public Transform point2;
    public Transform point1;
    private Transform target;
    // enemy setting
    private float attackTime;
    [SerializeField]
    private bool isAttack, inAttackArea, switchLongMod;
    // UI hp
    private float maxHealth;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitialBoss();
        InitialObjectCollect(this.gameObject);
        target = point1;
        switchLongMod = true;
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
    private void InitialBoss()
    {
        shortDamage  =   5;
        longDamage   =   10;

        enemyHealth = 20;
        attackPeriod = 1.5f;
        movingSpeed = 2f;

        longAttactRadius = 10f;
        shortAttackRadius = 4f;


        senseRadius = 15;
        rotateSpeed = 125f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();

        isAttack = false;
        inAttackArea = false;

    }
    private void CheckAttack()
    {   
        //check distance between player to switch attack mod
        if(Vector3.Distance(transform.position, playerCurrentPosition) > 20f)
        {
            switchLongMod = true;
        }
        else
        {
            switchLongMod = false;
        }
        // check inside or outside the attack area
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
        // check sence area for action
        if (enemyHealth > 00 && DetectCircleArea(senseRadius) && !isAttack && !inAttackArea)
        {   

            Rotation(playerCurrentPosition, this.gameObject, rb);
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
        }
    }
}
