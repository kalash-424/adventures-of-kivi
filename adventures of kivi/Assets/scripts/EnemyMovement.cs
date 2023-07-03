using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3f;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(enemySpeed, 0f);
    }
    void OnTriggerExit2D(Collider2D other){
            enemySpeed = -enemySpeed;
            FlipEnemyface();
    }

    void FlipEnemyface(){
            transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x) * 1.05f), 1.05f);
    }
    
}
