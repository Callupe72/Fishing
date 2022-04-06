using DG.Tweening;
using UnityEngine;

public class Duck : MonoBehaviour
{
    [SerializeField] Transform posToGo;
    public Vector3 vectorposToGo;
    [SerializeField] float timeToGo = 2f;
    [SerializeField] bool hasBeenCaught;
    [SerializeField] MeshRenderer meshRend;
    float randomZ;

    public ScriptableDucks scriptableDucks;
    bool goUp;
    float timeGoUp = 1f;
    float currentTime;

    void Start()
    {
        randomZ = Random.Range(-.5f, .5f);
        ChangeColor(scriptableDucks.color);
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomZ);
        transform.DOMove(vectorposToGo, timeToGo);
        timeToGo = scriptableDucks.timeToCross;
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
            if(scriptableDucks.color == Color.white)
            XPManager.Instance.AddXP(scriptableDucks.points, Color.red);
            else
                XPManager.Instance.AddXP(scriptableDucks.points, scriptableDucks.color);

        }
    }

    public void ChangeColor(Color newCol)
    {
        GetComponent<MeshFilter>().mesh = scriptableDucks.duckMesh;
        meshRend.materials = scriptableDucks.duckMats;

        if (newCol != Color.white)
        {
            Material newMat = meshRend.materials[0];
            newMat.SetColor("_BaseColor", newCol);
            meshRend.sharedMaterials[0] = newMat;
        }
    }

    public void RandomColor()
    {
        ChangeColor(Random.ColorHSV());
    }

    void Update()
    {
        float amplitude = 0;
        float frequency = 0;

        transform.position += amplitude * (Mathf.Sin(2 * Mathf.PI * frequency * Time.time) - Mathf.Sin(2 * Mathf.PI * frequency * (Time.time - Time.deltaTime))) * transform.up;
    }

}
