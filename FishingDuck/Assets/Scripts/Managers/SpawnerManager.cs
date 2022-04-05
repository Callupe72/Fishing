using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] Duck duckPrefab;
    [SerializeField] int duckNumbers;

    public static SpawnerManager Instance;

    [SerializeField] Transform duckSpawn;
    [SerializeField] Transform duckColl;
    int currentNum;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void SpawnDuck(List<ScriptableObjectives> obj)
    {
        if (obj == null)
            return;

        currentNum = duckNumbers;
        foreach (ScriptableObjectives item in obj)
        {
            for (int i = 0; i < item.duckNum; i++)
            {
                currentNum--;
                SpawnDuck(item.color);
            }
        }

        for (int i = 0; i < currentNum; i++)
        {
            SpawnDuck(null);
        }
    }

    void SpawnDuck(Color color)
    {
        if (color == Color.red)
            Debug.Log("<color=red>Color</color>" + currentNum);
        else if(color == Color.blue)
            Debug.Log("<color=blue>Color</color>" + currentNum);
        else if (color == Color.green)
            Debug.Log("<color=green>Color</color>" + currentNum);
        Duck duckSpawned = Instantiate(duckPrefab, duckSpawn.position, Quaternion.Euler(new Vector3(-90, 0, 0))).GetComponent<Duck>();
        Vector3 randomPos = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
        duckSpawned.transform.position += randomPos;
        duckSpawned.SetPosToGo(duckColl);
        if (color == null)
        {
            duckSpawned.RandomColor();
        }
        else
        {
            duckSpawned.ChangeColor(color);
        }
    }
}
