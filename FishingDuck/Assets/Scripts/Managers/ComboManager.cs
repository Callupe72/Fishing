using TMPro;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject textPrefab;
    [SerializeField] string[] textToSay;
    TextMeshProUGUI text;

    public static ComboManager Instance;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SpawnText(Color color, Vector3 position)
    {

        position = new Vector3(position.x, 0, -3);
        int random = Random.Range(0, textToSay.Length);

        TextMeshProUGUI text = Instantiate(textPrefab, position, Quaternion.identity).GetComponentInChildren<TextMeshProUGUI>();
        text.color = color;
        text.text = textToSay[random];
        Destroy(text.gameObject, 2.2f);
    }
}
