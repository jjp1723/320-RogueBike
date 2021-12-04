using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerController playerController;

    [SerializeField]
    private GameObject ai;
    private AIController aiController;

    //Card to turn
    [SerializeField]
    private List<GameObject> cards = new List<GameObject>();
    private Dictionary<string, TurnCard> turnDict = new Dictionary<string, TurnCard>();

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

        //playerController = player.GetComponent<PlayerController>();
        playerController = player.GetComponent<PlayerController>();
        aiController = ai.GetComponent<AIController>();

        //for (int i = 0; i < cards.Count; i++)
        //{
        //    Debug.Log(cards[i].name);
        //    turnDict[cards[i].name] = cards[i].GetComponent<TurnCard>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
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
                cards[i].SetActive(true);
                //Object.Destroy(shownCards[i]);
            }
            //shownCards.Clear();
            Time.timeScale = 1;
        }

        //can only play cards if paused
        if (Time.timeScale < 1)
        {
            //play card. Checking on up to make it only happen once
            if (Input.GetKeyUp(KeyCode.M))
            {
                player.transform.Rotate(0, 0, turnDict["RightTurn"].Angle);
            }
            if (Input.GetKeyUp(KeyCode.N))
            {
                player.transform.Rotate(0, 0, turnDict["LeftTurn"].Angle);
            }

            //test clicking on card with mouse
            if (Input.GetMouseButtonDown(0)) //left mouse bottom
            {
                Vector3 clickpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 click2D = new Vector2(clickpos.x, clickpos.y);

                RaycastHit2D hit = Physics2D.Raycast(click2D, Vector2.zero);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
            }
        }
    }
}
