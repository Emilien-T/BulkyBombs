using UnityEngine;

public class BombSpawner : Controller<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    private BombController currentBomb;

    // Update is called once per frame
    void Update()
    {
        
    }

    public BombController GetCurrentBomb()
    {
        return currentBomb;
    }

    public void SpawnBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, transform.position, bombPrefab.transform.rotation);
        currentBomb = bomb.GetComponent<BombController>();
        currentBomb.bombTimer = 15f;
    }
}
