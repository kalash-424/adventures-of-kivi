using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovements : MonoBehaviour
{
    [SerializeField] float runSpeed = 8f;
    [SerializeField] float jumpSpeed = 15f;
    [SerializeField] float climbSpeed = 4.2f;
    [SerializeField] Vector2 Deathkick = new Vector2(10f, 10f);
    [SerializeField] float SwimSpeed = 3f;
    [SerializeField] GameObject arrow;
    [SerializeField] Transform bow;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    SpriteRenderer mySpriteRenderer;
    float GravityScale = 4.5f;

    

    bool isAlive = true;
    void Start()
    {
        if(!isAlive) {return;}
        
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
      
        
        
    }

    void Update()
    {
        if(!isAlive) {return;}
        
        Run();
        FlipSprite();
        Climbing();
        // Swim();
        Die();

    }

    void OnMove(InputValue value)
    {   if(!isAlive) {return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(){
        if(!isAlive) {return;}
        
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }
    void OnFire(InputValue value){
            if(!isAlive){ return; }
            
            StartCoroutine(Fire());
            
            // myAnimator.SetTrigger("Shoot");
    }

IEnumerator Fire()  
{
	myAnimator.SetTrigger("Shoot");
    yield return new WaitForSeconds(0.5f);
    Instantiate(arrow, bow.position, transform.rotation);
}

    void Run()
    {
        
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        if(myRigidbody.velocity.x != 0) myAnimator.SetBool("isRunning", true);
        else myAnimator.SetBool("isRunning", false);
    }
    void FlipSprite(){
        if(myRigidbody.velocity.x != 0f) transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x) * 1.08f, 1.08f);
        
    }
    void Climbing(){
         if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myRigidbody.gravityScale = GravityScale;
            return;
         }

         Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x , moveInput.y * climbSpeed);
         myRigidbody.velocity = climbVelocity;
         myRigidbody.gravityScale = 0f;
        if(myRigidbody.velocity.y != 0 ) myAnimator.SetBool("isClimbing", true);
        else myAnimator.SetBool("isClimbing", false);
     }

    //  void Swim(){
    //     if(!myRigidbody.IsTouchingLayers(LayerMask.GetMask("water"))) {
    //        myRigidbody.gravityScale = 4.5f;
    //        myAnimator.SetBool("isSwimming",false);
    //        return;
    //     }
    //     Vector2 SwimVelocity = new Vector2 (myRigidbody.velocity.x , moveInput.y * SwimSpeed);
    //      myRigidbody.velocity = SwimVelocity;
    //      myRigidbody.gravityScale = 0.2f;
    //      myAnimator.SetBool("isSwimming",true);
    //  }

     void Die(){
           if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("enemy", "hazards"))) {
            isAlive = false;
            myAnimator.SetTrigger("Dead");
            myRigidbody.gravityScale = GravityScale;
            mySpriteRenderer.color = new Color(0.8773585f, 0.193681f, 0.2023178f, 0.7f);
            myRigidbody.velocity = Deathkick;
            FindObjectOfType<GameSession>().processPlayerDeath();
           }
     }
}
