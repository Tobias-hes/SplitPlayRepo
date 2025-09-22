using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//https://discussions.unity.com/t/exposing-dictionaries-in-the-inspector/887570
//https://www.youtube.com/watch?app=desktop&v=B2JsymHzgvE&ab_channel=OneWheelStudio
[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField]
    public Character activeCharacter;

    [Serializable]
    public struct ActiveCharacterDictionary
    {
        public Character key;
        public string value;

    }
    [SerializeField]
    public List<ActiveCharacterDictionary> activeCharacterChoices = new List<ActiveCharacterDictionary>();


    [Serializable]
    public struct GlassCannonDictionary
    {
        public GlassCannonStats key;
        public float value;

    }
    [SerializeField]
    public List<GlassCannonDictionary> glassCannonStats = new List<GlassCannonDictionary>();
    

    [Serializable]
    public struct TankDictionary
    {
        public TankStats key;
        public float value;
    }
    [SerializeField]
    public List<TankDictionary> tankStats = new List<TankDictionary>();
    


    [Serializable]
    public struct AbilityDictionary
    {
        public AbilityActive key;
        public bool value;
    }
    [SerializeField]
    public List<AbilityDictionary> abilityActive = new List<AbilityDictionary>();
   

    [SerializeField]
    public Vector2 PlayerTPPosition;
}

public enum Character
{
    GlassCannon,
    Tank,
    None
}
public enum GlassCannonStats
{
    PlayerHealth,
    PlayerLives,
    PlayerTPCoolDownTime,
    PlayerTPCoolDownTimeLeft,
    PlayerTPCoolDownTimeStart,
    PlayerTPLength,
    
}
public enum TankStats
{
    PlayerHealth,
    PlayerLives,
    ShieldHealth,
    ShieldHealthDamage,
    ShieldHCoolDown,
    ShieldHCoolDownLeft,
    ShieldHCoolDownSart,
    ShieldRadious
}
public enum AbilityActive
{
    AbilityActive,
    AbilityCoolDownActive,
}

