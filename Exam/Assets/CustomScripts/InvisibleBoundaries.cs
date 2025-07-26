using UnityEngine;

public class InvisibleBoundaries : MonoBehaviour
{
    [Header("Zone Settings")]
    public float zoneWidth = 50f;
    public float zoneLength = 50f;
    public float wallHeight = 10f;
    public float wallThickness = 1f;

    void Start()
    {
        CreateBoundaries();
    }

    void CreateBoundaries()
    {
        Vector3 center = transform.position;

        // Mur Nord
        CreateWall(new Vector3(center.x, center.y + wallHeight / 2, center.z + zoneLength / 2 + wallThickness / 2),
                   new Vector3(zoneWidth + wallThickness * 2, wallHeight, wallThickness));

        // Mur Sud  
        CreateWall(new Vector3(center.x, center.y + wallHeight / 2, center.z - zoneLength / 2 - wallThickness / 2),
                   new Vector3(zoneWidth + wallThickness * 2, wallHeight, wallThickness));

        // Mur Est
        CreateWall(new Vector3(center.x + zoneWidth / 2 + wallThickness / 2, center.y + wallHeight / 2, center.z),
                   new Vector3(wallThickness, wallHeight, zoneLength));

        // Mur Ouest
        CreateWall(new Vector3(center.x - zoneWidth / 2 - wallThickness / 2, center.y + wallHeight / 2, center.z),
                   new Vector3(wallThickness, wallHeight, zoneLength));
    }

    void CreateWall(Vector3 position, Vector3 size)
    {
        GameObject wall = new GameObject("InvisibleWall");
        wall.transform.position = position;
        wall.transform.SetParent(transform);

        BoxCollider collider = wall.AddComponent<BoxCollider>();
        collider.size = size;
        wall.tag = "Boundary";
    }
}