using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Witch : MonoBehaviour
{
    private Animator anim;
    public int health = 3;
    [Header("Projectile")]
    public float projectileDelay = .3f;
    public float projectilePauseTime = .7f;
    public GameObject projectile;
    [Header("Moving")]
    public float timeBeforeMoving = .6f;
    // Must be greater than .5
    public enum WitchPos { TopLeft, TopCenter, TopRight, Left, Right };
    private WitchPos myPos = WitchPos.TopLeft;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(fireProjectile());
        StartCoroutine(moveWitch());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator fireProjectile()
    {
        yield return new WaitForSeconds(projectileDelay);
        while (true)
        {
            yield return new WaitForSeconds(projectilePauseTime);
            GameObject newProjectile = Instantiate(projectile);
            newProjectile.transform.position = transform.Find("WitchCollider").position;
        }
    }

    private IEnumerator moveWitch()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectilePauseTime);
            SetPos();
        }
    }

    private void SetPos()
    {
        RandomNewPos();
        if (myPos == WitchPos.TopLeft)
        {
            anim.SetTrigger("FlyTopLeft");
        }
        else if (myPos == WitchPos.TopCenter)
        {
            anim.SetTrigger("FlyTopCenter");
        }
        else if (myPos == WitchPos.TopRight)
        {
            anim.SetTrigger("FlyTopRight");
        }
        else if (myPos == WitchPos.Right)
        {
            anim.SetTrigger("FlyRight");
        }
        else if (myPos == WitchPos.Left)
        {
            anim.SetTrigger("FlyLeft");
        }
    }

    private void RandomNewPos()
    {
        WitchPos oldPos = myPos;
        while (oldPos == myPos)
        {
            int newPosNum = Random.Range(0, 5);
            // The first number must be 0 and the last must be the length
            // of the number of WitchPos
            if (newPosNum == 0)
            {
                myPos = WitchPos.TopLeft;
            }
            else if (newPosNum == 1)
            {
                myPos = WitchPos.TopCenter;
            }
            else if (newPosNum == 2)
            {
                myPos = WitchPos.TopRight;
            }
            else if (newPosNum == 3)
            {
                myPos = WitchPos.Left;
            }
            else if (newPosNum == 4)
            {
                myPos = WitchPos.Right;
            }
        }
    }
}
