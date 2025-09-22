

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //https://www.youtube.com/watch?v=8ojGRMOzXrQ&ab_channel=NightRunStudio
// //https://www.youtube.com/watch?v=4I0vonyqMi8&ab_channel=Tarodev
// public class PlayerManager : MonoBehaviour
// {

//     [SerializeField]
//     private int playerLives = 3;
//     [SerializeField]
//     private int shieldHealth = 50;
//     [SerializeField]
//     private bool playerTPActive;
//     [SerializeField]
//     private Vector2 playerTPPosition;
//     public int Lives => playerLives;
//     public int ShieldHealth => shieldHealth;
//     public bool TPActive => playerTPActive;
//     public Vector2 TPPosition => playerTPPosition;
    
//     private PlayerType activeCharacter;
//     public PlayerType ActivePlayer => activeCharacter;

//     [SerializeField]
//     public PlayerAbilities playerAbilities;

//     [SerializeField]
//     public GameObject playerShield;






//     void Awake()
//     {
//         if (activePlayer == false)
//         {
//             SetRandomActivePlayer();
//         }

//     }
//     void FixedUpdate()
//     {
//         if (rb != null)
//         {
//             CheckTPPoint();
//             rb.linearVelocity = movement * moveSpeed * moveSpeedModifier;

//             if (Time.time - playerTPCoolDownTimeDelta <= playerTPCoolDownTime && playerTPCoolDownActive)
//             {
//                 UpdateTPCoolDownTimeLeft(playerTPCoolDownTime - (Time.time - playerTPCoolDownTimeDelta));
//             }
//             else if (playerTPCoolDownActive)
//             {
//                 UpdateTPCoolDownActive(false);
//                 UpdateTPCoolDownTime(0.00f);
//                 UpdateTPCoolDownTimeLeft(0.00f);
//             }
//         }
//     }

//     public void SetRandomActivePlayer()
//     {
//         activeCharacter = Random.value < 0.5f
//             ? PlayerType.GlassCannon
//             : PlayerType.Tank;
        

        
//     }
//     public void SetActivePlayer(string activeChoice)
//     {
//         activePlayer = activeChoice;
//     }
//     public void UpdatePlayerLives(int lives)
//     {
//         playerLives = lives;

//     }
//     public void UpdateShieldHealth(int health)
//     {
//         shieldHealth = health;

//     }
//     public void UpdateTPCoolDownTime(float currentTime)
//     {
//         playerTPCoolDownTimeDelta = currentTime;
//     }
//     public void UpdateTPCoolDownTimeLeft(float currentTime)
//     {
//         playerTPCoolDownTimeLeft = currentTime;
//     }
//     public void UpdateTPCoolDownActive(bool coolDownActive)
//     {
//         playerTPCoolDownActive = coolDownActive;
//     }
//     public void CheckTPPoint()
//     {
//         if (Vector2.Distance(playerTPPosition, rb.position) > 20)
//         {
//             playerTPPosition = Vector2.zero;
//             playerTPActive = false;
//         }
//     }

// }
// using System.Collections;
// using System.Linq;  
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// //https://www.youtube.com/watch?v=8ojGRMOzXrQ&ab_channel=NightRunStudio
// //https://www.youtube.com/watch?v=4I0vonyqMi8&ab_channel=Tarodev
// public class PlayerManager : MonoBehaviour
// {
//     [SerializeField]
//     private PlayerStats playerStats;
//     [SerializeField]
//     private GameObject playerShield;
//     public Dictionary<Character, string> ActiveCharacterChoices;
//     public Dictionary<GlassCannonStats, float> GlassCannonStat;
//     public Dictionary<TankStats, float> TankStat;
//     public Dictionary<AbilityActive, bool> AbilityIsActive;


//     void Awake()
//     {
//         List<PlayerStats.ActiveCharacterDictionary> activeCharacterChoices = playerStats.activeCharacterChoices;
//         List<PlayerStats.GlassCannonDictionary> glassCannonStats = playerStats.glassCannonStats;
//         List<PlayerStats.TankDictionary> tankStats = playerStats.tankStats;
//         List<PlayerStats.AbilityDictionary> abilityActive = playerStats.abilityActive;

//         ActiveCharacterChoices = activeCharacterChoices.ToDictionary(key => key.key, value => value.value);
//         GlassCannonStat = glassCannonStats.ToDictionary(key => key.key, value => value.value);
//         TankStat = tankStats.ToDictionary(key => key.key, value => value.value);
//         AbilityIsActive = abilityActive.ToDictionary(key => key.key, value => value.value);
//         SetRandomActivePlayer();
//     }
   

//     public void SetRandomActivePlayer()
//     {

//         if (Random.value < 0.5f)
//         {
//             playerStats.activeCharacter = Character.GlassCannon;
//         }
//         else
//         {
//             playerStats.activeCharacter = Character.Tank;
//         }
//     }
//     public void SetActivePlayer(Character activeChoice)
//     {
//         playerStats.activeCharacter = activeChoice;
//     }
//     public void UpdatePlayerLives(Character active, float lives)
//     {
//         if (active == Character.GlassCannon)
//         {
//             GlassCannonStat[GlassCannonStats.PlayerLives] = lives;
//         }
//         else
//         {
//             TankStat[TankStats.PlayerLives] = lives;
//         }


//     }
//     public void UpdatePlayerHealth(Character active, float health)
//     {
//         if (active == Character.GlassCannon)
//         {
//             GlassCannonStat[GlassCannonStats.PlayerHealth] = health;
//         }
//         else
//         {
//             TankStat[TankStats.PlayerHealth] = health;
//         }


