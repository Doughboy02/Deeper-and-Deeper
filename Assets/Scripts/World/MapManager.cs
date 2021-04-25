using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int ViewDistance;
    public GameObject[] MapBlocks;
    public static MapManager instance;

    private Dictionary<Vector3, GameObject> _map = new Dictionary<Vector3, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = GetComponent<MapManager>();
            StartCoroutine(GenerateMap(ViewDistance * 2 + 1));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator GenerateMap(int mapSize)
    {
        if (_map.Count > 0) _map.Clear();

        // update this random logic
        for (int z=0; z<mapSize; z++)
        {
            for (int x=0; x<mapSize; x++)
            {
                AddPosition(new Vector3(x, 0, z), Instantiate(GetBlockFromList()));
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
