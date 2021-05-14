using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControllScript : MonoBehaviour
{
    public GameObject self;
    public Transform ball;
    // Update is called once per frame
    void Update()
    {
        self.transform.position = new Vector3(ball.position.x, 6.14f, -5.96f);
    }
}
