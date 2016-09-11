using UnityEngine;
using System.Collections;

public class PlayScene_FloatingRobotaro : MonoBehaviour {
    void Update() {
        transform.position = new Vector3(transform.position.x, 0.5f*Mathf.PingPong(Time.time, 0.15f), transform.position.z);
    }
}
