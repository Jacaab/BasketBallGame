using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowScript : MonoBehaviour
{
    public GameObject parentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(parentObject.transform.position.x, 0.1f, parentObject.transform.position.z);
        this.transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
