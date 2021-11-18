using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SO_CardDisplay : MonoBehaviour
{
    [SerializeField]
    private SO_Card card;

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text descriptionText;


    void Start()
    {
        backgroundImage.sprite = card.Background;
        titleText.text = card.Title;
        descriptionText.text = card.Description;
    }
}
