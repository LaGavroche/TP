using UnityEngine;

public class TerrainWallGenerator : MonoBehaviour
{
    [Header("Wall Settings")]
    public GameObject wallPrefab; // Prefab du mur à instancier
    public float wallHeight = 5f;
    public float wallThickness = 1f;

    void Start()
    {
        GenerateWalls();
    }

    void GenerateWalls()
    {
        Terrain terrain = GetComponent<Terrain>();
        if (terrain == null) return;

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        Vector3 terrainPosition = transform.position;

        // Mur Nord (Z+)
        CreateWall(
            new Vector3(terrainPosition.x + terrainSize.x / 2, terrainPosition.y + wallHeight / 2, terrainPosition.z + terrainSize.z + wallThickness / 2),
            new Vector3(terrainSize.x, wallHeight, wallThickness)
        );

        // Mur Sud (Z-)
        CreateWall(
            new Vector3(terrainPosition.x + terrainSize.x / 2, terrainPosition.y + wallHeight / 2, terrainPosition.z - wallThickness / 2),
            new Vector3(terrainSize.x, wallHeight, wallThickness)
        );

        // Mur Est (X+)
        CreateWall(
            new Vector3(terrainPosition.x + terrainSize.x + wallThickness / 2, terrainPosition.y + wallHeight / 2, terrainPosition.z + terrainSize.z / 2),
            new Vector3(wallThickness, wallHeight, terrainSize.z)
        );

        // Mur Ouest (X-)
        CreateWall(
            new Vector3(terrainPosition.x - wallThickness / 2, terrainPosition.y + wallHeight / 2, terrainPosition.z + terrainSize.z / 2),
            new Vector3(wallThickness, wallHeight, terrainSize.z)
        );
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        if (wallPrefab != null)
        {
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity, transform);
            wall.transform.localScale = scale;
        }
        else
        {
            // Créer un cube basique si pas de prefab
            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = position;
            wall.transform.localScale = scale;
            wall.transform.SetParent(transform);
            wall.name = "TerrainWall";
        }
    }
}