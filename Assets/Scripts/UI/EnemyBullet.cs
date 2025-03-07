using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private float speed;
    private Vector2 _direction;
    private bool isReady;
    
    void Awake(){
        speed=5f;
        isReady=false;
    }

    //Function to set bullets direction
    internal void SetDirection(Vector2 direction){
        _direction= direction.normalized;
        isReady=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReady){
            Vector2 position=transform.position; //bullets position

            position += _direction * speed * Time.deltaTime; //computing next bullets position

            transform.position=position; //moving the bullet;

            Vector2 min=Camera.main.ViewportToWorldPoint(new Vector2(0,0)); //bottom-left
            Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(1,1)); //top-right
            if(position.y<min.y || position.x>max.x || position.x<min.x){
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag=="PlayerShipTag" || col.tag=="PlayerBulletTag"){
            Destroy(gameObject);
        }
    }
}
