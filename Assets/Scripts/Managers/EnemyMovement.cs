using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionAinm;
    private GameObject scoreTextUI;
    private float speed;
    private Vector2 max;
    // Start is called before the first frame update
    void Start()
    {
        speed=2f;
        scoreTextUI=GameObject.FindGameObjectWithTag("ScoreTextTag");
        max = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 posi=transform.position;

        transform.position = new Vector2(posi.x - speed * Time.deltaTime, posi.y);

        if(transform.position.x<max.x){
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
