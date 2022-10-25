using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteTags : MonoBehaviour
{
    private List<string> tags = new List<string>();
    private TextMeshPro targetText;
    public GameObject gameobject;
    private string s;

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
        this.tags = Tags;
        //ここにテキスト追加コード
        this.s = null;
        targetText = gameobject.GetComponent<TextMeshPro>();
        for(int i = 0;i<this.tags.Count;i++)
        {
            this.s += this.tags[i]+"\n";
        }
        targetText.text = s;
    }
}
