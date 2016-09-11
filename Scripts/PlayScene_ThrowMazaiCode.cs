using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayScene_ThrowMazaiCode : MonoBehaviour
{
    /* --------Inspectorで指定------ */
    public GameObject mazai_text_object;    // 魔剤コードのテキストメッシュ
    public float mazai_code_speed;          // 魔剤コードを投げる速度
    [Multiline]public string[] mazaiText;   // 複数行の魔剤コードの文字配列
    /* ----------------------------- */

    private int throw_timer;                // 魔剤コードを投げる際のタイマ
    private int throw_interval;             // 魔剤コードを投げる間隔
    private Color alpha = new Color(0, 0, 0, 0.01f);
    private bool is_game_clear;             // ゲームクリアかどうか
    private bool is_game_over;              // ゲームオーバーかどうか

    void Start()
    {
        throw_timer = 0;    // タイマ初期化
        throw_interval = 10; // インターバルの初期化
        is_game_clear = false;
        is_game_over = false;   // ゲームオーバーフラグ
    }

    void Update()
    {
        throw_timer++;
        throw_timer %= throw_interval;

        /* THROW_INTERVAL毎に以下の処理を行う */
        if(throw_timer == 0 && !is_game_clear && !is_game_over) {
            if (mazai_text_object != null) {
                var text_object = createMazaiCode(mazai_text_object);
                prepareFire(text_object);
            }
        }

        if(is_game_clear) {
            stopCode();
            disappearCode();
        }else if(is_game_over) {
            GameObject[] codes = GameObject.FindGameObjectsWithTag("Code");
            DestroyAll(codes, 0);
        }

        /* キー入力処理 */
        //if (Input.GetKeyDown(KeyCode.Return)) {
            //delayFire();
        //}
    }

    /**
     * 魔剤コードのprefabから、新しい魔剤コードを生成する
     * @param _mazai_text_object 魔剤コードのprefab
     * @return 新規魔剤コード
     * */
    GameObject createMazaiCode(GameObject _mazai_text_object)
    {
        GameObject txt_obj = Instantiate(mazai_text_object);    // 魔剤コード生成

        int rnd = Random.Range(0, mazaiText.Length);
        txt_obj.GetComponent<TextMesh>().text = mazaiText[rnd]; // コードの内容をランダムに指定する
        txt_obj.GetComponent<TextMesh>().color = new Color(255, 255, 255);

        return txt_obj;
    }

    /**
     * 発射準備する
     * @param _text_object 魔剤コード
     * */
    void prepareFire(GameObject _text_object)
    {
        Vector3 vec = this.transform.position;               // ロボ太郎の位置をとる
        vec += new Vector3(3.0f, 1.5f+Random.value * 3, -0.4f);     // 魔剤コードの発射位置を調節
        _text_object.transform.position = vec;               // 発射位置を適用する

        _text_object.GetComponent<PlayScene_MoveCode>().setSpeed(mazai_code_speed);
    }

    /**
     * 発射速度を遅くする
     * */
    void delayFire()
    {
        mazai_code_speed = (mazai_code_speed > 0.05) 
            ? mazai_code_speed-0.001f : 0;    // 速度調節
        //Debug.Log(mazai_code_speed);
    }

    /**
     * すべての魔剤コードの速度を0にする
     * */
    void stopCode()
    {
        GameObject[] codes = GameObject.FindGameObjectsWithTag("Code");

        foreach (GameObject code in codes) {
            code.GetComponent<PlayScene_MoveCode>().setSpeed(0);
        }
    }

    /**
     * 魔剤コードを徐々に消していく
     * */
    void disappearCode()
    {
        GameObject[] codes = GameObject.FindGameObjectsWithTag("Code");

        DestroyAll(codes, 3);

        foreach (GameObject code in codes)
        {

            var render = code.GetComponent<MeshRenderer>();
            if(render.material.color.a > 0) {
                    render.material.color -= alpha;
            }

            ParticleSystem particle = code.GetComponent<ParticleSystem>();
            if(!particle.isPlaying) {
                particle.Play();
            }
        }

    }

    /**
     * 配列内のゲームオブジェクトをすべて破棄する
     * */
    void DestroyAll(GameObject[] game_objects, float t)
    {
        foreach (GameObject obj in game_objects)
        {
            Destroy(obj, t);
        }
    }


    public void setGameClearFlg(bool flg)
    {
        is_game_clear = flg;
    }

    public void setGameOverFlg(bool flg)
    {
        is_game_over = flg;
    }

    public bool isGameClear() {
        return is_game_clear;
    }

    public bool isGameOver() {
        return is_game_over;
    }
}