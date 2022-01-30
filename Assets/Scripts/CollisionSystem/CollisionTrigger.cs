using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private ICollisionHAndler HAndler;

    // Start is called before the first frame update
    private void Start()
    {
        HAndler = GetComponentInParent<ICollisionHAndler>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HAndler.CollisionEnter(gameObject.name, collision.gameObject);        
    }
}
