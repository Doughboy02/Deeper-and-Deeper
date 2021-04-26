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

    public void ReceiveDamage(int damage, bool isPiercing)
    {
        if (!isPiercing && armor > 0)
        {
            armor -= damage;
            if (armor < 0)
            {
                damage = Mathf.Abs(armor);
                armor = 0;
            }
            else damage = 0;
        }

        health -= damage;
    }
}
