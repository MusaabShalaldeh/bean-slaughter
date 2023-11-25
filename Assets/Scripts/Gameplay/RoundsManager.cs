using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    #region singleton
    public static RoundsManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("References")]
    public TMP_Text RoundNumberText;
    public List<GameObject> Enemies;
    public Vector3 SpawnArea;

    [Header("Settings")]
    public int enemiesCount = 5;
    public float enemiesIncreasePercentage = 1.2f;

    // Private Variables
    int currentRound = 0;
    int spawnedEnemies;

    void Start()
    {
        StartRound();
    }

    void StartRound()
    {
        currentRound++;
        RoundNumberText.text = "Round " + currentRound.ToString();

        int num = (int)(enemiesCount * enemiesIncreasePercentage);
        enemiesCount = num;

        Debug.Log("Spawning " + num + " Enemies");
        SpawnEnemies(num);
    }

    void OnRoundEnd()
    {
        Player.instance.Heal(110);
        StartRound();
    }

    void SpawnEnemies(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy(Random.Range(0, Enemies.Count));
        }
    }

    void SpawnEnemy(int num)
    {
        GameObject obj = ObjectPool.instance.GetObject((ObjectPool.ObjectTypes)num, GetRandomValidPosition());
        if(obj != null)
        {
            spawnedEnemies++;
        }
    }

    public void OnEnemyDeath()
    {
        spawnedEnemies--;

        if (spawnedEnemies <= 0)
            OnRoundEnd();
    }

    Vector3 GetRandomValidPosition()
    {
        Vector3 pos = new Vector3(  Random.Range(-SpawnArea.x / 2, SpawnArea.x / 2),
                                    0,
                                    Random.Range(-SpawnArea.z / 2, SpawnArea.z / 2));

        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(new Vector3(), SpawnArea);
    }
}
