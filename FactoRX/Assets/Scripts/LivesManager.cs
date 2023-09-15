using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Systems/LivesManager")]
public class LivesManager : ScriptableObject
{
    [SerializeField]
    private int lives;

    public int Lives
    {
        get => currentLives;
        set
        {
            currentLives = value;
            OnLivesChanged(this, currentLives);
        }
    }

    private int currentLives;

    private void OnEnable()
    {
        currentLives = lives;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        currentLives = lives;
    }

    public event EventHandler<int> LivesChangedEvent;

    private void OnLivesChanged(object sender, int lives)
    {
        LivesChangedEvent?.Invoke(sender, lives);
    }
}
