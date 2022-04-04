using DG.Tweening;
using UnityEngine;

public class FishingRoad : MonoBehaviour
{
    bool isFishing;
    [SerializeField] float timeFishAnim = .5f;
    [SerializeField] Transform fishingRoadParent;
    [SerializeField] DuckSpawn duckSpawn;
    [SerializeField] FishingCollision fishingColl;

    Vector3 firstPos;

    Duck duckColliding;
    [SerializeField] float sensivity = 1000;
    [SerializeField] float maxClamp = 10;

    void Start()
    {
        firstPos = transform.position;
    }

    public bool GetIsFishing()
    {
        return isFishing;
    }
    public void StartFishing()
    {
        isFishing = true;
        fishingRoadParent.DORotate(new Vector3(0, 0, 0), timeFishAnim);
    }


    public void EndFishing()
    {
        transform.position = firstPos;
        isFishing = false;
        fishingRoadParent.DORotate(new Vector3(90, 0, 0), timeFishAnim);


        if (duckColliding != null)
        {
            GetDuck();
        }
    }

    void GetDuck()
    {
        duckColliding.SetHasBeenCaught(true);
        duckSpawn.SpawnNewDuck(duckColliding);
        duckColliding = null;
    }

    public void StartCollideDuck(Duck duckCollide)
    {
        Debug.Log("New Duck ");
        duckColliding = duckCollide;
    }
    public void EndCollideDuck(Duck duckCollide)
    {
        if (duckColliding == duckCollide)
        {
            duckColliding = null;
        }
    }

    public void MovePosition(Vector2 newPos)
    {
        float posZ = transform.position.z - (-newPos.y) / sensivity;
        posZ = Mathf.Clamp(posZ,firstPos.z - maxClamp, firstPos.z + maxClamp);
        transform.position = new Vector3(firstPos.x, firstPos.y, posZ);
    }
}
