using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    private GameObject canvas;
    public GameObject healthBar;
    public GameObject manaBar;
    public GameObject staminaBar;

    private Slider healthSlider;
    private Slider manaSlider;
    private Slider staminaSlider;

    public static float maxHealth = 100f;
    public float currentHealth;
    public static float maxMana = 100f;
    public float currentMana;
    public static float maxStamina = 50f;
    public float currentStamina;

    public static float attackDamage = 7;
    public static float spellDamage = 7;

    public static bool StatePanelOpen;

    private bool isInvincible;

    public float RecoverHealthDelay = 5f;
    public float RecoverManaDelay = 3f;
    public float RecoverStaminaDelay = 1f;

    private float RecoverManaTimer;
    private float RecoverHealthTimer;
    private float RecoverStaminaTimer;

    public float RecoverHealthQuantity = 2f;
    public float RecoverManaQuantity = 3f;
    public float RecoverStaminaQuantity = 5f;

    private bool canRecoverHealth = true;
    private bool canRecoverMana = true;
    private bool canRecoverStamina = true;

    //rivate Animator anim;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        if (healthBar == null)
        {
            healthBar = canvas.transform.Find("Health Bar").gameObject;
        }
        if (manaBar == null)
        {
            manaBar = canvas.transform.Find("Mana Bar").gameObject;
        }
        if (staminaBar == null)
        {
            staminaBar = canvas.transform.Find("Stamina Bar").gameObject;
        }

        //anim = gameObject.GetComponent<Animator>();
        healthSlider = healthBar.GetComponent<Slider>();
        manaSlider = manaBar.GetComponent<Slider>();
        staminaSlider = staminaBar.GetComponent<Slider>();
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
    }


    private void Update()
    {
        RecoverHealth();
        RecoverMana();
        RecoverStamina();
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
        healthSlider.value = currentHealth / maxHealth;
        manaSlider.value = currentMana / maxMana;
        staminaSlider.value = currentStamina / maxStamina;

        if (RecoverHealthTimer > RecoverHealthDelay)
        {
            canRecoverHealth = true;
        }
        else
        {
            RecoverHealthTimer += Time.deltaTime;
        }

        if (RecoverManaTimer > RecoverManaDelay)
        {
            canRecoverMana = true;
        }
        else
        {
            RecoverManaTimer += Time.deltaTime;
        }

        if (RecoverStaminaTimer > RecoverStaminaDelay)
        {
            canRecoverStamina = true;
        }
        else
        {
            RecoverStaminaTimer += Time.deltaTime;
        }
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible == false)
        {
           // anim.SetTrigger("Invincible");
            currentHealth -= damage;
            Debug.Log("HP: " + currentHealth);
            StartCoroutine(InvincibleTime(0.5f));
        }
        StopRecoverHealth();
    }

    public IEnumerator InvincibleTime(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }

    public void UseMana(float usedMana)
    {
        currentMana -= usedMana;
        StopRecoverMana();
    }

    public void UseStamina(float usedStanima)
    {
        currentStamina -= usedStanima;
        StopRecoverStamina();
    }


    public void RecoverHealth()
    {
        if (canRecoverHealth == true && currentHealth < maxHealth)
        {
            currentHealth += RecoverHealthQuantity * Time.deltaTime;
        }
    }
    public void RecoverMana()
    {
        if (canRecoverMana == true && currentMana < maxMana)
        {
            currentMana += RecoverManaQuantity * Time.deltaTime;
        }
    }
    public void RecoverStamina()
    {
        if (canRecoverStamina == true && currentStamina < maxStamina)
        {
            currentStamina += RecoverStaminaQuantity * Time.deltaTime;
        }
    }

    public void StopRecoverHealth()
    {
        canRecoverHealth = false;
        RecoverHealthTimer = 0;
    }
    public void StopRecoverMana()
    {
        canRecoverMana = false;
        RecoverManaTimer = 0;
    }
    public void StopRecoverStamina()
    {
        canRecoverStamina = false;
        RecoverStaminaTimer = 0;
    }
}
