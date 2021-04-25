using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour
{
    public GameObject Player;
    public int ViewRange = 3;
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
            RangeHandler.ClearMovementRange(MoveableTiles);
            RangeHandler.SetMovementRange(MovesAvailible, Player.transform.position, MoveableTiles);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            MovesAvailible = MovementRange;
        }

        if (CanMove) ChooseMovement();
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
            /*
            Vector3 previousPosition = Player.transform.position + Vector3.down;
            Player.transform.position = new Vector3(block.transform.position.x, 1, block.transform.position.z);
            MapManager.instance.UpdateMap(Player.transform.position + Vector3.down, previousPosition, block.MoveCost);
            MovesAvailible -= block.MoveCost;
            RangeHandler.ClearMovementRange(MoveableTiles);*/

            //Vector3 previousPosition = Player.transform.position + Vector3.down;
            Player.transform.position = new Vector3(block.transform.position.x, 1, block.transform.position.z);
            MovesAvailible -= block.MoveCost;
            RangeHandler.ClearMovementRange(MoveableTiles);


            //MapManager.instance.UpdateMap(Player.transform.position + Vector3.down, previousPosition, block.MoveCost);

            /*
            for(int z=0; z<MapManager.instance.MapWidth * 2 + 1; z++)
            {
                for (int x=0; x<MapManager.instance.MapWidth * 2 + 1; x++)
                {
                    MapManager.instance.View[x, z].transform.position += previousPosition - new Vector3(newPosition.x, 0, newPosition.z);
                }
            }

            */
            RaycastHit[] hits = Physics.BoxCastAll(Player.transform.position + Vector3.up * 2, new Vector3(ViewRange, 1, ViewRange) / 2, -Player.transform.up, Player.transform.rotation, 10, 1 << 9);
            if (hits != null)
            {
                print(hits.Length);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Player.transform.position - Player.transform.up, new Vector3(ViewRange, 1, ViewRange));
    }

    private Direction CalculateDirection(Vector3 previousPosition, Vector3 currentPosition)
    {
        Vector3 direction = previousPosition - currentPosition;
        print(direction);
        return Direction.Forward;
    }
}

public enum Direction
{
    Forward,
    Backward,
    Left,
    Right
}
