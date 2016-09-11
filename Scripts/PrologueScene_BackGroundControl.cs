using UnityEngine;
using System.Collections;

public class PrologueScene_BackGroundControl : MonoBehaviour {

    private float scroll_speed = .1f;

    private float y;
    private Vector2 offset;

    //スクロール速度の再設定．
    void setScrollSpeed(float ar_scroll_speed) {
        scroll_speed = ar_scroll_speed;
    }
	
	// Update is called once per frame
	void Update () {
        y = Mathf.Repeat(Time.time * scroll_speed, 1);

        offset = new Vector2(0, -y);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
