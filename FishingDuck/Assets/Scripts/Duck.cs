using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Duck : MonoBehaviour
{
    [SerializeField] Transform posToGo;
    [SerializeField] float timeToGo = 2f;
    [SerializeField] bool hasBeenCaught;
    [SerializeField] MeshRenderer meshRend;
    float randomZ;
    void Start()
    {
        randomZ = Random.Range(-.5f,.5f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomZ);
        Vector3 randomPosToGo = posToGo.position;
        randomPosToGo = new Vector3(randomPosToGo.x, randomPosToGo.y, randomPosToGo.z + randomZ);
        transform.DOMove(randomPosToGo, timeToGo);
    }

    public void SetPosToGo(Transform newPos)
    {
        posToGo = newPos;
    }

    void Reset()
    {
        SetPosToGo(FindObjectOfType<DuckSpawn>().transform);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<DuckSpawn>())
        {
            collision.gameObject.GetComponent<DuckSpawn>().SpawnNewDuck(this);
        }
    }

    public void SetHasBeenCaught(bool caught)
    {
        hasBeenCaught = caught;
        if (caught)
        {
            XPManager.Instance.AddXP(200);
        }
    }

    public void ChangeColor(Color newCol)
    {
        timeToGo = Random.RandomRange(1, 2);
        Material newMat = meshRend.materials[0];
        newMat.SetColor("_BaseColor", newCol);
        meshRend.sharedMaterials[0] = newMat;
        Debug.Log(newCol);
    }

    public void RandomColor()
    {
        ChangeColor(Random.ColorHSV());
    }

}
