using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public GameObject Player;
    public int MovementRange;
    public int MovesAvailible = 0;
    public bool CanMove;
    public List<GameObject> MoveableTiles = new List<GameObject>();

    private GameObject _hoveredObject;
    private Ray _ray;

    private void Start()
    {
        MovesAvailible = MovementRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ClearMovementRange();
            SetMoveRange(MovesAvailible, Player.transform.position + Vector3.down);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MovesAvailible = MovementRange;
        }

        if (CanMove) ChooseMovement();
    }

    public void SetMoveRange(int range, Vector3 currentPosition)
    {
        GameObject block;

        Vector3[] positions =
        {
            new Vector3(1, 0, 0),
            new Vector3(-1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 0, -1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, -1),
            new Vector3(-1, 0, 1),
            new Vector3(-1, 0, -1)

        };

        if (range == 0) return;

        for (int j = 0; j < positions.Length; j++)
        {
            if (MapManager.instance.TryGetPosition(currentPosition + positions[j], out block) && currentPosition + positions[j] != Player.transform.position + Vector3.down)
            {
                if (!MoveableTiles.Contains(block))
                {
                    MoveableTiles.Add(block);
                    block.GetComponent<MoveTile>().SetToMoveable();
                    block.GetComponent<MoveTile>().MoveCost = (int)Vector3.Distance(Player.transform.position + Vector3.down, block.transform.position);
                }

                SetMoveRange(range - 1, block.transform.position);
            }
        }
    }

    // Make Remove Move Range
    public void ClearMovementRange()
    {
        foreach(GameObject block in MoveableTiles)
        {
            block.GetComponent<MoveTile>().SetToNotMoveable();
        }

        MoveableTiles.Clear();
    }

    //update to check if valid movement
    public void ChooseMovement()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out RaycastHit hit, 100))
        {
            if (hit.transform.GetComponent<MoveTile>())
            {
                if (hit.transform.gameObject != _hoveredObject)
                {
                    _hoveredObject?.GetComponent<MoveTile>().NotHovering();
                }

                _hoveredObject = hit.transform.gameObject;
                _hoveredObject.GetComponent<MoveTile>().Hovering();
            }
        }
        else if (_hoveredObject != null)
        {
            _hoveredObject.GetComponent<MoveTile>().NotHovering();
            _hoveredObject = null;
        }

        if (Input.GetMouseButtonDown(0) && _hoveredObject != null && _hoveredObject.GetComponent<MoveTile>().CanMove)
        {
            MovePlayer(_hoveredObject.GetComponent<MoveTile>());
            _hoveredObject.GetComponent<MoveTile>().CanMove = false;
        }
    }

    public void MovePlayer(MoveTile block)
    {
        if (MovesAvailible >= block.MoveCost)
        {
            print(block.MoveCost);
            MovesAvailible -= block.MoveCost;
            Vector3 previousPosition = Player.transform.position;
            Player.transform.position = new Vector3(block.transform.position.x, 1, block.transform.position.z);
            UpdateSurroundings(CalculateDirection(previousPosition, Player.transform.position));
            ClearMovementRange();
        }
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
