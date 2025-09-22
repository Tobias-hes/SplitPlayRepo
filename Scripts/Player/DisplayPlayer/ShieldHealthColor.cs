using UnityEngine;

public class ShieldHealthColor : MonoBehaviour
{
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    SpriteRenderer playerShield;
    Rigidbody2D rb;
    Vector2 pos = Vector2.zero;
    float shieldHealth = 50;
    float shieldHealtPercent;
    float red = 0f;
    float blue= 1f;
    void Start()
    {
        playerShield = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (playerManager.GetActivePlayer() == Character.Tank)
        {
            ChangeShieldColor();        
        }
    }
    void ChangeShieldColor()
    {
        playerManager.UpdateAbility(pos, 0.5f);
        shieldHealtPercent = playerManager.GetPlayerAbilityStats(rb) / shieldHealth;
        playerShield.color = new Color(red + (1-shieldHealtPercent), 0.5f, blue - (1-shieldHealtPercent),0.05f);
    }
    
}
