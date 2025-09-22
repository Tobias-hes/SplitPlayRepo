using UnityEngine;

public class GlassCannonAbilities : MonoBehaviour, PlayerAbilityInterface
{
    public Character Character => Character.GlassCannon;
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    PlayerManager playerManager;
    void Awake()
    {
        if (playerManager == null)
            playerManager = GetComponent<PlayerManager>();
    }
    public void OnAbilitySet()
    {
        if (!playerManager.GetPlayerAbilityCoolDownState())
        {
            playerManager.UpdateAbilityActive(true);
            UpdateAbility(rb.position, 0);
        }
    }
    public void OnAbilityUse()
    {
        if (playerManager.GetTPLocation() != Vector2.zero && !playerManager.GetPlayerAbilityCoolDownState())
        {
            UpdateAbilityActive(false);
            UpdateCoolDownActive(true);
            rb.position = playerManager.GetTPLocation();
            UpdateCoolDownTime(Time.time);
            UpdateCoolDownTimeLeft(GetPlayerAbilityCoolDown());
        }

    }
    public float GetSpeedModifier()
    {
        return 1.0f;
    }

    public string GetPlayer()
    {
        return playerManager.ActiveCharacterChoices[Character.GlassCannon];
    }

    public void ApplyAbilityPowerUp()
    {
        playerManager.GlassCannonStat[GlassCannonStats.PlayerTPLength] += 5;
    }

    public void UpdatePlayerLives(float lives)
    {
        playerManager.GlassCannonStat[GlassCannonStats.PlayerLives] = lives;
    }
    public void UpdatePlayerHealth(float health)
    {
        playerManager.GlassCannonStat[GlassCannonStats.PlayerHealth] = health;
    }

    public float GetPlayerLives()
    {
        return playerManager.GlassCannonStat[GlassCannonStats.PlayerLives];
    }
    public float GetPlayerHealth()
    {
        return playerManager.GlassCannonStat[GlassCannonStats.PlayerHealth];
    }

    public void UpdateCoolDownTime(float currentTime)
    {
        playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart] = currentTime;
    }
    public void UpdateCoolDownTimeLeft(float currentTime)
    {
        playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeLeft] = currentTime;
    }
    public void CheckCoolDownTime()
    {
        if (Time.time - playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart] <= playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime] &&
        playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive])
        {
            UpdateCoolDownTimeLeft(playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime] -
            (Time.time - playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart]));
        }
        else if (playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive])
        {
            UpdateCoolDownActive(false);
            UpdateCoolDownTime(0.00f);
            UpdateCoolDownTimeLeft(0.00f);
        }
    }

    public void UpdateAbility(Vector2 playerPosition, float damage)
    {
        playerManager.playerStats.PlayerTPPosition = playerPosition;
    }
    public void UpdateAbilityActive(bool active)
    {
        playerManager.AbilityIsActive[AbilityActive.AbilityActive] = active;

    }
    public void UpdateCoolDownActive(bool coolDownActive)
    {
        playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive] = coolDownActive;
    }

    
    public float GetPlayerAbilityStats(Rigidbody2D player)
    {
        if (GetPlayerAbilityState())
        {
            return playerManager.GlassCannonStat[GlassCannonStats.PlayerTPLength] - Vector2.Distance(playerManager.playerStats.PlayerTPPosition, player.position);
        }
        else
        {
            return playerManager.GlassCannonStat[GlassCannonStats.PlayerTPLength];
        }
    }
    public bool GetPlayerAbilityState()
    {
        return playerManager.AbilityIsActive[AbilityActive.AbilityActive];
    }
    public bool GetPlayerAbilityCoolDownState()
    {
        return playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive];
    }
    public float GetPlayerAbilityCoolDown()
    {
        return playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime];
    }
    public float GetPlayerAbilityCoolDownLeft()
    {
        return playerManager.GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeLeft];
    }

}
