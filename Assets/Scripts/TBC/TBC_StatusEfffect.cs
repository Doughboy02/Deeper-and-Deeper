using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_StatusEfffect : MonoBehaviour
{
    public TBC_Entity attachedEntity;
    public int duration;

    public void InitializeStatusEffect(int duration, TBC_Entity entity)
    {
        this.duration = duration;
        attachedEntity = entity;
        attachedEntity.startTurnEvent.AddListener(() => ApplyEffect());
    }

    public virtual void ApplyEffect()
    {
        duration--;

        if (duration <= 0) Destroy(this);
    }
}
