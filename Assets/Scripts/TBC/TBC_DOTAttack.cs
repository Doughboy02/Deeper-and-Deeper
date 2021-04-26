using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_DOTAttack : TBC_Attack
{
    [Header("DOT")]
    public int dotDuration;
    public int dotDamage;
    public bool destroysArmor;

    public override void ApplyAttack(TBC_Entity entity)
    {
        if (destroysArmor) entity.healthModel.armor = 0;

        base.ApplyAttack(entity);

        TBC_DOTStatusEffect dot = entity.gameObject.AddComponent<TBC_DOTStatusEffect>();
        dot.InitializeStatusEffect(dotDuration, entity);
        dot.bleedDamage = dotDamage;
    }
}
