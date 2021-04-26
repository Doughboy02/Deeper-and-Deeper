using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_TurnHandler : MonoBehaviour
{
    public TBC_Entity ActiveTurnEntity
    {
        get { return TBC_GameManager.instance.ActiveTurnEntity; }
    }

    public void TrySelectTarget(TBC_Entity target)
    {
        if (TBC_GameManager.instance.playerSelectedAttack != null)
        {
            if (
                IsTargetingEnemy(TBC_GameManager.instance.playerSelectedAttack.targetType, target) ||
                IsTargetingPlayer(TBC_GameManager.instance.playerSelectedAttack.targetType, target)
            ) {
                if (TBC_GameManager.instance.playerSelectedAttack.TryAddTarget(target)) NextTurn();
                else target.spriteUI.selectedSprite.SetActive(true);

            }
        }
        else if (TBC_GameManager.instance.playerSelectedItem != null)
        {
            if (
                IsTargetingEnemy(TBC_GameManager.instance.playerSelectedItem.targetType, target) ||
                IsTargetingPlayer(TBC_GameManager.instance.playerSelectedItem.targetType, target)
            ) {
                if (TBC_GameManager.instance.playerSelectedItem.TryAddTarget(target)) NextTurn();
                else target.spriteUI.selectedSprite.SetActive(true);
            }
        }
    }

    public void NextTurn()
    {
        TBC_CanvasManager.instance.ClearSelectionButtons();
        TBC_CanvasManager.instance.descriptionText.text = "";
            
        TBC_GameManager.instance.SetNextEntityTurn();
        TBC_GameManager.instance.ActiveTurnEntity.startTurnEvent.Invoke();

        if (TBC_GameManager.instance.ActiveTurnEntity != null)
        {
            foreach (TBC_Attack attack in TBC_GameManager.instance.ActiveTurnEntity.attackList)
            {
                attack.cooldownCount--;
            }

            if (ActiveTurnEntity.GetType() == typeof(TBC_EnemyEntity))
            {
                TBC_Attack enemyAttack = null;

                while (enemyAttack == null)
                {
                    enemyAttack = ActiveTurnEntity.attackList[Random.Range(0, ActiveTurnEntity.attackList.Length)];

                    if (enemyAttack.cooldownCount > 0) enemyAttack = null;
                }
                enemyAttack.ResetTargets();

                if (enemyAttack.maxTargets < TBC_GameManager.instance.playerEntities.Count)
                {
                    bool finishedAttack = false;
                    while (!finishedAttack)
                    {
                        finishedAttack = enemyAttack.TryAddTarget(TBC_GameManager.instance.playerEntities[Random.Range(0, TBC_GameManager.instance.playerEntities.Count)]);
                    }
                }
                else
                {
                    foreach (TBC_PlayerEntity playerEntity in TBC_GameManager.instance.playerEntities)
                    {
                        enemyAttack.TryAddTarget(playerEntity);
                    }
                }

                NextTurn();
            }
        }
    }

    private bool IsTargetingPlayer(TargetTypes actionType, TBC_Entity target) => actionType == TargetTypes.Player && target.GetType() == typeof(TBC_PlayerEntity);
    private bool IsTargetingEnemy(TargetTypes actionType, TBC_Entity target) => actionType == TargetTypes.Enemy && target.GetType() == typeof(TBC_EnemyEntity);
}
