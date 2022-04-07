using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollision : MonoBehaviour
{
    [SerializeField] FishingRoad fishingRoad;
    [SerializeField] MeshRenderer mesh;
    [SerializeField] Material matNormal;
    [SerializeField] Material matGrow;
    [SerializeField] Texture matGrowText;
    [SerializeField] Texture matNormalText;

    void Reset()
    {
        fishingRoad = FindObjectOfType<FishingRoad>();
        mesh = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(fishingRoad.duckColliding == null)
        {
            mesh.sharedMaterial.SetTexture("_BaseMap", matNormalText);
            mesh.materials[0] = matNormal;
            mesh.sharedMaterials[0].SetColor("_BaseColor", Color.white);
            transform.DOScale(Vector3.one * 0.02043299f, .1f);
        }
        else
        {
            mesh.sharedMaterial.SetTexture("_BaseMap", matGrowText);
            transform.DOScale(Vector3.one * 0.0324435f * 1.2f, .1f);
            if (fishingRoad.duckColliding.scriptableDucks.color == Color.white)
                mesh.sharedMaterials[0].SetColor("_BaseColor", Color.red);
            else
            mesh.sharedMaterials[0].SetColor("_BaseColor", Color.green);
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
