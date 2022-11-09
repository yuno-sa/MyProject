using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class WriteTags : MonoBehaviour
{
    private List<string> tags;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Writetags(IEnumerable<string> t)
    {
        var i = 0;
        foreach(var tag in t){
          transform.GetChild(i).gameObject.GetComponent<ButtonScript>().ChangeText(tag);
          i++;
        }
    }
    public void ClearCanvas()
    {
        for(int i = 0;i<5;i++)
        {
            transform.GetChild(i).gameObject.GetComponent<ButtonScript>().Clear();
        }
    }
}
