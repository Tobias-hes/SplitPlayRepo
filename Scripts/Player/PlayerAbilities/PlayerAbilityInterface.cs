using UnityEngine;

public interface PlayerAbilityInterface
{
    Character Character { get; }
    void OnAbilitySet();
    void OnAbilityUse();

    float GetSpeedModifier();
    string GetPlayer();

    void ApplyAbilityPowerUp();
    void UpdatePlayerHealth(float lives);
    void UpdatePlayerLives(float health);
    float GetPlayerLives();
    float GetPlayerHealth();

    void UpdateAbility(Vector2 playerPosition, float damage);
    void UpdateAbilityActive(bool active);
    void UpdateCoolDownActive(bool coolDownActive);


    void UpdateCoolDownTime(float currentTime);
    void UpdateCoolDownTimeLeft(float currentTime);
    void CheckCoolDownTime();


    float GetPlayerAbilityStats(Rigidbody2D player);
    bool GetPlayerAbilityState();
    bool GetPlayerAbilityCoolDownState();
    float GetPlayerAbilityCoolDown();
    float GetPlayerAbilityCoolDownLeft();
}
