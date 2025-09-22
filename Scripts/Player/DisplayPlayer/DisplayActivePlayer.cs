using UnityEngine;
using TMPro;

public class DisplayActivePlayer : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    private TextMeshProUGUI playerNameDisplay;

    [SerializeField]
    private TextMeshProUGUI playerLivesDisplay;

    [SerializeField]
    private TextMeshProUGUI playerHealthDisplay;

    [SerializeField]
    private TextMeshProUGUI abilityStatsDisplay;

    [SerializeField]
    private TextMeshProUGUI coolDownTimeDisplay;
    

    private string activePlayer;
    private float playerLives;
    private float playerHealth;
    private float abilityStats;
    private float coolDownTimeLeft;

    private string lastActivePlayer;
    private float lastPlayerLives;
    private float lastPlayerHealth;
    private float lastAbilityStats;
    private float lastCoolDownTimeLeft;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateUIStats();
        DisplayPlayerUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUIStats();
        if (activePlayer != lastActivePlayer || playerLives != lastPlayerLives || playerHealth != lastPlayerHealth || abilityStats != lastAbilityStats || coolDownTimeLeft != lastCoolDownTimeLeft)
        {
            DisplayPlayerUI();
            UpdateLastUIStats();
        }
    }
    void UpdateUIStats()
    {
        activePlayer = playerManager.GetPlayer();
        playerLives = playerManager.GetPlayerLives();
        playerHealth = playerManager.GetPlayerHealth();
        abilityStats = playerManager.GetPlayerAbilityStats(rb);
        coolDownTimeLeft= playerManager.GetPlayerAbilityCoolDownLeft();
        
    }
    void UpdateLastUIStats()
    {
        lastActivePlayer = activePlayer;
        lastPlayerLives = playerLives;
        lastPlayerHealth = playerHealth;
        lastAbilityStats = abilityStats;
        lastCoolDownTimeLeft = coolDownTimeLeft;
    }

    void DisplayPlayerUI()
    {

        playerNameDisplay.text = "Character: " + activePlayer;
        playerLivesDisplay.text = "Lives: " + playerLives;
        playerHealthDisplay.text = "Health: " + playerHealth;

        if (activePlayer == "Tank")
        {
            abilityStatsDisplay.text = "Shield Health: " + Mathf.RoundToInt(abilityStats);
            coolDownTimeDisplay.text = "Shield Cool Down Time: " + Mathf.RoundToInt(coolDownTimeLeft);

        }
        else
        {
            abilityStatsDisplay.text = "TP Distance Remaining: " + Mathf.RoundToInt(abilityStats);
            coolDownTimeDisplay.text = "TP Cool Down Time: " + Mathf.RoundToInt(coolDownTimeLeft);
        }
    }
}
