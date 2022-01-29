using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Monster, Platformer};
    private PlayerState playerState = PlayerState.Platformer;
    [Header("Movement")]
    private Rigidbody2D myRigidbody;
    public float maxSpeed = 10f;
    public float acc = .5f;
    public float slowDown = .4f;
    public float jumpSpeed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // This will move the player based on the direction held
    public void Move()
    {
        float direction = Input.GetAxis("Horizontal");
        Vector2 velocity = myRigidbody.velocity;
        if (direction == 0)
        {
            //Slows down player
            if (velocity.x > 0)
            {
                velocity.x -= slowDown;
                if (velocity.x < 0)
                {
                    velocity.x = 0;
                }
            }
            else if (velocity.x < 0)
            {
                velocity.x += slowDown;
                if (velocity.x > 0)
                {
                    velocity.x = 0;
                }
            }
        }
        else if (direction > 0)
        {
            // Increases velocity forwards
            velocity.x += acc;
            if (velocity.x > maxSpeed)
            {
                velocity.x = maxSpeed;
            }
        }
        else if (direction < 0)
        {
            // Increases velocity backwards
            velocity.x -= acc;
            if (velocity.x < -maxSpeed)
            {
                velocity.x = -maxSpeed;
            }
        }
        myRigidbody.velocity = velocity;
    }
}
