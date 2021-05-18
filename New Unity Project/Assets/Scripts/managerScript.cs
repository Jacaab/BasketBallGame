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

    private Collider[] ballCollisions;

    public bool ballShotCheck = false;              // allows players to attempt a shot from outside this script by making this true
    private bool ballPossessed = false;             // locks certain actions happening when someone has possession of the ball
    private GameObject possessionTracker = null;    // tracks which player has the ball currently // used for calculating shot tragectory and setting the balls position to the hand

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
        _possessor.gameObject.GetComponent<playerControllerScript>().possession = _possessionLock;
        possessionTracker = _possessor;
        ballPossessed = _possessionLock;
    }

    // clear possession sets all possession variables to null
    void clearPossession()
    {
        //setPossessionTracker(null, false);
        getPossessionTracker().GetComponent<playerControllerScript>().possession = false;
        possessionTracker = null;
        ballPossessed = false;
    }

    // player attempts to shoot the ball
    void shotCall()
    {
        //ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        clearPossession();                                                                                                      // clear possession to stop the ball being forced onto the player hand pos            
                                                                                                                          
        // TODO: start a small timer to wait for the ball to leave the player                                             
        ball.GetComponent<SphereCollider>().enabled = true;                                                                     // re enable collision of the ball                     

        // TODO: apply a force to move the ball away  from the player > towards the hoop                                  
        ball.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);                                                    // reset rotation to prevent weird physics
        ball.GetComponent<Rigidbody>().AddForce(ball.transform.up * 7, ForceMode.VelocityChange);                               // add force upwards to create an arc
        ball.GetComponent<Rigidbody>().AddForce((hoop.transform.position - ball.transform.position) * 0.8f, ForceMode.VelocityChange); // add force towards the hoop   

        ballShotCheck = false;                                                                                                  // reset shot check system
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
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);       // set the ball velocity to 0 to prevent endlessly falling 
            
            //ball.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            ball.GetComponent<SphereCollider>().enabled = false;                // disabled to stop the ball trying to leave the set position   // re enable when no longer needed
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
                }

            }

        }

        // if a player attempts a shot then ball shot check will be changed to true
        if (ballShotCheck == true)
        {
            shotCall();
        }
        

    }
}
