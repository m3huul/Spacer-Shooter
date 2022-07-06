using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerShip;
    public GameObject PlayButton;
    public GameObject GameOver;

    public GameObject EnemySpawner;
    public GameObject scoreTextUI;
    public GameObject TimeCounterUI;
    public enum GameMangerState
    {
        Opening,
        Gameplay,
        GameOver,
    } 
    GameMangerState GMState;  
    // Start is called before the first frame update
    void Start()
    {
        GMState=GameMangerState.Opening;
    }

    void UpdateGameManagerState(){
        switch(GMState){
            case GameMangerState.Opening:
                PlayButton.SetActive(true);
                GameOver.SetActive(false);
                break;

            case GameMangerState.Gameplay:

                scoreTextUI.GetComponent<GameScore>().Score = 0;
                PlayButton.SetActive(false);
                PlayerShip.GetComponent<PlayerMovement>().Init();
                EnemySpawner.GetComponent<EnemySpawner>().StartSpawner();
                TimeCounterUI.GetComponent<TimeCounter>().StartTimeCounter();
                break;

            case GameMangerState.GameOver:  
                TimeCounterUI.GetComponent<TimeCounter>().StopTimeCounter(); 
                EnemySpawner.GetComponent<EnemySpawner>().StopSpawner();
                GameOver.SetActive(true);
                Invoke("ChangeToOpening",8f);

                break;      
        }

    }

    public void SetGameManagerState(GameMangerState state){
        GMState = state;
        UpdateGameManagerState();
    }
    
    public void StartGamePlay(){
        GMState=GameMangerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpening(){
        SetGameManagerState(GameMangerState.Opening);
    }
}
