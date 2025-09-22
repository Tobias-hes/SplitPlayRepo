// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// public class PlayerAbilities : MonoBehaviour
// {
//     [SerializeField]
//     PlayerManager playerManager;
//     [SerializeField]
//     GameObject playerShield;
//     Rigidbody2D rb;

//     public void SetPlayerAbility()
//     {
//         // rb = GetComponent<Rigidbody2D>();

//         // if (playerManager.GetActivePlayer() == Character.GlassCannon)
//         // {
//         //     playerManager.UpdateAbilityActive(true);
//         //     playerManager.UpdateTPPoint(rb.position);
//         // }
//         // else if (!playerManager.GetPlayerAbilityCoolDownState())
//         // {
//         //     playerManager.UpdateAbilityActive(true);
//         //     playerShield.SetActive(true);
//         // }

//     }
//     public void UsePlayerAbility()
//     {
//         // rb = GetComponent<Rigidbody2D>();

//         // if (playerManager.GetActivePlayer() == Character.GlassCannon)
//         // {
//         //     if (playerManager.GetTPLocation() != Vector2.zero && !playerManager.GetPlayerAbilityCoolDownState())
//         //     {
//         //         playerManager.UpdateAbilityActive(false);
//         //         playerManager.UpdateCoolDownActive(true);
//         //         rb.position = playerManager.GetTPLocation();
//         //         playerManager.UpdateTPCoolDownTime(Time.time);
//         //         playerManager.UpdateTPCoolDownTimeLeft(playerManager.GetPlayerAbilityCoolDown());
//         //     }
//         // }
//         // else
//         // {
//         //     playerManager.UpdateAbilityActive(false);
//         //     playerManager.UpdateCoolDownActive(true);
//         //     playerManager.UpdateShieldCoolDownTime(Time.time);
//         //     playerManager.UpdateShieldCoolDownTimeLeft(playerManager.GetPlayerAbilityCoolDown());
//         //     playerShield.SetActive(false);
//         // }
//     }

// }
// [SerializeField]
//     PlayerManager playerManager;
//     [SerializeField]
//     GameObject playerShield;
//     Rigidbody2D rb;

//     public void SetPlayerAbility()
//     {
//         rb = GetComponent<Rigidbody2D>();
        
//         if (playerManager.GetActivePlayer() == Character.GlassCannon)
//         {
//             playerManager.UpdateAbilityActive(true);
//             playerManager.UpdateTPPoint(rb.position);
//         }
//         else if(!playerManager.GetPlayerAbilityCoolDownState())
//         {
//             playerManager.UpdateAbilityActive(true);
//             playerShield.SetActive(true);
//         }

//     }
//     public void UsePlayerAbility()
//     {
//         rb = GetComponent<Rigidbody2D>();

//         if (playerManager.GetActivePlayer() == Character.GlassCannon)
//         {
//             if (playerManager.GetTPLocation() != Vector2.zero && !playerManager.GetPlayerAbilityCoolDownState())
//             {
//                 playerManager.UpdateAbilityActive(false);
//                 playerManager.UpdateCoolDownActive(true);
//                 rb.position = playerManager.GetTPLocation();
//                 playerManager.UpdateTPCoolDownTime(Time.time);
//                 playerManager.UpdateTPCoolDownTimeLeft(playerManager.GetPlayerAbilityCoolDown());
//             }
//         }
//         else
//         {
//             playerManager.UpdateAbilityActive(false);
//             playerManager.UpdateCoolDownActive(true);
//             playerManager.UpdateShieldCoolDownTime(Time.time);
//             playerManager.UpdateShieldCoolDownTimeLeft(playerManager.GetPlayerAbilityCoolDown());
//             playerShield.SetActive(false);
//         }
//     }  
