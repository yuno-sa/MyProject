using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class MyDistanceGrabber : MonoBehaviour
{
    public enum HandType
    {
        LEFT,
        RIGHT
    }

    [SerializeField] private HandType _handType = HandType.LEFT;
    [SerializeField] private float _castCircleRadius = 0.2f;
    [SerializeField] private float _maxCastDistance = 3f;
    //[SerializeField, Range(0f, 1f)] private float _powerAdjust = 0.2f;
    //[SerializeField] private float _minReleasePower = 1f;
    //[SerializeField] private float _maxReleasePower = 100f;

    private MyGrabbable _targetGrabbable;
    private MyGrabbable _grabbedGrabbable;
    private bool _isMenu;

    void Start()
    {
        this.FixedUpdateAsObservable().Subscribe(_ =>
        {
            // 掴めるオブジェクトの判定
            DetectGrabbable();
        }).AddTo(this);

        // 右か左かによってボタン、コントローラーの切り分け
        OVRInput.RawButton buttonType = OVRInput.RawButton.LHandTrigger;
        OVRInput.RawButton buttonTrigger = OVRInput.RawButton.LIndexTrigger;
        OVRInput.RawButton buttonAX = OVRInput.RawButton.X;
        OVRInput.RawButton buttonBY = OVRInput.RawButton.Y;
        //OVRInput.Controller controller = OVRInput.Controller.LTouch;
        _isMenu = false;

        switch (_handType)
        {
            case HandType.LEFT:
                buttonType = OVRInput.RawButton.LHandTrigger;
                buttonTrigger = OVRInput.RawButton.LIndexTrigger;
                buttonAX = OVRInput.RawButton.X;
                buttonBY = OVRInput.RawButton.Y;
                //controller = OVRInput.Controller.LTouch;
                break;
            case HandType.RIGHT:
                buttonType = OVRInput.RawButton.RHandTrigger;
                buttonTrigger = OVRInput.RawButton.RIndexTrigger;
                buttonAX = OVRInput.RawButton.A;
                buttonBY = OVRInput.RawButton.B;
                //controller = OVRInput.Controller.RTouch;
                break;
        }

        this.UpdateAsObservable()
            .Where(_ => OVRInput.GetDown(buttonType))
            .Subscribe(_ =>
            {
                if (_targetGrabbable != null)
                {
                    _targetGrabbable.Grab(transform);
                    _grabbedGrabbable = _targetGrabbable;
                }
            }).AddTo(this);

        // 中指ボタンが離されたとき
        this.UpdateAsObservable()
            .Where(_ => OVRInput.GetUp(buttonType))
            .Subscribe(_ =>
            {
                if (_grabbedGrabbable != null)
                {
                    Vector3 dir = transform.forward;
                    _grabbedGrabbable.Release();
                    _grabbedGrabbable.TagsClear();
                    _grabbedGrabbable = null;
                    _isMenu = false;
                }
            }).AddTo(this);

        this.UpdateAsObservable()
            .Where(_ => OVRInput.GetDown(buttonTrigger))
            .Subscribe(_ =>
            {
                if (_grabbedGrabbable != null)
                {
                    if(!_isMenu)
                    {
                        _grabbedGrabbable.SeeTags();
                        _isMenu = true;
                    }
                    else
                    {
                        _grabbedGrabbable.TagsClear();
                        _isMenu = false;
                    }
                }
            }).AddTo(this);
    }

    /// <summary>
    /// 掴めるオブジェクトを検知する
    /// </summary>
    void DetectGrabbable()
    {
        RaycastHit hit;
        Vector3 p1 = transform.position;
        int layerMask = LayerMask.GetMask(new string[] {"Grabbable"});
        if (Physics.SphereCast(p1, _castCircleRadius, transform.forward, out hit, _maxCastDistance, layerMask))
        {
            var grabbable = hit.collider.GetComponent<MyGrabbable>();
            if (grabbable)
            {
                if (_targetGrabbable != null)
                {
                    _targetGrabbable.OutLineEnabled = false;
                }
                grabbable.OutLineEnabled = true;
                _targetGrabbable = grabbable;
            }
        }
        else
        {
            if (_targetGrabbable != null)
            {
                _targetGrabbable.OutLineEnabled = false;
                _targetGrabbable = null;
            }
        }
    }
}
