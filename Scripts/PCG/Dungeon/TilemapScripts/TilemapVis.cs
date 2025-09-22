using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVis : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private GameObject torchPrefab;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideLeft, wallSideRight, wallBottm, wallFull,
    wallInnerCornerDownLeft, wallInnerCornerDownRight,
    wallDiagonalCornerDownLeft, wallDiagonalCornerDownRight,
    wallDiagonalCornerUpLeft, wallDiagonalCornerUpRight;
    [SerializeField]
    private Sprite
    powerUpSprite, chestSprite,  coinSprite, smallChestSprite,
    candleSprite, torchSprite;

    private bool useTorch = false;
    private float torchModx = 0.475f;
    private float torchMody = 0.5f;

    
    
    public void PaintEnviromentSprites(Dictionary<Vector2Int, HashSet<Vector2Int>> roomsPos)
    {
        PlaceSprites(roomsPos, floorTilemap, floorTile);
    }
    public void PaintPowerUpSprites(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }
    private void PlaceSprites(Dictionary<Vector2Int, HashSet<Vector2Int>> roomsPos, Tilemap tilemap, TileBase tile)
    {
        foreach (var room in roomsPos)
        {
            //PaintSingleTile(tilemap, tile, room);

        }
    }
    private void PlaceSingleSprite(Tilemap tilemap, TileBase tile, Vector2Int p)
    {
        var tilePos = tilemap.WorldToCell((Vector3Int)p);
        tilemap.SetTile(tilePos, tile);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPos)
    {
        PaintTiles(floorPos, floorTilemap, floorTile);
    }
    private void PaintTiles(IEnumerable<Vector2Int> pos, Tilemap tilemap, TileBase tile)
    {
        foreach (var p in pos)
        {
            PaintSingleTile(tilemap, tile, p);

        }
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile,  Vector2Int p)
    {
        if( useTorch)
        {
            var tilePos = tilemap.WorldToCell((Vector3Int)p);
            tilemap.SetTile(tilePos, tile);
            Instantiate(torchPrefab, new Vector3(tilePos.x + torchModx, tilePos.y + torchMody, 0), Quaternion.identity);
        }
        else
        {
            var tilePos = tilemap.WorldToCell((Vector3Int)p);
            tilemap.SetTile(tilePos, tile);
        }
    }

    public void ClearFloorTiles()
    {
        floorTilemap.ClearAllTiles();
    }
    public void ClearWallTiles()
    {
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBaseWall(Vector2Int pos, string neighborWallsBinary)
    {
        int typeASInt = Convert.ToInt32(neighborWallsBinary, 2);
        TileBase wallTile = null;
        if (UnityEngine.Random.Range(0, 100) < 5 && !useTorch)
        {
            useTorch = true;
        }
        else
        {
            useTorch = false;
        }

        if (WallTypesHelper.wallTop.Contains(typeASInt))
        {
            wallTile = wallTop;
            torchModx = 0.5f;
            torchMody = 0.25f;

        }
        else if (WallTypesHelper.wallSideRight.Contains(typeASInt))
        {
            wallTile = wallSideRight;
            torchModx = -0.1f;
            torchMody = 0.5f;

        }
        else if (WallTypesHelper.wallSideLeft.Contains(typeASInt))
        {
            wallTile = wallSideLeft;
            torchModx = 1.1f;
            torchMody = 0.5f;
        }
        else if (WallTypesHelper.wallBottm.Contains(typeASInt))
        {
            wallTile = wallBottm;
            torchModx = 0.5f;
            torchMody = 1.25f;
        }
        else if (WallTypesHelper.wallFull.Contains(typeASInt))
        {
            wallTile = wallFull;
            useTorch = false;
        }
        if (wallTile != null)
        {
            PaintSingleTile(wallTilemap, wallTile, pos);
            
        }
    }

    internal void PaintSingleCornerWall(Vector2Int pos, string neighborWalls)
    {
        int typeASInt = Convert.ToInt32(neighborWalls, 2);
        TileBase wallTile = null;
        if (UnityEngine.Random.Range(0, 100) < 5 && !useTorch)
        {
            useTorch = true;
        }
        else
        {
            useTorch = false;
        }
        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            wallTile = wallInnerCornerDownLeft;
            torchModx = 1f;
            torchMody = 0.5f;
        }
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeASInt))
        {
            wallTile = wallInnerCornerDownRight;
            torchModx = 0f;
            torchMody = 0.5f;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            wallTile = wallDiagonalCornerDownRight;
            torchModx = -0.25f;
            torchMody = 0.75f;
        }
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            wallTile = wallDiagonalCornerDownLeft;
            torchModx = 1.25f;
            torchMody = 0.75f;
        }

        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            wallTile = wallDiagonalCornerUpLeft;
            torchModx = 1.25f;
            torchMody = 0.5f;
        }
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            wallTile = wallDiagonalCornerUpRight;
            torchModx = -0.25f;
            torchMody = 0.5f;
        }

        if (wallTile != null)
        {
            PaintSingleTile(wallTilemap, wallTile, pos);
        }
    }
}
