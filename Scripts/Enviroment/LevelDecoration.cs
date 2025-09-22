using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class LevelDecoration : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private GameObject torchPrefab;

    private WalkFirstDungeonGen dungeonGen;
    [SerializeField] private Tilemap tilemapFloor;

    private void Start()
    {
        // Get references to the required components
        dungeonGen = GetComponent<WalkFirstDungeonGen>();

        // Place objects after the dungeon is generated
        PlaceObjectsInRooms();
    }

    private void PlaceObjectsInRooms()
    {
        // Get the list of room positions from the dungeon generator
        List<Vector2Int> roomPositions = GetRoomPositions();

        foreach (Vector2Int roomPos in roomPositions)
        {
            // Convert room position to world position
            Vector3 worldPos = tilemapFloor.CellToWorld((Vector3Int)roomPos);

            // Randomly decide what to place in the room
            int randomChoice = Random.Range(0, 3); // 0 = PowerUp, 1 = Chest, 2 = Torch

            switch (randomChoice)
            {
                case 0:
                    PlaceObject(powerUpPrefab, worldPos);
                    break;
                case 1:
                    PlaceObject(chestPrefab, worldPos);
                    break;
                case 2:
                    PlaceObject(torchPrefab, worldPos);
                    break;
            }
        }
    }

    private void PlaceObject(GameObject prefab, Vector3 position)
    {
        // Add a small random offset to avoid perfect alignment
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
        Instantiate(prefab, position + randomOffset, Quaternion.identity);
    }

    private List<Vector2Int> GetRoomPositions()
    {
        // Assuming WalkFirstDungeonGen has a method to retrieve room positions
        // Replace this with the actual method or logic to get room positions
        return dungeonGen.GetRoomPositions();
    }
}
