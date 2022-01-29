using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Monster, Platformer, Transforming};
    public PlayerState playerState = PlayerState.Platformer;
    private Animator myAnimator;
    [Header("General Physics")]
    private Rigidbody2D myRigidbody;
    private bool onGround; // Is the player on the ground
    public float upwardsGravity = .7f;
    // The scale of gravity when the player is jumping up
    public float downwardsGravity = 1f;
    // The scale of gravity when the player is falling down
    public float peakGravity = .5f;
    // The scale of the gravity at the peak of your jump
    public LayerMask GROUND_LAYER; // The layer with all objects you can jump off of
    public float groundCheckLength;
    // Length of the raycast to check if the player is on the ground.
    // This must be greater than half the height of the player, since
    // it starts in the center of the player object
    [Header("General Movement")]
    private bool canMove;
    // Determines if the player can move
    private bool facingRight;
    public Vector2 bounce = new Vector2(8, 2);
    // This is the speed of the bounce when you fly away from enemies or spikes
    public float bounceTime = .2f;
    [Header("Human Movement")]
    public float humanMaxSpeed = 10f;
    public float humanAcc = .5f;
    public float humanSlowDownAcc = .4f;
    public float humanJumpSpeed = 7f;
    public float extraJumpHeight = 2f;
    // When you reach the peak of your jump early, this will be extra velocity
    // to make your jump look like an arc
    [Header("Monster Movement")]
    public float monstMaxSpeed = 8f;
    public float monstAcc = .4f;
    public float monstSlowDownAcc = .3f;
    [Header("Health")]
    public int maxHealth = 3;
    private int curHealth;
    private bool invisible = false;
    public float invisiblityTime = .3f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        invisible = false;
        facingRight = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == PlayerState.Platformer)
        {
            // The code that updates while the player is a platformer
            AdjustGravity();
            GroundCheck();
            Jump();
            //changeFormStart();
            Move(humanMaxSpeed, humanAcc, humanSlowDownAcc);
        }
        else if (playerState == PlayerState.Monster)
        {
            // The code that updates while the player is a monster
            AdjustGravity();
            GroundCheck();
            Punch();
            //changeFormStart();
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
        if (velocity.y > 0 && Input.GetKeyUp("j"))
        {
            // This will occur when you let go of the jump button
            velocity.y = extraJumpHeight;
            myRigidbody.velocity = velocity;
            myRigidbody.gravityScale = peakGravity;
        }
        else if (velocity.y > 0)
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
        if (canMove)
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
            SetDirectionFacing(velocity.x);
        }
    }

    private void SetDirectionFacing(float velocityX)
    {
        if (velocityX > 0)
        {
            facingRight = true;
        }
        else if (velocityX < 0)
        {
            facingRight = false;
        }
    }

    // Updates the onGround variable based on if the player is on the ground or not
    // This is determined if they are standing on an object tagged "Ground"
    private void GroundCheck()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, GROUND_LAYER, 0);
    }

    // This allows the player to jump when they press j and are on the ground
    private void Jump()
    {
        if (Input.GetKeyDown("j") && onGround)
        {
            Vector2 velocity = myRigidbody.velocity;
            velocity.y = humanJumpSpeed;
            myRigidbody.velocity = velocity;
        }
    }

    private void Punch()
    {
        // To be implemented
    }
    
    // This will change forms between the monster and the platformer
    /*private void changeFormStart()
    {
        if (Input.GetKeyDown("k"))
        {
            StartCoroutine(changeForms());
        }
    }
    
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
        return null;
    }*/

    // Adds 1 health
    public void AddHealth()
    {
        AddHealth(1);
    }

    // Adds the provided amount of health
    public void AddHealth(int num)
    {
        curHealth += num;
        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
            Debug.Log("Added " + num + " Health");
        }
    }

    // Removes 1 health
    public void LoseHealth()
    {
        LoseHealth(1);
    }

    // Removes the provided amount of health
    public void LoseHealth(int num)
    {
        if (!invisible)
        {
            curHealth -= num;
            Debug.Log("Lost " + num + " Health");
            if (curHealth <= 0)
            {
                curHealth = 0;
                Death();
            }
            else
            {
                StartCoroutine(Invisiblity());
            }
        }
    }

    private void Death()
    {
        Debug.Log("Dead");
        //To be implemented
    }

    private IEnumerator Invisiblity()
    {
        invisible = true;
        Debug.Log("Invisibility Start");
        yield return new WaitForSeconds(invisiblityTime);
        Debug.Log("Invisibility End");
        invisible = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Spikes")
        {
            LoseHealth();
            BounceBack();
        }
    }

    // This will launch you back when you take damage
    private void BounceBack()
    {
        StartCoroutine(BounceFreeze());
        if (facingRight)
        {
            myRigidbody.velocity = bounce * new Vector2(-1, 1);
        }
        else
        {
            myRigidbody.velocity = bounce;
        }
    }

    // This will freeze player movement while they are bouncing back
    private IEnumerator BounceFreeze()
    {
        canMove = false;
        yield return new WaitForSeconds(bounceTime);
        canMove = true;
    }
}
