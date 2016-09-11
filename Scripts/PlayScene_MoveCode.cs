using UnityEngine;
using System.Collections;

public class PlayScene_MoveCode : MonoBehaviour {

    private float speed;
    //private bool is_game_over = false;

	// Update is called once per frame
	void Update ()
    {
        this.transform.position += new Vector3 (speed*-1, 0, 0);

        if(!GetComponent<Renderer>().isVisible){    // 画面外に出たら破棄
            Destroy(gameObject);
        }
	}

    public void setSpeed(float _speed)
    {
        speed = _speed;
    }

}
