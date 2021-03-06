using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    [SerializeField] bool canParticle = true;


    [System.Serializable]
    public struct Particles
    {
        public string name;
        public GameObject particle;
        public Vector3 scale;
        [Range(0, 5)] public float speed;
    }

    public Particles[] particles;

    GameObject lastParticles;

    public static ParticlesManager Instance;
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
        StartCoroutine(SpawnLeaves());
    }


    IEnumerator SpawnLeaves()
    {
        ParticlesManager.Instance.SpawnParticles("FallingLeaves", Camera.main.transform.position + new Vector3(0, 0, -.5f), Vector3.zero);
        yield return new WaitForSeconds(15);
        StartCoroutine(SpawnLeaves());
    }

    public void SpawnParticles(string name, Transform parentTransform, Vector3 rotation, bool setToParent)
    {
        if (!canParticle)
            return;
        Particles p = Array.Find(particles, particle => particle.name == name);

        GameObject particleGo = Instantiate(p.particle, parentTransform.position, Quaternion.Euler(rotation));
        if (setToParent)
            particleGo.transform.parent = parentTransform;
        lastParticles = particleGo;
        ParticleSystem particlesComponent = particleGo.GetComponent<ParticleSystem>();
        particlesComponent.Play();
        particleGo.transform.localScale = p.scale;
        lastParticles.name = name;
        particlesComponent.playbackSpeed = p.speed;
        foreach (Transform item in particleGo.transform)
        {
            if (GetComponent<ParticleSystem>())
            {
                item.GetComponent<ParticleSystem>().playbackSpeed = p.speed;
            }
        }
    }
    public void SpawnParticles(string name, Vector3 pos, Vector3 rotation)
    {
        if (!canParticle)
            return;
        Particles p = Array.Find(particles, particle => particle.name == name);

        GameObject particleGo = Instantiate(p.particle, pos, Quaternion.Euler(rotation));
        lastParticles = particleGo;
        ParticleSystem particlesComponent = particleGo.GetComponent<ParticleSystem>();
        particlesComponent.Play();
        particleGo.transform.localScale = p.scale;
        lastParticles.name = name;
        particlesComponent.playbackSpeed = p.speed;
        foreach (Transform item in particleGo.transform)
        {
            if (GetComponent<ParticleSystem>())
            {
                item.GetComponent<ParticleSystem>().playbackSpeed = p.speed;
            }
        }
    }

    public void SetCanParticles(bool isTrue)
    {
        canParticle = isTrue;
    }

    void Update()
    {
        if (GameObject.Find("LoadingPlayerWeapon") && !Input.GetButton("Fire"))
        {
            Destroy(GameObject.Find("LoadingPlayerWeapon"));
        }
    }

    public bool GetCanParticles()
    {
        return canParticle;
    }

    public GameObject GetLastParticles()
    {
        return lastParticles;
    }
}