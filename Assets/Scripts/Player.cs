using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum PlayerState { Monster, Platformer };//, Transforming};
    public PlayerState playerState = PlayerState.Platformer;
    public bool canChangeForms = true;
    private bool jumpPressed = false;
    private bool jumpReleased = false;
    private bool playingFootsteps;
    private MusicSystem musicSystem;
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
    public float jumpCheckBoxOffset = .1f;
    // The offset that the jump check box is moved down
    //public float groundCheckLength;
    // Length of the raycast to check if the player is on the ground.
    // This must be greater than half the height of the player, since
    // it starts in the center of the player object
    public float portalPullStrength = 5f;
    // Strength of the speed you are dragged through the portal
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
    public int curHealth;
    private bool invinicibility = false;
    public float invinicibilityTime = .3f;
    [Header("Model")]
    private Animator fullModelAnim;
    private Transform playerModel;
    // This is an empty that holds the other models
    private Transform platformerModel;
    private Transform monsterModel;
    // Start is called before the first frame update
    private Animator monsterAnim;
    private Animator platformerAnim;
    void Start()
    {
        musicSystem = transform.Find("/CameraAndPlayer/MusicSystem").GetComponent<MusicSystem>();
        // Sets up models
        playerModel = transform.Find("FullModel");
        platformerModel = transform.Find("FullModel/PlatformerModel");
        monsterModel = transform.Find("FullModel/MonsterModel");
        // Sets variable values
        myRigidbody = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        invinicibility = false;
        facingRight = true;
        canMove = true;
        //Sets animation
        platformerAnim = platformerModel.GetComponent<Animator>();
        monsterAnim = monsterModel.GetComponent<Animator>();
        fullModelAnim = GetComponent<Animator>();
        platformerAnim.SetBool("moving", false);
        monsterAnim.SetBool("moving", false);
        SetModel();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateAnimation();
        if (playerState == PlayerState.Platformer)
        {
            // The code that updates while the player is a platformer
            AdjustGravity();
            GroundCheck();
            Jump();
            changeFormStart();
            Move(humanMaxSpeed, humanAcc, humanSlowDownAcc);
        }
        else if (playerState == PlayerState.Monster)
        {
            // The code that updates while the player is a monster
            AdjustGravity();
            GroundCheck();
            Punch();
            changeFormStart();
            Move(monstMaxSpeed, monstAcc, monstSlowDownAcc);
        }
        /*else if (playerState == PlayerState.Transforming)
        {
            // The player cannot do anything while transforming
        }*/
    }

    // Applies gravity to the player
    private void AdjustGravity()
    {
        Vector2 velocity = myRigidbody.velocity;
        if (velocity.y > 0 && JumpReleased())
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
            UpdateAnimation(direction);
            monsterAnim.SetFloat("speed", Mathf.Abs(direction)); // sets walk speed
            platformerAnim.SetFloat("speed", Mathf.Abs(direction)); // sets walk speed
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
                StartCoroutine(playFootsteps());
                velocity.x += acc;
                if (velocity.x > maxSpeed)
                {
                    velocity.x = maxSpeed;
                }
            }
            else if (direction < 0)
            {
                // Increases velocity backwards
                StartCoroutine(playFootsteps());
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
            playerModel.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (velocityX < 0)
        {
            facingRight = false;
            playerModel.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    
    private IEnumerator playFootsteps()
    {
        if (!playingFootsteps && canMove && onGround)
        {
            playingFootsteps = true;
            if (playerState == PlayerState.Platformer)
            {
                musicSystem.playFootsteps(true);
            }
            else if (playerState == PlayerState.Monster)
            {
                musicSystem.playFootsteps(false);
            }
            yield return new WaitUntil(() => (musicSystem.footsteps.isPlaying == false));
            playingFootsteps = false;
        }
    }

    // Updates the onGround variable based on if the player is on the ground or not
    // This is determined if they are standing on an object tagged "Ground"
    private void GroundCheck()
    {
        bool prevOnGround = onGround;
        //onGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckLength, GROUND_LAYER, 0);
        Collider2D myCollider = GetComponent<Collider2D>();
        onGround = Physics2D.BoxCast(transform.position, myCollider.bounds.size, 0, Vector2.down, jumpCheckBoxOffset, GROUND_LAYER);
        if (!onGround)
        {
            platformerAnim.SetBool("inAir", true);
        }
        else
        {
            platformerAnim.SetBool("inAir", false);
            if (!prevOnGround)
            {
                musicSystem.playJumpLand();
            }
        }
    }

    private bool JumpPressed()
    {
        jumpPressed = jumpPressed || Input.GetKeyDown("j") || Input.GetKeyDown("x") || Input.GetKeyDown("w") || Input.GetKeyDown("up") || Input.GetKeyDown("space");
        return jumpPressed;
    }

    private bool JumpReleased()
    {
        if (jumpPressed)
        {
            jumpReleased = !(Input.GetKey("j") || Input.GetKey("x") || Input.GetKey("w") || Input.GetKey("up") || Input.GetKey("space"));
            if (jumpReleased)
            {
                jumpPressed = false;
            }
        }
        else
        {
            jumpReleased = false;
        }
        
        return jumpReleased;
    }

    private bool PunchPressed()
    {
        return Input.GetKeyDown("j") || Input.GetKeyDown("x") || Input.GetKeyDown("space");
    }

    private bool SwitchPressed()
    {
        return Input.GetKeyDown("k") || Input.GetKeyDown("z");
    }

    // This allows the player to jump when they press j and are on the ground
    private void Jump()
    {
        if (JumpPressed() && onGround && canMove)
        {
            musicSystem.playJumpUp();
            Vector2 velocity = myRigidbody.velocity;
            velocity.y = humanJumpSpeed;
            myRigidbody.velocity = velocity;
        }
    }

    enum PunchStateEnum {Windup, MidPunch, PunchEnded};
    private PunchStateEnum punchState = PunchStateEnum.PunchEnded;
    private void Punch()
    {
        if (PunchPressed() && punchState != PunchStateEnum.MidPunch)
        {
            monsterAnim.SetTrigger("punch");
            musicSystem.playPunch();
            Vector2 velocity = myRigidbody.velocity;
            velocity.x = 0;
            myRigidbody.velocity = velocity;
            canMove = false;
            punchState = PunchStateEnum.Windup;
            Debug.Log("ActivatingPunch");
        }
        if (monsterAnim.GetCurrentAnimatorStateInfo(0).IsName("MonsterPunch"))
        {
            punchState = PunchStateEnum.MidPunch;
        }
        if (!monsterAnim.GetCurrentAnimatorStateInfo(0).IsName("MonsterPunch") && punchState == PunchStateEnum.MidPunch)
        {
            Debug.Log("Releasing punch");
            canMove = true;
            punchState = PunchStateEnum.PunchEnded;
        }
    }

    // Updates the animation based on if the player is moving or is still
    private void UpdateAnimation(float direction)
    {
        //Vector2 velocity = myRigidbody.velocity;
        if (direction != 0)
        {
            platformerAnim.SetBool("moving", true);
            monsterAnim.SetBool("moving", true);
        }
        else
        {
            platformerAnim.SetBool("moving", false);
            monsterAnim.SetBool("moving", false);
        }
    }

    // This will change forms between the monster and the platformer
    private void changeFormStart()
    {
        if (SwitchPressed() && canChangeForms && canMove)
        {
            changeForms();
            //StartCoroutine(changeForms());
            Debug.Log("Swap");
        }
    }
    
    private void changeForms()
    {
        if (playerState == PlayerState.Platformer)
        {
            playerState = PlayerState.Monster;
            Vector2 velocity = myRigidbody.velocity;
            if (velocity.y > 0)
            {
                velocity.y = 0;
                myRigidbody.velocity = velocity;
            }
        }
        else if (playerState == PlayerState.Monster)
        {
            playerState = PlayerState.Platformer;
        }
        SetModel();
        //return null;
    }

    private void SetModel()
    {
        if (playerState == PlayerState.Platformer)
        {
            monsterModel.gameObject.SetActive(false);
            platformerModel.gameObject.SetActive(true);
        }
        else if (playerState == PlayerState.Monster)
        {
            platformerModel.gameObject.SetActive(false);
            monsterModel.gameObject.SetActive(true);
        }
    }

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
        if (!invinicibility)
        {
            curHealth -= num;
            Debug.Log("Lost " + num + " Health");
            if (playerState == PlayerState.Platformer)
            {
                musicSystem.playVocal(true);
            }
            else if (playerState == PlayerState.Monster)
            {
                musicSystem.playVocal(false);
            }
            if (curHealth <= 0)
            {
                curHealth = 0;
                Death();
            }
            else
            {
                StartCoroutine(Invinicibility());
            }
        }
    }

    private void Death()
    {
        Debug.Log("Dead");
        int thisScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(thisScene);
    }

    private IEnumerator Invinicibility()
    {
        invinicibility = true;
        fullModelAnim.SetBool("invincible", true);
        yield return new WaitForSeconds(invinicibilityTime);
        fullModelAnim.SetBool("invincible", false);
        invinicibility = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Spikes" || collision.transform.tag == "Witch" ||
            collision.transform.tag == "Projectile")
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SwitchPortal")
        {
            PortalPullStart(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SwitchPortal")
        {
            PortalPullEnd();
        }
    }

    private void PortalPullStart(Collider2D collision)
    {
        // Pulls you through the portal
        changeForms();
        canMove = false;
        Vector2 velocity = myRigidbody.velocity;
        Vector2 posDifference = new Vector2 (collision.transform.position.x, 0) - new Vector2(transform.position.x, 0);
        if (posDifference.x > 0)
        {
            posDifference.x = 1;
        }
        else
        {
            posDifference.x = -1;
        }
        velocity = posDifference * portalPullStrength * new Vector2(collision.transform.localScale.x, collision.transform.localScale.y);
        myRigidbody.velocity = velocity;
    }

    private void PortalPullEnd()
    {
        canMove = true;
    }
}
