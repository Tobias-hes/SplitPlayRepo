using UnityEngine;

public class TankAbilities : MonoBehaviour, PlayerAbilityInterface
{
    public Character Character => Character.Tank;
    [SerializeField]
    private GameObject playerShield;
    [SerializeField]
    PlayerManager playerManager;

    public void OnAbilitySet()
    {
        if (!playerManager.GetPlayerAbilityCoolDownState())
        {
            UpdateAbilityActive(true);
            playerShield.SetActive(true);
        }
    }
    public void OnAbilityUse()
    {
        UpdateAbilityActive(false);
        UpdateCoolDownActive(true);
        UpdateCoolDownTime(Time.time);
        UpdateCoolDownTimeLeft(GetPlayerAbilityCoolDown());
        playerShield.SetActive(false);

    }
    public float GetSpeedModifier()
    {
        return 0.65f;
    }
    public string GetPlayer()
    {
        return playerManager.ActiveCharacterChoices[Character.Tank];
    }
    public void ApplyAbilityPowerUp()
    {
        playerManager.TankStat[TankStats.ShieldHealth] += 25;
        playerManager.TankStat[TankStats.ShieldHealthDamage] = playerManager.TankStat[TankStats.ShieldHealth];
    }

    public void UpdatePlayerLives(float lives)
    {
        playerManager.TankStat[TankStats.PlayerLives] = lives;
    }
    public void UpdatePlayerHealth(float health)
    {
        playerManager.TankStat[TankStats.PlayerHealth] = health;
    }

    public float GetPlayerLives()
    {
        return playerManager.TankStat[TankStats.PlayerLives];
    }
    public float GetPlayerHealth()
    {
        return playerManager.TankStat[TankStats.PlayerHealth];
    }



    public void UpdateAbility(Vector2 playerPosition, float damage)
    {

        playerManager.TankStat[TankStats.ShieldHealthDamage] -= damage;
        if (playerManager.TankStat[TankStats.ShieldHealthDamage] <= 0)
        {
            playerManager.TankStat[TankStats.ShieldHealthDamage] = playerManager.TankStat[TankStats.ShieldHealth];
            UpdateAbilityActive(false);
            UpdateCoolDownActive(true);
            UpdateCoolDownTime(Time.time);
            UpdateCoolDownTimeLeft(GetPlayerAbilityCoolDown());
            playerShield.SetActive(false);
        }

    }
    public void UpdateAbilityActive(bool active)
    {
        playerManager.AbilityIsActive[AbilityActive.AbilityActive] = active;

    }
    public void UpdateCoolDownActive(bool coolDownActive)
    {
        playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive] = coolDownActive;
    }


    public void UpdateCoolDownTime(float currentTime)
    {
        playerManager.TankStat[TankStats.ShieldHCoolDownSart] = currentTime;
    }
    public void UpdateCoolDownTimeLeft(float currentTime)
    {
        playerManager.TankStat[TankStats.ShieldHCoolDownLeft] = currentTime;
    }
    public void CheckCoolDownTime()
    {
        if (Time.time - playerManager.TankStat[TankStats.ShieldHCoolDownSart] <= playerManager.TankStat[TankStats.ShieldHCoolDown] && playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive])
        {
            UpdateCoolDownTimeLeft(playerManager.TankStat[TankStats.ShieldHCoolDown] - (Time.time - playerManager.TankStat[TankStats.ShieldHCoolDownSart]));
        }
        else if (playerManager.AbilityIsActive[AbilityActive.AbilityCoolDownActive])
        {
            UpdateCoolDownActive(false);
            UpdateCoolDownTime(0.00f);
            UpdateCoolDownTimeLeft(0.00f);
        }


    }

    public float GetPlayerAbilityStats(Rigidbody2D player)
    {
        return playerManager.TankStat[TankStats.ShieldHealthDamage];
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
        return playerManager.TankStat[TankStats.ShieldHCoolDown];
    }
    public float GetPlayerAbilityCoolDownLeft()
    {
        return playerManager.TankStat[TankStats.ShieldHCoolDownLeft];
    }

}
