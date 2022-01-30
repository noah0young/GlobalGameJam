using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;



public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform MainTarget;
    [SerializeField] private Transform EnemyTF;

    public float Enemspeed = 3.0f;
    public float NextPosition = 3f;

    Path path;
    int currentWayPoint;
    bool ReachedEndPoint = false;

    Seeker seeker;
    Rigidbody2D RB;
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 5f);
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(RB.position, MainTarget.position, OnPathComplete); 
        }
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWayPoint == path.vectorPath.Count)
        {
            ReachedEndPoint = true;
            return;
        }
        else
        {
            ReachedEndPoint = false;
        }
        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - RB.position).normalized;
        Vector2 force = direction * Enemspeed * Time.deltaTime;

        RB.AddForce(force);

        float distance = Vector2.Distance(RB.position,path.vectorPath[currentWayPoint]);

        if (distance < NextPosition)
        {
            currentWayPoint++;
        }
        if (RB.velocity.x >= 0.01f)
        {
            EnemyTF.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (RB.velocity.x <= -0.01f)
        {
            EnemyTF.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}

/*
public class Enemy : MonoBehaviour, ICollisionHAndler
{
    // Start is called before the first frame update
    public float speed = 3.0f;
    private Rigidbody enemRB;
    //private GameObject PlayertoAtt;
    //private SpawnMG Manager;
    //public int enem_Life = 1;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform GenTarget;
    [SerializeField] private Transform MainTarget;

    [SerializeField]private float AtckCD;
    private bool AbleToAtck = true;
    private float TimeSinceAtck;

    //public ParticleSystem DeathParticle;
    void Start()
    {
        enemRB = GetComponent<Rigidbody>();
        //PlayertoAtt = GameObject.Find("Winston");
        //enem_Life = 5;

        //SPAWN MANAGER LINK!!!!!
        //Manager = GameObject.Find("GameManager").GetComponent<SpawnMG>();
        Debug.Log("Game Start");

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 lookdirection = (PlayertoAtt.transform.position - transform.position).normalized;
        // enemRB.AddForce( lookdirection * speed);        
        EnemAttack();
    }
    private void AtckEnd()
    {
        animator.SetBool("Attack", false);
    }

    private void EnemAttack()
    {
        //cooldown mechanic
        if (!AbleToAtck)
        {
            TimeSinceAtck += Time.deltaTime;//cooldown in effect
        }
        if (TimeSinceAtck >= AtckCD)
        {
            AbleToAtck = true;//new attack
        }
        if (AbleToAtck && MainTarget!=null)
        {
            AbleToAtck = false;
            TimeSinceAtck = 0;
            animator.SetBool("Attack",true);
        }
    }
    private void LookForPlayer()
    {

    }
    public void CollisionEnter(string colliderName, GameObject other)
    {
        //enem_Life--;
        //CREATE HIT FUNCTION FOR PLAYER

        //needs fix transform on y
        if (colliderName == "Sight" && other.tag == "Weapon")//follows
        {
            if(MainTarget == null)
            {
                this.MainTarget = other.transform;
                Debug.Log("Looking at player and following");
            }
        }

        if (other.tag == "Weapon" && Manager.MainAbility)//Enemy death condition
        {

            Destroy(this);
            //DeathParticle.Play();
            Debug.Log("Enemy dies!");
        }

        if (colliderName == "EnemyWeapon" && other.tag == "Player")//Enemy death condition
        {

            //TakingHit();
            //ENEMYHitParticle.Play();
            //Change the player bool by delta decrease - GetComponent<Player>().bool = true;
            Debug.Log("Enemy jabs at the player!");
        }

    }
    /*
     TakingHit(){
        if(player.Stats.LifeAmount > 0){//GameOver != true;
            UIManager.Instance.LifeRemoved();//Or GameManager    
            player.get
        }
    }
     */

/*
    public void CollisionExit(string colliderName, GameObject other)
    {
        if (colliderName == "Sight" && other.tag =="Player")
        {
            MainTarget = null;
        }
    }
}
*/