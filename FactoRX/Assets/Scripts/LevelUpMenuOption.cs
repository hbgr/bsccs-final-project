using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenuOption : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private Image image;

    private LevelUpBehaviour behaviour;

    public void SetProperties(LevelUpBehaviour levelUpBehaviour)
    {
        nameText.text = levelUpBehaviour.Name;
        descriptionText.text = levelUpBehaviour.Description;
        image.sprite = levelUpBehaviour.Sprite;
        behaviour = levelUpBehaviour;
    }

    public void Select()
    {
        behaviour.Apply();
    }
}
