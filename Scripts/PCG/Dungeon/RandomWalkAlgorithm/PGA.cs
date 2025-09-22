using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//Random Walk Algorithm - P4 - Unity Procedural Generation of a 2D Dungeon - https://www.youtube.com/watch?v=F_Zc1nvtB0o&list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v&index=4&ab_channel=SunnyValleyStudio


public static class PGA
{
    public static HashSet<Vector2Int> GeneratePath(Vector2Int startPos, int length)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();
        path.Add(startPos);

        var prevPos = startPos;

        for (int i = 0; i < length; i++)
        {
            var newPos = prevPos + GenerateRandom2D.GetRandomCardDirec();
            path.Add(newPos);
            prevPos = newPos;
        }

        return path;
    }
    public static List<Vector2Int> GenerateWalkPath(Vector2Int startPos, int walkLength)
    {
        List<Vector2Int> walk = new List<Vector2Int>();
        var dir = GenerateRandom2D.GetRandomCardDirec();
        var currentPos = startPos;
        walk.Add(currentPos);

        for (int i = 0; i < walkLength; i++)
        {
            currentPos += dir;
            walk.Add(currentPos);
        }
        return walk;
    }
    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();

        roomQueue.Enqueue(spaceToSplit);
        while (roomQueue.Count > 0)
        {

            var room = roomQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minWidth, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minHeight, roomQueue, room);
                    }
                    else if(room.size.x >= minWidth && room.size.y >= minHeight )
                    {
                        roomList.Add(room);
                    }
                }
                else
                {
                    
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomList.Add(room);
                    }

                }
            }
            
        }
        return roomList;
    }
    private static void SplitVertically(int minWidth,  Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var xSplit= Random.Range(1, room.size.x);
        BoundsInt room1x = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2x = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), 
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomQueue.Enqueue(room1x);
        roomQueue.Enqueue(room2x);
    }
    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);
        BoundsInt room1y = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit,  room.size.z)); 
        BoundsInt room2y = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), 
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomQueue.Enqueue(room1y);
        roomQueue.Enqueue(room2y);
    }
}
public static class GenerateRandom2D
{
    public static List<Vector2Int> cardDirecList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 0), // Right
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0) // Left
        
    };
    public static List<Vector2Int> diagDirecList = new List<Vector2Int>
    {
        new Vector2Int(1, 1), // UP-Right
        new Vector2Int(1, -1), // RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) // LEFT-UP
    };

    public static List<Vector2Int> allDirecList = new List<Vector2Int>
    {
        new Vector2Int(0, 1), // Up
        new Vector2Int(1, 1), // UP-Right

        new Vector2Int(1, 0), // Right
        new Vector2Int(1, -1), // RIGHT-DOWN

        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, -1), // DOWN-LEFT 

        new Vector2Int(-1, 0), // Left
        new Vector2Int(-1, 1) // LEFT-UP
};

    public static Vector2Int GetRandomCardDirec()
    {
        int randomIndex = UnityEngine.Random.Range(0, cardDirecList.Count);
        return cardDirecList[randomIndex];
    }
}
