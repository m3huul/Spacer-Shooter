using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private Text scoreTextUI;
    [SerializeField] private int score;

    public int Score
    {
        get{
            return this.score;
        }
        set{
            this.score=value;
            UpdateScoreTextUI();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreTextUI=GetComponent<Text>();
    }

    // Update is called once per frame
    void UpdateScoreTextUI(){
        string scoreStr=string.Format("{0:00000}",score);
        scoreTextUI.text=scoreStr;
    }
}
