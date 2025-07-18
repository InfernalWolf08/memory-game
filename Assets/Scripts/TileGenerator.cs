using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [Header("Settings")]
    public Vector2 startPoint;
    public GameObject tile;
    public Vector2 gridSize;
    public float offset;

    [Header("Data")]
    public GameObject[] tiles;
    private int amount;

    void Awake()
    {
        // Initialize
        amount = 1;
        tiles = generate(startPoint, tile, gridSize, offset);
    }

    // Custom functions
    public GameObject[] generate(Vector2 startPoint, GameObject tile, Vector2 gridSize, float offset)
    {
        // Generate tiles
        for (int y=0; y<gridSize.y; y++)
        {
            for (int x=0; x<gridSize.x; x++)
            {
                GameObject clone = Instantiate(tile, new Vector3(startPoint.x+(offset*x), startPoint.y+(offset*y), 20), Quaternion.identity, transform);
                clone.name += $" ({amount})";
                amount += 1;
            }
        }

        // Put tiles into an array
        int tileAmount = transform.childCount;
        GameObject[] tiles = new GameObject[tileAmount];

        for (int i=0; i<tileAmount; i++)
        {
            tiles[i] = transform.GetChild(i).gameObject;
        }

        return tiles;
    }
}