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
    public bool affectsAllEnemies;

    private void Awake()
    {
        selectedTargets = new List<TBC_Entity>();
    }

    public bool AddTarget(TBC_Entity entity)
    {
        selectedTargets.Add(entity);
        if (selectedTargets.Count >= maxTargets)
        {
            ApplyAttackToTargets();
            return true;
        }

        return false;
    }

    public virtual void ApplyAttackToTargets()
    {
        foreach(TBC_Entity entity in selectedTargets)
        {
            entity.DealDamage(damage);
        }
        cooldownCount = cooldownDuration;
    }

    public void ResetTargets() => selectedTargets.Clear();
}
