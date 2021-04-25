using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string description;
    public int maxTargets = 1;
    public List<TBC_Entity> selectedTargets;
    public bool affectsAll;
    public TargetTypes targetType;
}
