using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSource : MonoBehaviour
{
    [Header("References")]
    public GameObject Coin;

    [Header("Settings")]
    public int scoreReward = 15;
    public int coinsReward = 1;

    public void DropRewards()
    {
        RewardScore();
        DropCoins(coinsReward);
    }

    void RewardScore()
    {
        GameManager.instance.EarnScore(scoreReward);
    }

    void DropCoins(int coinsToDrop)
    {
        if (Coin == null)
            return;

        for(int i = 0; i < coinsToDrop; i++)
        {
            DropCoin(GetRandomPosition());
        }
    }

    void DropCoin(Vector3 pos)
    {
        ObjectPool.instance.GetObject(ObjectPool.ObjectTypes.coin, pos);
    }

    Vector3 GetRandomPosition()
    {
        Vector3 pos = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f),
                                  0,
                                  Random.Range(transform.position.z - 0.5f, transform.position.z + 0.5f));

        return pos;
    }
}
