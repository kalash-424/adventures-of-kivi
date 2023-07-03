using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] float arrowSpeed = 20f;
    float xspeed;
    PlayerMovements player;
    Rigidbody2D myRigidbody ;
    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovements>();
        transform.localScale = new Vector3(Mathf.Sign(player.transform.localScale.x), 1f, 1f);
       xspeed = player.transform.localScale.x * arrowSpeed;
       
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2(xspeed, 0f);
        
    }
    void OnTriggerEnter2D(Collider2D other){
            if(other.tag == "Enemy") {Destroy(other.gameObject);
            Destroy(gameObject);}
            if(other.tag == "bouce" || other.tag == "ground" || other.tag == "hazards") {
                Destroy(gameObject);}
    }
    // void OnCollisonEnter2D(Collider2D other){
    //     if(other.tag == "bouce" || other.tag == "ground" || other.tag == "hazards") Destroy(gameObject);
    // }
}
