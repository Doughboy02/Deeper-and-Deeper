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

    // Update is called once per frame
    void Update()
    {
        if (CanMove) ChooseMovement();
    }

    //update to check if valid movement
    public void ChooseMovement()
    {
        float faceDirectionAngle = PlayerModel.transform.localEulerAngles.y;
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            movementDirection += -transform.right * MovementSpeed * Time.deltaTime;
            faceDirectionAngle = 225;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDirection += transform.right * MovementSpeed * Time.deltaTime;
            faceDirectionAngle = 45;
        }

        if (Input.GetKey(KeyCode.W))
        {
            movementDirection += transform.forward * MovementSpeed * Time.deltaTime;
            faceDirectionAngle = 315;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDirection += -transform.forward * MovementSpeed * Time.deltaTime;
            faceDirectionAngle = 135;
        }

        LookAtMouse();
        //PlayerModel.transform.LookAt(movementDirection + transform.position, PlayerModel.transform.up);
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

            PlayerModel.transform.LookAt(new Vector3(pointToLook.x, PlayerModel.transform.position.y, pointToLook.z));
            LightSource.transform.LookAt(new Vector3(pointToLook.x, PlayerModel.transform.position.y, pointToLook.z));
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
