using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackGroundRenderer : MonoBehaviour {

    Color32 background_color;

    private GameObject panel;

    public struct ColorChangeFreqParam{
        public float coefficient , length;

        public ColorChangeFreqParam(float coe_,float len_) {
            coefficient = coe_;
            length = len_;
        }
    }
    ColorChangeFreqParam param;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "TitleScene" ||
                 SceneManager.GetActiveScene().name == "PrologueScene") {
            param = new ColorChangeFreqParam(255.0f, 1.0f);
        }
        else if (SceneManager.GetActiveScene().name == "PlayScene")
            param = new ColorChangeFreqParam(680.0f, 0.374f);

        background_color = new Color32(128, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
       changeBackGroundColor();
	}

    public void changeBackGroundColor() {
        Renderer rend = GetComponent<Renderer>();
        rend.material.SetColor("_EmisColor", background_color);
        background_color = 
            new Color32(255, 
             (byte)(255 - param.coefficient * Mathf.PingPong(Time.time, param.length)), 255, 128);
    }
}
