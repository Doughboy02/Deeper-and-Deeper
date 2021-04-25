using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int MapWidth;
    public GameObject[] MapBlocks;
    public static MapManager instance;

    [SerializeField]
    public GameObject[,] View;
    private Dictionary<Vector3, GameObject> _map = new Dictionary<Vector3, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = GetComponent<MapManager>();
            StartCoroutine(GenerateMap(MapWidth));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMap(Vector3 position, Vector3 previousPosition, int range)
    {
        /*
        // OOOOOOOOOOOF PLEASE DONT LOOK AT THIS BAD CODE.... it works....
        Vector3 direction = (position - previousPosition).normalized;
        direction = new Vector3(Mathf.CeilToInt(direction.x), 0, Mathf.CeilToInt(direction.z));
        Vector3 newPosition = position + (direction * (ViewDistance));

        if (direction.x != 0 && direction.z != 0)
        {
            AddBlocks(new Vector3(newPosition.x, 0, (int)newPosition.z / 2), new Vector3(direction.x, 0, 0));
            AddBlocks(new Vector3((int)newPosition.x / 2, 0, newPosition.z), new Vector3(0, 0, direction.z));
        }
        else
        {
            AddBlocks(newPosition, direction);
        }*/
    }

    private void AddBlocks(Vector3 newPosition, Vector3 direction)
    {
        if (!TryGetPosition(newPosition, out GameObject block))
        {
            for (int i = -MapWidth; i <= MapWidth; i++)
            {
                AddPosition(newPosition + Vector3.Cross(direction, Vector3.up).normalized * i, Instantiate(GetBlockFromList()));
            }
        }
    }

    public IEnumerator GenerateMap(int mapSize)
    {
        View = new GameObject[mapSize, mapSize];
        if (_map.Count > 0) _map.Clear();

        // update this random logic
        for (int z=0; z<mapSize; z++)
        {
            for (int x=0; x<mapSize; x++)
            {
                AddPosition(new Vector3(x, 0, z), Instantiate(GetBlockFromList()));
                View[x, z] = _map[new Vector3(x, 0, z)];
                yield return null;
            }
        }
    }

    private GameObject GetBlockFromList()
    {
        return MapBlocks[Random.Range(0, MapBlocks.Length)];
    }

    /// <summary>
    /// Tries to get the block on the map, returns false if doesnt exist.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="block"></param>
    /// <returns></returns>
    public bool TryGetPosition(Vector3 position, out GameObject block)
    {
        if (_map.TryGetValue(position, out block))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Adds new position or updates existing one.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="block"></param>
    public void AddPosition(Vector3 position, GameObject block)
    {
        if (_map.ContainsKey(position))
        {
            _map[position] = block;
        }
        else
        {
            _map.Add(position, block);
        }

        block.transform.position = position;
        block.transform.parent = transform;
    }

    /// <summary>
    /// Removes position from map if it exists.
    /// </summary>
    /// <param name="position"></param>
    public void RemovePosition(Vector3 position)
    {
        if (_map.ContainsKey(position))
        {
            _map.Remove(position);
        }
    }
}
