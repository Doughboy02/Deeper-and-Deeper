using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Transform entityListParent;
    public Transform itemListParent;
    public TBC_Entity[] EntityList
    {
        get
        {
            TBC_Entity[] list = new TBC_Entity[entityListParent.childCount];
            for(int x = 0; x < list.Length; x++)
            {
                list[x] = entityListParent.GetChild(x).GetComponent<TBC_Entity>();
            }

            return list;
        }
    }
    public Item[] ItemList
    {
        get
        {
            Item[] list = new Item[itemListParent.childCount];
            for(int x = 0; x < list.Length; x++)
            {
                list[x] = itemListParent.GetChild(x).GetComponent<Item>();
            }

            return list;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
}
