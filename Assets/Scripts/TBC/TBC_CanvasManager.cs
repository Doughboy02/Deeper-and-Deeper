using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBC_CanvasManager : MonoBehaviour
{
    public static TBC_CanvasManager instance;

    public Transform selectionButtonParentTransform;
    public GameObject selectionButtonPrefab;
    public Text descriptionText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        descriptionText.text = "";
    }

    private void Start()
    {
        ClearSelectionButtons();
    }

    public void OnActionButtonSelected(int buttonIndex)
    {
        ClearSelectionButtons();
        descriptionText.text = "";

        switch (buttonIndex)
        {
            case 0:
                SpawnActiveEntityAttacks();
                break;
        }
    }

    public void SpawnActiveEntityAttacks()
    {
        foreach(TBC_Attack attack in TBC_GameManager.instance.ActiveTurnEntity.attackList)
        {
            TBC_SelectionButton selectionButton = Instantiate(selectionButtonPrefab, selectionButtonParentTransform).GetComponent<TBC_SelectionButton>();
            selectionButton.SetupUI(attack);
        }
    }

    public void ClearSelectionButtons()
    {
        foreach (Transform selectionButton in selectionButtonParentTransform) Destroy(selectionButton.gameObject);
    }
}
