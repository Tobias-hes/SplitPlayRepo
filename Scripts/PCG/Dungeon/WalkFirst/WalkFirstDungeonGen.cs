
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RangeAttribute = UnityEngine.RangeAttribute;
using Random = UnityEngine.Random;

public class WalkFirstDungeonGen : RWDG
{
    
    [SerializeField]
    public int passLength, walkCount, walkWidth;

    [SerializeField]
    [Range(0.1f, 1)]
    public float roomPercent;

    public Dictionary<Vector2Int, HashSet<Vector2Int>> roomsDictionary
    = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    public HashSet<Vector2Int> floorPosistions, corridorPositions;
    public HashSet<Vector2Int> potentialRoomPos = new HashSet<Vector2Int>();
    public HashSet<Vector2Int> roomPos = new HashSet<Vector2Int>();


    public List<Color> roomColors = new List<Color>();

    [SerializeField]
    public bool showRoomGizmos = false, showCorridorGizmos;


    protected override void RunProceduralGeneration()
    {
        tilemapVis = GetComponent<TilemapVis>();
        ClearRooms();
        WalkFirstGeneration();
        tilemapVis.PaintFloorTiles(floorPosistions); // Paint the floor tiles on the tilemap
        DungeonWallGen.GenerateWalls(floorPosistions, tilemapVis); // Generate walls based on the floor positions
        PopulatePowerUps();
        tilemapVis.PaintEnviromentSprites(roomsDictionary);
    }
    public Vector2Int CalculatePlayerStart()
    {
        foreach (var room in roomsDictionary)
        {
            return room.Key;
        }
        return CalculatePlayerStart();
        
    }
    public Vector2Int CalculatePlayerEnd()
    {
        int idx = 0;
        foreach (var room in roomsDictionary)
        {
            if (idx == 1)
            {
                return room.Key;
            }
            idx += 1;
        }
        return CalculatePlayerEnd();
        
    }
    public List<Vector2Int> GetRoomPositions()
    {
        return roomsDictionary.Keys.ToList();
    }
    // public void PopulatePowerUps()
    // {
    //     int idx = 0;
    //     int ranIdx = Random.Range(2, roomsDictionary.Count);
    //     foreach (var room in roomsDictionary)
    //     {
    //         if (idx >= ranIdx)
    //         {
    //             tilemapVis.PaintPowerUpSprites(room.Value);
    //         }
    //         idx += 1;
    //     }

    // }
    public List<Vector2Int> PopulatePowerUps()
    {
        int idx = 0;
        int ranIdx = Random.Range(2, roomsDictionary.Count);
        List<Vector2Int> roomPosList = new List<Vector2Int>();
        foreach (var room in roomsDictionary)
        {
            if (idx >= ranIdx)
            {
                roomPosList.Add(room.Key);
            }
            idx += 1;
        }
        return roomPosList;
    }
    public void ClearRooms()
    {
        roomsDictionary.Clear();
        roomColors.Clear();
        roomsDictionary = new Dictionary<Vector2Int, HashSet<Vector2Int>>();
        floorPosistions = new HashSet<Vector2Int>();
        corridorPositions = new HashSet<Vector2Int>();
        potentialRoomPos = new HashSet<Vector2Int>();
        roomPos = new HashSet<Vector2Int>();
        potentialRoomPos = new HashSet<Vector2Int>();
        floorPosistions = new HashSet<Vector2Int>();
        corridorPositions = new HashSet<Vector2Int>();
        roomPos = new HashSet<Vector2Int>();
    }

    public void SaveRooms(Vector2Int pos, HashSet<Vector2Int> roomFloor)
    {
        roomsDictionary[pos] = roomFloor;
        roomColors.Add(new Color(Random.value, Random.value, Random.value));
    }

    public void WalkFirstGeneration()
    {

        List<List<Vector2Int>> walks = CreateWalk();
        
        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPosistions);
        CreateRoomsAtDeadEnds(deadEnds);
        FindRoomPos(potentialRoomPos);

        floorPosistions.UnionWith(roomPos); // Combine the floor positions with the room positions

        for (int i = 0; i < walks.Count; i++)
        {
            walks[i] = ChangeWalkWidth(walks[i], walkWidth);
            floorPosistions.UnionWith(walks[i]);
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (showCorridorGizmos && corridorPositions != null)
        {
            Gizmos.color = Color.gray;
            foreach (var cell in corridorPositions)
            {
                Gizmos.DrawCube(new Vector3(cell.x + 0.5f, cell.y + 0.5f, 0), UnityEngine.Vector3.one);
            }
        }
    }
   
