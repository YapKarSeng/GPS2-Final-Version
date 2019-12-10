﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public static int CountEnemyAlive = 0;//Improve founction 01, when before enemy been destory or arrive end point, spwan next wave.
    public Wave[] waves;
    public WaveRight[] rightwaves;
    public Transform START;
    public Transform RIGHTSTART;
    public float waveRate = 0.3f;
    public float waveRateRight = 1f;
    public float LeftEnemyStartSpawnTime = 3f;
    public float RightEnemyStartSpawnTime = 6f;


    public int gameStart;

    void Start()
    {
        gameStart = 2;
    }
    public void CountDownEnemy()
    {
        gameStart -= 1;
    }
    public void Update()
    {
        if (gameStart == 1)
        {
            StartCountDown();
        }
    }

    void StartCountDown()
    {
        StartCoroutine(WaitPlayer());
        gameStart--;
    }

    public IEnumerator WaitPlayer()
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(13);//13
        SoundManager.PlaySound("Startsound");
        StartCoroutine(WaitLeftEnemySpawn());
        StartCoroutine(WaitRightEnemySpawn());
    }

    IEnumerator WaitLeftEnemySpawn()
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(LeftEnemyStartSpawnTime);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator WaitRightEnemySpawn()
    {
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(RightEnemyStartSpawnTime);
        StartCoroutine(SpawnEnemyRight());
    }

    IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (CountEnemyAlive > 100)//If anyone enemy in the map,new enemy will not spawn.
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
    }

    IEnumerator SpawnEnemyRight()
    {
        foreach (WaveRight wave in rightwaves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyRightPrefab, RIGHTSTART.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            yield return new WaitForSeconds(waveRateRight);
        }
        while (CountEnemyAlive > 100)
        {
            yield return 0;
        }
        
    }
}
