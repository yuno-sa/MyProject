using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ChangeButton : MonoBehaviour
{

    [SerializeField, Tooltip("ボタン")]
    Button button = null;
    TextMeshProUGUI buttontext = null;

    // Start is called before the first frame update
    void Start()
    {
        buttontext = transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        //buttontext = button.GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeText(string s)
    {
        //this.gameObject.SetActive(true);
        buttontext.text = s;
    }
}
