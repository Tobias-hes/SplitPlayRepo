using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RWDG : AbstractDungeonGen
{

    [SerializeField]
    public RandomWalkData randomWalkParam;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParam, startPos);


        tilemapVis.PaintFloorTiles(floorPos); // Paint the floor tiles on the tilemap
        DungeonWallGen.GenerateWalls(floorPos, tilemapVis); // Generate walls based on the floor positions
    }

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkData Param, Vector2Int pos)
    {
        var currentPos = pos; // Starting position for the random walk
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>(); // HashSet to store the positions of the floor tiles
        for (int i = 0; i < Param.iterations; i++)
        {
            var path = PGA.GeneratePath(currentPos, Param.walkLength); // Generate a path from the current position
            floorPos.UnionWith(path); // Add the path to the floor positions

            if (Param.startRandomlyEachIteration) {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count)); // Randomly select a new starting position from the floorPos
            }
        }
        return floorPos;
    }
}




