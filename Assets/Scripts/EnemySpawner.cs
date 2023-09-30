using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float MaxSpawnRate=7f;

    void SpawnEnemy(){
        Vector2 min=Camera.main.ViewportToWorldPoint(new Vector2(0,0));//bottom-left
        Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(1,1));//top-right

        GameObject enemyposi=(GameObject)Instantiate(Enemy);
#if UNITY_ANDROID
        enemyposi.transform.position=new Vector2(Random.Range(min.x, max.x), max.y);
#else
        enemyposi.transform.position=new Vector2(max.x,Random.Range(min.y,max.y));
#endif

        nextSpawn();
    }

    void nextSpawn(){
        float spawnInSecs;
        if(MaxSpawnRate>1f){
            spawnInSecs=Random.Range(1f,MaxSpawnRate);
        }
        else{
            spawnInSecs=1f;
        }
        Invoke("SpawnEnemy",spawnInSecs);
    }

    void IncSpawnRate(){
        if(MaxSpawnRate>1f){
            --MaxSpawnRate;
        }
        if(MaxSpawnRate==1f){
            CancelInvoke("IncSpawnRate");
        }
    }

    public void StartSpawner(){
        MaxSpawnRate=7f;
        Invoke("SpawnEnemy",MaxSpawnRate);

        InvokeRepeating("IncSpawnRate",0f,20f);
    }

    public void StopSpawner(){
        CancelInvoke("SpawnEnemy");
        CancelInvoke("IncSpawnRate");
    }
}
