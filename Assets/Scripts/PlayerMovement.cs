using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public GameObject GameManager;

    public GameObject PlayerBullet;
    public GameObject PlayerBullet01;
    public GameObject PlayerBullet02;
    public GameObject ExplosionAinm;
    public float speed;

    public Text LiveUIText;
    const int MaxLives=3;
    int Lives;

    public void Init(){
        Lives=MaxLives;
        LiveUIText.text=Lives.ToString();
        transform.position=new Vector2(0,0); //setting position back to center
        gameObject.SetActive(true);
    }

    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")){
            GameObject bullet01=(GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position=PlayerBullet01.transform.position;

            GameObject bullet02=(GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position=PlayerBullet02.transform.position;
        }

        float x=Input.GetAxisRaw("Horizontal");
        float y=Input.GetAxisRaw("Vertical");
        Vector2 direction=new Vector2(x,y).normalized;
        move(direction);
    }

    void move(Vector2 direction)
    {
        Vector2 min=Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        Vector2 max=Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        Vector2 pos=transform.position;
        pos += direction*speed*Time.deltaTime;

        pos.x=Mathf.Clamp(pos.x,min.x,max.x);
        pos.y=Mathf.Clamp(pos.y,min.y,max.y);

        transform.position=pos;
    }

    void OnTriggerEnter2D(Collider2D col) //this func triggers when there is a collision in the game
    {   
        //Detecting collision
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag")){
            PlayExplosion();
            Lives--;
            LiveUIText.text=Lives.ToString();

            if(Lives==0){
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameMangerState.GameOver);

                gameObject.SetActive(false);
            }
            
        }
    }

    void PlayExplosion(){ //function to play explosion on collision
        GameObject explosion=(GameObject)Instantiate (ExplosionAinm);

        explosion.transform.position=transform.position;
        
    }
}
