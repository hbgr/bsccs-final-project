using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviourExtended
{
    [SerializeField]
    private ScriptableInputEvents inputEvents;

    [SerializeField]
    private GameObject menuObject;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private ScoreManager score;

    // Start is called before the first frame update
    void Start()
    {
        inputEvents.OnAction2Event += OnAction2;
    }    

    protected override void OnDestroy()
    {
        base.OnDestroy();

        inputEvents.OnAction2Event -= OnAction2;
    }

    protected override void OnGameStateChanged(object sender, GameState state)
    {
        if (activeGameStates.Contains(state))
        {
            menuObject.SetActive(true);
            SetText(score.Score, score.HighScore);
        }
        else
        {
            menuObject.SetActive(false);
        }
    }

    private void OnAction2(object sender, InputAction.CallbackContext context)
    {
        if (!Enabled) return;

        if (context.performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void SetText(int score, int highscore)
    {
        scoreText.text = $"You scored {score} points!";
        if (score > highscore)
        {
            highScoreText.text = $"Wow! New HighScore!";
        }
        else
        {
            highScoreText.text = $"HighScore: {highscore}";
        }
    }
}
