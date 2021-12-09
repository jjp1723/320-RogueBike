using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSounds : MonoBehaviour
{
    public void OnHover()
    {
        AkSoundEngine.PostEvent("card_hover", gameObject);
    }
    public void OnClick()
    {
        AkSoundEngine.PostEvent("card_use", gameObject);
    }
}
