using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialScene_TutorialControl : MonoBehaviour {
    public GameObject[] scene;
    private int asdf=0;

	// Use this for initialization
	void Start () {
        for(int i = 1; i < scene.Length; i++) {
            scene[i].SetActive(false);
        }
        scene[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
         if (Input.GetKeyDown("return"))
        {
            SoundManager.Instance.PlaySE(1);

            scene[asdf].SetActive(false);
            asdf++;
            if (scene.Length != asdf)
            {
                scene[asdf].SetActive(true);
            }
            else
            {
                SoundManager.Instance.StopBGM();
                SceneManager.LoadScene("PlayScene");
                //チュートリアル終了時の動作
            }
            
            
            //Debug.Log("uoooo");
        }
	}
}
