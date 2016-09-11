using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayScene_JudgeGame: MonoBehaviour {

    public Text GameJudgeLabel;
    public Text SourceBackGroundLabel;
    public GameObject MazaiGauge;

    public GameObject DenxChanHPGauge;
    public GameObject RobotaroHPGauge;

    public Text PlayerScoreLabel;

    // Use this for initialization"; 
    void Start () {
        StopAllCoroutines();
        StartCoroutine("gameLoop");
    }
	
    private IEnumerator gameLoop() {
        yield return countDown();
        SoundManager.Instance.PlayBGM(1);

        while (true) {
            yield return GetComponent<PlayScene_DispAndJudgeSourceCode>().dispSource();
            if (GameObject.Find("Robotaro").GetComponent<PlayScene_HpBarControl>().getHP() <= 30) {
                StopCoroutine(GetComponent<PlayScene_DispAndJudgeSourceCode>().dispSource());

                GetComponent<PlayScene_DispAndJudgeSourceCode>().enabled = false;
                SourceBackGroundLabel.enabled = false;

                SoundManager.Instance.PlaySE(10);
                GameJudgeLabel.text = "<color=yellow>最終形態</color>";
                yield return new WaitForSeconds(1);
                GameJudgeLabel.text = "";

                GetComponent<PlayScene_ThrowMazaiCode>().enabled = true;
                GetComponent<PlayScene_Renda>().enabled = true;
                MazaiGauge.SetActive(true);
                break;
            }
            else if(GameObject.Find("denxc").GetComponent<PlayScene_HpBarControl>().getHP()<=0) {
               GetComponent<PlayScene_ThrowMazaiCode>().setGameOverFlg(true);
               GetComponent<PlayScene_ThrowMazaiCode>().setGameClearFlg(false);

               GetComponent<PlayScene_DispAndJudgeSourceCode>().enabled = false;
               SourceBackGroundLabel.enabled = false;

               break;
            }
        }

        while (true) {
            yield return GetComponent<PlayScene_Renda>().rendaEnter();
            if (GetComponent<PlayScene_ThrowMazaiCode>().isGameClear() == true ||
                GetComponent<PlayScene_ThrowMazaiCode>().isGameOver() == true) {
                StopCoroutine(GetComponent<PlayScene_Renda>().rendaEnter());
                break;
            }
        }

        MazaiGauge.SetActive(false);
        PlayerScoreLabel.enabled = false;

        yield return gameEnd();
        SceneManager.LoadScene("TitleScene");
    }

    private IEnumerator countDown() {
        yield return new WaitForSeconds(1);

        for(int i = 0; i<3; i++) {
            SoundManager.Instance.PlaySE(6);
            GameJudgeLabel.text = "<color=#00ffff>" + (3 - i).ToString() + "</color>";
            yield return new WaitForSeconds(1);
        }
        SoundManager.Instance.PlaySE(7);
        GameJudgeLabel.text = "<color=#00ffff>スタート！</color>";
        yield return new WaitForSeconds(1);

        GameJudgeLabel.text = "";
    }
    private IEnumerator gameEnd() {
        SoundManager.Instance.StopBGM();
        DenxChanHPGauge.SetActive(false);
        RobotaroHPGauge.SetActive(false);

        if (GetComponent<PlayScene_ThrowMazaiCode>().isGameClear()) {
            GameJudgeLabel.text = "<color=yellow>GAME CLEAR!" +
                "\n<size=40>Score:" + GetComponent<PlayScene_Score>().ShowScore() + "</size></color>";
            SoundManager.Instance.PlaySE(2);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("TitleScene");
        }

        if (GetComponent<PlayScene_ThrowMazaiCode>().isGameOver()) {
            GameJudgeLabel.text = "<color=#ff0000ff>GAME OVER</color>";
            SoundManager.Instance.PlaySE(3);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("TitleScene");
        }
        yield return null;
    }
}
