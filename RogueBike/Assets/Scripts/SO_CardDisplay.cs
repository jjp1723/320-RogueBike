using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class SO_CardDisplay : MonoBehaviour
     , IPointerClickHandler // 2
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{
    [SerializeField]
    private SO_Card card;

    [SerializeField]
    private Image backgroundImage;
    [SerializeField]
    private TMP_Text titleText;
    [SerializeField]
    private TMP_Text descriptionText;

    [SerializeField]
    private UnityEvent onClick;


    void Start()
    {
        backgroundImage.sprite = card.Background;
        titleText.text = card.Title;
        descriptionText.text = card.Description;
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        onClick.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 150, gameObject.transform.position.z);
        gameObject.transform.localScale = new Vector3(4, 4, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.localScale = new Vector3(2, 2, 1);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 150, gameObject.transform.position.z);
    }
}
