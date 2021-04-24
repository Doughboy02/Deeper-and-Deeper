using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBC_EntityPositioner : MonoBehaviour
{
    public GameObject[] playerEntities;
    public GameObject[] enemyEntities;
    public float preferedSpacing;
    public float minSpacing;

    private void Start()
    {
        UpdateEntityPositions();
    }

    public void UpdateEntityPositions()
    {
        bool playerUsePreferedSpacing = preferedSpacing > 0 && Screen.width / 2 > (playerEntities.Length - 1) * preferedSpacing;
        for(int x = 0; x < playerEntities.Length; x++)
        {
            float entitySpace = playerUsePreferedSpacing ? preferedSpacing : Screen.width / 2 / (playerEntities.Length + 1);
            entitySpace = Mathf.Max(entitySpace, minSpacing);

            float screenX = entitySpace * (x + 1);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 0));
            newPosition.y = playerEntities[x].transform.position.y;
            newPosition.z = playerEntities[x].transform.position.z;

            playerEntities[x].transform.position = newPosition;
        }

        bool enemyUsePreferedSpacing = preferedSpacing > 0 && Screen.width / 2 > (enemyEntities.Length - 1) * preferedSpacing;
        for(int x = 0; x < enemyEntities.Length; x++)
        {
            float entitySpace = enemyUsePreferedSpacing ? preferedSpacing : Screen.width / 2 / (enemyEntities.Length + 1);
            entitySpace = Mathf.Max(entitySpace, minSpacing);

            float screenX = Screen.width - entitySpace * (x + 1);
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 0));
            newPosition.y = enemyEntities[x].transform.position.y;
            newPosition.z = enemyEntities[x].transform.position.z;

            enemyEntities[x].transform.position = newPosition;
        }
    }
}
