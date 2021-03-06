using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPManager : MonoBehaviour
{
    [SerializeField] Image xpBarBack;
    [SerializeField] Image xpBar;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] GameObject xpText;
    [SerializeField] Transform xpTextParent;
    [SerializeField] float timeLevelTransition = .5f;
    [SerializeField] RectTransform xpBackground;

    float playerXp;
    [SerializeField]float xpBeforeNextLvl = 2000;
    float xpMax;

    [SerializeField] bool canXpBar = true;
    [SerializeField] bool canXpText = true;

    [SerializeField] TextMeshProUGUI textScore;

    int level = -1;

    bool canFollow;
    float speed = 10f;
    [SerializeField] float timeBeforeFollow = .5f;

    public static XPManager Instance;
    bool canLevelGrowUp;

    void Awake()
    {
        canXpBar = true;
        canXpText = true;

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
    void Start()
    {
        textScore.text = playerXp.ToString() + "  /  " + xpBeforeNextLvl.ToString();
        LevelUp();
    }

    void Update()
    {

        xpBackground.DOScale(1f, timeLevelTransition);
        if (canFollow)
        {
            xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, xpBarBack.fillAmount, Time.deltaTime * speed);
            if (xpBarBack.fillAmount >= 0.99)
            {
                LevelUp();
            }
            if (xpBar.fillAmount >= xpBarBack.fillAmount - 0.00005)
            {
                canFollow = false;
            }
        }

        if (canLevelGrowUp)
        {
            
        }

        if (!canXpBar)
        {
            if (xpBarBack.fillAmount >= 0.99)
            {
                LevelUp();
            }
        }
    }

    public void AddXP(int xp, Color color)
    {
        GameManager.Instance.Hide();

        playerXp += xp;

        if (playerXp < 0)
            playerXp = 0;

        if (canXpText)
        {
            TextMeshProUGUI text = Instantiate(xpText, xpTextParent).GetComponent<TextMeshProUGUI>();
            text.rectTransform.anchoredPosition = new Vector2(xpBarBack.fillAmount * xpBar.rectTransform.sizeDelta.x + 0 - xpBar.rectTransform.sizeDelta.x / 3, -40);
            
            if(xp>0)
                text.text = "+" + xp + " xp";
            else
                text.text = xp + " xp";
            text.color = color;
        }
        xpBar.color = color;
        color = color * new Vector4(1, 1, 1, .5f);
        xpBarBack.color = color;


        if (canXpBar && xp > 0)
        {
            xpBackground.DOScale(1.5f, 0.001f);
            StartCoroutine(WaitBeforeFollow(xp));
        }
        else
        {
            xpBar.fillAmount = playerXp / xpBeforeNextLvl;
            xpBarBack.fillAmount = playerXp / xpBeforeNextLvl;
        }

        float textInt = playerXp;

        if (textInt >= xpBeforeNextLvl)
            textInt -= xpBeforeNextLvl;
        
        textScore.text = textInt.ToString()  + "  /  " +  xpBeforeNextLvl.ToString();
    }

    IEnumerator WaitBeforeFollow(int xp)
    {
        //White Bar
        xpBarBack.fillAmount = playerXp / xpBeforeNextLvl;
        yield return new WaitForSeconds(timeBeforeFollow);

        //Red Bar
        canFollow = true;
    }

    void LevelUp()
    {
        level++;
        xpBar.fillAmount = 0;
        xpBarBack.fillAmount = 0;
        //AudioManager.Instance.Play2DSound("PlayerLevelUp");
        if (level > 0)
        {
            xpMax += playerXp;
            playerXp -= xpBeforeNextLvl;
            levelTxt.DOColor(Color.red, 0.001f);
            levelTxt.transform.DOScale(3, 0.001f);
            canLevelGrowUp = true;
            StartCoroutine(WaitBeforeFollow(Mathf.RoundToInt(playerXp)));
            GameManager.Instance.LevelTransition();
            levelTxt.DOColor(Color.black, timeLevelTransition).SetUpdate(true);
            levelTxt.transform.DOScale(1, timeLevelTransition).SetUpdate(true);
            AudioManager.Instance.Play2DSound("LevelUp");
            ParticlesManager.Instance.SpawnParticles("LevelUp", Camera.main.transform.position + new Vector3(0, -.5f, -.5f), Vector3.zero);
        }

    }

    public bool GetXpBar()
    {
        return canXpBar;
    }

    public bool GetXpText()
    {
        return canXpText;
    }

    public void SetXpText(bool isTrue)
    {
        canXpText = isTrue;
    }

    public void SetXpBar(bool isTrue)
    {
        canXpBar = isTrue;
    }
}
