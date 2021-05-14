using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballCoreScript : MonoBehaviour {
	public GameObject self;

	// Update is called once per frame
	void Update () {
		self.transform.rotation = Quaternion.Euler(45, 0, 0);

	}
}
