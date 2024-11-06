using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxspeed = new Vector2(4.0f, 4.0f);
    [SerializeField] Vector2 timetomaxspeed = new Vector2(2.0f, 2.0f);
    [SerializeField] Vector2 timetostop = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 stopclamp = new Vector2(1.0f, 1.0f);
    Vector2 moveDirection = Vector2.zero;
    Vector2 moveVelocity = Vector2.zero;
    Vector2 moveFriction = Vector2.zero;
    Vector2 stopFriction = Vector2.zero;
    Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        
        moveVelocity = (2 * maxspeed) / timetomaxspeed; 
        moveFriction = -2 * (maxspeed / (timetomaxspeed * timetomaxspeed)); 
        stopFriction = -2 * (maxspeed / (timetostop * timetostop));         
    }
    public void Move()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");

        if (moveDirection != Vector2.zero)
        {
            
            moveVelocity -= moveDirection*getFriction()*Time.fixedDeltaTime;

            
            moveVelocity.x = Mathf.Clamp(moveVelocity.x, -maxspeed.x, maxspeed.x);
            moveVelocity.y = Mathf.Clamp(moveVelocity.y, -maxspeed.y, maxspeed.y);
        }
        else
        {
            
            moveVelocity += moveVelocity*getFriction()*Time.fixedDeltaTime;

            
            if (Mathf.Abs(moveVelocity.x) < stopclamp.x) {
                moveVelocity.x = 0;
            }
            if (Mathf.Abs(moveVelocity.y) < stopclamp.y) {
                moveVelocity.y = 0;
            }
        }

        rb.velocity = moveVelocity;
    }

    public Vector2 getFriction()
    {
        
        if(moveDirection != Vector2.zero){
            return moveFriction;
        }
        else{
            return stopFriction;
        }
    }

    public bool isMoving()
    {
        return moveVelocity != Vector2.zero;
    }
}
