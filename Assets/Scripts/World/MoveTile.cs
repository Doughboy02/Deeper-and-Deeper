using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public bool CanMove = false;
    public int MoveCost = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hovering()
    {
        GetComponent<Renderer>().material.SetInt("_IsHovered", 1);
    }

    public void NotHovering()
    {
        GetComponent<Renderer>().material.SetInt("_IsHovered", 0);
    }

    public void SetToMoveable()
    {
        CanMove = true;
        GetComponent<Renderer>().material.SetInt("_IsInMoveRange", 1);
    }

    public void SetToNotMoveable()
    {
        CanMove = false;
        GetComponent<Renderer>().material.SetInt("_IsInMoveRange", 0);
        MoveCost = 0;
    }


}
