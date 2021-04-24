using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_GameManager : MonoBehaviour
{
    public static TBC_GameManager instance;

    public TBC_EntityPositioner positioner;

    public TBC_Entity[] playerEntities;
    public TBC_Entity[] enemyEntities;
    public TBC_Entity ActiveEntity
    {
        get
        {
            if (activeEntityIndex < playerEntities.Length) return playerEntities[activeEntityIndex];

            return enemyEntities[activeEntityIndex - playerEntities.Length];
        }
    }
    public int activeEntityIndex; //Entity who's taking their turn

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        positioner = GetComponent<TBC_EntityPositioner>();
    }

    private void Start()
    {
        positioner.UpdateEntityPositions();
    }
}
