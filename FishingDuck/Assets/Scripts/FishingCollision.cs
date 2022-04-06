using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollision : MonoBehaviour
{
    [SerializeField] FishingRoad fishingRoad;
    [SerializeField] MeshRenderer mesh;

    void Reset()
    {
        fishingRoad = FindObjectOfType<FishingRoad>();
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(fishingRoad.duckColliding == null)
        {
            mesh.sharedMaterials[0].SetColor("_BaseColor", Color.white);
        }
        else
        {
            if(fishingRoad.duckColliding.scriptableDucks.color == Color.white)
                mesh.sharedMaterials[0].SetColor("_BaseColor", Color.red);
            else
            mesh.sharedMaterials[0].SetColor("_BaseColor", fishingRoad.duckColliding.scriptableDucks.color);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Duck>())
        {
            fishingRoad.StartCollideDuck(collision.GetComponent<Duck>());
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Duck>())
        {
            fishingRoad.EndCollideDuck(collision.GetComponent<Duck>());
        }
    }
}
