using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objectMarker : MonoBehaviour
{
    //表示対象オブジェクト
    public Transform target;

    //表示するUI
    public GameObject targetUi;

    //オブジェクト位置のオフセット
    public Vector3 offset;

    private RectTransform parentUi;

    public void initialize(Transform target) 
    {

        this.target = target;

    }

    // Start is called before the first frame update
    void Start()
    {
        parentUi = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        var targetUiTran = targetUi.transform;
        var cameraTransform = Camera.main.transform;

        //カメラの向きベクトル
        var cameraDir = cameraTransform.forward;

        //オブジェクトの位置
        var targetWorldPos = target.position + offset;

        //カメラからターゲットへのベクトル
        var targetDir = targetWorldPos - cameraTransform.position;

        //カメラ前方かどうかを判定
        var isFront = Vector3.Dot(cameraDir, targetDir) > 0;


        //カメラ位置による表示有無
        //targetUi.SetActive(isFront);
        //if (!isFront) return;

        //オブジェクトのワールド座標　→　スクリーン座標
        var targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);

        //スクリーン座標変換　→　UIローカル座標変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentUi,
            targetScreenPos,
            null,
            out var uiLocalPos
            );

        if (uiLocalPos == new Vector2(0, 0)) return;

        //RectTransformのローカル座標を更新
        targetUiTran.localPosition = uiLocalPos;
    }
}
