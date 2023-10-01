using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpProgressBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform backgroundTransform;

    [SerializeField]
    private RectTransform barTransform;

    [SerializeField]
    private TextMeshProUGUI currentLevelText;

    [SerializeField]
    private TextMeshProUGUI nextLevelText;

    [SerializeField]
    private ExperienceManager expManager;

    // Start is called before the first frame update
    void Start()
    {
        expManager.ExperienceChangedEvent += OnExperienceChanged;
    }

    private void OnExperienceChanged(object sender, ExperienceManager e)
    {
        var maxWidth = backgroundTransform.rect.width;
        float barPercent = (float)e.Experience / (float)e.LevelUpThreshold;
        barTransform.sizeDelta = new Vector2(barPercent * maxWidth, barTransform.sizeDelta.y);

        currentLevelText.text = $"{e.Level}";
        nextLevelText.text = $"{e.Level + 1}";
    }    

    private void OnDestroy()
    {
        expManager.ExperienceChangedEvent -= OnExperienceChanged;
    }
}
