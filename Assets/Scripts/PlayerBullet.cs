using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posi=transform.position;
        Vector2 position=new Vector2(posi.x + speed *Time.deltaTime,posi.y );
        transform.position=position;
        Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(1,1));
        if(transform.position.x>max.x){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        if((col.tag=="EnemyShipTag") || (col.tag=="EnemyBulletTag")){
            Destroy(gameObject);
        }
    }
}
