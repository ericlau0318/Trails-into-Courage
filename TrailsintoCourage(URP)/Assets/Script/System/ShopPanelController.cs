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
    public static bool skillOneUsing;
    public static bool skillTwoUsing;
    public static bool skillThreeUsing;

    // Start is called before the first frame update
    void Start()
    {
        if (skillOneUsing == true)
        {
            IsUseBtn1();
        }
        if (skillTwoUsing == true)
        {
            IsUseBtn2();
        }
        if (skillThreeUsing == true)
        {
            IsUseBtn3();
        }
        if (Level1GameManager.IsLevel1Pass == true)
        {
            IceSkillsText.SetActive(false);
            UsingBtn2.SetActive(true);
        }
        if (Level2GameManager.IsLevel2Pass == true)
        {
            ThunderSkillsText.SetActive(false);
            UsingBtn3.SetActive(true);
        }
    }
    public void IsUseBtn1()
    {
        skillOneUsing = true;
        skillTwoUsing = false;
        skillThreeUsing = false;
        UsingBtn1Text.text = "Is Use";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn2()
    {
        skillOneUsing = false;
        skillTwoUsing = true;
        skillThreeUsing = false;
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Is Use";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn3()
    {
        skillOneUsing = false;
        skillTwoUsing = false;
        skillThreeUsing = true;
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Is Use";
    }
}
