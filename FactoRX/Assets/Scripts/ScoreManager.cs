using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Systems/ScoreManager")]
public class ScoreManager : ScriptableObject
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private int highScore;

    public int HighScore
    {
        get => highScore;
        set
        {
            highScore = value;
            OnScoreChanged(this, this);
        }
    }

    private int score;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreChanged(this, this);
        }
    }

    private void OnEnable()
    {
        Score = 0;
        events.ScorePointsEvent += OnScorePoints;
        events.GameStateChangedEvent += OnGameStateChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    private void OnDisable()
    {
        Score = 0;
        events.ScorePointsEvent -= OnScorePoints;
        events.GameStateChangedEvent -= OnGameStateChanged;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnScorePoints(object sender, int e)
    {
        Score += e;
    }

    private void OnGameStateChanged(object sender, GameState e)
    {
        if (e == GameState.GameOver)
        {
            if (Score > HighScore)
            {
                HighScore = Score;
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Score = 0;
    }

    public event EventHandler<ScoreManager> ScoreChangedEvent;

    private void OnScoreChanged(object sender, ScoreManager scoreManager)
    {
        ScoreChangedEvent?.Invoke(sender, scoreManager);
    }
}
