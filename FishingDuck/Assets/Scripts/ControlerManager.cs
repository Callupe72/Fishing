using UnityEngine;

public class ControlerManager : MonoBehaviour
{
    public FishingRoad fishingRoad;
    Vector2 positionAtFirstInput;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, .5f);
    }

    private void Reset()
    {
        fishingRoad = FindObjectOfType<FishingRoad>();
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (Input.GetMouseButton(0))
        {
            if (!fishingRoad.GetIsFishing())
            {
                fishingRoad.StartFishing();
            }

            Touch touch = Input.GetTouch(0);
            fishingRoad.MovePosition(positionAtFirstInput - touch.position);
        }
        else
        {
            if (fishingRoad.GetIsFishing())
            {
                fishingRoad.EndFishing();
            }
        }


        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            positionAtFirstInput = touch.position;
        }
    }
}
