using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayScene_DispAndJudgeSourceCode : MonoBehaviour {

    public UnityEngine.UI.Text txtSource,
                               txtSourceBackGround;
    public  string[]   sources;
    private string     player_input;
    public  string[]   correct_answer;

    public  float time_out_sec = 1.0f;
    private float time_elapsed = 0.0f; 

    private int str_ptr = 0;
    private int src_ptr = 0;

    private string color_mode;
    private string prev_str;
    private bool   return_is_pressed = false;

    void Start()
    {
        color_mode = "<color=#00E4DBFF>";
        prev_str = "";

        player_input = "";

        txtSource          .text = color_mode + sources[0][0].ToString() + "</color>";
        txtSourceBackGround.text = sources[0];
    }
	// Update is called once per frame
	void Update () {
	}

    public IEnumerator dispSource() {
            yield return null;
            if (return_is_pressed == false) color_mode = "<color=#00E4DBFF>";

            time_elapsed += Time.deltaTime;

            if (str_ptr != 0 && Input.GetKey(KeyCode.Return) && return_is_pressed == false) {

                return_is_pressed = true;

                player_input += str_ptr.ToString() + "_";

                color_mode = "<color=yellow>";
                changeCharColor();
            }

            if (time_elapsed >= time_out_sec) {

                str_ptr++;

                return_is_pressed = false;

                //文字列最後の文字まで来たら正解かどうか判定し，次の文字列に移行する．
                if (str_ptr == sources[src_ptr].Length) {
                    judgeSource();

                    src_ptr = Random.Range(0, sources.Length);
                    str_ptr = 0;

                    txtSource.text = color_mode + sources[src_ptr][str_ptr].ToString() + "</color>";
                    txtSourceBackGround.text = sources[src_ptr];

                    prev_str = "";
                    player_input = "";
                }
                else {
                    if (str_ptr != 0) {
                        SoundManager.Instance.PlaySE(0);
                    }

                    prev_str = txtSource.text;
                    txtSource.text += color_mode + sources[src_ptr][str_ptr].ToString() + "</color>";
                }

                time_elapsed = 0.0f;
        }
    }

    //文字列中の文字色を変更するメソッド
    void changeCharColor() {
        SoundManager.Instance.PlaySE(1);
        txtSource.text = prev_str + color_mode + sources[src_ptr][str_ptr].ToString() + "</color>";
        color_mode = "<color=#00E4DBFF>";
    }

    //入力の正誤判定
    void judgeSource() {
        if (player_input == correct_answer[src_ptr]) {
            SoundManager.Instance.PlaySE(4);
            GetComponent<PlayScene_Score>().addScore(10);
            GameObject.Find("Robotaro").GetComponent<PlayScene_HpBarControl>().takenDamage("robotaro",20);
        }
        else {
            SoundManager.Instance.PlaySE(5);
            GetComponent<PlayScene_Score>().addScore(-15);
            GameObject.Find("denxc").GetComponent<PlayScene_HpBarControl>().takenDamage("denxchan",20);
        }
    }
}
