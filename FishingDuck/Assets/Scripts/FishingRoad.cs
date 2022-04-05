using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class FishingRoad : MonoBehaviour
{
    bool isFishing;
    [SerializeField] float timeFishAnim = .5f;
    [SerializeField] Transform fishingRoadParent;
    [SerializeField] DuckSpawn duckSpawn;
    [SerializeField] Transform fishingCrochet;

    [SerializeField] GameObject detectionArea;
    Vector3 firstPos;

    public Duck duckColliding;
    [SerializeField] float sensivity = 1000;
    [SerializeField] float maxClamp = 10;
    TweenerCore<Quaternion, Vector3, QuaternionOptions> rotate;

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
        fishingRoadParent.DORotateQuaternion(Quaternion.Euler(new Vector3(180, 0, 0)), timeFishAnim).OnComplete(SpawnTarget);
        
    }


    public void EndFishing()
    {
        UnSpawnTarget();
        rotate.Kill();
        transform.position = firstPos;
        isFishing = false;
        fishingRoadParent.DORotateQuaternion(Quaternion.Euler(new Vector3(-90, 0, 0)), timeFishAnim);

        if (duckColliding != null)
        {
            GetDuck();
        }
    }

    void SpawnTarget()
    {
        detectionArea.SetActive(true);
    }

    void UnSpawnTarget()
    {
        detectionArea.SetActive(false);
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

    private void Update()
    {
        if (detectionArea.activeInHierarchy)
        {
            detectionArea.transform.position = new Vector3(fishingCrochet.transform.position.x, detectionArea.transform.position.y, fishingCrochet.transform.position.z);
        }
    }
}
