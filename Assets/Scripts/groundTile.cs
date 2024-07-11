using UnityEngine;

public class groundTile : MonoBehaviour
{
    groundSpawner groundSpawner;
    // Start is called before the first frame update
    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<groundSpawner>();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            groundSpawner.SpawnTile(true); // Yeni zemin karesi oluþtur
            Destroy(gameObject, 2); // Bu zemin karesini yok et (opsiyonel)
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    public GameObject obstaclePrefab;

    public void spawnObstacle ()
    {

        // int obstacleSpawnIndex = Random.Range(2, 5);
        // Transform spawnPoint = transform.GetChild(obstacleSpawnIndex).transform;
        // Instantiate(obstaclePrefab, spawnPoint.position, Quaternion.identity, transform);
        int obstacleSpawnIndex = Random.Range(2, 5);
        if (obstacleSpawnIndex < transform.childCount)
        {
            Transform spawnPoint = transform.GetChild(obstacleSpawnIndex);

            // Adjust the y position
            Vector3 adjustedPosition = spawnPoint.position;
            adjustedPosition.y += 0.2f; // Increase y position by 1 unit (adjust the value as needed)

            Instantiate(obstaclePrefab, adjustedPosition, Quaternion.identity, transform);
        }
        else
        {
            Debug.LogWarning("Obstacle spawn index is out of bounds!");
        }

    }


}
