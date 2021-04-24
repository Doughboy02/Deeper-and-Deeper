using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_Entity : MonoBehaviour
{
    public HealthModel health;
    public GameObject attackListParent;
    public TBC_Attack[] attackList;

    private void Awake()
    {
        health = GetComponent<HealthModel>();
        attackList = attackListParent.GetComponentsInChildren<TBC_Attack>();
    }
}
