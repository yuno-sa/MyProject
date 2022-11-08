using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class ButtonScript : MonoBehaviour
{

    [SerializeField, Tooltip("ボタン")]
    //public GameObject button;
    private TextMeshProUGUI buttontext;
    private Vector3 initialPos;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        buttontext = transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        initialPos = new Vector3(transform.position.x,target.position.y-transform.position.y,transform.position.z);
        transform.position = new Vector3(100,100,100);
        //buttontext = button.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clear()
    {
        buttontext.text = "Clear";
    }
    public void ChangeText(string s)
    {
        if(transform.position == new Vector3(100,100,100))
        {
            transform.position = new Vector3(target.position.x,target.position.y+initialPos.y,target.position.z);
            transform.rotation = target.rotation;
            buttontext.text = s;
        }else{
            transform.position = new Vector3(100,100,100);
        }
    }
    public void OnClick()
    {
        //ここにシーン遷移を入れる？
        //ボタンの文字を参照。
        //ifで分岐、それぞれのタグだけ集めたシーンを作って移動するようにする
        //だといちいち書き換えないといけないから、シーンの名前をタグの名前と共通にして、シーンを名前から検索する
        /*
        SceneManager.LoadScene("MainScene");シーン遷移
        SceneManager.LoadScene(ボタンのテキストを取得して、そのまま入れる→遷移できるようにする);
        */
        //空のオブジェクトを作って、それぞれのタグの名前にする。そのオブジェクトを中心にして、円形に配置するようにする、とか
        //transform.position = new Vector3(100,100,100);
        GameObject[] items = GameObject.FindGameObjectsWithTag("All");
        GameObject[] targets = GameObject.FindGameObjectsWithTag(buttontext.ToString());
        buttontext.text = items.Count().ToString();
        /*for(int i = 0;i<items.Count();i++)
        {
            if(!targets.Contains(items[i]))
            {
                items[i].SetActive(false);
            }
        }*/
    }
    //もう一個メソッドを作って、Writetagsが呼ばれた時に、オブジェクトを初期化するコードを追加する
}
