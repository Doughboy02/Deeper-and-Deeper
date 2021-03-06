using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_EntityPositionHandler : MonoBehaviour
{
    public List<TBC_Entity> PlayerEntities
    {
        get { return TBC_GameManager.instance.playerEntities; }
    }
    public List<TBC_Entity> EnemyEntities
    {
        get { return TBC_GameManager.instance.enemyEntities; }
    }
    public float preferedSpacing;
    public float minSpacing;

    private void Start()
    {
        UpdateEntityPositions();
    }

    public void UpdateEntityPositions()
    {
        bool playerUsePreferedSpacing = preferedSpacing > 0 && Screen.width / 2 > (PlayerEntities.Count - 1) * preferedSpacing;
        for(int x = 0; x < PlayerEntities.Count; x++)
        {
            float entitySpace = playerUsePreferedSpacing ? preferedSpacing : Screen.width / 2 / (PlayerEntities.Count + 1);
            entitySpace = Mathf.Max(entitySpace, minSpacing);

            float screenX = entitySpace * (x + 1);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 0));
            newPosition.y = PlayerEntities[x].transform.position.y;
            newPosition.z = PlayerEntities[x].transform.position.z;

            PlayerEntities[x].transform.position = newPosition;
        }

        bool enemyUsePreferedSpacing = preferedSpacing > 0 && Screen.width / 2 > (EnemyEntities.Count - 1) * preferedSpacing;
        for(int x = 0; x < EnemyEntities.Count; x++)
        {
            float entitySpace = enemyUsePreferedSpacing ? preferedSpacing : Screen.width / 2 / (EnemyEntities.Count + 1);
            entitySpace = Mathf.Max(entitySpace, minSpacing);

            float screenX = Screen.width - entitySpace * (x + 1);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 0));
            newPosition.y = EnemyEntities[x].transform.position.y;
            newPosition.z = EnemyEntities[x].transform.position.z;

            EnemyEntities[x].transform.position = newPosition;
        }
    }
}
