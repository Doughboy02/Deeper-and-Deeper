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
    public GameObject selectedSprite;
    public GameObject deadSprite;

    private void Awake()
    {
        activeTurnObject.SetActive(false);
        selectedSprite.SetActive(false);
        deadSprite.SetActive(false);
    }

    public void UpdateHealth(int health, float healthPercent)
    {
        healthText.text = health > 0 ? health.ToString() : "";
        healthBarSprite.fillAmount = healthPercent;
        deadSprite.SetActive(health <= 0);
    }

    public void SetTurnUI(bool isEntityTurn) => activeTurnObject.SetActive(isEntityTurn);
}
