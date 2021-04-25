using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_GameManager : MonoBehaviour
{
    public static TBC_GameManager instance;

    public TBC_EntityPositionHandler positionHandler;
    public TBC_TurnHandler turnHandler;

    public TBC_PlayerEntity[] playerEntities;
    public TBC_EnemyEntity[] enemyEntities;
    public TBC_Entity ActiveTurnEntity
    {
        get
        {
            if (activeTurnEntityIndex < playerEntities.Length) return playerEntities[activeTurnEntityIndex];

            return enemyEntities[activeTurnEntityIndex - playerEntities.Length];
        }
    }
    public int activeTurnEntityIndex; //Entity who's taking their turn
    public TBC_Attack playerSelectedAttack;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }

    private void Start()
    {
        positionHandler.UpdateEntityPositions();
        ActiveTurnEntity.spriteUI.SetTurnUI(true);
    }

    public void SetNextEntityTurn()
    {
        playerSelectedAttack = null;

        ActiveTurnEntity.spriteUI.SetTurnUI(false);
        activeTurnEntityIndex =
            activeTurnEntityIndex + 1 >= playerEntities.Length + enemyEntities.Length ?
            0 :
            activeTurnEntityIndex + 1;
        ActiveTurnEntity.spriteUI.SetTurnUI(true);
    }
}
