using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class SO_Card : ScriptableObject
{
    [SerializeField]
    private string title;
    public string Title { get { return title; } }

    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    [SerializeField]
    private Sprite background;
    public Sprite Background { get { return background; } }

}
