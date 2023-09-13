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
    private IntVariable lives;

    // Start is called before the first frame update
    void Start()
    {
        lives.ValueChangedEvent += OnValueChanged;
        SetText(lives.Value);
    }

    private void OnValueChanged(object sender, IntVariable lives)
    {
        SetText(lives.Value);
    }

    private void SetText(int lives)
    {
        livesText.text = $"Lives: {lives}";
    }
}
