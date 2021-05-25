using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject hoop;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public bool ballShotCheck = false;              // allows players to attempt a shot from outside this script by making this true

    private Collider[] ballCollisions;

    private int scoreP1 = 0;
    private int scoreP2 = 0;
    private bool goal1 = false;                     // make sure the ball went through the hoop the correct way
    private bool goal2 = false;


    private bool ballPossessed = false;             // locks certain actions happening when someone has possession of the ball
    private GameObject possessionTracker = null;    // tracks which player has the ball currently // used for calculating shot tragectory and setting the balls position to the hand
    private GameObject lastPossessed = null;        // track who last had the ball // used in score system and future game rules


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

    // point has been scored
    void pointScore()
    {
        if (lastPossessed == player1)
        {
            scoreP1 += 2;
            Debug.Log(scoreP1);
        }
        else if (lastPossessed == player2)
        {
            scoreP2 += 2;
            Debug.Log(scoreP2);
        }

        goal1 = false;
        goal2 = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //  if the ball is in possession of a player
        if (getBallPossessed() == true)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);                    // set the ball velocity to 0 to prevent endlessly falling 
            
            
            ball.GetComponent<SphereCollider>().enabled = false;                             // disabled to stop the ball trying to leave the set position   // re enable when no longer needed
            ball.transform.position = getPossessionTracker().transform.GetChild(1).position; // set ball position to the players hand position
        }

        //   if the ball is not possessed by a player
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
                if (index.CompareTag("goal2") && goal1)
                {
                    pointScore();
                }
            }

        }

        // if a player attempts a shot then ball shot check will be changed to true
        if (ballShotCheck == true)
        {
            goal1 = false;
            goal2 = false;
            shotCall();
        }

        if (goal1 && goal2)
        {
            pointScore();
        }
    }
}