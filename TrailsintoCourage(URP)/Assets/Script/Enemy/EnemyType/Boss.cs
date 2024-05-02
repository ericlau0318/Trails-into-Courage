using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Searcher;
using UnityEngine;

public class Boss : EnemyValue
{
    private readonly string boss = "BOSS";
    private Rigidbody rb;
    public AudioSource SwordAttackAudio,SpellCastAudio;
    // attack mood/state
    [SerializeField]
    private int shortDamage;
    public int longDamage, longSpecialDamage;
    private float longAttackRadius, shortAttackRadius;
    public GameObject fireRing, magicBall, mutiMagicBall;
    public Transform magicPosition01, magicPosition02, magicPosition03;
    public GameObject sword;
    private BoxCollider swordCollider;
    // enemy setting
    [SerializeField]
    private float longAttackTime, longSpecialAttackTime, shortAttackTime;
    private float longAttackPeriod, shortAttackPeriod, longSpecialAttackPeriod;
    private float damageTime;
    [SerializeField]
    private bool isAttack, inLongAttackArea, switchLongMod;
    // UI hp
    public float maxHealth;
    public float currentHealth;
    private float halfHealth;
    public float rotate;
    public float recoverHealth;
    public AnimatorStateInfo stateInfo;
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
        if (hurtTime > 0)
        {
            hurtTime -= Time.deltaTime;
        }
        if (damageTime > 0)
        {
            damageTime -= Time.deltaTime;
        }
        UpdateEnemyUI(currentHealth, maxHealth);
        UpdateCurrentPosition(this.gameObject);
        CheckAttack();
        ChasingPlayer();
        DrawLine();
        EnemyDied(exp);
    }
    // Boss setting / component
    private void InitialBoss()
    {
        enemyHealth                 =       200;
        recoverHealth               =       3;
        exp                         =       20;
        movingSpeed                 =       4;
        shortDamage                 =       8;
        longSpecialDamage           =       7;
        longDamage                  =       5;
        damageTime                  =       1;
        longAttackPeriod            =       2;
        longSpecialAttackPeriod     =       4;
        shortAttackPeriod           =       1;
        longAttackRadius            =       14;
        shortAttackRadius           =       1.5f;

        rotateSpeed                 =       125f;

        maxHealth                   =       enemyHealth;
        currentHealth               =       maxHealth;
        halfHealth                  =       maxHealth / 2;
        rb                          =       GetComponent<Rigidbody>();
        swordCollider               =       sword.GetComponent<BoxCollider>();
        isAttack                    =       false;
        inLongAttackArea            =       false;
        switchLongMod               =       true;
    }
    private void CheckAttack()
    {
        //check distance between player to switch attack mod
        if (Vector3.Distance(transform.position, playerCurrentPosition) > 9.5f)
        {
            switchLongMod = true;
            swordCollider.enabled = false;
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
        }
        // attack fector attack time/ attack area/ attacking?
        if (switchLongMod && inLongAttackArea && enemyHealth > 0)
        {   // atual attack
            SpellCastAudio.Play();
            if (longAttackTime <= 0 && !isAttack)
            {
                isAttack = true;
                enemyAnimator.SetTrigger("MultipleMagic");
                Debug.Log("longSimple");

            }
            if (enemyHealth <= halfHealth && longSpecialAttackTime <= 0 && !isAttack)
            {
                isAttack = true;
                enemyAnimator.SetTrigger("Magic");
                Debug.Log("longSpecial");
            }
        }
        // attack fector attack time/ attack area/ attacking?
        if (shortAttackTime <= 0 && DetectCircleArea(shortAttackRadius) && !isAttack && enemyHealth > 0)
        {   // atual attack
            SwordAttackAudio.Play();
            isAttack = true;
            enemyAnimator.SetTrigger("Sword");
            Debug.Log("short");
        }
        // count attack period time
        if (longAttackTime > 0)
        {
            longAttackTime -= Time.deltaTime;
        }

        if (longSpecialAttackTime > 0)
        {
            longSpecialAttackTime -= Time.deltaTime;
        }
        // count attack period time
        if (shortAttackTime > 0)
        {
            shortAttackTime -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        EnemyHurtByMagic(other, boss);
        EnemyHurtBySword(other, boss);

        if (other.CompareTag("Player"))
        {
            playerState.TakeDamage(shortDamage);
        }
    }
    private void ChasingPlayer()
    {
        Rotation(playerCurrentPosition, this.gameObject, rb, rotate);
        if (switchLongMod)
        {
            senseRadius = longAttackRadius + 12;
            // check sence area for action
            if (enemyHealth > 0 && DetectCircleArea(senseRadius) && !isAttack && !inLongAttackArea)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
        else
        {
            senseRadius = shortAttackRadius + 6;
            // check sence area for action  // check inside or outside the attack area
            if (enemyHealth > 0 && DetectCircleArea(senseRadius) && !isAttack && !DetectCircleArea(shortAttackRadius))
            {
                transform.position = Vector3.MoveTowards(transform.position, playerCurrentPosition, movingSpeed * Time.deltaTime);
            }
        }
    }
    private void StartSwordAttack()
    {
        swordCollider.enabled = true;
        isAttack = false;
    }

    //check animation finish
    private void FinishSwordAttack()
    {
        // reset attack period time
        shortAttackTime = shortAttackPeriod;
        swordCollider.enabled = false;
        isAttack = false;
    }
    private void FinishMagicAttack()
    {
        // reset attack period time
        Instantiate(fireRing, playerCurrentPosition, Quaternion.identity);
        longSpecialAttackTime       =       longSpecialAttackPeriod;
        isAttack                    =       false;
    }
    private void FinishMultipleMagic()
    {
        if(enemyHealth <= halfHealth)
        {
            Instantiate(magicBall, magicPosition01.transform.position, Quaternion.identity);
            Instantiate(mutiMagicBall, magicPosition02.transform.position, Quaternion.identity);
            Instantiate(mutiMagicBall, magicPosition03.transform.position, Quaternion.identity);
        }
        else
            Instantiate(magicBall, magicPosition01.transform.position, Quaternion.identity);
        longAttackTime              =       longAttackPeriod;
        isAttack                    =       false;
    }
    private void DrawLine()
    {   // sense area point
        Vector3 senseRightward = new(enemyCurrentPositionX + senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseLeftward = new(enemyCurrentPositionX - senseRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 senseForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + senseRadius);
        Vector3 senseBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - senseRadius);
        // sense area Ê®×Ö
        Debug.DrawLine(senseBackward, senseForward, Color.blue);
        Debug.DrawLine(senseLeftward, senseRightward, Color.blue);
        // long attack area point
        Vector3 attackRightward = new(enemyCurrentPositionX + longAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackLeftward = new(enemyCurrentPositionX - longAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 attackForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + longAttackRadius);
        Vector3 attackBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - longAttackRadius);
        // attack area Ê®×Ö
        Debug.DrawLine(attackForward, attackBackward, Color.red);
        Debug.DrawLine(attackLeftward, attackRightward, Color.red);
        // short attack area point
        Vector3 shortattackRightward = new(enemyCurrentPositionX + shortAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 shortattackLeftward = new(enemyCurrentPositionX - shortAttackRadius, enemyCurrentPositionY, enemyCurrentPositionZ);
        Vector3 shortattackForward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ + shortAttackRadius);
        Vector3 shortattackBackward = new(enemyCurrentPositionX, enemyCurrentPositionY, enemyCurrentPositionZ - shortAttackRadius);
        // attack area Ê®×Ö
        Debug.DrawLine(shortattackForward, shortattackBackward, Color.green);
        Debug.DrawLine(shortattackLeftward, shortattackRightward, Color.green);
    }
}
