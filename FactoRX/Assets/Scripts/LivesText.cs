using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private LivesManager lives;

    // Start is called before the first frame update
    void Start()
    {
        lives.LivesChangedEvent += OnLivesChanged;
        SetText(lives.Lives);
    }

    private void OnLivesChanged(object sender, int lives)
    {
        SetText(lives);
    }

    private void SetText(int lives)
    {
        livesText.text = $"x {lives}";
    }
}
