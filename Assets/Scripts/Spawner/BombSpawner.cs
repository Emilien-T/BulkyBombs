using System.Collections;
using UnityEngine;

public class BombSpawner : Controller<BombSpawner>
{
    [SerializeField] private GameObject bombPrefab;
    private BombController currentBomb;
    public int score;
    public AnimationCurve curve;

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
        currentBomb.bombTimer = 55f - (30) * curve.Evaluate(Mathf.Clamp(score, 0,5)/5);
        StartCoroutine(bombComingInSound(0));
    }
    private IEnumerator bombComingInSound(float delay) 
    {
        yield return new WaitForSeconds(delay);
        AudioLibrary.Instance.BombComingIn();
    }
}
