using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        panel.SetActive(false);
    }
}
