using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHandler : MonoBehaviour
{
    private static Vector3[] _moveDirections =
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

    public static void SetMovementRange(int range, Vector3 startingPosition, List<GameObject> MoveableTiles)
    {
        CalculateMoveRange(range, startingPosition + Vector3.down, startingPosition + Vector3.down, MoveableTiles);
    }

    public static void CalculateMoveRange(int range, Vector3 currentPosition, Vector3 startingPosition, List<GameObject> MoveableTiles)
    {
        GameObject block;

        if (range == 0) return;

        for (int j = 0; j < _moveDirections.Length; j++)
        {
            if (MapManager.instance.TryGetPosition(currentPosition + _moveDirections[j], out block) && currentPosition + _moveDirections[j] != startingPosition)
            {
                if (!MoveableTiles.Contains(block))
                {
                    MoveableTiles.Add(block);
                    block.GetComponent<MoveTile>().SetToMoveable();
                    block.GetComponent<MoveTile>().MoveCost = (int)Vector3.Distance(startingPosition, block.transform.position);
                }

                CalculateMoveRange(range - 1, block.transform.position, startingPosition, MoveableTiles);
            }
        }
    }

    public static void ClearMovementRange(List<GameObject> MoveableTiles)
    {
        foreach (GameObject block in MoveableTiles)
        {
            block.GetComponent<MoveTile>().SetToNotMoveable();
        }

        MoveableTiles.Clear();
    }
}
