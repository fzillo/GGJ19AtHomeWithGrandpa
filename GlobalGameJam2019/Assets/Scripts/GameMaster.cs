using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public BulletSpawnController bulletSpawnController;
    
    public float bulletIntervalSeconds = 2f;
    public float lastSpawn = 0;

    public Animator bossMonster;

    public Lifemeter enemy;
    public Lifemeter player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastSpawn > bulletIntervalSeconds)
        {
            bulletSpawnController.SpawnRandomWaveFromPool();
            lastSpawn = Time.time;
        }

        if(enemy.isSuperAngry() || player.isSuperAngry())
        {
            badEnding();
        }
        else if(enemy.isCalmedDown())
        {
            goodEnding();
        }

    }

    private void goodEnding()
    {
        Debug.Log("good ending!!!!");
    }

    void badEnding(){
        Debug.Log("bad ending!!!!");
    }
}
