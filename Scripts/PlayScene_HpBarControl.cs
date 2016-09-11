using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayScene_HpBarControl : MonoBehaviour {

    public GameObject gameobject;
    public float HP = 100.0f;
    public string slider_str;
    public string child_str;
    private ParticleSystem particle;
    Slider hp;

	// Use this for initialization
	void Start () {
        hp = GameObject.Find(slider_str).GetComponent<Slider>();
//gameObject.GetComponent<Slider>();
        hp.value = HP / 100.0f;

        particle = GameObject.Find(child_str).GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        hp.value = HP / 100.0f;
	}

    public void takenDamage(string ar_name,float ar_damage) {
        if (ar_name == "robotaro")
            SoundManager.Instance.PlaySE(8);
        else
            SoundManager.Instance.PlaySE(9);
        particle.Play();
        HP -= ar_damage;
    }

    public float getHP() {
        return HP;
    }
}
