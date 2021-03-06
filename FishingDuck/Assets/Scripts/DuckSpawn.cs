using UnityEngine;

public class DuckSpawn : MonoBehaviour
{
    [SerializeField] Transform duckSpawn;
    [SerializeField] GameObject duckPrefab;


    void Start()
    {
        SpawnNewDuck(null);
    }

    public void SpawnNewDuck(Duck collidingDuck)
    {
        SpawnerManager.Instance.MakeRandom();
        if (collidingDuck != null)
            Destroy(collidingDuck.gameObject);
    }
}
