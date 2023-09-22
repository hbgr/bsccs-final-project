using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Systems/ExperienceManager")]
public class ExperienceManager : ScriptableObject
{
    [SerializeField]
    private ScriptableGameEvents events;

    [SerializeField]
    private int levelUpThresholdGrowth;

    private int levelUpThreshold;

    public int LevelUpThreshold
    {
        get => levelUpThreshold;
        private set
        {
            levelUpThreshold = value;
            OnExperienceChanged(this, this);
        }
    }

    private int experience;

    public int Experience
    {
        get => experience;
        private set
        {
            experience = value;
            OnExperienceChanged(this, this);
        }
    }

    private int level;

    public int Level => level;

    private void OnEnable()
    {
        experience = 0;
        level = 0;
        levelUpThreshold = levelUpThresholdGrowth;
        events.ExperienceGainedEvent += OnExperienceGained;
        events.GainPercentLevelEvent += OnGainPercentLevel;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        events.ExperienceGainedEvent -= OnExperienceGained;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnExperienceGained(object sender, int e)
    {
        Experience += e;

        if (Experience >= LevelUpThreshold)
        {
            level++;
            events.OnLevelUp(this, EventArgs.Empty);
            int leftoverExp = Experience % LevelUpThreshold;
            Experience = leftoverExp;
            LevelUpThreshold += levelUpThresholdGrowth;
        }
    }

    private void OnGainPercentLevel(object sender, float e)
    {
        int exp = Mathf.RoundToInt(e * LevelUpThreshold);
        OnExperienceGained(sender, exp);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        experience = 0;
        level = 0;
        levelUpThreshold = levelUpThresholdGrowth;
    }

    public event EventHandler<ExperienceManager> ExperienceChangedEvent;

    private void OnExperienceChanged(object sender, ExperienceManager expManager)
    {
        ExperienceChangedEvent?.Invoke(sender, expManager);
    }
}
