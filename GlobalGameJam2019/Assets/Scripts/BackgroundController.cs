using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

	public BackgroundMovement background1;
	public BackgroundMovement background2;
	// Use this for initialization
	void Start () {
		background2.Reset ();
	}
}
