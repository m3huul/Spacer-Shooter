using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed;
    Vector2 max;
    Vector2 min;
    void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posi=transform.position;

#if UNITY_ANDROID
        transform.position = new Vector2(posi.x, posi.y + speed * Time.deltaTime);
        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
#else
        transform.position = new Vector2(posi.x + speed *Time.deltaTime,posi.y );
        if (transform.position.x>max.x){
            Destroy(gameObject);
        }
#endif



    }

    void OnTriggerEnter2D(Collider2D col){
        if((col.tag=="EnemyShipTag") || (col.tag=="EnemyBulletTag")){
            Destroy(gameObject);
        }
    }
}
