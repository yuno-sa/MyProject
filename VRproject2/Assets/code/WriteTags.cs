using System;
using System.Collections;
using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class WriteTags : MonoBehaviour
{
    private List<string> tags = new List<string>();
    //private TextMeshPro targetText;
    //public GameObject gameobject;
    private GameObject child;
    private List<GameObject> childs;
    private ChangeButton cb;
    //private string s;
    //private List<GameObject> Buttons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void writetags(List<string> Tags)
    {
        childs = new List<GameObject>();
        tags = Tags;
        //child = transform.GetChild(0).gameObject;
        //ここがおかしい↓
        //child.GetComponent<ChangeButton>().ChangeText(this.tags[0]);
        //this.child.SetActive(false);
        //ここにテキスト追加コード
        //this.s = null;
        //targetText.text = "";
        for(int i = 0;i<tags.Count;i++)
        {
            this.childs.Add(transform.GetChild(i).gameObject);
        }
        for(int i = 0;i<tags.Count;i++)
        {
            this.childs[i].GetComponent<ChangeButton>().ChangeText(tags[i]);
            this.childs[i].gameObject.SetActive(true);
        }
        //this.childs[i].GetComponent<ChangeButton>().ChangeText(this.tags[i]);
        //this.childs[1].gameObject.SetActive();
        /*for(int i = 0;i<this.tags.Count;i++)
        {
            this.child = transform.GetChild(i).gameObject;
            //this.child.gameObject.SetActive(true);
            this.child.GetComponent<ChangeButton>().ChangeText(this.tags[i]);
        }*/
        /*targetText = transform.Find("Text").gameObject.GetComponent<TextMeshPro>();
        for(int i = 0;i<this.tags.Count;i++)
        {
            this.s += this.tags[i]+"\n";
        }
        targetText.text = s;*/
    }
}
