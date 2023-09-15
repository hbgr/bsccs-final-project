using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void OnDisable()
    {
        events.ExperienceGainedEvent -= OnExperienceGained;
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

    public event EventHandler<ExperienceManager> ExperienceChangedEvent;

    private void OnExperienceChanged(object sender, ExperienceManager expManager)
    {
        ExperienceChangedEvent?.Invoke(sender, expManager);
    }
}
