using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateFunctionController : MonoBehaviour
{

    private void Start()
    {
        db = new DBManager();
        com = GameObject.FindWithTag("common").GetComponent<common>();
        //データの取得
        upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);
        Debug.Log(upd);
        //データ取得不可なら作成
        if(upd == null)
        {
            upd = new DBManager.UpgradedData();
            db.setUpgradeData(upd);
        }
        loadUpgradeData();
    }

    DBManager db;
    DBManager.UpgradedData upd;
    common com;





    //ゲームデータロード
    void loadUpgradeData()
    {

        com.frier.transform.position = new Vector3(upd.frierPositionX, 0.1f, upd.frierPositionZ);
        com.friedStation.transform.position = new Vector3(upd.friedStationPositionX, 0.1f, upd.friedStationPositionZ);
        com.reizouko.transform.position = new Vector3(upd.reizoukoPositionX, 0.1f, upd.reizoukoPositionZ);
        com.bake.transform.position = new Vector3(upd.bakePositionX, 0.1f, upd.bakePositionZ);
        com.frier2.SetActive(upd.frier2);
        com.frier2.transform.position = new Vector3(upd.frier2PositionX, 0.1f, upd.frier2PositionZ);
        com.friedStation2.SetActive(upd.friedStation2);
        com.friedStation2.transform.position = new Vector3(upd.friedStation2PositionX, 0.1f, upd.friedStation2PositionZ);
        com.gachaView.SetActive(upd.gachaView);
        com.employeeView.SetActive(upd.employeeView);
        com.dismissalView.SetActive(upd.dismissalView);
        com.upgrade1.SetActive(upd.upgrade1);
        com.destroy1.SetActive(upd.destroy1);
        com.upgrade1wall.SetActive(upd.upgrade1wall);
        com.upgrade2.SetActive(upd.upgrade2);
        com.destroy2.SetActive(upd.destroy2);
        com.upgrade2wall.SetActive(upd.upgrade2wall);
        com.upgrade3.SetActive(upd.upgrade3);
        com.destroy3.SetActive(upd.destroy3);
        com.upgrade3wall.SetActive(upd.upgrade3wall);
        com.upgrade4.SetActive(upd.upgrade4);
        com.upgrade5.SetActive(upd.upgrade5);
        com.upgrade6.SetActive(upd.upgrade6);
        com.salesView.SetActive(upd.salesView);
        com.driveThrow.SetActive(upd.driveThrow);
        com.wall1.SetActive(upd.wall1);
        com.wall2.SetActive(upd.wall2);
        com.wall3.SetActive(upd.wall3);
        com.wall4.SetActive(upd.wall4);
        com.wall5.SetActive(upd.wall5);
        com.wall6.SetActive(upd.wall6);
        com.wall7.SetActive(upd.wall7);
        com.hamcon.setSpecifyCanCreateHamburgNum(upd.pateMaxNum);
        com.potatoController.setSpecifyCanCreatePotatoNum(upd.potatoMaxNum);
        com.donucon.setSpecifyCanCreateDonutNum(upd.donutMaxNum);
        com.onicon.setSpecifyCanCreateOnionNum(upd.onionMaxNum);
        com.chiccon.setSpecifyCanCreateChickenNum(upd.chickenMaxNum);
        com.ribcon.setSpecifyCanCreateRibNum(upd.ribMaxNum);
        com.bchicon.setSpecifyCanCreateBigChickenNum(upd.bigChickenMaxNum);
        com.traySelectButton.SetActive(upd.traySelectButton);
        com.trayStrage2.SetActive(upd.trayStrage2);
        com.successButton2.SetActive(upd.successButton2);
        com.trayStrage3.SetActive(upd.trayStrage3);
        com.successButton3.SetActive(upd.successButton3);
        com.parking1.SetActive(upd.parking1);
        com.parking2.SetActive(upd.parking2);
        com.parking3.SetActive(upd.parking3);
        com.parking4.SetActive(upd.parking4);
        com.parking5.SetActive(upd.parking5);
        com.parking6.SetActive(upd.parking6);
        com.parking7.SetActive(upd.parking7);
        com.parking8.SetActive(upd.parking8);
        com.parking9.SetActive(upd.parking9);
        com.parking10.SetActive(upd.parking10);
        com.parking11.SetActive(upd.parking11);
        com.parking12.SetActive(upd.parking12);
        com.parking13.SetActive(upd.parking13);
        com.parking14.SetActive(upd.parking14);
        com.upgradeTerrace1.SetActive(upd.upgradeTerrace1);
        com.upgradeTerrace2.SetActive(upd.upgradeTerrace2);
        com.destroyTerrace1.SetActive(upd.destroyTerrace1);
        com.destroyTerrace2.SetActive(upd.destroyTerrace2);
        com.terrace3.SetActive(upd.terrace3);
        com.terrace4.SetActive(upd.terrace4);
        com.terrace5.SetActive(upd.terrace5);
        com.terrace6.SetActive(upd.terrace6);
        com.terrace7.SetActive(upd.terrace7);
        com.terrace8.SetActive(upd.terrace8);
        com.terrace9.SetActive(upd.terrace9);
        com.terrace10.SetActive(upd.terrace10);
        com.terrace11.SetActive(upd.terrace11);
        com.terrace12.SetActive(upd.terrace12);
        com.terrace13.SetActive(upd.terrace13);
        com.donutButton.SetActive(upd.donutButton);
        com.onionButton.SetActive(upd.onionButton);
        com.chickenButton.SetActive(upd.chickenButton);
        com.ribButton.SetActive(upd.ribButton);
        com.bigChickenButton.SetActive(upd.bigChickenButton);
        com.freezHamburgButton.SetActive(upd.freezHamburgButton);
        com.freezPotatoButton.SetActive(upd.freezPotatoButton);
        com.freezDonutButton.SetActive(upd.freezDonutButton);
        com.freezOnionButton.SetActive(upd.freezOnionButton);
        com.freezChickenButton.SetActive(upd.freezChickenButton);
        com.freezRibButton.SetActive(upd.freezRibButton);
        com.freezBigChickenButton.SetActive(upd.freezBigChickenButton);

    }



    public void upgradeFunction(string updateName)
    {
       

        switch (updateName)
        {
            case "駐車場1":
                com.parking1.SetActive(true);
                getUpgradeData();
                upd.parking1 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場2":
                com.parking2.SetActive(true);
                getUpgradeData();
                upd.parking2 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場3":
                com.parking3.SetActive(true);
                getUpgradeData();
                upd.parking3 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場4":
                com.parking4.SetActive(true);
                getUpgradeData();
                upd.parking4 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場5":
                com.parking5.SetActive(true);
                getUpgradeData();
                upd.parking5 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場6":
                com.parking6.SetActive(true);
                getUpgradeData();
                upd.parking6 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場7":
                com.parking7.SetActive(true);
                getUpgradeData();
                upd.parking7 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場8":
                com.parking8.SetActive(true);
                getUpgradeData();
                upd.parking8 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場9":
                com.parking9.SetActive(true);
                getUpgradeData();
                upd.parking9 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場10":
                com.parking10.SetActive(true);
                getUpgradeData();
                upd.parking10 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場11":
                com.parking11.SetActive(true);
                getUpgradeData();
                upd.parking11 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場12":
                com.parking12.SetActive(true);
                getUpgradeData();
                upd.parking12 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場13":
                com.parking13.SetActive(true);
                getUpgradeData();
                upd.parking13 = true;
                db.setUpgradeData(upd);
                break;

            case "駐車場14":
                com.parking14.SetActive(true);
                getUpgradeData();
                upd.parking14 = true;
                db.setUpgradeData(upd);
                break;

            case "トレー置き場2":
                com.trayStrage2.SetActive(true);
                com.traySelectButton.SetActive(true);
                com.traySelectText.text = "①完成";
                getUpgradeData();
                upd.trayStrage2 = true;
                upd.traySelectButton = true;
                db.setUpgradeData(upd);
                break;

            case "トレー置き場3":
                com.trayStrage3.SetActive(true);
                getUpgradeData();
                upd.trayStrage3 = true;
                db.setUpgradeData(upd);
                break;

            case "フライヤー2":
                //フライヤー表示
                com.frier2.SetActive(true);
                getUpgradeData();
                upd.frier2 = true;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "ポテト":
                //ポテトメニュー追加
                db.setMenu(AppConst.JOB_POTATO);
                break;

            case "フライヤーステーション2":
                //フライヤーステーション表示
                com.friedStation2.SetActive(true);
                getUpgradeData();
                upd.friedStation2 = true;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "オニオンリング":
                //オニオンリングメニュー追加
                db.setMenu(AppConst.JOB_ONIONRING);
                com.onionButton.SetActive(true);
                getUpgradeData();
                upd.onionButton = true;
                db.setUpgradeData(upd);
                break;

            case "店舗拡張1":
                //店舗拡張
                com.upgrade1.SetActive(true);
                com.upgrade1wall.SetActive(true);
                com.wall1.SetActive(true);
                com.wall2.SetActive(true);
                com.destroy1.SetActive(false);
                com.frier.transform.position = new Vector3(-17.71f, 0.1f, -37.6f);
                com.friedStation.transform.position = new Vector3(-17.56f, 0.1f, -35.28f);
                getUpgradeData();
                upd.upgrade1 = true;
                upd.destroy1 = false;
                upd.upgrade1wall = true;
                upd.wall1 = true;
                upd.wall2 = true;
                upd.frierPositionX = -17.71f;
                upd.frierPositionZ = -37.6f;
                upd.friedStationPositionX = -17.56f;
                upd.friedStationPositionZ = -35.28f;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "雇用":
                //雇用機能追加
                com.employeeView.SetActive(true);
                com.dismissalView.SetActive(true);
                getUpgradeData();
                upd.employeeView = true;
                upd.dismissalView = true;
                db.setUpgradeData(upd);
                break;

            case "雇用最大人数+1":
                //雇用最大人数+1
                getUpgradeData();
                upd.employeeMax = upd.employeeMax + 1;
                db.setUpgradeData(upd);
                break;

            case "ドーナツ":
                //ドーナツ
                db.setMenu(AppConst.JOB_DONUT);
                com.donutButton.SetActive(true);
                getUpgradeData();
                upd.donutButton = true;
                db.setUpgradeData(upd);
                break;

            case "フライドチキン":
                //フライドチキン
                db.setMenu(AppConst.JOB_FRIEDCHICKEN);
                com.chickenButton.SetActive(true);
                getUpgradeData();
                upd.chickenButton = true;
                db.setUpgradeData(upd);
                break;

            case "店舗拡張2":
                //店舗拡張
                com.upgrade2.SetActive(true);
                com.destroy2.SetActive(false);
                com.wall2.SetActive(false);
                com.wall6.SetActive(true);
                com.reizouko.transform.position = new Vector3(-13.769f, 0f, -40.342f);
                getUpgradeData();
                upd.upgrade2 = true;
                upd.destroy2 = false;
                upd.wall2 = false;
                upd.wall6 = true;
                upd.reizoukoPositionX = -13.769f;
                upd.reizoukoPositionZ = -40.342f;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "店舗拡張3":
                com.upgrade3.SetActive(true);
                com.destroy3.SetActive(false);
                com.wall1.SetActive(false);
                com.wall5.SetActive(true);
                com.bake.transform.position = new Vector3(-14.2f,0.1f,-30.9f);
                getUpgradeData();
                upd.upgrade3 = true;
                upd.destroy3 = false;
                upd.wall1 = false;
                upd.wall5 = true;
                upd.bakePositionX = -14.2f;
                upd.bakePositionZ = -30.9f;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "店舗拡張4":
                com.upgrade4.SetActive(true);
                com.upgrade1wall.SetActive(false);
                com.wall3.SetActive(true);
                com.wall4.SetActive(true);
                com.wall5.SetActive(false);
                com.wall6.SetActive(false);
                com.frier.transform.position = new Vector3(-18.91f, 0.1f, -37.6f);
                com.friedStation.transform.position = new Vector3(-18.83f, 0.1f, -35.28f);
                com.frier2.transform.position = new Vector3(-18.91f, 0.1f, -39.28f);
                com.friedStation2.transform.position = new Vector3(-18.82f, 0.1f, -32.91f);
                getUpgradeData();
                upd.upgrade4 = true;
                upd.upgrade1wall = false;
                upd.wall3 = true;
                upd.wall4 = true;
                upd.wall5 = false;
                upd.wall6 = false;
                upd.frierPositionX = -18.91f;
                upd.frierPositionZ = -37.6f;
                upd.friedStationPositionX = -18.83f;
                upd.friedStationPositionZ = -35.28f;
                upd.frier2PositionX = -18.91f;
                upd.frier2PositionZ = -39.28f;
                upd.friedStation2PositionX = -18.82f;
                upd.friedStation2PositionZ = -32.91f;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "店舗拡張5":
                com.upgrade5.SetActive(true);
                com.upgrade2wall.SetActive(false);
                com.wall7.SetActive(false);
                com.wall4.SetActive(false);
                com.reizouko.transform.position = new Vector3(-13.769f, 0f, -41.64f);

                getUpgradeData();
                upd.upgrade5 = true;
                upd.upgrade2wall = false;
                upd.wall7 = false;
                upd.wall4 = false;
                upd.reizoukoPositionX = -13.769f;
                upd.reizoukoPositionZ = -41.64f;
                db.setUpgradeData(upd);
                updateObjectTransform();

                break;

            case "店舗拡張6":
                com.upgrade6.SetActive(true);
                com.upgrade3.SetActive(false);
                com.wall3.SetActive(false);
                com.bake.transform.position = new Vector3(-14.12f, 0.1f, -29.583f);
                getUpgradeData();
                upd.upgrade6 = true;
                upd.upgrade3wall = false;
                upd.wall3 = false;
                upd.bakePositionX = -14.12f;
                upd.bakePositionZ = -29.583f;
                db.setUpgradeData(upd);
                updateObjectTransform();
                break;

            case "営業":
                //営業機能
                com.salesView.SetActive(true);
                getUpgradeData();
                upd.salesView = true;
                db.setUpgradeData(upd);
                break;

            case "チラシ営業":
                //チラシ営業
                db.setSalesListData("チラシ営業", 50000, 40);
                break;

            case "サラダ":
                //サラダ
                db.setMenu(AppConst.JOB_SALAD);
                break;

            case "鳥の丸揚げ":
                db.setMenu(AppConst.JOB_BIGFRIEDCHICKEN);
                com.bigChickenButton.SetActive(true);
                getUpgradeData();
                upd.bigChickenButton = true;
                db.setUpgradeData(upd);
                break;

            case "リブ":
                db.setMenu(AppConst.JOB_RIB);
                com.ribButton.SetActive(true);
                getUpgradeData();
                upd.ribButton = true;
                db.setUpgradeData(upd);
                break;

            case "ラジオ営業":
                //ラジオ営業
                db.setSalesListData("ラジオ営業", 100000, 20);
                break;

            case "テレビ営業":
                //テレビ営業
                db.setSalesListData("テレビ営業", 200000, 0);
                break;

            case "ドライブスルー":
                //ドライブスルー
                com.driveThrow.SetActive(true);
                getUpgradeData();
                upd.driveThrow = true;
                db.setUpgradeData(upd);
                break;

            case "パティ作成数+1":
                //パティ作成数+1
                com.hamcon.setCanCreateHamburgNum();
                getUpgradeData();
                upd.pateMaxNum = upd.pateMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "ポテト作成数+1":
                //ポテト作成数+1
                com.potatoController.setCanCreatePotatoNum();
                getUpgradeData();
                upd.potatoMaxNum = upd.potatoMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "オニオンリング作成数+1":
                com.onicon.setCanCreateOnionNum();
                getUpgradeData();
                upd.onionMaxNum = upd.onionMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "ドーナツ作成数+1":
                com.donucon.setCanCreateDonutNum();
                getUpgradeData();
                upd.donutMaxNum = upd.donutMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "フライドチキン作成数+1":
                com.chiccon.setCanCreateChickenNum();
                getUpgradeData();
                upd.chickenMaxNum = upd.chickenMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "鳥の丸揚げ作成数+1":
                com.bchicon.setCanCreateBigChickenNum();
                getUpgradeData();
                upd.bigChickenMaxNum = upd.bigChickenMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "リブ作成数+1":
                com.ribcon.setCanCreateRibNum();
                getUpgradeData();
                upd.ribMaxNum = upd.ribMaxNum + 1;
                db.setUpgradeData(upd);
                break;

            case "ガチャ":
                //ガチャ機能
                com.gachaView.SetActive(true);
                getUpgradeData();
                upd.gachaView = true;
                db.setUpgradeData(upd);
                break;

            case "飲食席拡張1":
                com.upgradeTerrace1.SetActive(true);
                com.destroyTerrace1.SetActive(false);
                com.destroyTerrace2.SetActive(true);
                getUpgradeData();
                upd.upgradeTerrace1 = true;
                upd.destroyTerrace1 = false;
                upd.destroyTerrace2 = true;
                db.setUpgradeData(upd);
                break;

            case "飲食席拡張2":
                com.upgradeTerrace2.SetActive(true);
                com.destroyTerrace2.SetActive(false);
                getUpgradeData();
                upd.upgradeTerrace2 = true;
                upd.destroyTerrace2 = false;
                db.setUpgradeData(upd);
                break;

            case "席拡張1":
                com.terrace3.SetActive(true);
                getUpgradeData();
                upd.terrace3 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張2":
                com.terrace4.SetActive(true);
                getUpgradeData();
                upd.terrace4 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張3":
                com.terrace5.SetActive(true);
                getUpgradeData();
                upd.terrace5 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張4":
                com.terrace6.SetActive(true);
                getUpgradeData();
                upd.terrace6 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張5":
                com.terrace7.SetActive(true);
                getUpgradeData();
                upd.terrace7 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張6":
                com.terrace8.SetActive(true);
                getUpgradeData();
                upd.terrace8 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張7":
                com.terrace9.SetActive(true);
                getUpgradeData();
                upd.terrace9 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張8":
                com.terrace10.SetActive(true);
                getUpgradeData();
                upd.terrace10 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張9":
                com.terrace11.SetActive(true);
                getUpgradeData();
                upd.terrace11 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張10":
                com.terrace12.SetActive(true);
                getUpgradeData();
                upd.terrace12 = true;
                db.setUpgradeData(upd);
                break;

            case "席拡張11":
                com.terrace13.SetActive(true);
                getUpgradeData();
                upd.terrace13 = true;
                db.setUpgradeData(upd);
                break;

            case "パティ準備作業":
                com.freezHamburgButton.SetActive(true);
                getUpgradeData();
                upd.freezHamburgButton = true;
                db.setUpgradeData(upd);
                break;

            case "ポテト準備作業":
                com.freezPotatoButton.SetActive(true);
                getUpgradeData();
                upd.freezPotatoButton = true;
                db.setUpgradeData(upd);
                break;

            case "ドーナツ準備作業":
                com.freezDonutButton.SetActive(true);
                getUpgradeData();
                upd.freezHamburgButton = true;
                db.setUpgradeData(upd);
                break;

            case "オニオンリング準備作業":
                com.freezOnionButton.SetActive(true);
                getUpgradeData();
                upd.freezOnionButton = true;
                db.setUpgradeData(upd);
                break;

            case "フライドチキン準備作業":
                com.freezChickenButton.SetActive(true);
                getUpgradeData();
                upd.freezChickenButton = true;
                db.setUpgradeData(upd);
                break;

            case "リブ準備作業":
                com.freezRibButton.SetActive(true);
                getUpgradeData();
                upd.freezRibButton = true;
                db.setUpgradeData(upd);
                break;

            case "ビッグフライドチキン準備作業":
                com.freezBigChickenButton.SetActive(true);
                getUpgradeData();
                upd.freezBigChickenButton = true;
                db.setUpgradeData(upd);
                break;
        }
    }

    //店舗拡張に伴い位置情報を更新
    void updateObjectTransform()
    {
        com.bakePosition = GameObject.FindWithTag("bakePoint").transform;
        com.reizoukoPosition = GameObject.FindWithTag("reizoukoPoint").transform;
        try
        {
            com.frier1Position = GameObject.FindWithTag("frier1Point").transform;
        }
        catch { }
        try
        {
            com.friedStation1Position = GameObject.FindWithTag("friedStation1Point").transform;
        }
        catch { }
        try
        {
            com.frier2Position = GameObject.FindWithTag("frier2Point").transform;
        }
        catch { }
        try
        {
            com.friedStation2Position = GameObject.FindWithTag("friedStation2Point").transform;
        }
        catch { }
        
    }


    //データベースから最新データを取得
    void getUpgradeData()
    {
        upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);
    }
}
