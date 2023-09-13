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
    private GemStore gemStore;

    // Start is called before the first frame update
    void Start()
    {
        gemStore.GemValueChangedEvent += OnGemValueChanged;
    }

    private void OnGemValueChanged(object sender, GemStore e)
    {
        var maxWidth = backgroundTransform.rect.width;
        float barPercent = (float)e.Gems / (float)e.LevelUpThreshold;
        barTransform.sizeDelta = new Vector2(barPercent * maxWidth, barTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        gemStore.GemValueChangedEvent -= OnGemValueChanged;
    }
}
