using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateController : MonoBehaviour
{
    public int STRValue = 0;
    public Text STRValueText;

    public int INTValue = 0;
    public Text INTValueText;

    public int HPValue = 0;
    public Text HPValueText;

    public int MPValue = 0;
    public Text MPValueText;

    public int SPValue = 0;
    public Text SPValueText;

    public float gridOffset = 50f;

    public static int Exp = 0;
    private int NextLevelExp = 10;
    public Text ExpText;

    public static int Level = 1;
    public Text LevelText;
    
    public static int StatePoint=10;
    public Text StatePointText;

    public RectTransform panelRectTransform;
    public float moveDuration = 1f;
    public float returnMoveDuration = 0.5f;
    public Vector2 targetPosition;

    public GameObject GameOverPanel;
    private bool isPanelMoving = false;
    private Vector2 initialPosition;
    private float moveTimer = 0f;
    private bool isPanelVisible = false;

    private string currentSceneName;

    private GameObject player;
    private PlayerController playerController;

    void Start()
    {//test
        //STRGridObjects= StartSpawnGrid(STRValue, STRGridPrefab, STRGridParent);
        //INTGridObjects= StartSpawnGrid(INTValue, INTGridPrefab, INTGridParent);
        //HPGridObjects= StartSpawnGrid(HPValue, HPGridPrefab, HPGridParent);
        //MPGridObjects= StartSpawnGrid(MPValue, MPGridPrefab, MPGridParent );
        //SPGridObjects= StartSpawnGrid(SPValue, SPGridPrefab, SPGridParent);

        UpdateValueText(STRValue, STRValueText);
        UpdateValueText(INTValue, INTValueText);
        UpdateValueText(HPValue, HPValueText);
        UpdateValueText(MPValue, MPValueText);
        UpdateValueText(SPValue, SPValueText);

        initialPosition = panelRectTransform.anchoredPosition;
        targetPosition = Vector2.zero;
        GameOverPanel.SetActive(false);
        currentSceneName = SceneManager.GetActiveScene().name;

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();

    }
    private void Update()
    {
        LevelUp();
        OpenStatePanel();
        OpenGameOverPanel();
        ExpText.text = "Exp: " + Exp + " / " + NextLevelExp;
    }
    private void OpenGameOverPanel()
    {
        if (playerController.isPlayerDead == true)
        {
            GameOverPanel.SetActive(true);
        }
    }

    private void OpenStatePanel()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPanelMoving && currentSceneName == "Main Town")
        {
            if (isPanelVisible)
            {
                PlayerState.StatePanelOpen = false;
                StartCoroutine(MovePanel(initialPosition, returnMoveDuration));
                Debug.Log("Start");
            }
            else
            {
                PlayerState.StatePanelOpen = true;
                StartCoroutine(MovePanel(targetPosition, moveDuration));
                Debug.Log("Pause");
            }
        }
    }
    /*
    public GameObject[] StartSpawnGrid(int value, GameObject Prefab, Transform Parent) 
    {
        GameObject[] arrayObject = new GameObject[value];
        for (int i = 0; i < value; i++)
        {
            arrayObject[i] = Instantiate(Prefab, Parent);
        }
        return arrayObject;

    }
    */ /// /// /// /// /// /// /// /// /// /// /// Button  /// /// /// /// /// /// /// /// /// /// ///
    public void IncreaseSTRValue()
    {
        if (StatePoint > 0 && STRValue < 99)
        {
            STRValue++;
            StatePoint--;
            PlayerState.attackDamage = 5 + STRValue * 2;
            // Debug.Log(PlayerState.attackDamage);
            //CreateGrid(STRValue,STRGridPrefab,STRGridParent,ref STRGridObjects);
            UpdateValueText(STRValue, STRValueText);
        }
    }
    public void IncreaseINTValue()
    {
        if (StatePoint > 0 && INTValue<99)
        {
            INTValue++;
            StatePoint--;
            PlayerState.spellDamage = 5 + INTValue * 2;
            //CreateGrid(INTValue,INTGridPrefab,INTGridParent, ref INTGridObjects);
            UpdateValueText(INTValue, INTValueText);
        }
    }
    public void IncreaseHPValue()
    {
        if (StatePoint > 0 && HPValue <99)
        {
            HPValue++;
            StatePoint--;
            PlayerState.maxHealth = 50 + HPValue * 5;
            //CreateGrid(HPValue,HPGridPrefab,HPGridParent, ref HPGridObjects);
            UpdateValueText(HPValue, HPValueText);
        }
    }
    public void IncreaseMPValue()
    {
        if (StatePoint > 0&& MPValue<99)
        {
            MPValue++;
            StatePoint--;
            PlayerState.maxMana = 50 + MPValue * 5;
            //CreateGrid(MPValue,MPGridPrefab,MPGridParent, ref MPGridObjects);
            UpdateValueText(MPValue, MPValueText);
        }
    }
    public void IncreaseSPValue()
    {
        if (StatePoint > 0 && SPValue<99)
        {
            SPValue++;
            StatePoint--;
            PlayerState.maxStamina = 50 + SPValue * 5;
            //CreateGrid(SPValue, SPGridPrefab, SPGridParent, ref SPGridObjects);
            UpdateValueText(SPValue, SPValueText);
        }
    }

    public void DecreaseSTRValue()
    {
        if (STRValue > 0)
        {
            STRValue--;
            StatePoint++;
            PlayerState.attackDamage -= 2;
            // Debug.Log(PlayerState.attackDamage);
            //DestroyGrid(STRValue,STRGridParent, STRGridObjects);
            UpdateValueText(STRValue, STRValueText);
        }
    }

    public void DecreaseINTValue()
    {
        if (INTValue > 0)
        {
            INTValue--;
            StatePoint++;
            PlayerState.spellDamage -= 2;

            //DestroyGrid(INTValue, INTGridObjects);
            UpdateValueText(INTValue, INTValueText);
        }
    }

    public void DecreaseHPValue()
    {
        if (HPValue > 0)
        {
            HPValue--;
            StatePoint++;
            PlayerState.maxHealth -= 5;
            //DestroyGrid(HPValue, HPGridObjects);
            UpdateValueText(HPValue, HPValueText);
        }
    }

    public void DecreaseMPValue()
    {
        if (MPValue > 0)
        {
            MPValue--;
            StatePoint++;
            PlayerState.maxMana -= 5;
            // DestroyGrid(MPValue, MPGridObjects);
            UpdateValueText(MPValue, MPValueText);
        }
    }

    public void DecreaseSPValue()
    {
        if (SPValue > 0)
        {
            SPValue--;
            StatePoint++;
            PlayerState.maxStamina -= 5;
            //DestroyGrid(SPValue, SPGridObjects);
            UpdateValueText(SPValue, SPValueText);
        }
    }

    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /*
       private void CreateGrid(int value,GameObject Prefab,Transform Parent,ref GameObject[] arrayObject)
    {
            StatePoint--;
        
            GameObject[] newGridObjects = new GameObject[value];

            for (int i = 0; i < Mathf.Min(value,arrayObject.Length); i++)
            {
                newGridObjects[i] = arrayObject[i];
            }

            float offsetX = (value - 1) * gridOffset;
            Vector3 newPosition = Parent.position + new Vector3(offsetX, 0f, 0f);
        for (int i = arrayObject.Length; i < value; i++)
        {

            newGridObjects[value-1] = Instantiate(Prefab, newPosition, Quaternion.identity, Parent);
            newPosition += new Vector3(gridOffset, 0f, 0f);
        }
            arrayObject = newGridObjects;
    
        GameObject newGrid = Instantiate(Prefab, Parent);
        newGrid.transform.localPosition = new Vector3(value * gridOffset, 0, 0);
        newGrid.transform.SetAsLastSibling();
    }

    private void DestroyGrid(int value, Transform Parent, GameObject[] arrayObject)
    {
        if (value > 0)
        {
            Transform lastChild = Parent.GetChild(value - 1);
            lastChild.SetAsLastSibling();
            Destroy(lastChild.gameObject);
        }
        StatePoint ++;
    }
*/
    private void UpdateValueText(int value, Text Valuetext)
    {
        Valuetext.text = value.ToString();
        StatePointText.text = "Points: " + StatePoint.ToString();
    }

    private void LevelUp()
    {
        if (Exp >= NextLevelExp)
        {
            Exp -= NextLevelExp;
            ExpText.text = "Exp: " + Exp + " / " + NextLevelExp;
            Level++;
            LevelText.text = "Level: " + Level;
            StatePoint+=2;
            StatePointText.text = "Points: " + StatePoint;
            NextLevelExp += 10;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
       //     Exp += 4;
            ExpText.text = "Exp: " + Exp + " / " + NextLevelExp;
        }

    }
    public void GainExp(int exe)
    {
        Exp += exe;
    }

    private IEnumerator MovePanel(Vector2 destination, float duration)
    {
        isPanelMoving = true;
        moveTimer = 0f;

        while (moveTimer < duration)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / duration);
            panelRectTransform.anchoredPosition = Vector2.Lerp(panelRectTransform.anchoredPosition, destination, t);
            yield return null;
        }

        isPanelMoving = false;
        isPanelVisible = !isPanelVisible;
    }
}
