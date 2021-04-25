using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string description;
    public int maxTargets = 1;
    public List<TBC_Entity> selectedTargets;
    public bool affectsAll;
    public TargetTypes targetType;

    public bool TryAddTarget(TBC_Entity entity)
    {
        if (!selectedTargets.Contains(entity))
        {
            selectedTargets.Add(entity);

            int targetEntityCount = 0;
            switch (targetType)
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
                ApplyItemToTargets();

                return true;
            }
        }

        return false;
    }

    public virtual void ApplyItemToTargets()
    {
        Destroy(gameObject);
    }
}
