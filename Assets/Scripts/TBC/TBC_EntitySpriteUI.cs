using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TBC_EntitySpriteUI : MonoBehaviour
{
    public SpriteRenderer entitySprite;
    public Image healthBarSprite;
    public Text healthText;
    public GameObject activeTurnObject;

    private void Awake()
    {
        activeTurnObject.SetActive(false);
    }

    public void UpdateHealth(int health, float healthPercent)
    {
        healthText.text = health.ToString();
        healthBarSprite.fillAmount = healthPercent;
    }

    public void SetTurnUI(bool isEntityTurn) => activeTurnObject.SetActive(isEntityTurn);
}
