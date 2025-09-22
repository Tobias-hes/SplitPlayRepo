using System;
using System.Collections.Generic;
using UnityEngine;

public static class DungeonWallGen
{
    public static void GenerateWalls(HashSet<Vector2Int> floorPos, TilemapVis tilemapVis)
    {
        var basetWallPos = FindWallsDirections(floorPos, GenerateRandom2D.cardDirecList);
        var cornerWallPos = FindWallsDirections(floorPos, GenerateRandom2D.diagDirecList);
        CreateBasocWalls(tilemapVis, basetWallPos, floorPos);
        CreateCornerWalls(tilemapVis, cornerWallPos, floorPos);
    }
    private static void CreateCornerWalls(TilemapVis tilemapVis, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in cornerWallPos)
        {
            string neighborWalls = "";
            foreach (var dir in GenerateRandom2D.allDirecList)
            {
                var neighborPos = pos + dir;
                if (floorPos.Contains(neighborPos))
                {
                    neighborWalls += "1";
                }
                else
                {
                    neighborWalls += "0";
                }
            }
            tilemapVis.PaintSingleCornerWall(pos, neighborWalls);
        }
    }
    private static void CreateBasocWalls(TilemapVis tilemapVis, HashSet<Vector2Int> basetWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in basetWallPos)
        {
            string neighborWalls = "";
            foreach (var dir in GenerateRandom2D.cardDirecList)
            {
                var neighborPos = pos + dir;
                if (floorPos.Contains(neighborPos))
                {
                    neighborWalls += "1";
                }
                else
                {
                    neighborWalls += "0";
                }
            }
            tilemapVis.PaintSingleBaseWall(pos, neighborWalls);
        }
    }
    private static HashSet<Vector2Int> FindWallsDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var dir in directList)
            {
                var partnerPos = pos + dir;
                if (!floorPos.Contains(partnerPos))
                {
                    wallPos.Add(partnerPos);
                }
            }
        }
        return wallPos;
    }
}
