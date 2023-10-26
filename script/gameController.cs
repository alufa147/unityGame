using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    DBManager db;


    // Start is called before the first frame update
    void Start()
    {
        db = new DBManager();
        Application.targetFrameRate = AppConst.FRAME_LATE;
        

        //初回起動時の処理
        DBManager.StartUp su = (DBManager.StartUp)db.getData(AppConst.FIRST_STARTFLAG_FILE_NAME);
        if(su == null)
        {

            //スタートアップフラグＯＮ
            db.setStartupFlag();

        }
    }





}
