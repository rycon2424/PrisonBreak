using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proceduralbehaviour : MonoBehaviour
{
    [HideInInspector]
    public static Proceduralbehaviour instance;
    public float perlinSeed;

    [Header("World Gen Settings")]
    public int seed;
    public int worldSize;
    public float maxWorldHeight;
    public GameObject module;
    public Terrain t;

    private ProceduralTerrain pt;

    public List<HeightPass> passes = new List<HeightPass>();

    void Awake()
    {
        if (instance != this)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
    }

    void Start()
    {
        perlinSeed = Random.Range(0.0f, 1000000f);
        pt = new ProceduralTerrain(worldSize, worldSize, passes);
        t.terrainData.heightmapResolution = worldSize;
        t.terrainData.size = new Vector3(worldSize*2.56f, maxWorldHeight, worldSize*2.56f);
        Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Generate();
        }
    }

    public void Generate()
    {
        SetSeed(seed);
    
        foreach (var obj in GameObject.FindGameObjectsWithTag("Procedural"))
        {
            Destroy(obj);
        }

        pt.Generate();
        t.terrainData.SetHeights(0, 0, pt.GetNormalizedHeights());

    }

    public void SetSeed(int s)
    {
        Random.InitState(s);
        if (seed != s)
        {
            seed = s;
        }
        perlinSeed = Random.Range(0.0f, 1000000f);
    }
    
}