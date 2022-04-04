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
        Duck duckSpawned = Instantiate(duckPrefab, duckSpawn.position, Quaternion.Euler(new Vector3(-90,0,0))).GetComponent<Duck>();
        duckSpawned.SetPosToGo(transform);
        if (collidingDuck != null)
            Destroy(collidingDuck.gameObject);
    }
}
