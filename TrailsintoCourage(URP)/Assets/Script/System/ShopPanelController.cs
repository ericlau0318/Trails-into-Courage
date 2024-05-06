using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class ShopPanelController : MonoBehaviour
{
    public GameObject ThunderSkillsText;
    public GameObject IceSkillsText;
    public GameObject UsingBtn1;
    public GameObject UsingBtn2;
    public GameObject UsingBtn3;
    public Text UsingBtn1Text;
    public Text UsingBtn2Text;
    public Text UsingBtn3Text;
    public static bool spellFireUsing=true;
    public static bool spellIceUsing;
    public static bool spellFlashUsing;

    // Start is called before the first frame update
    void Start()
    {
        UpdateMagicShopUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IceSkillsText.SetActive(false);
            UsingBtn2.SetActive(true);
            ThunderSkillsText.SetActive(false);
            UsingBtn3.SetActive(true);
        }
    }
    public void UpdateMagicShopUI()
    {
        if (spellFireUsing == true)
        {
            IsUseBtn1();
        }
        if (spellIceUsing == true)
        {
            IsUseBtn2();
        }
        if (spellFlashUsing == true)
        {
            IsUseBtn3();
        }
        if (Level1GameManager.IsLevel1Pass == true )
        {
            IceSkillsText.SetActive(false);
            UsingBtn2.SetActive(true);
        }
        if (Level2GameManager.IsLevel2Pass == true )
        {
            ThunderSkillsText.SetActive(false);
            UsingBtn3.SetActive(true);
        }
    }
    public void IsUseBtn1()
    {
        spellFireUsing = true;
        spellIceUsing = false;
        spellFlashUsing = false;
        UsingBtn1Text.text = "Using";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn2()
    {
        spellFireUsing = false;
        spellIceUsing = true;
        spellFlashUsing = false;
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Using";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn3()
    {
        spellFireUsing = false;
        spellIceUsing = false;
        spellFlashUsing = true;
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Using";
    }
}
