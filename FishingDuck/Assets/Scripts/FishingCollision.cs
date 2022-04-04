using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCollision : MonoBehaviour
{
    [SerializeField] FishingRoad fishingRoad;
    

    void Reset()
    {
        fishingRoad = FindObjectOfType<FishingRoad>();
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
