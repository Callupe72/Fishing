using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : MonoBehaviour
{

    [SerializeField] Volume volume;
    Bloom b;
    Vignette vg;
    LensDistortion lsd;
    ChromaticAberration ca;
    ColorAdjustments colorA;

    [SerializeField] float bloomValue = .7f;
    float lerp;

    [SerializeField] Color colWantedRed = new Color(255, 160, 160);
    [SerializeField] Color colWantedGreen = new Color(255, 160, 160);
    State state;
    public static PostProcessManager Instance;
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
    private void Start()
    {
        volume.profile.TryGet(out b);
        volume.profile.TryGet(out vg);
        volume.profile.TryGet(out lsd);
        volume.profile.TryGet(out ca);
        volume.profile.TryGet(out colorA);
    }

    public void SetRedColor()
    {
        state = State.GoToRed;
    }
    public void SetGreenColor()
    {
        state = State.GoToGreen;
    }

    private void Update()
    {
        if (state == State.GoToRed)
        {
            lerp += Time.deltaTime * 5;
            Color color = Color.Lerp(Color.white, colWantedRed, lerp);
            colorA.colorFilter.value = color;
            if (lerp > 1)
            {
                state = State.RedGoToWhite;
                lerp = 0;
            }
        }
        else if(state == State.RedGoToWhite)
        {
            lerp += Time.deltaTime * 5;
            Color color = Color.Lerp(colWantedRed, Color.white, lerp);
            colorA.colorFilter.value = color;
            if (lerp > 1)
            {
                state = State.Nothing;
                lerp = 0;
            }
        }
        else if (state == State.GoToGreen)
        {
            lerp += Time.deltaTime * 5;
            Color color = Color.Lerp(Color.white, colWantedGreen, lerp);
            colorA.colorFilter.value = color;
            if (lerp > 1)
            {
                state = State.GreenGoToWhite;
                lerp = 0;
            }
        }
        else if (state == State.GreenGoToWhite)
        {
            lerp += Time.deltaTime * 5;
            Color color = Color.Lerp(colWantedGreen, Color.white, lerp);
            colorA.colorFilter.value = color;
            if (lerp > 1)
            {
                state = State.Nothing;
                lerp = 0;
            }
        }
    }

    void ColorGoBackToWhite()
    {

    }

    public enum State
    {
        Nothing,
        GoToRed,
        GoToGreen,
        RedGoToWhite,
        GreenGoToWhite,
    }
}