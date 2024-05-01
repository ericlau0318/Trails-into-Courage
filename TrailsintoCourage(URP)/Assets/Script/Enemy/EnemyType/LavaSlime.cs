using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSlime : EnemyValue
{
    private readonly string lavaSlime = "LavaSlime";
    private Rigidbody rb;
    public GameObject fireBall;
    public Transform magicPoint;

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
        EnemyDied(exp);
    }
    // Archer setting / component
    private void InitialMummy()
    {
        damage                  =       4;
        enemyHealth             =       20;
        exp                     =       7;
        attackPeriod            =       1.5f;
        movingSpeed             =       2f;
        attackRadius            =       3f;
        senseRadius             =       6;
        rotateSpeed             =       125f;

        maxHealth = enemyHealth;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();

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
            Instantiate(fireBall, magicPoint.transform.position, Quaternion.identity);
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
        EnemyHurtByMagic(other, lavaSlime);
        EnemyHurtBySword(other, lavaSlime);
    }
    private void ChasingPlayer()
    {
        if (spawner.volcano && enemyHealth > 00)
        {   // check for swaning/??¡Á? or not

            // check sence area if false swan to walk point to point
            if (!DetectCircleArea(senseRadius))
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, movingSpeed * Time.deltaTime);

                // Check if the target is reached
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {
                    // Toggle the target
                    if (movingToPoint1)
                    {

                        target = point2;
                        Rotation(target.transform.position, this.gameObject, rb, 90);
                        movingToPoint1 = false;
                    }
                    else
                    {
                        target = point1;
                        Rotation(target.transform.position, this.gameObject, rb, 90);
                        movingToPoint1 = true;
                    }
                }

            }

            else if (DetectCircleArea(senseRadius) && !isAttack && !inAttackArea)
            {
                Rotation(playerCurrentPosition, this.gameObject, rb, 90);
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
    }
}
