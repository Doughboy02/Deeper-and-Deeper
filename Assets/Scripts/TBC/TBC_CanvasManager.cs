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
        SpawnAttackSelections();
    }

    public void OnActionButtonSelected(int buttonIndex)
    {
        ClearSelectionButtons();
        descriptionText.text = "";

        switch (buttonIndex)
        {
            case 0:
                SpawnAttackSelections();
                break;
            case 1:
                SpawnItemSelections();
                break;
        }
    }

    public void SpawnAttackSelections()
    {
        ClearSelectionButtons();

        foreach(TBC_Attack attack in TBC_GameManager.instance.ActiveTurnEntity.attackList)
        {
            TBC_SelectionButton selectionButton = Instantiate(selectionButtonPrefab, selectionButtonParentTransform).GetComponent<TBC_SelectionButton>();
            selectionButton.SetupUI(attack);
        }
    }

    public void SpawnItemSelections()
    {
        foreach(Item item in Player.instance.ItemList)
        {
            TBC_SelectionButton selectionButton = Instantiate(selectionButtonPrefab, selectionButtonParentTransform).GetComponent<TBC_SelectionButton>();
            selectionButton.SetupUI(item);
        }
    }

    public void ClearSelectionButtons()
    {
        foreach (Transform selectionButton in selectionButtonParentTransform) Destroy(selectionButton.gameObject);
    }
}
