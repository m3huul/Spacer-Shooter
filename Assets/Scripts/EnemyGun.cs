using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("FireEnemyBullet",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet(){
        //reference to the player ship
        GameObject PlayerShip=GameObject.Find("PlayerGO");

        //checkin if the player is dead or not
        if(PlayerShip!=null){
            GameObject bullet=(GameObject)Instantiate(EnemyBullet); //instantiate a enemy bullet

            bullet.transform.position=transform.position; //set bullets initial position

            Vector2 direction= PlayerShip.transform.position-bullet.transform.position; //computing the bullet direction

            bullet.GetComponent<EnemyBullet>().SetDirection(direction); //referncing to the enemybullet script and sending the direction value
        }

    }
}
