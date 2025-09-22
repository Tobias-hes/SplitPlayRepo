using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//https://www.youtube.com/watch?v=8ojGRMOzXrQ&ab_channel=NightRunStudio
//https://www.youtube.com/watch?v=4I0vonyqMi8&ab_channel=Tarodev
public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    public PlayerStats playerStats;


    PlayerAbilityInterface glassCannonAbilities;
    List<PlayerStats.ActiveCharacterDictionary> activeCharacterChoices => playerStats.activeCharacterChoices;
    List<PlayerStats.GlassCannonDictionary> glassCannonStats => playerStats.glassCannonStats;
    List<PlayerStats.TankDictionary> tankStats => playerStats.tankStats;
    List<PlayerStats.AbilityDictionary> abilityActive => playerStats.abilityActive;

    [SerializeField ]
    private GameObject tankPlayer;
    [SerializeField]
    private GameObject glassCannonPlayer;

    PlayerAbilityInterface tankAbilities;
    PlayerAbilityInterface activeHandler;

    private bool characterSwitchActive;
    private float characterSwitchCoolDownTime = 5.0f;
    private float characterSwitchCoolDownTimeStart;
    private float characterSwitchCoolDownTimeLeft;
    
    public Dictionary<Character, string> ActiveCharacterChoices;
    public Dictionary<GlassCannonStats, float> GlassCannonStat;
    public Dictionary<TankStats, float> TankStat;
    public Dictionary<AbilityActive, bool> AbilityIsActive;




    
    
    private void OnEnable()
    {
        SetValues();
    }
    private void OnDisable()
    {
        UpdatePlayerStats();
        
    }
    public void SetValues()
    {
        glassCannonAbilities = GetComponent<GlassCannonAbilities>();
        tankAbilities = GetComponent<TankAbilities>();
        activeHandler = glassCannonAbilities;
        ActiveCharacterChoices = activeCharacterChoices.ToDictionary(key => key.key, value => value.value);
        GlassCannonStat = glassCannonStats.ToDictionary(key => key.key, value => value.value);
        TankStat = tankStats.ToDictionary(key => key.key, value => value.value);
        AbilityIsActive = abilityActive.ToDictionary(key => key.key, value => false);
        SetRandomActivePlayer();
    }
    public void UpdatePlayerStats()
    {
        playerStats.activeCharacterChoices = ActiveCharacterChoices.Select(acc => new PlayerStats.ActiveCharacterDictionary { key = acc.Key, value = acc.Value }).ToList();

        playerStats.glassCannonStats = GlassCannonStat.Select(gcs => new PlayerStats.GlassCannonDictionary { key = gcs.Key, value = gcs.Value }).ToList();

        playerStats.tankStats = TankStat.Select(ts => new PlayerStats.TankDictionary { key = ts.Key, value = ts.Value }).ToList();

        playerStats.abilityActive = AbilityIsActive.Select(ais => new PlayerStats.AbilityDictionary { key = ais.Key, value = ais.Value }).ToList();
    }




    public void SetRandomActivePlayer()
    {
        glassCannonAbilities = GetComponent<GlassCannonAbilities>();
        tankAbilities = GetComponent<TankAbilities>();

        if (Random.value < 0.5f)
        {
            glassCannonPlayer.SetActive(true);
            tankPlayer.SetActive(false);
            playerStats.activeCharacter = Character.GlassCannon;
            activeHandler = glassCannonAbilities;
            glassCannonPlayer.transform.position = tankPlayer.transform.position;
        }
        else
        {
            glassCannonPlayer.SetActive(false);
            tankPlayer.SetActive(true);
            playerStats.activeCharacter = Character.Tank;
            activeHandler = tankAbilities;
            tankPlayer.transform.position = glassCannonPlayer.transform.position;
        }
    }
    public void SetActivePlayer(Character activeChoice)
    {
        glassCannonAbilities = GetComponent<GlassCannonAbilities>();
        tankAbilities = GetComponent<TankAbilities>();
        

        if (characterSwitchActive != true)
        {
            if (activeChoice == Character.GlassCannon)
            {
                glassCannonPlayer.SetActive(true);
                tankPlayer.SetActive(false);
                glassCannonPlayer.transform.position = tankPlayer.transform.position;
                activeHandler = glassCannonAbilities;
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
                glassCannonPlayer.SetActive(false);
                tankPlayer.SetActive(true);
                tankPlayer.transform.position = glassCannonPlayer.transform.position;
                activeHandler = tankAbilities;
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
            UpdateAbilityActive(false);
            UpdateCoolDownActive(false);
            UpdateCharacterSwitchCoolDownActive(true);
            UpdateCharacterSwitchCoolDownTimeLeft(characterSwitchCoolDownTime);
            UpdateCharacterSwitchCoolDownTime(Time.time);
            UpdateCoolDownTime(0.00f);
            UpdateCoolDownTimeLeft(0.00f);
            UpdateAbility(Vector2.zero, 0f);
            playerStats.activeCharacter = activeChoice;
        }
        
        
    }
    public void CheckCharacterCoolDownTime()
    {
        if (Time.time - characterSwitchCoolDownTimeStart <= characterSwitchCoolDownTime && characterSwitchActive)
        {
            UpdateCharacterSwitchCoolDownTimeLeft(characterSwitchCoolDownTime - (Time.time - characterSwitchCoolDownTimeStart));
        }
        else if (characterSwitchActive)
        {
            UpdateCharacterSwitchCoolDownActive(false);
            UpdateCharacterSwitchCoolDownTime(0.00f);
            UpdateCharacterSwitchCoolDownTimeLeft(0.00f);
        }


    }
    void UpdateCharacterSwitchCoolDownActive(bool active)
    {
        characterSwitchActive = active;
        
    }
    void UpdateCharacterSwitchCoolDownTimeLeft(float time)
    {
        characterSwitchCoolDownTimeLeft = time;
    }
    void UpdateCharacterSwitchCoolDownTime(float time)
    {
        characterSwitchCoolDownTimeStart = time;
    }


    public string GetPlayer() => activeHandler?.GetPlayer() ?? null;

    public void OnAbilitySet() => activeHandler?.OnAbilitySet();
    public void OnAbilityUse() => activeHandler?.OnAbilityUse();

    public float GetSpeedModifier() => activeHandler?.GetSpeedModifier() ?? 1f;

    public void ApplyAbilityPowerUp() => activeHandler?.ApplyAbilityPowerUp();
    public void UpdatePlayerHealth(float lives) => activeHandler?.UpdatePlayerHealth(lives);
    public void UpdatePlayerLives(float health) => activeHandler?.UpdatePlayerLives(health);
    public float GetPlayerLives() => activeHandler?.GetPlayerLives() ?? 0f;
    public float GetPlayerHealth() => activeHandler?.GetPlayerHealth() ?? 0f;

    public void UpdateAbility(Vector2 playerPosition, float damage) => activeHandler?.UpdateAbility(playerPosition, damage);
    public void UpdateAbilityActive(bool active) => activeHandler?.UpdateAbilityActive(active);
    public void UpdateCoolDownActive(bool coolDownActive) => activeHandler?.UpdateCoolDownActive(coolDownActive);


    public void UpdateCoolDownTime(float currentTime) => activeHandler?.UpdateCoolDownTime(currentTime);
    public void UpdateCoolDownTimeLeft(float currentTime) => activeHandler?.UpdateCoolDownTimeLeft(currentTime);
    public void CheckCoolDownTime() => activeHandler?.CheckCoolDownTime();


    public float GetPlayerAbilityStats(Rigidbody2D player) => activeHandler?.GetPlayerAbilityStats(player)?? 0f;
    public bool GetPlayerAbilityState() => activeHandler?.GetPlayerAbilityState() ?? false;
    public bool GetPlayerAbilityCoolDownState() => activeHandler?.GetPlayerAbilityCoolDownState()?? false;
    public float GetPlayerAbilityCoolDown() => activeHandler?.GetPlayerAbilityCoolDown()?? 0f;
    public float GetPlayerAbilityCoolDownLeft() => activeHandler?.GetPlayerAbilityCoolDownLeft()?? 0f;

    public void CheckTPPoint(Vector2 playerPosition)
    {
        if (activeHandler == glassCannonAbilities)
        {
            if (Vector2.Distance(playerStats.PlayerTPPosition, playerPosition) > GlassCannonStat[GlassCannonStats.PlayerTPLength])
            {
                playerStats.PlayerTPPosition = Vector2.zero;
                UpdateAbilityActive(false);
            }
        }
        
    }
    public Character GetActivePlayer()
    {
        return playerStats.activeCharacter;
    }
    public Vector2 GetTPLocation()
    {
        return playerStats.PlayerTPPosition;
    }
}


