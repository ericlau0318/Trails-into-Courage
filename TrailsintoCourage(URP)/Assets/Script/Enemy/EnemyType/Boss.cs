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
    public int longDamage;
    private float longAttackRadius, shortAttackRadius;
    public GameObject fire;

    // enemy setting
    private float longAttackTime, shortAttackTime;
    private float longAttackPeriod, shortAttackPeriod;
    [SerializeField]
    private bool isAttack, inLongAttackArea, inShortAttackArea, switchLongMod;
    // UI hp
    private float maxHealth;
    private float currentHealth;

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
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        EnemyDied();
    }
    // Boss setting / component
    private void InitialBoss()
    {   
        movingSpeed         =   3;
        shortDamage         =   6;
        longDamage          =   8;

        enemyHealth         =   20;
        longAttackPeriod    =   3;
        shortAttackPeriod   =   2;       
        longAttackRadius    =   12;
        shortAttackRadius   =   1;

        rotateSpeed = 125f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        enemyAnimator = GetComponent<Animator>();

        isAttack            =   false;
        inLongAttackArea    =   false;
        inShortAttackArea   =   false;

    }
    private void CheckAttack()
    {   
        //check distance between player to switch attack mod
        if(Vector3.Distance(transform.position, playerCurrentPosition) > 10.5f)
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
            // check inside or outside the attack area
            if (DetectCircleArea(shortAttackRadius))
            {
                inLongAttackArea = true;
            }
            else
            {
                inLongAttackArea = false;
            }
        }
        
        // attack fector attack time/ attack area/ attacking?
        if (longAttackTime <= 0 && inLongAttackArea && !isAttack && enemyHealth > 0 )
        {   // atual attack
            isAttack = true;

            //enemyAnimator.SetTrigger("isAttack");
            Instantiate(fire, playerCurrentPosition, Quaternion.identity);
            Debug.Log("long");
            // reset attack period time
            longAttackTime = longAttackPeriod;
        }
        // count attack period time
        else if (longAttackTime > 0)
        {
            longAttackTime -= Time.deltaTime;
            isAttack = false;
        }

        // attack fector attack time/ attack area/ attacking?
        if (shortAttackTime <= 0 && inShortAttackArea && !isAttack && enemyHealth > 0)
        {   // atual attack
            isAttack = true;
            //enemyAnimator.SetTrigger("isAttack");
            Debug.Log("short");
            // reset attack period time
            shortAttackTime = shortAttackPeriod;
        }
        // count attack period time
        else if (shortAttackTime > 0)
        {
            shortAttackTime -= Time.deltaTime;
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
        if (switchLongMod)
        {
            senseRadius = longAttackRadius + 10;
            // check sence area for action
            if (enemyHealth > 00 && DetectCircleArea(senseRadius) && !isAttack && !inLongAttackArea)
            {   

                Rotation(playerCurrentPosition, this.gameObject, rb);
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
        else
        {
            senseRadius = shortAttackRadius + 8;
            // check sence area for action
            if (enemyHealth > 00 && DetectCircleArea(senseRadius) && !isAttack && !DetectCircleArea(shortAttackRadius))
            {

                Rotation(playerCurrentPosition, this.gameObject, rb);
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }

    }
}
