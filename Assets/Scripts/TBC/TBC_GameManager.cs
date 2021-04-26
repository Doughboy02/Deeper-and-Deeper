using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_GameManager : MonoBehaviour
{
    public static TBC_GameManager instance;

    public TBC_EntityPositionHandler positionHandler;
    public TBC_TurnHandler turnHandler;

    public List<TBC_Entity> playerEntities;
    public List<TBC_Entity> enemyEntities;
    public TBC_Entity ActiveTurnEntity
    {
        get
        {
            if (activeTurnEntityIndex < playerEntities.Count) return playerEntities[activeTurnEntityIndex];

            return enemyEntities[activeTurnEntityIndex - playerEntities.Count];
        }
    }
    public int activeTurnEntityIndex; //Entity who's taking their turn
    public TBC_Attack playerSelectedAttack;
    public Item playerSelectedItem;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    private void Start()
    {
        foreach(TBC_Entity entity in Player.instance.EntityList)
        {
            entity.gameObject.SetActive(true);
            playerEntities.Add(entity);
        }

        positionHandler.UpdateEntityPositions();
        ActiveTurnEntity.spriteUI.SetTurnUI(true);
    }

    public void SetNextEntityTurn()
    {
        if (playerSelectedAttack != null)
        {
            playerSelectedAttack.ResetTargets();
            playerSelectedAttack = null;
        }

        ActiveTurnEntity.spriteUI.SetTurnUI(false);
        activeTurnEntityIndex =
            activeTurnEntityIndex + 1 >= playerEntities.Count + enemyEntities.Count ?
            0 :
            activeTurnEntityIndex + 1;
        ActiveTurnEntity.spriteUI.SetTurnUI(true);
    }
}
