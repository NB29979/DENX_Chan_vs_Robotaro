using UnityEngine;
using System.Collections;

public class payload : MonoBehaviour {
    public float limit;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < limit) { Destroy(gameObject);}
	}

}
