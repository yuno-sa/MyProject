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
    //private GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        buttontext = transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        initialPos = new Vector3(transform.position.x,target.position.y-transform.position.y,transform.position.z);
        transform.position = new Vector3(100,100,100);
        //items = GameObject.FindGameObjectsWithTag("All");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clear()
    {
        transform.position = new Vector3(100,100,100);
    }
    public void ChangeText(string s)
    {
        transform.position = new Vector3(target.position.x,target.position.y+initialPos.y,target.position.z);
        transform.rotation = target.rotation;
        buttontext.text = s;
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
        /*GameObject[] goArray = GameObject.FindObjectsOfType<GameObject>();
        for(int i = 0;i<goArray.Count;i++)
        {
            if(!goArray[i].HasTag(buttontext.ToString()))
            {
                goArray[i].SetActive(false);
            }
        }*/
        GameObject cube = GameObject.Find("Cube");
        cube.SetActive(false);
        //GameObject[] targets = GameObject.FindGameObjectsWithTag(buttontext.ToString());
        //buttontext.text = items.Count().ToString();
    }
}
