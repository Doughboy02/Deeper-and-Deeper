using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBC_SelectionButton : MonoBehaviour
{
    public Button selectionButton;
    public Text selectionButtonText;
    public GameObject cooldownUI;
    public Text cooldownText;

    public TBC_Attack selectAttack;
    public Item selectItem;

    private void Start()
    {
        selectionButton.onClick.AddListener(delegate {
            if (selectAttack != null)
            {
                selectAttack.ResetTargets();

                TBC_CanvasManager.instance.descriptionText.text = selectAttack.description;
                TBC_GameManager.instance.playerSelectedAttack = selectAttack;
            }
            else if (selectItem != null)
            {
                TBC_CanvasManager.instance.descriptionText.text = selectItem.description;
                TBC_GameManager.instance.playerSelectedItem = selectItem;
            }
        });
    }

    public void SetupUI(TBC_Attack attack)
    {
        selectionButtonText.text = attack.name;

        if (attack.cooldownCount > 0)
        {
            cooldownUI.SetActive(true);
            cooldownText.text = attack.cooldownCount.ToString();
            selectionButton.interactable = false;
        }
        else
        {
            cooldownUI.SetActive(false);
            selectionButton.interactable = true;
        }

        selectAttack = attack;
    }

    public void SetupUI(Item item)
    {
        selectionButtonText.text = item.name;
        selectItem = item;
        cooldownUI.SetActive(false);
    }
}
