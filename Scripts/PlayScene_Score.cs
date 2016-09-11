using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayScene_Score : MonoBehaviour {

    private int score;
    public Text ScoreLabel;

	// Use this for initialization
	void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        ScoreLabel.text = "Score: " + ShowScore();
	}

    public void addScore(int add_point)
    {
        score += add_point;
    }

    public string ShowScore()
    {
        return  score.ToString();
    }
}
