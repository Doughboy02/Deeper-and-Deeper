using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TBC_Entity : MonoBehaviour
{
    public HealthModel healthModel;
    public TBC_EntitySpriteUI spriteUI;
    public GameObject attackListParent;
    public TBC_Attack[] attackList;
    public UnityEvent startTurnEvent;
    public bool IsDead
    {
        get { return healthModel.health <= 0; }
    }

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
        if (!IsDead) TBC_GameManager.instance.turnHandler.TrySelectTarget(this);
    }

    public void DealDamage(int damage, bool isPiercing)
    {
        healthModel.ReceiveDamage(damage, isPiercing);
        spriteUI.UpdateHealth(healthModel.health, healthModel.HealthPercent);

        if (IsDead)
        {
            if (TBC_GameManager.instance.playerEntities.Contains(this)) TBC_GameManager.instance.playerEntities.Remove(this);
            else if (TBC_GameManager.instance.enemyEntities.Contains(this)) TBC_GameManager.instance.enemyEntities.Remove(this);
        }
    }
}
