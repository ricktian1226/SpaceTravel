using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private int score = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Add(int value)
    {
        score += value;
        updateUI();
    }

    void updateUI()
    {
        SingletonMonoBehaviour<GameManager>.Instance.uiConfiguration.ScoreLabel.text = string.Format("Score : {0}", score);
    }
}
