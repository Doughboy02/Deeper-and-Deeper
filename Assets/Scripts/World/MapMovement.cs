using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlayerModel;
    public GameObject LightSource;
    public int MovementSpeed = 3;
    public bool CanMove;

    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        FreezeMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove) ChooseMovement();
    }

    public void FreezeMovement()
    {
        rigidbody.useGravity = false;
        CanMove = false;
    }

    public void UnfreezeMovement()
    {
        rigidbody.useGravity = true;
        CanMove = true;
    }

    //update to check if valid movement
    public void ChooseMovement()
    {
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection += -transform.right * MovementSpeed * Time.deltaTime;
            PlayerModel.GetComponent<SpriteRenderer>().flipX = false;
            PlayerModel.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += transform.right * MovementSpeed * Time.deltaTime;
            PlayerModel.GetComponent<SpriteRenderer>().flipX = true;
            PlayerModel.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += transform.forward * MovementSpeed * Time.deltaTime;
            PlayerModel.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDirection += -transform.forward * MovementSpeed * Time.deltaTime;
            PlayerModel.GetComponent<Animator>().SetTrigger("Walk");
        }

        if (movementDirection == Vector3.zero) PlayerModel.GetComponent<Animator>().SetTrigger("StopWalk");

        LookAtMouse();
        transform.position += movementDirection;
    }

    private void LookAtMouse()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.cyan);

            LightSource.transform.LookAt(new Vector3(pointToLook.x, PlayerModel.transform.position.y, pointToLook.z));
            LightSource.transform.eulerAngles = new Vector3(0, LightSource.transform.eulerAngles.y, 0);
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
