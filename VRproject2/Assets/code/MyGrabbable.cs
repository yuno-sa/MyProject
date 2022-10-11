using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

[RequireComponent(typeof(Outline))]
public class MyGrabbable : MonoBehaviour
{
    public bool IsGrabbed
    {
        get => _isGrabbed;
    }

    private Rigidbody _rigidbody;
    private Transform _defaultParent;
    private Outline _outline;
    private bool _isGrabbed = false;
    private Vector3 initialPos;
    private Quaternion initialRot;
    private CustomTag ct;
    private List<string> tags = new List<string>();

    public bool OutLineEnabled
    {
        get { return _outline.enabled; }
        set { _outline.enabled = value; }
    }

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }

    void Start()
    {
        _defaultParent = transform.parent;
        if (_rigidbody != null){
           _rigidbody.isKinematic = false;
           initialPos = transform.position;
           initialRot = transform.rotation;
        }

    }

    private IDisposable _grabDisposable;
    public void Grab(Transform handTrans)
    {
        if (_isGrabbed)
        {
            return;
        }

        if (_rigidbody != null)
        {
            _rigidbody.isKinematic = true;
        }
        _isGrabbed = true;
        _grabDisposable?.Dispose();
        _grabDisposable = this.UpdateAsObservable()
            // 手まで限りなく近くなるまで自分を近づける
            .TakeWhile(_ => (transform.position - handTrans.position).sqrMagnitude > 0.1f * 0.1f).Subscribe(
                _ =>
                {
                    // 手までオブジェクトを近づける
                    Vector3 dir = (handTrans.position - transform.position).normalized;
                    transform.position += dir * (10f * Time.deltaTime);
                }, () =>
                {
                    // 購読完了時
                    // 手まで限りなく近くなった場合、親を手にして完了
                    transform.parent = handTrans;
                    transform.position = handTrans.position;
                }).AddTo(this);
    }
    public void SeeTags()
    {
        if(!_isGrabbed)
        {
            return;
        }
        int i = 0;
        while(ct.GetAtIndex(i)!=null)
        {
            tags.Add(ct.GetAtIndex(i));
            i++;
        }
        //GameObject child = transform.GetChild(1).gameObject;
        //child.SetActive(true);
    }
    public void Release(Vector3 controllerAcc)
    {
        if (!_isGrabbed)
        {
            return;
        }
        if (_rigidbody != null)
        {
          _grabDisposable?.Dispose();
          _grabDisposable = this.UpdateAsObservable()
              .TakeWhile(_ => (transform.position - initialPos).sqrMagnitude > 0.1f * 0.1f).Subscribe(
                  _ =>
                  {
                      Vector3 dir = (initialPos - transform.position).normalized;
                      transform.position += dir * (10f * Time.deltaTime);
                  }, () =>
                  {
                      transform.parent = _defaultParent;
                      transform.position = initialPos;
                      transform.rotation = initialRot;

                  }).AddTo(this);
            _rigidbody.isKinematic = false;

        }
        _isGrabbed = false;
    }
}
