using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;
    [SerializeField] List<ScriptableObjectives> objectivesSo;

    [SerializeField] Animation animator;
    List<ScriptableObjectives> listToSend = new List<ScriptableObjectives>();
    [SerializeField] Image[] objectives;

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
    void Start()
    {
        ChangeObjectives();
    }

    void Update()
    {

    }

    void ChangeObjectives()
    {
        List<ScriptableObjectives> lists = objectivesSo;
        listToSend.Clear();

        for (int i = 0; i < 3; i++)
        {
            int index = Random.RandomRange(0, lists.Count);
            objectives[i].sprite = lists[index].objectiveIconee;
            listToSend.Add(lists[index]);
            lists.RemoveAt(index);
        }

        animator.Play();
        SpawnerManager.Instance.SpawnDuck(listToSend);
    }


}
