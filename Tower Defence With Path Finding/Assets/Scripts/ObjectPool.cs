using System.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] [Range(0, 50)] int poolSize = 3;
    [SerializeField] [Range(0.1f, 15f)] float enemySpawnTime = 1f;

    GameObject[] pool;

    void Awake(){
        PopulatePool();
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for(int i=0; i<pool.Length; i++){
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(EnemySpawner());
    }


    void Update()
    {

    }

    IEnumerator EnemySpawner()
    {
        while(poolSize>0){
            // poolSize--;
            EnableObjectsInPool();
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    void EnableObjectsInPool()
    {
        for(int i=0;i<pool.Length;i++){
            if(pool[i].activeInHierarchy == false){
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
