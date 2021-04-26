using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_PlayerEntity : TBC_Entity
{
    private void Start()
    {
        startTurnEvent.AddListener(delegate {
            TBC_CanvasManager.instance.SpawnAttackSelections();
        });
    }
}
