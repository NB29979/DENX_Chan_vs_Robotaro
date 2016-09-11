using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayScene_Renda : MonoBehaviour {

    public GameObject MazaiGauge;

    private float renda_gage;
    private int KEY_DOWN_INTERVAL = 20;
    private int frame_cnt;
    private bool is_key_recept;


	// Use this for initialization
    void Start () {
        renda_gage = 50;
        frame_cnt = 0;
        is_key_recept = true;
    }
	
	// Update is called once per frame
	void Update () 
    {

	}
    public IEnumerator rendaEnter() {
        frame_cnt++;
        if (frame_cnt == KEY_DOWN_INTERVAL) {
            is_key_recept = true;
            frame_cnt = 0;
            GetComponent<PlayScene_Score>().addScore(-2);
        }

        if(Input.GetKeyDown(KeyCode.Return)){
            if (is_key_recept) {
                SoundManager.Instance.PlaySE(11);
                renda_gage -= 20;
                is_key_recept = false;
            }
        }

        if (renda_gage <= 0 &&
            GameObject.Find("denxc").GetComponent<PlayScene_HpBarControl>().getHP() > 0) {
            renda_gage = 0;
            GetComponent<PlayScene_ThrowMazaiCode>().setGameClearFlg(true);
        }
        else if (renda_gage > 100) {
            renda_gage = 100;
            GetComponent<PlayScene_ThrowMazaiCode>().setGameOverFlg(true);
        }else{
            renda_gage += 0.82f; 
        }

        MazaiGauge.GetComponent<Slider>().value = renda_gage;
        yield return null;
    }
}