    public void OnDrawGizmos()
    {
        if (!showRoomGizmos || roomsDictionary == null || roomsDictionary.Count == 0)
            return;

        int idx = 0;
        foreach (var room in roomsDictionary)
        {
            // grab the color you saved for this room
            Color c = roomColors[idx++];
            Gizmos.color = c;

            // draw one cube per floor tile in that room
            foreach (var cell in room.Value)
            {
                Gizmos.DrawCube(new Vector3(cell.x, cell.y, 0), Vector3.one);
            }
            // draw one cube for center floor tile in that room
            Gizmos.color = Color.black;
            Gizmos.DrawCube(new Vector3(room.Key.x, room.Key.y, 0), Vector3.one * 4.0f*idx);
        }
    }

    public List<Vector2Int> ChangeWalkWidth(List<Vector2Int> walk, int walkWidth)
    {
        List<Vector2Int> newWalk = new List<Vector2Int>();
        for (int j = 0; j < walkWidth / 2; j++)
        {
            for (int i = 1; i < walk.Count; i++)
            {
                for (int x = -1; x < 2; x++)
                {
                    for (int y = -1; y < 2; y++)
                    {
                        Vector2Int dirFromCell = walk[i] - walk[i - 1];
                        Vector2Int walkOffSet = GetDirection90From(dirFromCell) * j;
                        newWalk.Add(walk[i - 1] + new Vector2Int(x, y) + walkOffSet);
                    }
                }
            }
        }
        return newWalk;
    }

    public Vector2Int GetDirection90From(Vector2Int direc)
    {
        if(direc == Vector2Int.up)
        {
            return Vector2Int.right;
        }
        if (direc == Vector2Int.down)
        {
            return Vector2Int.left;
        }
        if (direc == Vector2Int.left)
        {
            return Vector2Int.up;
        }
        if (direc == Vector2Int.right)
        {
            return Vector2Int.down;
        }
        return Vector2Int.zero;

    }

    public void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds)
    {
        foreach (var deadEnd in deadEnds)
        {
            var roomFloor = RunRandomWalk(randomWalkParam, deadEnd);
            roomFloor.UnionWith(ExtendFloorPositions(roomFloor, GenerateRandom2D.cardDirecList));
            roomPos.UnionWith(roomFloor);
            SaveRooms(deadEnd, roomFloor);
        }
    }

    public List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPos)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (Vector2Int pos in floorPos)
        {
            int neighborCount = 0;
            foreach (Vector2Int dir in GenerateRandom2D.cardDirecList)
            {
                Vector2Int neighborPos = pos + dir;
                if (floorPos.Contains(neighborPos))
                {
                    neighborCount++;
                }
            }
            if (neighborCount == 1) // Dead end condition
            {
                deadEnds.Add(pos);
            }
        }
        return deadEnds;
    }
    private static HashSet<Vector2Int> ExtendFloorPositions(HashSet<Vector2Int> floorPos, List<Vector2Int> directList)
    {
        HashSet<Vector2Int> newFloorPos = new HashSet<Vector2Int>();
        foreach (var pos in floorPos)
        {
            foreach (var dir in directList)
            {
                var partnerPos = pos + dir;
                if (!floorPos.Contains(partnerPos))
                {
                    newFloorPos.Add(partnerPos);
                }
            }
        }
        floorPos.UnionWith(newFloorPos);
        return newFloorPos;
    }

    public void FindRoomPos(HashSet<Vector2Int> potentialRoomPos)
    {

        int roomCount = Mathf.RoundToInt(potentialRoomPos.Count * roomPercent);

        List<Vector2Int> potentialRoomList = potentialRoomPos.OrderBy(x => Guid.NewGuid()).Take(roomCount).ToList();

        foreach (var roomPosition in potentialRoomList)
        {
            var roomFloor = RunRandomWalk(randomWalkParam, roomPosition);
            roomFloor.UnionWith(ExtendFloorPositions(roomFloor, GenerateRandom2D.cardDirecList));
            roomPos.UnionWith(roomFloor);
            SaveRooms(roomPosition, roomFloor);
        }
    }

    public List<List<Vector2Int>> CreateWalk()
    {
        var currentPos = startPos;
        potentialRoomPos.Add(currentPos); // Add the starting position to potential room positions
        List<List<Vector2Int>> tempWalk = new List<List<Vector2Int>>();

        for (int i = 0; i < walkCount; i++)
        {
            var path = PGA.GenerateWalkPath(currentPos, passLength);
            tempWalk.Add(path);
            currentPos = path[path.Count - 1];
            potentialRoomPos.Add(currentPos);
            floorPosistions.UnionWith(path);
        }
        
        corridorPositions = new HashSet<Vector2Int>(floorPosistions);
        return tempWalk;
    }
}
