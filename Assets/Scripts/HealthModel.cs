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
}
