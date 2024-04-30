using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanelController : MonoBehaviour
{
    public GameObject ThunderSkillsText;
    public GameObject IceSkillsText;
    public GameObject UsingBtn1;
    public GameObject UsingBtn2;
    public GameObject UsingBtn3;
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
}
