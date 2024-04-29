using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician : GrassLandType
{
    private readonly string magician = "Magician";
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
        InitialMagician();
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
            ChasingPlayerGrassLand(this.gameObject, rb, isAttack, inAttackArea, movingSpeed);
            GrassLandEnemyDied(exp);
        }
    }
    // Archer setting / component
    private void InitialMagician()
    {
        damage                  =       4;
        enemyHealth             =       12;
        attackPeriod            =       3.5f;
        exp                     =       4;
        movingSpeed             =       2f;
        attackRadius            =       7f;
        senseRadius             =       8f;
        rotateSpeed             =       125f;
    
        hurtTime                =       0.5f;
    
        maxHealth               =       enemyHealth;
        currentHealth           =       maxHealth;
        rb                      =       GetComponent<Rigidbody>();
        level1GameManager       =       FindObjectOfType<Level1GameManager>();

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
        EnemyHurtBySpell(other, magician);
        EnemyHurtBySword(other, magician);
    }
}
