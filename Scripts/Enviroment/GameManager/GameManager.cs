
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int game_level = 0;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Rigidbody2D tankPlayer, glassCannonPlayer;
    [SerializeField]
    GameObject endPoint;
    [SerializeField]
    GameObject powerUp;
    [SerializeField]
    Sprite tankPowerUp, glassPowerUp;
    [SerializeField]
    WalkFirstDungeonGen walkFirstDungeonGen;
    [SerializeField]
    PlayerStats playerStats;
    public Vector2Int PlayerStartPos => walkFirstDungeonGen.CalculatePlayerStart();
    public Vector2Int PlayerEndPos => walkFirstDungeonGen.CalculatePlayerEnd();
    public List<Vector2Int> roomPos => walkFirstDungeonGen.PopulatePowerUps();



    void Start()
    {
        player.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);
        tankPlayer.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);
        glassCannonPlayer.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);
        endPoint.transform.position = new Vector3(PlayerEndPos.x, PlayerEndPos.y, 0);
        foreach (var room in roomPos)
        {
            Instantiate(powerUp, new Vector3(room.x, room.y, 0), Quaternion.identity);
        }
        if(playerStats.activeCharacter == Character.GlassCannon)
        {
            foreach (var power in GameObject.FindGameObjectsWithTag("Player Power Up"))
            {
                foreach (Transform child in power.transform)
                {
                    if (child.gameObject.name == "Glass Power Up")
                    {
                        child.gameObject.SetActive(true);
                    }
                    else if (child.gameObject.name == "Tank Power Up")
                    {
                        child.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            foreach (var power in GameObject.FindGameObjectsWithTag("Player Power Up"))
            {
                foreach (Transform child in power.transform)
                {
                    if (child.gameObject.name == "Glass Power Up")
                    {
                        child.gameObject.SetActive(false);
                    }
                    else if (child.gameObject.name == "Tank Power Up")
                    {
                        child.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    public void OnQuitGame()
    {
        // close the standalone application
        Application.Quit();
    }

    public void OnResetGame()
    {
        if (game_level < 5)
        {
            walkFirstDungeonGen.randomWalkParam.walkLength += 5;
            walkFirstDungeonGen.passLength += 5;
            walkFirstDungeonGen.randomWalkParam.iterations += 5;
            walkFirstDungeonGen.walkCount += 1;
            game_level += 1;

        }
        else
        {
            walkFirstDungeonGen.randomWalkParam.walkLength = 50;
            walkFirstDungeonGen.passLength = 75;
            walkFirstDungeonGen.randomWalkParam.iterations = 50;
            walkFirstDungeonGen.walkCount = 5;
            game_level = 0;

        }
        foreach (var torch in GameObject.FindGameObjectsWithTag("Torch"))
        {
            Destroy(torch);
        }

        walkFirstDungeonGen.ClearRooms();
        walkFirstDungeonGen.GenerateDungeon();

        foreach (var power in GameObject.FindGameObjectsWithTag("Player Power Up"))
        {
            Destroy(power);
        }
        foreach (var room in roomPos)
        {
            Instantiate(powerUp, new Vector3(room.x, room.y, 0), Quaternion.identity);
        }
        endPoint.transform.position = new Vector3(PlayerEndPos.x, PlayerEndPos.y, 0);

        player.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);
        tankPlayer.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);
        glassCannonPlayer.transform.position = new Vector3(PlayerStartPos.x, PlayerStartPos.y, 0);

        
        // string name = SceneManager.GetActiveScene().name;
        // SceneManager.LoadScene(name);
    }
    
}
