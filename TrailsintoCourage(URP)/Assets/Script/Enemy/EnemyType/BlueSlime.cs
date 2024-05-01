using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.UI;

public class BlueSlime : GrassLandType
{
    private readonly string blueSlime = "blueSlime";
    private Rigidbody rb;
    public float rotateAngle;
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
        if (!level1GameManager.fullfillTarget)
        {
            currentHealth = enemyHealth;
            hurtTime -= Time.deltaTime;
            DrawLineArea();
            UpdateEnemyUI(currentHealth, maxHealth);
            UpdateCurrentPosition(this.gameObject);
            CheckAttack();
            ChasingPlayerGrassLand(this.gameObject, rb, isAttack, inAttackArea, movingSpeed, rotateAngle);
            GrassLandEnemyDied(exp);
        }       
    }
    // Blue Slime settin value / component
    private void InitialBlueSlime()
    {
        damage                  =       3;
        enemyHealth             =       30;
        exp                     =       4;
        attackPeriod            =       0.8f;
        movingSpeed             =       2.5f;
        attackRadius            =       3f;
        senseRadius             =       4;
        rotateSpeed             =       125f;
        rotateAngle             =       90;   
        maxHealth               =       enemyHealth;
        currentHealth           =       maxHealth;
        rb                      =       GetComponent<Rigidbody>();
        level1GameManager       =       FindObjectOfType<Level1GameManager>();

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
        }
        else if (attackTime > 0)
        {   // count attack period time
            attackTime -= Time.deltaTime;
            isAttack = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurtByMagic(other, blueSlime);
        EnemyHurtBySword(other, blueSlime);
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
