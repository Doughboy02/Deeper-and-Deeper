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
        actionButton.onClick.AddListener(delegate
        {
            TBC_CanvasManager.instance.OnActionButtonSelected(transform.GetSiblingIndex());
            if (TBC_GameManager.instance.playerSelectedAttack != null)
            {
                TBC_GameManager.instance.playerSelectedAttack.ResetTargets();
                TBC_GameManager.instance.playerSelectedAttack = null;
            }
        });
    }
}
