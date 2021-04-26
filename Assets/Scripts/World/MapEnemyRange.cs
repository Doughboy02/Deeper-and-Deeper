using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnemyRange : MonoBehaviour
{
    public SpriteRenderer Sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Sprite.enabled)
        {
            print("you're in trouble now buddy");
        }
    }
}
