﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain
{
    public int width;
    public int depth;
    public float[,] heights;
    public List<HeightPass> passes;

    public ProceduralTerrain(int width, int depth, List<HeightPass> passes)
    {
        this.width = width;
        this.depth = depth;
        this.passes = passes;
        heights = new float[width, depth];
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

                heights[x, z] = CalculateHeight(x, z);
            }
        }
    }

    public float[,] GetHeightsNormalized()
    {
        float[,] norm = new float[width, depth];
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                norm[x, z] = Utils.Map(heights[x, z], 0, Proceduralbehaviour.instance.maxHeight, 0, 1);
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
                    float xCoord = Proceduralbehaviour.instance.perlinSeedX + x / (float)Proceduralbehaviour.instance.worldSize * passes[i].detail;
                    float yCoord = Proceduralbehaviour.instance.perlinSeedY + z / (float)Proceduralbehaviour.instance.worldSize * passes[i].detail;
                    val = Mathf.PerlinNoise(xCoord, yCoord) * passes[i].height;
                    //val = Mathf.PerlinNoise(ProceduralBehaviour.instace.perlinSeed + (x/passes[i].detail), ProceduralBehaviour.instace.perlinSeed + (z/passes[i].detail)) * passes[i].height;
                    break;
                case HeightPass.PassType.RandomBased:
                    val = Random.value * passes[i].height;
                    break;
            }
            hv += val;
        }

        return hv;
    }


}