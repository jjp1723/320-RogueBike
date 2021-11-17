using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController.CheckForInput();

        //slow down time i.e. 'pause'
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //set to 0 for true pause
            Time.timeScale = 0.2f; 
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1;
        }
    }
}
