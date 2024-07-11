using UnityEngine;

public class groundSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject groundTile;
    Vector3 nextSpawnPoint;
    int obstacleSpawnOK =0;
    public void SpawnTile(bool spawnItems)
    {
       GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
       nextSpawnPoint= temp.transform.GetChild(1).transform.position;
        if(spawnItems ==true)
        {
            obstacleSpawnOK = Random.Range(0, 2);
            if(obstacleSpawnOK==0)
                temp.GetComponent<groundTile>().spawnObstacle();
        }
    }
    // Update is called once per frame
    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            if(i<5)
            {
                SpawnTile(false); 
            }
            else
                SpawnTile(true);
        }
    }       
}
