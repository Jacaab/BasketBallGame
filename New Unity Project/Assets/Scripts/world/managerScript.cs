using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class managerScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject hoop;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject resultsScreen;

    public bool ballShotCheck = false;              // allows players to attempt a shot from outside this script by making this true

    private Collider[] ballCollisions;
    private Collider[] tackleCheck;

    private int scoreP1 = 0;
    private int scoreP2 = 0;
    private bool goal1 = false;                     // make sure the ball went through the hoop the correct way
    private bool goal2 = false;
    public Text p1Text;
    public Text p2Text;
    public Text winner;

    private bool ballPossessed = false;             // locks certain actions happening when someone has possession of the ball
    private GameObject possessionTracker = null;    // tracks which player has the ball currently // used for calculating shot tragectory and setting the balls position to the hand
    private GameObject lastPossessed = null;        // track who last had the ball // used in score system and future game rules
    private GameObject tackleTarget;

    // is the ball in possession?
    bool getBallPossessed()
    {
        return ballPossessed;
    }

    // who has the ball?
    GameObject getPossessionTracker()
    {
        return possessionTracker;
    }

    // set who has the ball
    // ( who has the ball now? , set to true )
    void setPossessionTracker(GameObject _possessor, bool _possessionLock)
    {
        if (_possessor == player1)
        {
            _possessor.gameObject.GetComponent<playerControllerScript>().possession = _possessionLock;
        }
        else if (_possessor == player2)
        {
            _possessor.gameObject.GetComponent<player2ControllerScript>().possession = _possessionLock;
        }

        possessionTracker = _possessor;
        ballPossessed = _possessionLock;
        winner.text = getPossessionTracker().name;

        clearGoal();
    }

    // clear possession sets all possession variables to null
    void clearPossession()
    {
        if (getPossessionTracker() == player1)
        {
            getPossessionTracker().GetComponent<playerControllerScript>().possession = false;
        }
        else if (getPossessionTracker() == player2)
        {
            getPossessionTracker().GetComponent<player2ControllerScript>().possession = false;
        }
        possessionTracker = null;
        ballPossessed = false;
    }

    // player attempts to shoot the ball
    void shotCall()
    {
        clearGoal();
        float shotMod = 10;
        // calculate the shot accuracy based on shot takers stats
        if (getPossessionTracker() == player1)
        {
            shotMod = getPossessionTracker().GetComponent<playerControllerScript>().shotAccuracy;                               // get the shot takers accuracy stat
        }
        else if (getPossessionTracker() == player2)
        {
            shotMod = getPossessionTracker().GetComponent<player2ControllerScript>().shotAccuracy;                               // get the shot takers accuracy stat
        }

        shotMod = shotMod / 10;                                                                                                   // turn shot acc stat into a decimal
        shotMod = Random.Range(-shotMod, shotMod);                                                                                    // use decimal as a random multiplier from negative stat to positive stat

        clearPossession();                                                                                                      // clear possession to stop the ball being forced onto the player hand pos            

        ball.GetComponent<SphereCollider>().enabled = true;                                                                     // re enable collision of the ball                     

        // apply a force to move the ball away from the player > towards the hoop                                  
        ball.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);                                                    // reset rotation to prevent weird physics      
        ball.GetComponent<Rigidbody>().AddForce(ball.transform.up * (7 + shotMod), ForceMode.VelocityChange);                     // add force upwards to create an arc
        ball.GetComponent<Rigidbody>().AddForce((hoop.transform.position - ball.transform.position) * 0.8f, ForceMode.VelocityChange); // add force towards the hoop   

        ballShotCheck = false;                                                                                                  // reset shot check system
    }

    public void tackle(GameObject _tackler)
    {
        Debug.Log("tackle called");
        if (_tackler == player1)
        {
            tackleTarget = player2;
        }
        else if (_tackler == player2)
        {
            tackleTarget = player1;
        }

        tackleCheck = Physics.OverlapSphere(_tackler.transform.position, 1f);     // search for a player
        foreach (Collider index in tackleCheck)
        {
            Debug.Log(index.gameObject.name);
            if (index.gameObject == tackleTarget)
            {
                Debug.Log("ballfound");
                clearPossession();
                setPossessionTracker(_tackler, true);
            }
        }
    }
    // point has been scored
    void pointScore()
    {
            if (lastPossessed == player1)
            {
                scoreP1 += 2;
                p1Text.text = scoreP1.ToString();
            }
            else if (lastPossessed == player2)
            {
                scoreP2 += 2;
                p2Text.text = scoreP2.ToString();
            }
        clearGoal();
    }

    void clearGoal()
    {
        goal1 = false;
        goal2 = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Update()
    {
        //  if the ball is in possession of a player
        if (getBallPossessed() == true)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);                    // set the ball velocity to 0 to prevent endlessly falling 


            ball.GetComponent<SphereCollider>().enabled = false;                             // disabled to stop the ball trying to leave the set position   // re enable when no longer needed
            ball.transform.position = getPossessionTracker().transform.GetChild(1).position; // set ball position to the players hand position
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //  if the ball is not possessed by a player
        if (getBallPossessed() == false)
        {
            ballCollisions = Physics.OverlapSphere(ball.transform.position, 0.55f);     // search for a player coliding with the ball
            foreach (Collider index in ballCollisions)
            {
                if (index.CompareTag("playerEntity"))
                {
                    setPossessionTracker(index.gameObject, true);                       // if a player is found set the player to the possession tracker
                    lastPossessed = index.gameObject;
                }

                if (index.CompareTag("goal1"))
                {
                    goal1 = true;
                }
                if (index.CompareTag("goal2"))
                {

                    goal2 = true;
                    if(goal1)
                    { 
                         pointScore();
                    }
                }
            }

        }

        // if a player attempts a shot then ball shot check will be changed to true
        if (ballShotCheck == true)
        {
            shotCall();
        }

        if (scoreP1 > 18 || scoreP2 > 18)
        {
            resultsScreen.SetActive(true);
        }

    }
}