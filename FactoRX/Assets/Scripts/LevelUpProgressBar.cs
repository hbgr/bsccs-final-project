using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpProgressBar : MonoBehaviour
{
    [SerializeField]
    private RectTransform backgroundTransform;

    [SerializeField]
    private RectTransform barTransform;

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
    }    

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        expManager.ExperienceChangedEvent -= OnExperienceChanged;
    }
}
