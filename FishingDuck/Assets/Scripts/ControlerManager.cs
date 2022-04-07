using UnityEngine;

public class ControlerManager : MonoBehaviour
{
    public FishingRoad fishingRoad;
    Vector2 positionAtFirstInput;
    float timePress;

    public bool isFishing;

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

            timePress += Time.deltaTime;

            int touchesInt = Input.touchCount;
            if (touchesInt > 0)
            {
                Touch touch = Input.GetTouch(0);
                fishingRoad.MovePosition(positionAtFirstInput - touch.position);
            }
            else
            {
                Vector2 mousePosScreen = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                fishingRoad.MovePosition(positionAtFirstInput - mousePosScreen);
            }

            if (timePress > .25f)
            {
                isFishing = true;
            }
        }
        else
        {
            timePress = 0;
            isFishing = false;
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
        else
        {
            positionAtFirstInput = Input.mousePosition;
        }
    }
}
