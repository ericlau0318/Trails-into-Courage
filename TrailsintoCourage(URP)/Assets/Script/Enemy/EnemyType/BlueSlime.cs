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
    private bool isAttack, inAttackArea;
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
        hurtTime -= Time.deltaTime;
        DrawLineArea();
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayerGrassLand(this.gameObject, rb, isAttack, inAttackArea, movingSpeed);
        EnemyDied();
        //RandomCirclePoint();
    }
    // Blue Slime settin value / component
    private void InitialBlueSlime()
    {
        damage = 2;
        enemyHealth = 20;
        attackPeriod = 0.8f;
        movingSpeed = 2.5f;
        attackRadius = 3f;
        senseRadius = 4;
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
        // check the attack animation when finish
        /*if (!enemyAnimator.GetBool("Attack"))
        {
            isAttack = false;
        }*/
        // attack fector attack time/ attack area/ attacking?
        if (attackTime <= 0 && inAttackArea && !isAttack && enemyHealth > 0)
        {   // atual attack
            isAttack = true;
            // attack
            enemyAnimator.SetTrigger("isAttack");
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
                // reset attack period time
                attackTime = attackPeriod;
            }
        }
    }
}
