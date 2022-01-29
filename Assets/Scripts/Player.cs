using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Monster, Platformer, Transforming};
    public PlayerState playerState = PlayerState.Platformer;
    [Header("General Physics")]
    private Rigidbody2D myRigidbody;
    public float upwardsGravity = .7f;
    // the acceleration of gravity when the player is jumping up
    public float downwardsGravity = 1f;
    // the acceleration of gravity when the player is falling down
    [Header("Human Movement")]
    public float humanMaxSpeed = 10f;
    public float humanAcc = .5f;
    public float humanSlowDownAcc = .4f;
    public float humanJumpSpeed = 7f;
    [Header("Monster Movement")]
    public float monstMaxSpeed = 8f;
    public float monstAcc = .4f;
    public float monstSlowDownAcc = .3f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Platformer)
        {
            // The code that updates while the player is a platformer
            AdjustGravity();
            changeFormStart();
            Move(humanMaxSpeed, humanAcc, humanSlowDownAcc);
        }
        else if (playerState == PlayerState.Monster)
        {
            // The code that updates while the player is a monster
            AdjustGravity();
            changeFormStart();
            Move(monstMaxSpeed, monstAcc, monstSlowDownAcc);
        }
        else if (playerState == PlayerState.Transforming)
        {
            // The player cannot do anything while transforming
        }
    }

    // Applies gravity to the player
    private void AdjustGravity()
    {
        Vector2 velocity = myRigidbody.velocity;
        if (velocity.y > 0)
        {
            myRigidbody.gravityScale = upwardsGravity;
        }
        else
        {
            myRigidbody.gravityScale = downwardsGravity;
        }
    }

    // This will move the player based on the direction held
    public void Move(float maxSpeed, float acc, float slowDown)
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

    // This will change forms between the monster and the platformer
    private void changeFormStart()
    {
        if (Input.GetKeyDown("k"))
        {
            //StartCoroutine(changeForms());
        }
    }
    /*
    private IEnumerator changeForms()
    {

        if (playerState == PlayerState.Platformer)
        {
            playerState = PlayerState.Monster;
        }
        else if (playerState == PlayerState.Monster)
        {
            playerState = PlayerState.Platformer;
        }
    }*/
}
