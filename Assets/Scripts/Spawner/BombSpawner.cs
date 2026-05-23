using UnityEngine;

public class BombSpawner : Controller<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
        bomb.GetComponent<BombController>().bombTimer = 4f;
    }
}
