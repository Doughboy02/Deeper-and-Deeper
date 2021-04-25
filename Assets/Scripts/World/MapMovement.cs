using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public GameObject Player;
    public int MovementSpeed = 3;
    public bool CanMove;

    // Update is called once per frame
    void Update()
    {
        if (CanMove) ChooseMovement();
    }

    //update to check if valid movement
    public void ChooseMovement()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += -transform.right * MovementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * MovementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * MovementSpeed * Time.deltaTime;
        }
    }
}

public enum Direction
{
    Forward,
    Backward,
    Left,
    Right
}
