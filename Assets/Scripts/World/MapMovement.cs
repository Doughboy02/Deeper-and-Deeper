using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public GameObject Player;

    private GameObject _hoveredObject;
    private Ray _ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W)) MovePlayer(transform.forward + transform.position);
        ChooseMovement();
    }

    //update to check if valid movement
    public void ChooseMovement()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit hit, 100))
        {
            if (hit.transform.CompareTag("Block"))
            {
                if (hit.transform.gameObject != _hoveredObject)
                {
                    _hoveredObject?.GetComponent<Renderer>().material.SetInt("_IsHovered", 0);
                }

                _hoveredObject = hit.transform.gameObject;
                _hoveredObject.GetComponent<Renderer>().material.SetInt("_IsHovered", 1);
            }
        }
        else
        {
            _hoveredObject?.GetComponent<Renderer>().material.SetInt("_IsHovered", 0);
        }

        if (Input.GetMouseButtonDown(0) && _hoveredObject.CompareTag("Block"))
        {
            MovePlayer(new Vector3(_hoveredObject.transform.position.x, 1, _hoveredObject.transform.position.z));
        }
    }

    public void MovePlayer(Vector3 newPosition)
    {
        Vector3 previousPosition = Player.transform.position;
        Player.transform.position = newPosition;
        UpdateSurroundings(CalculateDirection(previousPosition, Player.transform.position));
    }

    private Direction CalculateDirection(Vector3 previousPosition, Vector3 currentPosition)
    {
        Vector3 direction = previousPosition - currentPosition;
        print(direction);
        return Direction.Forward;
    }

    public void UpdateSurroundings(Direction direction)
    {

    }
}

public enum Direction
{
    Forward,
    Backward,
    Left,
    Right
}
