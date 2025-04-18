using UnityEngine;
using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

public class TerrainScript : MonoBehaviour
{
    
}

public struct TerrainMeshVariables {
    [Range(1, 255)] public int terrainMeshDetail;
    public float terrainWidth;
    public float height;
    public int
}
