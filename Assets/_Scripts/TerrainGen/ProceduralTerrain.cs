using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain
{
    public int width;
    public int depth;
    public float[,] heights;

    public List<HeightPass> passes = new List<HeightPass>();

    public ProceduralTerrain(int width, int depth, List<HeightPass> passes)
    {
        this.width = width;
        this.depth = depth;
        this.passes = passes;
        heights = new float[width,depth];
    }

    public void UpdatePasses(List<HeightPass> passes)
    {
        this.passes = passes;
    }

    public void Generate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                heights[x,z] = CalculateHeight(x, z);
            }
        }
    }

    public float[,] GetNormalizedHeights()
    {
        float[,] norm = new float[width, depth];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                norm[x, z] = Utils.Map(heights[x,z],0,Proceduralbehaviour.instance.maxWorldHeight,0,1);
            }  
        }
        return norm;
    }

    float CalculateHeight(int x, int z)
    {
        float hv = 0;
        for (int i = 0; i < passes.Count; i++)
        {
            float val = 0;
            switch (passes[i].passType)
            {
                case HeightPass.PassType.PerlinBased:
                    val = Mathf.PerlinNoise(Proceduralbehaviour.instance.perlinSeed + x / passes[i].detail, Proceduralbehaviour.instance.perlinSeed + z / passes[i].detail) * passes[i].height;
                    hv += val;
                    break;
                case HeightPass.PassType.RandomBased:
                    val = Random.value * passes[i].height;
                    hv += val;
                    break;
                case HeightPass.PassType.ImageBased:
                    val = passes[i].spritePass.texture.GetPixel(x, z).grayscale;
                    //Debug.Log(val); //Very intense (lots of logs) only do when absolutely neccesary, then be patient
                    hv *= val;
                    break;
            }
            
        }
        return hv;
    }

}