//     }
//     public void UpdateShieldHealth(float damage)
//     {
//         TankStat[TankStats.ShieldHealthDamage] -= damage;
//         if (TankStat[TankStats.ShieldHealthDamage] <= 0)
//         {
//             TankStat[TankStats.ShieldHealthDamage] = TankStat[TankStats.ShieldHealth];
//             UpdateAbilityActive(false);
//             UpdateCoolDownActive(true);
//             UpdateShieldCoolDownTime(Time.time);
//             UpdateShieldCoolDownTimeLeft(GetPlayerAbilityCoolDown());
//             playerShield.SetActive(false);
//         }
        
//     }
//     public void UpdateShieldCoolDownTime(float currentTime)
//     {
//         TankStat[TankStats.ShieldHCoolDownSart] = currentTime;
//     }
//     public void UpdateShieldCoolDownTimeLeft(float currentTime)
//     {
//         TankStat[TankStats.ShieldHCoolDownLeft] = currentTime;
//     }
//     public void UpdateTPCoolDownTime(float currentTime)
//     {
//         GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart] = currentTime;
//     }
//     public void UpdateTPCoolDownTimeLeft(float currentTime)
//     {
//         GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeLeft] = currentTime;
//     }
//     public void CheckTPPoint(Vector2 playerPosition)
//     {
//         if (Vector2.Distance(playerStats.PlayerTPPosition, playerPosition) > GlassCannonStat[GlassCannonStats.PlayerTPLength])
//         {
//             playerStats.PlayerTPPosition = Vector2.zero;
//             UpdateAbilityActive(false);
//         }
//     }
//     public void UpdateTPPoint(Vector2 playerPosition)
//     {
//         playerStats.PlayerTPPosition = playerPosition;
//     }
//     public void UpdateAbilityActive(bool active)
//     {
//         AbilityIsActive[AbilityActive.AbilityActive] = active;

//     }
//     public void UpdateCoolDownActive(bool coolDownActive)
//     {
//         AbilityIsActive[AbilityActive.AbilityCoolDownActive] = coolDownActive;
//     }
//     public void CheckCoolDownTime()
//     {
        
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             if (Time.time - GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart] <= GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime] && AbilityIsActive[AbilityActive.AbilityCoolDownActive])
//             {
//                 UpdateTPCoolDownTimeLeft(GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime] - (Time.time - GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeStart]));
//             }
//             else if (AbilityIsActive[AbilityActive.AbilityCoolDownActive])
//             {
//                 UpdateCoolDownActive(false);
//                 UpdateTPCoolDownTime(0.00f);
//                 UpdateTPCoolDownTimeLeft(0.00f);
//             }
//         }
//         else
//         {
//             if (Time.time - TankStat[TankStats.ShieldHCoolDownSart] <= TankStat[TankStats.ShieldHCoolDown] && AbilityIsActive[AbilityActive.AbilityCoolDownActive])
//             {
//                 UpdateShieldCoolDownTimeLeft(TankStat[TankStats.ShieldHCoolDown] - (Time.time - TankStat[TankStats.ShieldHCoolDownSart]));
//             }
//             else if (AbilityIsActive[AbilityActive.AbilityCoolDownActive])
//             {
//                 UpdateCoolDownActive(false);
//                 UpdateShieldCoolDownTime(0.00f);
//                 UpdateShieldCoolDownTimeLeft(0.00f);
//             }
//         }

//     }

//     public Character GetActivePlayer()
//     {
//         return playerStats.activeCharacter;
//     }
//     public string GetPlayer()
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             return ActiveCharacterChoices[Character.GlassCannon];
//         }
//         else
//         {
//             return ActiveCharacterChoices[Character.Tank];
//         }
//     }
//     public float GetPlayerLives()
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             return GlassCannonStat[GlassCannonStats.PlayerLives];
//         }
//         else
//         {
//             return TankStat[TankStats.PlayerLives];
//         }
//     }
//     public float GetPlayerHealth()
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             return GlassCannonStat[GlassCannonStats.PlayerHealth];
//         }
//         else
//         {
//             return TankStat[TankStats.PlayerHealth];
//         }
//     }
//     public float GetPlayerAbilityStats(Rigidbody2D player)
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             if (GetPlayerAbilityState())
//             {
//                 return GlassCannonStat[GlassCannonStats.PlayerTPLength] - Vector2.Distance(playerStats.PlayerTPPosition, player.position);
//             }
//             else
//             {
//                 return GlassCannonStat[GlassCannonStats.PlayerTPLength];
//             }
            
//         }
//         else
//         {
//             return TankStat[TankStats.ShieldHealthDamage];
//         }
//     }
//     public bool GetPlayerAbilityState()
//     {
//         return AbilityIsActive[AbilityActive.AbilityActive];
//     }
//     public bool GetPlayerAbilityCoolDownState()
//     {
//         return AbilityIsActive[AbilityActive.AbilityCoolDownActive];
//     }
//     public float GetPlayerAbilityCoolDown()
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             return GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTime];
//         }
//         else
//         {
//             return TankStat[TankStats.ShieldHCoolDown];
//         }
//     }
//     public float GetPlayerAbilityCoolDownLeft()
//     {
//         if (playerStats.activeCharacter == Character.GlassCannon)
//         {
//             return GlassCannonStat[GlassCannonStats.PlayerTPCoolDownTimeLeft];
//         }
//         else
//         {
//             return TankStat[TankStats.ShieldHCoolDownLeft];
//         }
//     }
//     public Vector2 GetTPLocation()
//     {
//         return playerStats.PlayerTPPosition;
//     }
// }