using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField] Duck duckPrefab;
    [SerializeField] int duckNumbers;

    public static SpawnerManager Instance;
    public List<int> intList = new List<int>();

    [SerializeField] Transform duckSpawn;
    [SerializeField] Transform duckColl;
    int currentNum;
    [SerializeField] float raidusSpawn = 1.5f;

    [SerializeField] ScriptableDucks[] duckScripts;
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

    private void Start()
    {
        Spawn();
    }
    public void SpawnDuck(List<ScriptableObjectives> obj)
    {
        return;
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

    void Spawn()
    {
        intList.Add(duckScripts[0].spawnRate);
        for (int i = 1; i < duckScripts.Length; i++)
        {
            if (!duckScripts[i].isNormal)
                intList.Add(intList[i - 1] + duckScripts[i].spawnRate);
        }

        intList.Add(100 - (intList[intList.Count - 1]));


        for (int i = 0; i < duckNumbers; i++)
        {
            int random = Random.Range(0, 99);
            int doEverything = 0;
            while (doEverything < intList.Count)
            {
                if (random > intList[doEverything])
                    doEverything++;
                else
                {
                    SpawnNewDuck(duckScripts[doEverything]);
                    break;
                }
            }
           
        }
    }

    public void SpawnNewDuck(ScriptableDucks sd)
    {
        Duck duckSpawned = Instantiate(duckPrefab, duckSpawn.position, Quaternion.Euler(new Vector3(-90, 0, 0))).GetComponent<Duck>();
        Vector3 randomPos = new Vector3(Random.Range(-raidusSpawn, raidusSpawn), 0, Random.Range(-raidusSpawn, raidusSpawn));
        duckSpawned.scriptableDucks = sd;
        duckSpawned.transform.position += randomPos;
        duckSpawned.SetPosToGo(duckColl);
        randomPos = new Vector3(0, 0, randomPos.z);
        duckSpawned.vectorposToGo = duckColl.position + randomPos;
    }

    void SpawnDuck(Color color)
    {
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
