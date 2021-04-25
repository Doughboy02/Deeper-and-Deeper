using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_TurnHandler : MonoBehaviour
{
    public TBC_Entity ActiveTurnEntity
    {
        get { return TBC_GameManager.instance.ActiveTurnEntity; }
    }

    public void TrySelectTargetToAttack(TBC_Entity target)
    {
        if (TBC_GameManager.instance.playerSelectedAttack != null)
        {
            bool playerCheck = TBC_GameManager.instance.playerSelectedAttack.targetType == TargetTypes.Player && target.GetType() == typeof(TBC_PlayerEntity);
            bool enemyCheck = TBC_GameManager.instance.playerSelectedAttack.targetType == TargetTypes.Enemy && target.GetType() == typeof(TBC_EnemyEntity);

            if (playerCheck || enemyCheck)
            {
                TBC_GameManager.instance.playerSelectedAttack.TryAddTarget(target);

                if (TBC_GameManager.instance.playerSelectedAttack.IsMaxTargets) NextTurn();
                else
                {
                    //Add ui for a selected enemy
                }
            }
        }
    }

    public void NextTurn()
    {
        TBC_CanvasManager.instance.ClearSelectionButtons();
        TBC_CanvasManager.instance.descriptionText.text = "";

        TBC_GameManager.instance.SetNextEntityTurn();

        if (ActiveTurnEntity.GetType() == typeof(TBC_EnemyEntity))
        {
            TBC_Attack enemyAttack = null;

            while(enemyAttack == null)
            {
                enemyAttack = ActiveTurnEntity.attackList[Random.Range(0, ActiveTurnEntity.attackList.Length)];

                if (enemyAttack.cooldownCount > 0) enemyAttack = null;
            }
            enemyAttack.ResetTargets();

            if (enemyAttack.maxTargets < TBC_GameManager.instance.playerEntities.Length)
            {
                while (enemyAttack.selectedTargets.Count < enemyAttack.maxTargets)
                {
                    enemyAttack.TryAddTarget(TBC_GameManager.instance.playerEntities[Random.Range(0, TBC_GameManager.instance.playerEntities.Length)]);
                }
            }
            else
            {
                foreach(TBC_PlayerEntity playerEntity in TBC_GameManager.instance.playerEntities)
                {
                    enemyAttack.TryAddTarget(playerEntity);
                }
            }

            NextTurn();
        }
    }
}
