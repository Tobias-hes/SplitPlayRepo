using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class AbstractDungeonGen : MonoBehaviour
{
    [SerializeField]
    protected TilemapVis tilemapVis = null; // Reference to the TilemapVis script
    [SerializeField]
    protected Vector2Int startPos = Vector2Int.zero; // Starting position for the dungeon generation


    void Awake()
    {
        GenerateDungeon();
    }
    public void GenerateDungeon()
    {
        Random.InitState(Environment.TickCount);
        tilemapVis.ClearFloorTiles();
        tilemapVis.ClearWallTiles();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}

