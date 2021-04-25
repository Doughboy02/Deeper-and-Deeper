using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_Attack : MonoBehaviour
{
    public string AttackName
    {
        get { return gameObject.name; }
    }
    public string description;
    public int damage;
    public int cooldownDuration;
    public int cooldownCount;
    public int maxTargets = 1;
    public List<TBC_Entity> selectedTargets;
    public bool affectsAll;
    public TargetTypes targetType;

    private void Awake()
    {
        selectedTargets = new List<TBC_Entity>();
    }

    public bool TryAddTarget(TBC_Entity entity)
    {
        if (!selectedTargets.Contains(entity))
        {
            selectedTargets.Add(entity);
            entity.spriteUI.selectedSprite.SetActive(true);

            int targetEntityCount = 0;
            switch(targetType)
            {
                case TargetTypes.Player:
                    targetEntityCount = TBC_GameManager.instance.playerEntities.Count;
                    break;
                case TargetTypes.Enemy:
                    targetEntityCount = TBC_GameManager.instance.enemyEntities.Count;
                    break;
            }

            if (affectsAll || selectedTargets.Count >= maxTargets || selectedTargets.Count >= targetEntityCount)
            {
                ApplyAttackToTargets();

                return true;
            }
        }
        
        return false;
    }

    public void ApplyAttackToTargets()
    {
        foreach(TBC_Entity entity in selectedTargets)
        {
            entity.DealDamage(damage);
        }
        cooldownCount = cooldownDuration;

        ResetTargets();
    }

    public void ResetTargets()
    {
        foreach (TBC_Entity entity in selectedTargets) entity.spriteUI.selectedSprite.SetActive(false);

        selectedTargets.Clear();
    }
}

public enum TargetTypes
{
    Player,
    Enemy,
}