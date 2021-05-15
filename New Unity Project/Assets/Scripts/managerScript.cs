using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerScript : MonoBehaviour
{
    public GameObject ball;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    private Collider[] ballCollisions;

    public bool ballShotCheck = false;
    private bool ballPossessed = false;
    private GameObject possessionTracker = null;

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
    void setPossessionTracker(GameObject possessor, bool _possessionLock)
    {
        possessionTracker = possessor;
        ballPossessed = _possessionLock;
    }

    // clear possession sets all possession variables to null
    void clearPossession()
    {
        setPossessionTracker(null, false);
    }

    // ball drop funcrion
    void shotCall()
    {
        clearPossession();          // clear possession to stop the ball being forced onto the player hand pos
                                    // apply a force to move the ball away  from the player > towards the hoop
                                    // start a small timer to wait for the ball to leave the player
        ball.GetComponent<SphereCollider>().enabled = false;        // re enable collision of the ball
        ballShotCheck = false;                                      // reset shot check system
        Debug.Log("Shot call Func");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (getBallPossessed() == true)
        {
            ball.GetComponent<SphereCollider>().enabled = false;    // disabled to stop the ball trying to leave the set position   // re enable when no longer needed
            ball.transform.position = getPossessionTracker().transform.GetChild(1).position;


        }


        if (getBallPossessed() == false)
        {
            ballCollisions = Physics.OverlapSphere(ball.transform.position, 0.5f);
            foreach (Collider index in ballCollisions)
            {
                if (index.CompareTag("playerEntity"))
                {
                    setPossessionTracker(index.gameObject, true);
                }

            }

        }

        if (ballShotCheck == true)
        {
            shotCall();
        }
        

    }
}
