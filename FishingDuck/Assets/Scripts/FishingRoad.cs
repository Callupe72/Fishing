using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishingRoad : MonoBehaviour
{
    bool isFishing;
    [SerializeField] float timeFishAnim = .5f;
    [SerializeField] Transform fishingRoadParent;
    [SerializeField] DuckSpawn duckSpawn;
    [SerializeField] Transform fishingCrochet;

    [SerializeField] GameObject detectionArea;
    Vector3 firstPos;

    [SerializeField] ControlerManager controler;

    public Duck duckColliding;
    [SerializeField] float sensivity = 1000;
    [SerializeField] float maxClamp = 10;
    TweenerCore<Quaternion, Quaternion, NoOptions> rotate;

    [SerializeField] Image duckImg;
    [SerializeField] GameObject duckAnim;
    [SerializeField] int numberFramesStop = 1;
    [SerializeField] int numberFramesWhite = 1;
    [SerializeField] Image imgWhite;

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
        fishingRoadParent.DORotateQuaternion(Quaternion.Euler(new Vector3(-180, 0, -105.719f)), timeFishAnim).OnComplete(SpawnTarget);
    }


    public void EndFishing()
    {
        UnSpawnTarget();
        rotate.Kill();
        transform.position = firstPos;
        isFishing = false;
        fishingRoadParent.DORotateQuaternion(Quaternion.Euler(new Vector3(-90, 0, -105.719f)), timeFishAnim).OnComplete(UnSpawnTarget);

        if (duckColliding != null)
        {
            GetDuck();
        }
    }

    void SpawnTarget()
    {

        if (!controler.isFishing)
            return;

        detectionArea.SetActive(true);

        AudioManager.Instance.Play2DSound("TouchWater");
        StartCoroutine(SpawnParticle());
    }

    IEnumerator SpawnParticle()
    {
        yield return new WaitForSeconds(.01f);
        ParticlesManager.Instance.SpawnParticles("FishingTouchWater", detectionArea.transform.position, Vector3.zero);
    }

    void UnSpawnTarget()
    {
        detectionArea.SetActive(false);
    }

    void GetDuck()
    {
        StartCoroutine(StopTime());
        if (duckColliding.scriptableDucks.color != Color.white)
        {
            duckImg.color = duckColliding.scriptableDucks.color;
            duckAnim.SetActive(true);
            ComboManager.Instance.SpawnText(duckColliding.scriptableDucks.color, duckColliding.transform.position + new Vector3(0, 1, 0)); ;
            StartCoroutine(ActiveFalseDuck());
            ParticlesManager.Instance.SpawnParticles("GetDuck", duckColliding.transform, Vector3.zero, false);

            AudioManager.Instance.Play2DSound("GetDuck");
            AudioManager.Instance.Play2DSound("GetDuck2");
            CameraShake.Instance.Shake(.1f, .015f);
            PostProcessManager.Instance.SetGreenColor();
            ParticlesManager.Instance.SpawnParticles("GetDuckText", duckColliding.transform.position + new Vector3(-.25f, .5f, 0), Vector3.zero);
            StartCoroutine(Flash());
            ChangeFov();
        }
        else
        {
            CameraShake.Instance.Shake(.15f, .01f);
            AudioManager.Instance.Play2DSound("GetBadDuck");
            PostProcessManager.Instance.SetRedColor();
            ParticlesManager.Instance.SpawnParticles("GetBadDuck", duckColliding.transform.position + new Vector3(-.25f, .5f, 0), Vector3.zero);
        }
        duckColliding.SetHasBeenCaught(true);
        duckSpawn.SpawnNewDuck(duckColliding);
        duckColliding = null;
    }

    IEnumerator StopTime()
    {
        Time.timeScale = 0;
        for (int i = 0; i < numberFramesStop; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        Time.timeScale = 1;
    }
    IEnumerator Flash()
    {
        imgWhite.gameObject.SetActive(true);
        for (int i = 0; i < numberFramesWhite; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        imgWhite.gameObject.SetActive(false);
    }

    public void ChangeFov()
    {
        float fovBefore = Camera.main.fieldOfView;
        Camera.main.fieldOfView = 90;
        StartCoroutine(FovGoBack(fovBefore));
    }

    IEnumerator FovGoBack(float currentFovs)
    {
        while (Camera.main.fieldOfView > currentFovs)
        {
            Camera.main.fieldOfView -= 1 * 3f;
            yield return new WaitForSeconds(.05f);
        }

        Camera.main.fieldOfView = currentFovs;
    }

    IEnumerator ActiveFalseDuck()
    {
        yield return new WaitForSeconds(1f);
        duckAnim.SetActive(false);
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
        posZ = Mathf.Clamp(posZ, firstPos.z - maxClamp, firstPos.z + maxClamp);
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
