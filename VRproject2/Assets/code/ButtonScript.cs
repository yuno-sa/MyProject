using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System.Linq;

public class ButtonScript : MonoBehaviour
{

    [SerializeField, Tooltip("ボタン")]
    private TextMeshProUGUI buttontext;
    private Vector3 initialPos;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        buttontext = transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        initialPos = new Vector3(transform.position.x,target.position.y-transform.position.y,transform.position.z);
        transform.position = new Vector3(10,10,10);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Clear()
    {
        transform.position = new Vector3(10,10,10);
    }
    public void ChangeText(string s)
    {
        transform.position = new Vector3(target.position.x,target.position.y+initialPos.y,target.position.z);
        transform.rotation = target.rotation;
        buttontext.text = s;
    }
    public void OnClick()
    {
        buttontext = transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>();
        int i = 0;
        foreach (var potentialTarget in FindObjectsOfType<CustomTag>())
        {
            potentialTarget.gameObject.GetComponent<MyGrabbable>().Release();
            if (potentialTarget.HasTag(buttontext.text))
            {
                if(i>=5)
                {
                    potentialTarget.gameObject.transform.position = new Vector3((float)(-0.8+(i-5)*0.3),(float)1.6,(float)2.3);
                    i++;
                }
                else
                {
                    potentialTarget.gameObject.transform.position = new Vector3((float)(-0.8+i*0.3),(float)1.6,(float)2.5);
                    i++;
                }
            }
            else
            {
                potentialTarget.gameObject.transform.position = new Vector3(10,10,10);
            }
        }
        i = 0;
    }
}
