using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject ai;
    private AIController aiController;
    private PlayerController aiPlayerController;

    //Card to turn
    [SerializeField]
    private List<GameObject> cards = new List<GameObject>();
    private Dictionary<string, TurnCard> turnDict = new Dictionary<string, TurnCard>();

    [SerializeField]
    private TMP_Text timerText;
    private float time;

    [SerializeField]
    private TMP_Text lapCounter;
    
    [SerializeField]
    private TMP_Text countInText;

    [SerializeField]
    private HighScoreManager hsManager;

    private bool aiFinished = false;
    //private List<GameObject> shownCards = new List<GameObject>();

    //[SerializeField]
    //private Camera camera;

    //[SerializeField]
    //private GameObject leftTurnCard_TEMP;

    // Start is called before the first frame update
    void Start()
    {
        //camera = GetComponent<Camera>();

        GameObject leftTurnCard = GameObject.Find("LeftTurn_Card");
        leftTurnCard.SetActive(false);

        cards.Add(leftTurnCard);

        GameObject rightTurnCard = GameObject.Find("RightTurn_Card");
        rightTurnCard.SetActive(false);

        cards.Add(rightTurnCard);

        GameObject cancelVelocityCard = GameObject.Find("CancelVelocity_Card");
        cancelVelocityCard.SetActive(false);

        cards.Add(cancelVelocityCard);

        //playerController = player.GetComponent<PlayerController>();
        playerController = player.GetComponent<PlayerController>();
        aiController = ai.GetComponent<AIController>();
        aiPlayerController = ai.GetComponent<PlayerController>();

        //for (int i = 0; i < cards.Count; i++)
        //{
        //    Debug.Log(cards[i].name);
        //    turnDict[cards[i].name] = cards[i].GetComponent<TurnCard>();
        //}

        time = -3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time < 1)
        {
            float positiveTime = Mathf.Abs(time);
            float scale = 10 * (positiveTime - (int)positiveTime);
            countInText.gameObject.transform.localScale = new Vector3(scale, scale);

            if (time < 0)
            {
                countInText.text = "" + Mathf.Ceil(positiveTime);
                return;
            }
            else
            {
                countInText.text = "Go!";
            }
        }
        else if (time < 3 && time > 1.6)
        {
            countInText.gameObject.SetActive(false);
        }

        if(aiPlayerController.Lap - 1 == aiPlayerController.MaxLap && !aiFinished)
        {
            aiFinished = true;
            PlayerPrefs.SetFloat("AITime", time);
        }
        //Player has hit the last lap
        if (playerController.Lap - 1 == playerController.MaxLap /*|| aiPlayerController.Lap - 1 == aiPlayerController.MaxLap*/)
        {

            PlayerPrefs.SetFloat("PlayerTime", time);
            SceneManager.LoadScene(2);
        }
        
        timerText.text = "Time: " + string.Format("{0,2:00}:{1,2:00.00}", (int)time / 60, time % 60);
        lapCounter.text = "Lap " + string.Format("{0}/{1}", playerController.Lap, playerController.MaxLap);

        playerController.CheckForInput();
        //aiController.Update();

        //slow down time i.e. 'pause'
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //set to 0 for true pause
            Time.timeScale = 0.2f; 
            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].SetActive(true);
                //shownCards.Add(Object.Instantiate(cards[i], new Vector3(-5 + i * 5,-3,0), Quaternion.identity));
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].transform.localScale = new Vector3(2, 2, 1);
                cards[i].transform.position = new Vector3(cards[i].transform.position.x, 88, cards[i].transform.position.z);
                cards[i].SetActive(false);
                //Object.Destroy(shownCards[i]);
            }
            //shownCards.Clear();
            Time.timeScale = 1;
        }

        //can only play cards if paused
        //if (Time.timeScale < 1)
        //{
        //    //play card. Checking on up to make it only happen once
        //    if (Input.GetKeyUp(KeyCode.M))
        //    {
        //        player.transform.Rotate(0, 0, turnDict["RightTurn"].Angle);
        //    }
        //    if (Input.GetKeyUp(KeyCode.N))
        //    {
        //        player.transform.Rotate(0, 0, turnDict["LeftTurn"].Angle);
        //    }

        //    //test clicking on card with mouse
        //    if (Input.GetMouseButtonDown(0)) //left mouse bottom
        //    {
        //        Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

        //        RaycastHit2D hit = Physics2D.Raycast(click2D, Vector2.zero);
        //        if (hit.collider != null)
        //        {
        //            Debug.Log(hit.collider.gameObject.name);
        //        }
        //    }
        //}
    }
}
