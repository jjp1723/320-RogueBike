using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreManager : MonoBehaviour
{
    private float prevHS;
    public float playerTime;
    public float aiTime;


    [SerializeField]
    private TMP_Text aiTimeText;
    
    [SerializeField]
    private TMP_Text playerTimeText;

    [SerializeField]
    private TMP_Text fastestTimeText;

    private bool firstGame = true;

    // Start is called before the first frame update
    void Start()
    {
        prevHS = PlayerPrefs.GetFloat("highscore");
        playerTime = PlayerPrefs.GetFloat("PlayerTime");
        aiTime = PlayerPrefs.GetFloat("AITime");

        firstGame = PlayerPrefs.GetInt("firstGame") == 0;

        if (firstGame)
        {
            PlayerPrefs.SetInt("firstGame", 1);
            prevHS = playerTime;
            PlayerPrefs.SetFloat("highscore", prevHS);
        }
        else if (playerTime < prevHS)
        {
            prevHS = playerTime;
            PlayerPrefs.SetFloat("highscore", prevHS);
        }

        aiTimeText.text = "AI Time: " + string.Format("{0,2:00}:{1,2:00.00}", (int)aiTime / 60, aiTime % 60);
        playerTimeText.text = "Player Time: " + string.Format("{0,2:00}:{1,2:00.00}", (int)playerTime / 60, playerTime % 60);
        fastestTimeText.text = "Fastest Time: " + string.Format("{0,2:00}:{1,2:00.00}", (int)prevHS / 60, prevHS % 60);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
