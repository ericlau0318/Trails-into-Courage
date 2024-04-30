using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
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
        UsingBtn1Text.text = "Is Use";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn2()
    {
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Is Use";
        UsingBtn3Text.text = "Not Use";
    }
    public void IsUseBtn3()
    {
        UsingBtn1Text.text = "Not Use";
        UsingBtn2Text.text = "Not Use";
        UsingBtn3Text.text = "Is Use";
    }
}
