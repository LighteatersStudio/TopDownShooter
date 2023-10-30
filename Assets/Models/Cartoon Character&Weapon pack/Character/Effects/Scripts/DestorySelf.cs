using UnityEngine;
using System.Collections;

public class DestorySelf : MonoBehaviour {

	public float dtime = 2.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		dtime -= Time.deltaTime;
		if(Time.frameCount % 10 == 0)
			if(dtime < 0.0f)
				Destroy(this.gameObject);
	}
}
