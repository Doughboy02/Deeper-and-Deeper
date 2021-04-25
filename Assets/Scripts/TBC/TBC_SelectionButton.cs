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

    private void Start()
    {
        selectionButton.onClick.AddListener(delegate {
            if (selectAttack != null)
            {
                selectAttack.ResetTargets();

                TBC_CanvasManager.instance.descriptionText.text = selectAttack.description;
                TBC_GameManager.instance.playerSelectedAttack = selectAttack;
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
        }
        else cooldownUI.SetActive(false);

        selectAttack = attack;
    }
}
