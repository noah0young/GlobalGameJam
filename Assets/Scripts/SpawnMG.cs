using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMG : MonoBehaviour
{

    public GameObject enemPrefab;
    public Vector3 spawnRange = new Vector3(1, 0, 0);
    private float StartDelay = 1;
    private float RepeatDelay = 3;
    private Player playerScript;
    public bool MainAbility = false;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnRangeObstacle", StartDelay, RepeatDelay);
        playerScript = GameObject.Find("Player1").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void spawnRangeObstacle()
    {
        //ADD BOOL
        if (gameOver == false)
        {
            Debug.Log("USing spawnRangeObstacle script!");
            Instantiate(enemPrefab, spawnRange, enemPrefab.transform.rotation);
        }


    }


}
