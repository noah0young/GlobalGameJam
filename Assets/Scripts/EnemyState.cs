using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class EnemyState : MonoBehaviour
{
    //public float speed = 3.0f;
    //private Rigidbody enemRB;
    //private GameObject PlayertoAtt;
    //private SpawnMG Manager;
    //public int enem_Life = 1;
    //public ParticleSystem DeathParticle;
    
    public AIPath aIPath;

    
    // Start is called before the first frame update
    void Start()
    {
        //enemRB = GetComponent<Rigidbody>();
        //PlayertoAtt = GameObject.Find("Player");
        //enem_Life = 5;
        //Manager = GameObject.Find("GameManager").GetComponent<SpawnMG>();
        //Debug.Log("USing EnemyBehavior script!");
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 lookdirection = (PlayertoAtt.transform.position - transform.position).normalized;
        //enemRB.AddForce(lookdirection * speed);

        if (aIPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }else if (aIPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //enem_Life--;
        if (collision.gameObject.CompareTag("Player") /*&& Manager.Fireup*/)
        {

            Destroy(gameObject);
            //DeathParticle.Play();
            Debug.Log("DEATH!");
        }
    }
}
