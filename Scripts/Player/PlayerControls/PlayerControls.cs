using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControls : MonoBehaviour
{
    
    [SerializeField]
    PlayerManager playerManager;
    [SerializeField]
    GameManager gameManager;
    Rigidbody2D rb;
    [SerializeField]
    float MoveSpeedModifier => playerManager.GetSpeedModifier();
    [SerializeField]
    Vector2 movement;
    [SerializeField]
    float moveSpeed = 25.00f;
    [SerializeField]
    Animator animator;
    [SerializeField]
    GameObject playerCharacter;

    [SerializeField]
    private InputActionReference moveAction, attackAction, aimAction, setAbilityAction, useAbilityAction, glassAction, tankAction;


    private void OnEnable()
    {
        moveAction.action.performed += MovePlayer;
        moveAction.action.canceled += StopPlayer;
        attackAction.action.performed += Attack;
        aimAction.action.performed += Aim;
        setAbilityAction.action.performed += SetPlayerAbility;
        useAbilityAction.action.performed += UsePlayerAbility;
        glassAction.action.performed += Glass;
        tankAction.action.performed += Tank;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= MovePlayer;
        moveAction.action.canceled -= StopPlayer;
        attackAction.action.performed -= Attack;
        aimAction.action.performed -= Aim;
        setAbilityAction.action.performed -= SetPlayerAbility;
        useAbilityAction.action.performed -= UsePlayerAbility;
        glassAction.action.performed -= Glass;
        tankAction.action.performed -= Tank;
    }

  
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
        
        rb.linearVelocity = moveSpeed * MoveSpeedModifier * movement;
        playerManager.CheckTPPoint(rb.position);
        playerManager.CheckCoolDownTime();
        playerManager.CheckCharacterCoolDownTime();

    }
    private void MovePlayer(InputAction.CallbackContext context)
    {
        animator.SetBool("Run", true);
        movement = context.ReadValue<Vector2>();
        playerCharacter.transform.localScale = new Vector3(movement.x > 0 ? 1 : -1, 1, 1);
        Debug.Log("movement: " + movement.ToString());
        movement.Normalize();
        
    }
    private void StopPlayer(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
        animator.SetBool("Run", false);
    }


    private void Tank(InputAction.CallbackContext context)
    {
        Debug.Log("Player Set Tank");
        playerManager.SetActivePlayer(Character.Tank);
    }

    private void Glass(InputAction.CallbackContext context)
    {
        Debug.Log("Player Set Glass");
        playerManager.SetActivePlayer(Character.GlassCannon);
    }

   

    private void UsePlayerAbility(InputAction.CallbackContext context)
    {
        Debug.Log("Player Used Ability");
        playerManager.OnAbilityUse();
    }

    private void SetPlayerAbility(InputAction.CallbackContext context)
    {
        Debug.Log("Player Ability Set");
        playerManager.OnAbilitySet();
    }

    private void Aim(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    private void Attack(InputAction.CallbackContext context)
    {
        Debug.Log("Player Attacked");
        animator.SetTrigger("Attack");
    }
   
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "End Point")
        {
            Debug.Log("Hit End Point!!");
            gameManager.OnResetGame();
            playerManager.UpdatePlayerStats();
            playerManager.SetValues();
            
        }
        if (collision.gameObject.tag == "Player Power Up")
        {
            Destroy(collision.gameObject);
            playerManager.ApplyAbilityPowerUp();

        }
    } 
}