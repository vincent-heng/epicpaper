﻿using UnityEngine;
using System.Collections;


public class CoinGenerator : MonoBehaviour
{
    public const string SMALLCOIN = "SmallCoin";
    public const string BIGGERCOIN = "BiggerCoin";
    public const string BIGCOIN = "BigCoin";


    public ObjectPool PoolSmallCoin { get; set; }
    public ObjectPool PoolBiggerCoin { get; set; }
    public ObjectPool PoolBigCoin { get; set; }
    
    void Start()
    {
        PoolSmallCoin = gameObject.AddComponent<ObjectPool>();
        PoolBiggerCoin = gameObject.AddComponent<ObjectPool>();
        PoolBigCoin = gameObject.AddComponent<ObjectPool>();
        PoolSmallCoin.Initialize(20, (GameObject)Resources.Load<GameObject>(SMALLCOIN));
        PoolBiggerCoin.Initialize(20, (GameObject)Resources.Load<GameObject>(BIGGERCOIN));
        PoolBigCoin.Initialize(20, (GameObject)Resources.Load<GameObject>(BIGCOIN));
    }


    private ArrayList GenerateCoins(int experience)
    {
        ArrayList coins = new ArrayList();
        int compteur = 1;
        do
        {
            if (compteur > 10000)
            {
                coins.Add(BIGCOIN);
            }
            else if (compteur > 100)
            {
                coins.Add(BIGGERCOIN);
            }
            else
            {
                coins.Add(SMALLCOIN);
            }
            compteur = compteur * 10;
        } while (compteur < experience);
        return coins;
    }


    public void SpawnGold(int experience, Vector3 position, Quaternion rotation)
    {
        ArrayList coinsToSpawn = GenerateCoins(experience);
        foreach (string coin in coinsToSpawn)
        {
            GameObject objectToSpawn = getCoinObjectFromName(coin);
            Vector3 random1 = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(2.5f, 5f), UnityEngine.Random.Range(-2f, 2f));
            Vector3 pos = position + random1;
            objectToSpawn.transform.position = pos;
            objectToSpawn.transform.rotation = rotation;
            Rigidbody rbd = objectToSpawn.GetComponent<Rigidbody>();
            rbd.velocity = new Vector3(Random.Range(-2, 2), Random.Range(1, 2), Random.Range(-2, 2));
            rbd.angularVelocity = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), Random.Range(-20, 20));
            objectToSpawn.SetActive(true);
        }
    }


    private GameObject getCoinObjectFromName(string name)
    {
        switch (name)
        {
            case SMALLCOIN:
                return PoolSmallCoin.GetObject();
            case BIGGERCOIN:
                return PoolBigCoin.GetObject();
            case BIGCOIN:
                return PoolBigCoin.GetObject();
            default:
                return PoolSmallCoin.GetObject();
        }

    }

}