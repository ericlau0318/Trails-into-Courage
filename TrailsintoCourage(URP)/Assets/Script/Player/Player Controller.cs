using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Transform mainCamera;
    public Transform FPPfixationPoint;
    public Transform TPPfixationPoint;
    private Vector3 moveDirection;

    public float movingSpeed = 5f;
    public float runningSpeed = 8f;

    public float rollingSpeed = 10f;
    public float rollingTime = 0.25f;
    public float rollingStaminaCost = 10f;
    private float rollCD;
    public float rollingCoolDown = 1f;

    private float pressSpaceTime;
    public float MinJumpVelocity = 2f;
    public float MaxJumpVelocity = 5f;
    public LayerMask WhatLayerCanJump;

    private float pressLStime;
    private bool isGrounded;
    private Transform groundChecker;

    public GameObject weapon;
    private bool isAttacking = true;
    public GameObject weaponHolder;
    private BoxCollider weaponCollider;
    private float attackCD;
    public float attackCoolDown = 1f;

    public GameObject spell;
    private bool isCastingSpell = false;
    public GameObject spellHolder;
    public float spellManaCost = 10f;
    private float spellCD;
    public float spellCoolDown = 1f;

    public static bool isPlayerTalking;
    public  bool isPlayerDead;
    public GameObject PlayerDeadBody;
    private Animator anim;

    private PlayerState playerState;
    public float CheckGroundPointSize=0.2f;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        groundChecker = gameObject.transform.Find("GroundChecker");
        mainCamera = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        GameObject holdingweapon = Instantiate(weapon, weaponHolder.transform);
        weaponCollider = holdingweapon.GetComponent<BoxCollider>();
        playerState = gameObject.GetComponent<PlayerState>();

    }

    private void Update()
    {
        if (PlayerState.StatePanelOpen == false  && isPlayerTalking == false)
        {
            Movement();
            RunandRoll();
            Jump();
            WeaponAttacking();
            SpellingCast();

            PlayerDead();
        }
        if (isPlayerTalking == true)
        {
            moveDirection.z = 0;
            moveDirection.x = 0;
        }

        PlayerAnimation();
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, CheckGroundPointSize, WhatLayerCanJump);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection = mainCamera.TransformDirection(moveDirection);
        moveDirection.y = 0;
        rb.velocity = new Vector3(moveDirection.x * movingSpeed, rb.velocity.y, moveDirection.z * movingSpeed);
        if (moveDirection.magnitude != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.1f);
        }
    }


    private void RunandRoll()
    {
        //Running and Rolling
        if (rollCD < rollingCoolDown)
        {
            rollCD += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            pressLStime = 0f;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            pressLStime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) && pressLStime < 0.2f && isGrounded && rollCD >= rollingCoolDown && playerState.currentStamina >= rollingStaminaCost)
        {
            rollCD = 0;
            anim.SetTrigger("Rolling");
            playerState.UseStamina(rollingStaminaCost);
            StartCoroutine(Roll());
        }

        else if (Input.GetKey(KeyCode.LeftShift) /*&& pressLStime >= 0.2f*/ && isGrounded)
        {
            rb.velocity = new Vector3(moveDirection.x * runningSpeed, rb.velocity.y, moveDirection.z * runningSpeed);
        }
    }

    private void Jump()
    {
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressSpaceTime = 0f;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log(pressSpaceTime);
            pressSpaceTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            anim.SetTrigger("Jump");
            if (pressSpaceTime * 30 <= MinJumpVelocity)
            {
                rb.velocity = new Vector3(rb.velocity.x, MinJumpVelocity, rb.velocity.z);
            }

            if ((pressSpaceTime * 30 > MinJumpVelocity) && (pressSpaceTime * 30 < MaxJumpVelocity))
            {
                rb.velocity = new Vector3(rb.velocity.x, pressSpaceTime * 30, rb.velocity.z);
            }

            if (pressSpaceTime * 30 >= MaxJumpVelocity)
            {
                rb.velocity = new Vector3(rb.velocity.x, MaxJumpVelocity, rb.velocity.z);
            }
        }
    }

    private void WeaponAttacking()
    {
        //Weapon Attack
        if (attackCD < attackCoolDown)
        {
            attackCD += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCD >= attackCoolDown)
            {
                attackCD = 0;
                anim.SetTrigger("WeaponAttack");
                StartCoroutine(WeaponAttack(0.5f));
            }

        }
    }

    private void SpellingCast()
    {
        //Spell Cast
        if (spellCD < spellCoolDown)
        {
            spellCD += Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(1) && playerState.currentMana >= spellManaCost)
        {
            if (spellCD >= spellCoolDown)
            {
                spellCD = 0;
                anim.SetTrigger("SpellCast");
                StartCoroutine(SpellCast(0.5f));
                playerState.UseMana(spellManaCost);
            }

        }
    }

    private void PlayerDead()
    {
        if (playerState.currentHealth <= 0)
        {
            isPlayerDead = true;
            GameObject playerDead = Instantiate(PlayerDeadBody, gameObject.transform.position, gameObject.transform.rotation);
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerState.TakeDamage(50);
        }
    }

    IEnumerator Roll()
    {
        float startTime = Time.time;

        while (Time.time < startTime + rollingTime)
        {

            transform.Translate(Vector3.forward * rollingSpeed * Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator WeaponAttack(float duration)
    {
        weaponCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        weaponCollider.enabled = false;
    }

    IEnumerator SpellCast(float duration)
    {
        yield return new WaitForSeconds(duration);
        GameObject spellObject = Instantiate(spell, spellHolder.transform.position, spellHolder.transform.rotation);

    }

    private void PlayerAnimation()
    {
        if (isPlayerTalking == true)
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Running", false);
            anim.SetBool("isGrounded", true);
        }
        if (isPlayerTalking == false)
            {
                if ((moveDirection.z != 0 || moveDirection.x != 0))
                {
                anim.SetBool("Walk", true);
                }
            else
                {
                anim.SetBool("Walk", false);
                anim.SetBool("Running", false);
                }   

            if (isGrounded == true)
            {
                anim.SetBool("isGrounded", true);
            }
            else
            {
                anim.SetBool("isGrounded", false);
            }


            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveDirection.z != 0 || moveDirection.x != 0))
                {
                anim.SetBool("Running", true);
                }
            if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                anim.SetBool("Running", false);
                }
        }
    }



}
