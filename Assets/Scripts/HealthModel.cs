using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModel : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float HealthPercent
    {
        get { return (float)health / maxHealth; }
    }

    public int armor;

    public void ReceiveDamage(int damage)
    {
        int damageAfterArmor = armor - damage;

        if (damageAfterArmor < 0) health -= Mathf.Abs(damageAfterArmor);
    }
}
