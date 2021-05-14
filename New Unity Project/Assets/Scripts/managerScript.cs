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

    private bool ballPossessed = false;
    private GameObject possessionTracker = null;


    // is the ball in possession?
    bool getBallPossessed()
    {
        return ballPossessed;
    }

    // set ball possession check
    void setBallPossession(bool swap)
    {
        ballPossessed = swap;
    }

    // who has the ball?
    GameObject getPossessionTracker()
    {
        return possessionTracker;
    }

    // set who has the ball
    void setPossessionTracker(GameObject possessor, bool possessionLock)
    {
        possessionTracker = possessor;
        setBallPossession(possessionLock);
    }

    // clear possession
    void clearPossession()
    {
        setPossessionTracker(null, false);
    }

    void pickupCollision(Collision col)
    {
        if (col.gameObject.tag == "playerEntity")
        {
            setPossessionTracker(col.gameObject, true);
        }
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
            ball.transform.position = getPossessionTracker().GetComponent("handPos").transform.position;
        }

        if (getBallPossessed() == false)
        {
            //pickupCollision()
        }

    }
}
