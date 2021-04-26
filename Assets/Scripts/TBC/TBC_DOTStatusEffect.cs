using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_DOTStatusEffect : TBC_StatusEfffect
{
    public int bleedDamage = 1;

    public override void ApplyEffect()
    {
        attachedEntity.DealDamage(bleedDamage, false);

        base.ApplyEffect();
    }
}
