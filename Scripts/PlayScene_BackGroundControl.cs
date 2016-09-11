using UnityEngine;
using System.Collections;

public class PlayScene_BackGroundControl : MonoBehaviour {

    private float scroll_speed = .5f;

    private float x;
    private Vector2 offset;

    //スクロール速度の再設定．
    //でんくすちゃんがこけたときなどスクロールを停止する際に使用する．
    void setScrollSpeed(float ar_scroll_speed) {
        scroll_speed = ar_scroll_speed;
    }
	
	// Update is called once per frame
	void Update () {
        x = Mathf.Repeat(Time.time * scroll_speed, 1);

        offset = new Vector2(x, 0);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
