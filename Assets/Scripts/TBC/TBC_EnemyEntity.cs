using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_EnemyEntity : TBC_Entity
{
    private void OnMouseDown()
    {
        TBC_GameManager.instance.TrySelectTargetToAttack(this);
    }
}
