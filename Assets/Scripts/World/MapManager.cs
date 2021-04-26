using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public int MapWidth;
    public int MapLength;
    public float TerrainVariation = 10;
    public GameObject[] MapBlocks;
    public static MapManager instance;
    public MapMovement Player;
    public int EnemyAmount = 1;
    public GameObject[] EnemyList;

    [SerializeField]
    private Dictionary<Vector3, GameObject> _map = new Dictionary<Vector3, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = GetComponent<MapManager>();
            StartCoroutine(GenerateMap());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject GetEnemy()
    {
        EnemyAmount--;
        return EnemyList[Random.Range(0, EnemyList.Length)];
    }

    public IEnumerator GenerateMap()
    {
        if (_map.Count > 0) _map.Clear();

        // update this random logic
        for (int z=0; z<MapWidth; z++)
        {
            for (int x=0; x<MapLength; x++)
            {
                AddPosition(new Vector3(x, 0, z), Instantiate(GetBlockFromList(x, z)));
                _map[new Vector3(x, 0, z)].GetComponent<MoveTile>().SetOutOfView();
                yield return null;
            }
        }

        Player.GetComponent<MapMovement>().UnfreezeMovement();
    }

    private GameObject GetBlockFromList(float x, float z)
    {
        float height = Mathf.PerlinNoise(x / TerrainVariation, z / TerrainVariation);
        int randomNum = (int)(height * MapBlocks.Length);

        return MapBlocks[randomNum];
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
