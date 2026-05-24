using System.Collections;
using UnityEngine;

public class BombSpawner : Controller<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    private BombController currentBomb;

    private void Start()
    {
        SpawnBomb();
    }
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
        currentBomb.bombTimer = 45f;
        StartCoroutine(bombComingInSound(0));
    }
    private IEnumerator bombComingInSound(float delay) 
    {
        yield return new WaitForSeconds(delay);
        AudioLibrary.Instance.BombComingIn();
    }
}
