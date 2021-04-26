using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_DOTAttack : TBC_Attack
{
    [Header("DOT")]
    public int dotDuration;
    public int dotDamage;

    public override void ApplyAttack(TBC_Entity entity)
    {
        base.ApplyAttack(entity);

        TBC_DOTStatusEffect dot = entity.gameObject.AddComponent<TBC_DOTStatusEffect>();
        dot.InitializeStatusEffect(dotDuration, entity);
        dot.bleedDamage = dotDamage;
    }
}
