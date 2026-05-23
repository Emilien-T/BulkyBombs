using UnityEngine;

public class BombSpawner : Controller<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    //For debug
    [SerializeField] private float spawnInterval = 2f;

    // Update is called once per frame
    void Update()
    {
        while (true)
        {
            SpawnBomb();
            System.Threading.Thread.Sleep((int)(spawnInterval * 1000));
        }
    }

    public void SpawnBomb()
    {
        Instantiate(bombPrefab, transform.position, Quaternion.identity);
    }
}
