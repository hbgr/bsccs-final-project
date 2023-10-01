using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private ScoreManager score;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        score.ScoreChangedEvent += OnScoreChanged;
    }    

    private void OnDestroy()
    {
        score.ScoreChangedEvent -= OnScoreChanged;
    }

    private void OnScoreChanged(object sender, ScoreManager e)
    {
        SetText(e.Score);
    }

    private void SetText(int score)
    {
        scoreText.text = $"{score}";
    }
}
