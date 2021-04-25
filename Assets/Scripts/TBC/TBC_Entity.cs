using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_Entity : MonoBehaviour
{
    public HealthModel healthModel;
    public TBC_EntitySpriteUI spriteUI;
    public GameObject attackListParent;
    public TBC_Attack[] attackList;

    private void Awake()
    {
        healthModel = GetComponent<HealthModel>();
        spriteUI = GetComponentInChildren<TBC_EntitySpriteUI>();
        attackList = attackListParent.GetComponentsInChildren<TBC_Attack>();
    }

    private void Start()
    {
        spriteUI.UpdateHealth(healthModel.health, healthModel.HealthPercent);
    }

    private void OnMouseDown()
    {
        TBC_GameManager.instance.turnHandler.TrySelectTargetToAttack(this);
    }

    public void DealDamage(int damage)
    {
        healthModel.health -= damage;
        spriteUI.UpdateHealth(healthModel.health, healthModel.HealthPercent);
    }
}
