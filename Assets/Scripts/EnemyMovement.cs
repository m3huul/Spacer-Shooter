using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    GameObject scoreTextUI;
    float speed;
    public GameObject ExplosionAinm;
    // Start is called before the first frame update
    void Start()
    {
        speed=2f;
        scoreTextUI=GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posi=transform.position;

        posi=new Vector2(posi.x - speed * Time.deltaTime, posi.y);

        transform.position=posi;

        Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(0,0));

        if(posi.x<max.x){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerBulletTag")){
            PlayExplosion();

            scoreTextUI.GetComponent<GameScore>().Score +=100;

            Destroy(gameObject);
        }
    }

    void PlayExplosion(){ //function to play explosion on collision
        GameObject explosion=(GameObject)Instantiate (ExplosionAinm);

        explosion.transform.position=transform.position;
        
    }
}
