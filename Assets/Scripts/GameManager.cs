using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerShip;
    [SerializeField] private GameObject EnemySpawner;
    [SerializeField] private GameObject scoreTextUI;
    [SerializeField] private GameObject TimeCounterUI;
    [SerializeField] private GameScore Current;
    [SerializeField] private Text HighScoreText;
    [SerializeField] private CanvasGroup GameplayUI;
    [SerializeField] private CanvasGroup OpeningUI;
    [SerializeField] private CanvasGroup GameOverUI;
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
        UpdateGameManagerState();
    }

    void UpdateGameManagerState(){
        switch(GMState){

            case GameMangerState.Opening:
                GameOverUI.alpha = 0f;
                if (PlayerPrefs.HasKey("HighScore"))
                {
                    HighScoreText.gameObject.SetActive(true);
                    HighScoreText.text = "HIGHEST SCORE:\n" + PlayerPrefs.GetInt("HighScore");
                }
                else
                {
                    HighScoreText.gameObject.SetActive(false);
                }
                StopAllCoroutines();
                StartCoroutine(Lerp(OpeningUI, 2f, 0f, 1f));

                PlayerShip.transform.position=new Vector3(0,0,0);
                PlayerShip.SetActive(true);
                break;

            case GameMangerState.Gameplay:
                OpeningUI.alpha = 0f;

                StopAllCoroutines();
                StartCoroutine(Lerp(GameplayUI, 2f, 0, 1));

                scoreTextUI.GetComponent<GameScore>().Score = 0;

                PlayerShip.GetComponent<PlayerMovement>().Init();
                EnemySpawner.GetComponent<EnemySpawner>().StartSpawner();
                TimeCounterUI.GetComponent<TimeCounter>().StartTimeCounter();
                break;

            case GameMangerState.GameOver:
                if (PlayerPrefs.HasKey("HighScore") && PlayerPrefs.GetInt("HighScore")<Current.Score)
                {
                    PlayerPrefs.SetInt("HighScore", Current.Score);
                }
                else
                {
                    PlayerPrefs.SetInt("HighScore", Current.Score);
                }
                GameplayUI.alpha = 0f;
                StopAllCoroutines();
                StartCoroutine(Lerp(GameOverUI, 2f, 0, 1));

                TimeCounterUI.GetComponent<TimeCounter>().StopTimeCounter(); 
                EnemySpawner.GetComponent<EnemySpawner>().StopSpawner();

                Invoke("ChangeToOpening",8f);
                break;      
        }

    }

    public void SetGameManagerState(int i){  //0 to set the gamestate to opening and 1 for game over.
        switch(i){
          case 0:
            GMState = GameMangerState.Opening;
            break;
          case 1:
            GMState = GameMangerState.GameOver;
            break;
        }
        UpdateGameManagerState();
    }
    
    public void StartGamePlay(){
        GMState=GameMangerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpening(){
        SetGameManagerState(0); //setting game state to opening.
    }

    IEnumerator Lerp(CanvasGroup group, float seconds, float from, float to)
    {
        float startTime = Time.time;
        float endTime=startTime + seconds;
        while(Time.time < endTime)
        {
            float elapsedTime = Time.time - startTime;
            float alpha = Mathf.Lerp(from, to, elapsedTime / seconds);
            group.alpha = alpha;
            yield return null;
        }
        group.alpha = to;
    }
}
