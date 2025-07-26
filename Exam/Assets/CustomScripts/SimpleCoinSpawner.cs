using UnityEngine;

public class SimpleCoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab;
    public int totalCoins = 20;

    [Header("Zone 1 - Entree")]
    public Vector3 zone1Center = new Vector3(0, 0, -13);
    public Vector3 zone1Size = new Vector3(4.8f, 1, 5.5f);
    public int zone1Coins = 3;

    [Header("Zone 2 - Couloir Haut")]
    public Vector3 zone2Center = new Vector3(-9, 0, 13.7f);
    public Vector3 zone2Size = new Vector3(7, 3, 10);
    public int zone2Coins = 5;

    [Header("Zone 3 - Centre")]
    public Vector3 zone3Center = new Vector3(-12.8f, 0, -2);
    public Vector3 zone3Size = new Vector3(5, 3, 11);
    public int zone3Coins = 4;

    [Header("Zone 4 - Couloir Droit")]
    public Vector3 zone4Center = new Vector3(9.5f, 0, -5);
    public Vector3 zone4Size = new Vector3(2.6f, 3, 13);
    public int zone4Coins = 4;

    [Header("Zone 5 - Couloir Central Horizontal")]
    public Vector3 zone5Center = new Vector3(-1, 0, 5);
    public Vector3 zone5Size = new Vector3(22, 3, 2);
    public int zone5Coins = 4;

    [Header("Zone 6 - Zone Centre-Droite")]
    public Vector3 zone6Center = new Vector3(1, 0, 12);
    public Vector3 zone6Size = new Vector3(6, 3, 5);
    public int zone6Coins = 3;

    [Header("Zone 7 - Couloir Bas")]
    public Vector3 zone7Center = new Vector3(-7, 0, -11);
    public Vector3 zone7Size = new Vector3(2.4f, 3, 8);
    public int zone7Coins = 4;

    [Header("Zone 8 - Zone Est")]
    public Vector3 zone8Center = new Vector3(6, 0, -12);
    public Vector3 zone8Size = new Vector3(3, 3, 3);
    public int zone8Coins = 3;

    [Header("Zone 9 - Intersections")]
    public Vector3 zone9Center = new Vector3(-0.5f, 0, 0);
    public Vector3 zone9Size = new Vector3(15, 3, 2.5f);
    public int zone9Coins = 2;

    [Header("Zone 10 - Haut Droite")]
    public Vector3 zone10Center = new Vector3(8, 0, 10);
    public Vector3 zone10Size = new Vector3(4, 3, 6);
    public int zone10Coins = 3;

    [Header("Zone 11 - Coin Supérieur Droit")]
    public Vector3 zone11Center = new Vector3(12, 0, 16);
    public Vector3 zone11Size = new Vector3(3, 3, 4);
    public int zone11Coins = 2;

    [Header("Zone 12 - Petit Espace Haut")]
    public Vector3 zone12Center = new Vector3(-2, 0, 9);
    public Vector3 zone12Size = new Vector3(4, 3, 3);
    public int zone12Coins = 2;

    [Header("Spawn Settings")]
    public float minHeight = 0.5f;
    public float maxHeight = 2f;

    void Start()
    {
        if (coinPrefab != null)
        {
            SpawnCoins();
        }
    }

    [Header("Grid Settings")]
    public float coinSpacing = 2f; // Espacement entre les pièces
    public float heightAboveGround = 1f; // Hauteur au-dessus du sol

    public void SpawnCoins()
    {
        ClearOldCoins();

        // Calculer automatiquement le nombre de pièces pour chaque zone
        int zone1Count = CalculateMaxCoins(zone1Size);
        int zone2Count = CalculateMaxCoins(zone2Size);
        int zone3Count = CalculateMaxCoins(zone3Size);
        int zone4Count = CalculateMaxCoins(zone4Size);
        int zone5Count = CalculateMaxCoins(zone5Size);
        int zone6Count = CalculateMaxCoins(zone6Size);
        int zone7Count = CalculateMaxCoins(zone7Size);
        int zone8Count = CalculateMaxCoins(zone8Size);
        int zone9Count = CalculateMaxCoins(zone9Size);
        int zone10Count = CalculateMaxCoins(zone10Size);
        int zone11Count = CalculateMaxCoins(zone11Size);
        int zone12Count = CalculateMaxCoins(zone12Size);

        SpawnInZone(zone1Center, zone1Size, zone1Count, "Zone1");
        SpawnInZone(zone2Center, zone2Size, zone2Count, "Zone2");
        SpawnInZone(zone3Center, zone3Size, zone3Count, "Zone3");
        SpawnInZone(zone4Center, zone4Size, zone4Count, "Zone4");
        SpawnInZone(zone5Center, zone5Size, zone5Count, "Zone5");
        SpawnInZone(zone6Center, zone6Size, zone6Count, "Zone6");
        SpawnInZone(zone7Center, zone7Size, zone7Count, "Zone7");
        SpawnInZone(zone8Center, zone8Size, zone8Count, "Zone8");
        SpawnInZone(zone9Center, zone9Size, zone9Count, "Zone9");
        SpawnInZone(zone10Center, zone10Size, zone10Count, "Zone10");
        SpawnInZone(zone11Center, zone11Size, zone11Count, "Zone11");
        SpawnInZone(zone12Center, zone12Size, zone12Count, "Zone12");

        int totalCoins = zone1Count + zone2Count + zone3Count + zone4Count + zone5Count +
                        zone6Count + zone7Count + zone8Count + zone9Count + zone10Count +
                        zone11Count + zone12Count;

        Debug.Log($"Toutes les zones remplies ! Total: {totalCoins} pièces créées !");
    }

    int CalculateMaxCoins(Vector3 zoneSize)
    {
        // Calculer combien de pièces peuvent tenir dans la zone avec l'espacement
        int coinsX = Mathf.FloorToInt(zoneSize.x / coinSpacing);
        int coinsZ = Mathf.FloorToInt(zoneSize.z / coinSpacing);

        // Minimum 1 pièce par zone, maximum selon la taille
        return Mathf.Max(1, coinsX * coinsZ);
    }

    void SpawnInZone(Vector3 center, Vector3 size, int count, string zoneName)
    {
        Vector3 worldCenter = transform.position + center;

        // Calculer la grille optimale
        int coinsX = Mathf.FloorToInt(size.x / coinSpacing);
        int coinsZ = Mathf.FloorToInt(size.z / coinSpacing);

        // Espacement réel entre les pièces
        float actualSpacingX = size.x / (coinsX + 1);
        float actualSpacingZ = size.z / (coinsZ + 1);

        int coinsSpawned = 0;

        // Spawn en grille parfaite
        for (int x = 0; x < coinsX; x++)
        {
            for (int z = 0; z < coinsZ; z++)
            {
                // Position dans la grille
                Vector3 gridPos = worldCenter + new Vector3(
                    -size.x / 2 + actualSpacingX * (x + 1),
                    0,
                    -size.z / 2 + actualSpacingZ * (z + 1)
                );

                // Raycast pour trouver le sol
                Vector3 rayStart = new Vector3(gridPos.x, gridPos.y + 50f, gridPos.z);
                RaycastHit hit;

                Vector3 finalPos;
                if (Physics.Raycast(rayStart, Vector3.down, out hit, 100f))
                {
                    finalPos = hit.point + Vector3.up * heightAboveGround; // Utilise la hauteur personnalisée
                }
                else
                {
                    finalPos = new Vector3(gridPos.x, transform.position.y + heightAboveGround, gridPos.z);
                }

                GameObject coin = Instantiate(coinPrefab, finalPos, Quaternion.identity, transform);
                coin.name = $"Coin_{zoneName}_{coinsSpawned + 1}";

                coinsSpawned++;
            }
        }

        Debug.Log($"Zone {zoneName}: {coinsSpawned} pièces (grille {coinsX}x{coinsZ})");
    }

    Vector3 GetRandomPositionInZone(Vector3 center, Vector3 size)
    {
        Vector3 worldCenter = transform.position + center;

        float randomX = Random.Range(-size.x / 2, size.x / 2);
        float randomZ = Random.Range(-size.z / 2, size.z / 2);

        return new Vector3(worldCenter.x + randomX, worldCenter.y, worldCenter.z + randomZ);
    }

    void ClearOldCoins()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            if (child.name.StartsWith("Coin_Zone1_") ||
                child.name.StartsWith("Coin_Zone2_") ||
                child.name.StartsWith("Coin_Zone3_") ||
                child.name.StartsWith("Coin_Zone4_") ||
                child.name.StartsWith("Coin_Zone5_") ||
                child.name.StartsWith("Coin_Zone6_") ||
                child.name.StartsWith("Coin_Zone7_") ||
                child.name.StartsWith("Coin_Zone8_") ||
                child.name.StartsWith("Coin_Zone9_") ||
                child.name.StartsWith("Coin_Zone10_") ||
                child.name.StartsWith("Coin_Zone11_") ||
                child.name.StartsWith("Coin_Zone12_"))
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 zone1WorldCenter = transform.position + zone1Center;
        Gizmos.DrawWireCube(zone1WorldCenter, zone1Size);

        Gizmos.color = Color.yellow;
        Vector3 zone2WorldCenter = transform.position + zone2Center;
        Gizmos.DrawWireCube(zone2WorldCenter, zone2Size);

        Gizmos.color = Color.green;
        Vector3 zone3WorldCenter = transform.position + zone3Center;
        Gizmos.DrawWireCube(zone3WorldCenter, zone3Size);

        Gizmos.color = Color.blue;
        Vector3 zone4WorldCenter = transform.position + zone4Center;
        Gizmos.DrawWireCube(zone4WorldCenter, zone4Size);

        Gizmos.color = Color.magenta;
        Vector3 zone5WorldCenter = transform.position + zone5Center;
        Gizmos.DrawWireCube(zone5WorldCenter, zone5Size);

        Gizmos.color = Color.cyan;
        Vector3 zone6WorldCenter = transform.position + zone6Center;
        Gizmos.DrawWireCube(zone6WorldCenter, zone6Size);

        Gizmos.color = new Color(1f, 0.5f, 0f); // Orange
        Vector3 zone7WorldCenter = transform.position + zone7Center;
        Gizmos.DrawWireCube(zone7WorldCenter, zone7Size);

        Gizmos.color = new Color(0.5f, 0f, 1f); // Violet
        Vector3 zone8WorldCenter = transform.position + zone8Center;
        Gizmos.DrawWireCube(zone8WorldCenter, zone8Size);

        Gizmos.color = new Color(0f, 1f, 0.5f); // Vert clair
        Vector3 zone9WorldCenter = transform.position + zone9Center;
        Gizmos.DrawWireCube(zone9WorldCenter, zone9Size);

        Gizmos.color = new Color(1f, 0f, 0.5f); // Rose
        Vector3 zone10WorldCenter = transform.position + zone10Center;
        Gizmos.DrawWireCube(zone10WorldCenter, zone10Size);

        Gizmos.color = new Color(0.5f, 0.5f, 0f); // Jaune foncé
        Vector3 zone11WorldCenter = transform.position + zone11Center;
        Gizmos.DrawWireCube(zone11WorldCenter, zone11Size);

        Gizmos.color = Color.black; // Noir - très visible !
        Vector3 zone12WorldCenter = transform.position + zone12Center;
        Gizmos.DrawWireCube(zone12WorldCenter, zone12Size);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}