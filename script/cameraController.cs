using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    [SerializeField]
    private ScreenInput _input;
    private float Speed = 0.007f;
    private bool MoveFlg;
    private Vector3 pos;
    private Camera _camera;
    private float StartFieldOfView;
    bool cameraControllerToggle = true;

    private void Start()
    {
        _input.SetPinchLimits(-20, 20, 0.4f);
        _camera = this.transform.GetComponent<Camera>();
        StartFieldOfView = _camera.fieldOfView;
    }

    // カメラ位置の更新
    void Update()
    {
        if (!cameraControllerToggle) return;
        //Debug.Log("camera trueです。" + cameraControllerToggle.ToString());

        if (_input.GetNowSwipe() != ScreenInput.SwipeDirection.NONE && !_input.GetPinchFlg())
        {
            if (!MoveFlg) pos = this.transform.localPosition;
            MoveFlg = true;
            float inputx = pos.x + _input.GetSwipeRangeVec().y * Speed;
            float inputz = pos.z - _input.GetSwipeRangeVec().x * Speed;
            float inputy = pos.y;

            if (inputz < -42)
            {
                inputz = -42;
            }
            if(inputz > -25)
            {
                inputz = -25;
            }
            if(inputx < -14)
            {
                inputx = -14;
            }
            if(inputx > 23)
            {
                inputx = 23;
            }
            if(inputy > 20)
            {
                inputy = 20;
            }

            if(inputy < 1)
            {
                inputy = 1;
            }
            
            

            this.transform.localPosition = new Vector3(
                inputx,
                inputy,
                inputz);
            
        }
        else
        {
            MoveFlg = false;
        }

        if (_input.GetPinchFlg())
        {
            _camera.fieldOfView = StartFieldOfView - _input.GetPinchDistance();
        }

    }

    public void cameraOn()
    {
        cameraControllerToggle = true;
        _input.on();
    }

    public void cameraOff()
    {
        cameraControllerToggle = false;
        _input.off();
    }
}
