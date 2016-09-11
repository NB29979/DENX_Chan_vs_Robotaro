using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScene_PressEnterKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Return)) {
            SoundManager.Instance.PlaySE(1);
            SceneManager.LoadScene("PrologueScene");
        }
    }
}
