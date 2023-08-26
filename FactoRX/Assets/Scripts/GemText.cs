using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemText : MonoBehaviour
{
    [SerializeField]
    private GemStore gemStore;

    [SerializeField]
    private TextMeshProUGUI gemCounterText;

    // Start is called before the first frame update
    void Start()
    {
        gemStore.GemValueChangedEvent += OnGemValueChanged;
        SetGemText(gemStore.Gems);
    }

    private void OnGemValueChanged(object sender, GemStore store)
    {
        SetGemText(gemStore.Gems);
    }

    private void SetGemText(int gems)
    {
        gemCounterText.text = $"{gems}";
    }
}
