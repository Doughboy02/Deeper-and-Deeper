using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBC_ActionButton : MonoBehaviour
{
    public Button actionButton;
    public GameObject selectedText;

    private void Start()
    {
        selectedText.SetActive(false);
        actionButton.onClick.AddListener(() => TBC_CanvasManager.instance.OnActionButtonSelected(transform.GetSiblingIndex()));
    }
}
