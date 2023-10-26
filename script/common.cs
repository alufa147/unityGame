using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class common : MonoBehaviour
{
    //各場所の位置情報
    public Transform rejiPosition;
    public Transform drinkPosition;
    public Transform bakePosition;
    public Transform reizoukoPosition;
    public Transform friedStation1Position;
    public Transform counterPosition;
    public Transform frier1Position;
    public Transform frier2Position;
    public Transform friedStation2Position;

    //生成するためのオブジェクト
    
    public GameObject fullCupObject;
    public GameObject karaCupObject;
    public GameObject closedCupObject;
    public GameObject boxInPotato;
    public GameObject hamburger;
    public GameObject donut;
    public GameObject onion;
    public GameObject cupInOnion;
    public GameObject friedChicken;
    public GameObject rib;
    public GameObject bigFriedChicken;
    public GameObject salad;

    //ネットインオブジェクト
    public GameObject netInPoteto;
    public GameObject netInDonut;
    public GameObject netInOnion;
    public GameObject netInChicken;
    public GameObject netInRib;
    public GameObject netInBigChicken;

    //ドア
    public GameObject door;

    /// <summary>
    /// 注文コントローラー
    /// </summary>
    public chumonController chumoncontroller;

    /// <summary>
    /// トレーコントローラー
    /// </summary>
    public trayController trayController;

    /// <summary>
    /// ポテトコントローラー
    /// </summary>
    public PotatoController potatoController;

    /// <summary>
    /// ドーナツコントローラー
    /// </summary>
    public donutController donucon;

    /// <summary>
    /// ハンバーグコントローラー
    /// </summary>
    public hamburgController hamcon;

    /// <summary>
    /// ロケーションコントローラー
    /// </summary>
    public locationController locacon;

    /// <summary>
    /// オニオンコントローラー
    /// </summary>
    public onionController onicon;

    /// <summary>
    /// チキンコントローラー
    /// </summary>
    public chickenController chiccon;

    /// <summary>
    /// リブコントローラー
    /// </summary>
    public ribController ribcon;

    /// <summary>
    /// ビッグチキンコントローラー
    /// </summary>
    public bigChickenController bchicon;

    /// <summary>
    /// 人型オブジェクトコントローラー
    /// </summary>
    public humanObjectController hoc;

    /// <summary>
    /// viewコントローラー
    /// </summary>
    public viewController vc;

    /// <summary>
    /// 業績などの管理コントローラー
    /// </summary>
    public profitController procon;

    /// <summary>
    /// 時間コントローラー
    /// </summary>
    public timeController ticon;

    /// <summary>
    /// 納品場所コントローラー
    /// </summary>
    public counterController counterController;

    /// <summary>
    /// 店員ジョブ表示コントローラー
    /// </summary>
    public salesPersonViewController salesPersonController;

    /// <summary>
    /// 注文リスト表示用コントローラー
    /// </summary>
    public orderViewController orderViewController;

    /// <summary>
    /// ダイアログ生成用コントローラー
    /// </summary>
    public dialogController dialogController;

    /// <summary>
    /// ハート管理コントローラー
    /// </summary>
    public heartController heartController;

    /// <summary>
    /// アップグレードダイアログコントローラー
    /// </summary>
    public upgradeDialogController upgradeDialogController;

    public int instantiateCarCustomerMaxInterval = 70;
    public int instantiateCarCustomerMinInterval = 40;
    public int instantiateWalkCustomerMaxInterval = 70;
    public int instantiateWalkCustomerMinInterval = 40;

    //フライヤー
    public GameObject frier;

    //フライヤーステーション
    public GameObject friedStation;

    //フライヤー2
    public GameObject frier2;

    public GameObject friedStation2;

    //冷蔵庫
    public GameObject reizouko;

    //コンロ
    public GameObject bake;

    //店舗アップグレード１
    public GameObject upgrade1;

    //店舗アップグレード１に伴う削除オブジェクト
    public GameObject destroy1;

    //店舗アップグレード１の壁
    public GameObject upgrade1wall;

    //ガチャ機能UI
    public GameObject gachaView;

    //雇用機能UI
    public GameObject employeeView;

    //解雇機能
    public GameObject dismissalView;

    //店舗アップグレード２
    public GameObject upgrade2;

    //店舗アップグレードに伴う削除オブジェクト
    public GameObject destroy2;

    //店舗アップグレード２の壁
    public GameObject upgrade2wall;

    //店舗アップグレード3
    public GameObject upgrade3;

    //店舗アップグレード3に伴う削除オブジェクト
    public GameObject destroy3;

    //店舗アップグレード３の壁
    public GameObject upgrade3wall;

    //店舗アップグレード4
    public GameObject upgrade4;


    //店舗アップグレード5
    public GameObject upgrade5;

    //店舗アップグレード6
    public GameObject upgrade6;

    //営業機能UI
    public GameObject salesView;

    //ドライブスルー
    public GameObject driveThrow;

    //1段階右壁
    public GameObject wall1;

    //1段階左壁
    public GameObject wall2;

    //2段階右壁
    public GameObject wall3;

    //2段階左壁
    public GameObject wall4;

    //2段階右上壁
    public GameObject wall5;

    //2段階左上壁
    public GameObject wall6;

    //左下壁
    public GameObject wall7;

    //トレー置き場1
    public GameObject trayStrage1;

    //トレー置き場2
    public Text traySelectText;
    public GameObject traySelectButton;
    public GameObject trayStrage2;
    public GameObject successButton2;

    //トレー置き場3
    public GameObject trayStrage3;
    public GameObject successButton3;

    //駐車場
    public GameObject defaultParking1;
    public GameObject defaultParking2;
    public GameObject defaultParking3;
    public GameObject parking1;
    public GameObject parking2;
    public GameObject parking3;
    public GameObject parking4;
    public GameObject parking5;
    public GameObject parking6;
    public GameObject parking7;
    public GameObject parking8;
    public GameObject parking9;
    public GameObject parking10;
    public GameObject parking11;
    public GameObject parking12;
    public GameObject parking13;
    public GameObject parking14;

    //テラス側店舗拡張
    public GameObject upgradeTerrace1;
    public GameObject upgradeTerrace2;
    public GameObject destroyTerrace1;
    public GameObject destroyTerrace2;

    //テラス席拡張
    public GameObject terrace3;
    public GameObject terrace4;
    public GameObject terrace5;
    public GameObject terrace6;
    public GameObject terrace7;
    public GameObject terrace8;
    public GameObject terrace9;
    public GameObject terrace10;
    public GameObject terrace11;
    public GameObject terrace12;
    public GameObject terrace13;

    // Get系指示ボタン
    public GameObject donutButton;
    public GameObject onionButton;
    public GameObject chickenButton;
    public GameObject ribButton;
    public GameObject bigChickenButton;

    // 準備ボタン
    public GameObject freezHamburgButton;
    public GameObject freezPotatoButton;
    public GameObject freezDonutButton;
    public GameObject freezOnionButton;
    public GameObject freezChickenButton;
    public GameObject freezRibButton;
    public GameObject freezBigChickenButton;


    //ゲーム停止フラグ
    public bool pose = false;

    //現在押下している指示ボタン
    public GameObject instructure;

    //現在選択中の店員
    public GameObject selectStaff;

    // 注文可能数
    public int canOrderNum = 2;
}
