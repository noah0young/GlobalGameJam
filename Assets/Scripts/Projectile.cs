using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Vector2 velocity;
    public float speed = 1f;
    public float activeTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        velocity = transform.Find("/Player").position - transform.position;
        velocity = velocity.normalized * speed;
        myRigidbody.velocity = velocity;
        StartCoroutine(timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Removes the projectile when its time is up
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(activeTime);
        Destroy(gameObject);
    }

    /*
    // Sets the max value of a vector2 to 1 and the rest
    // proportional to the original values
    private Vector2 MaxToOne(Vector2 original)
    {
        float maxVal = 1;
        if (original.x > original.y)
        {
            if (original.x != 0)
            {
                maxVal = original.x;
            }
        }
        else
        {
            if (original.y != 0)
            {
                maxVal = original.y;
            }
        }
        return original / maxVal;
    }*/
}
