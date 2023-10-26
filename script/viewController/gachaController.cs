using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gachaController : MonoBehaviour
{

    common com;

    public GameObject gachaView;


    GameObject gachaViewClone;


    GameObject gachagacha;
    GameObject moneyButton;
    GameObject heartButton;
    Animator nobuAnim;
    GameObject gachaObject;
    GameObject openGachaObject;
    GameObject salesPersonGachaResult;
    GameObject salesPersonImage;
    GameObject okButton;
    GameObject lankImage;
    GameObject deleteWindow;
    float animStateTime = 0f;

    // ガチャの値段
    private int cost = 1;

    // ハート使用時の値段
    private int heartCost = 5;

    private void Update()
    {
        //ガチャのアニメーション
        if (nobuAnim != null)
        {
            animStateTime += Time.deltaTime;
            nobuAnim.SetInteger("ID", 1);
            if (animStateTime > 4.2f && animStateTime < 6.2f)
            {
                gachaObject.SetActive(true);
                gachaObject.GetComponent<Animator>().SetInteger("ID", 1);

            }
            if (animStateTime > 6.6f)
            {
                gachaObject.GetComponent<Animator>().SetInteger("ID", 2);
                if (animStateTime > 11.2)
                {

                    openGachaObject.SetActive(true);
                    gachaObject.SetActive(false);
                }
            }
            if (animStateTime > 13f)
            {
                salesPersonImage.SetActive(true);
                lankImage.SetActive(true);
                nobuAnim = null;
                salesPersonGachaResult.SetActive(true);
                okButton.SetActive(true);
                gachagacha.SetActive(false);
                Time.timeScale = 0;
                animStateTime = 0;
            }

        }
    }

    //ガチャの表示
    public void gacha()
    {
        DBManager db = new DBManager();
        com = GameObject.FindWithTag("common").GetComponent<common>();
        animStateTime = 0;

        //gachaViewの表示
        gachaViewClone = Instantiate(gachaView);
        gachaViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        gachaViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        //nobu取得
        Animator nobu = gachaViewClone.transform.GetChild(1).GetChild(0).gameObject.GetComponent<Animator>();

        // deleteWindow
        deleteWindow = gachaViewClone.transform.GetChild(0).gameObject;
        deleteWindow.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(gachaViewClone);
            GameObject.FindWithTag("viewController").GetComponent<viewController>().onCancelButtonClick();
        });

        // ガチャガチャ
        gachagacha = gachaViewClone.transform.GetChild(1).gameObject;

        // heartButton
        heartButton = gachaViewClone.transform.GetChild(2).gameObject;

        // moneyButton
        moneyButton = gachaViewClone.transform.GetChild(3).gameObject;

        // 店員画像
        salesPersonImage = gachaViewClone.transform.GetChild(4).gameObject;

        // ランク画像
        lankImage = gachaViewClone.transform.GetChild(5).gameObject;

        // 結果
        salesPersonGachaResult = gachaViewClone.transform.GetChild(6).gameObject;

        // OKボタン
        okButton = gachaViewClone.transform.GetChild(7).gameObject;

        // ガチャ
        gachaObject = gachaViewClone.transform.GetChild(1).GetChild(1).gameObject;

        // 開いたガチャ
        openGachaObject = gachaViewClone.transform.GetChild(1).GetChild(2).gameObject;

        string name = sharedFunction.createName(db.getSalesPersonNameList());
        string[] lank = getSalesPersonLank();
        string resourceName = getSalesPersonResourceName();

        //ガチャリソース設定
        gachaObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(lank[1]);
        openGachaObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(lank[2]);

        //ガチャ結果入力
        salesPersonGachaResult.GetComponent<Text>().text = name + "です。よろしくお願いいたします。";

        //取得店員の画像設定
        salesPersonImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(resourceName);

        //ランク画像の設定
        lankImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(lank[0]);
        Debug.Log(lank);

        //OKボタン取得
        okButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(gachaViewClone);
            GameObject.FindWithTag("viewController").GetComponent<viewController>().onCancelButtonClick();
        });




        //moneyボタンを押した時の処理
        //ノブが回転し、ランダムでガチャポンが生成され下に落ちる
        moneyButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            // ダイアログを生成
            GameObject dialog = com.dialogController.instantiateDialog();

            // メッセージを設定
            com.dialogController.setDialogMessage(dialog, cost + "円消費してガチャを回しますか？");

            // キャンセルボタンの処理
            com.dialogController.setCancelButton(dialog, () => { Destroy(dialog); });

            // ボタンの処理
            com.dialogController.setOkButton(dialog, () =>
            {

                // 費用計算
                // 資金がある場合
                if (com.procon.getFund() > cost)
                {
                    Destroy(dialog);

                    // 所持金から徴収
                    com.procon.setMoney(-cost);

                    // UI制御
                    moneyButton.SetActive(false);
                    deleteWindow.SetActive(false);
                    heartButton.SetActive(false);

                    //店員データの登録
                    db.setEmployeeData(name, lank[0], resourceName);

                    // アニメーショントリガー
                    nobuAnim = nobu;

                    // 時は動き出す
                    Time.timeScale = 1;

                }
                else
                {
                    // 資金がない場合
                    // キャンセルボタンを隠す
                    com.dialogController.setCancelButtonHidden(dialog);

                    // メッセージ表示
                    com.dialogController.setDialogMessage(dialog, AppConst.NOMONEY_MESSAGE);

                    // OKボタンの処理を設定
                    com.dialogController.setOkButton(dialog, () => { Destroy(dialog); });
                }

            });



        });

        //heartボタンを押した時の処理
        //ノブが回転し、ランダムでガチャポンが生成され下に落ちる
        heartButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            // ダイアログを生成
            GameObject dialog = com.dialogController.instantiateDialog();

            // メッセージを設定
            com.dialogController.setDialogMessage(dialog, "ハートを" + heartCost + "個消費してガチャを回しますか？");

            // キャンセルボタンの処理
            com.dialogController.setCancelButton(dialog, () => { Destroy(dialog); });

            // ボタンの処理
            com.dialogController.setOkButton(dialog, () =>
            {

                // 費用計算
                // 資金がある場合
                if (com.heartController.getHeart() > heartCost)
                {
                    Destroy(dialog);

                    // 所持金から徴収
                    com.heartController.setHeart(-heartCost);

                    // UI制御
                    moneyButton.SetActive(false);
                    deleteWindow.SetActive(false);
                    heartButton.SetActive(false);

                    //店員データの登録
                    db.setEmployeeData(name, lank[0], resourceName);

                    // アニメーショントリガー
                    nobuAnim = nobu;

                    // 時は動き出す
                    Time.timeScale = 1;

                }
                else
                {
                    // 資金がない場合
                    // キャンセルボタンを隠す
                    com.dialogController.setCancelButtonHidden(dialog);

                    // メッセージ表示
                    com.dialogController.setDialogMessage(dialog, AppConst.NOHEART_MESSAGE);

                    // OKボタンの処理を設定
                    com.dialogController.setOkButton(dialog, () => { Destroy(dialog); });
                }

            });
        });
    }


    //店員画像ファイル名取得
    string getSalesPersonResourceName()
    {
        //ファイル名を配列で持っておいて、ランダムで返却
        string[] resource = { "man1", "man2", "woman1", "woman2" };
        return resource[Random.Range(0, 4)];
    }

    //ガチャで出現するランクの確率制御
    string[] getSalesPersonLank()
    {
        //乱数の生成
        int random = Random.Range(0, 1000);
        string[] result = new string[3];
        if (random >= 1 && random <= 10)
        {
            //Sクラス
            result[0] = "S";
            result[1] = "gold";
            result[2] = "opengold";
            return result;
        }
        else if (random >= 11 && random <= 40)
        {
            //Aクラス
            result[0] = "A";
            result[1] = "black";
            result[2] = "openblack";
            return result;
        }
        else if (random >= 41 && random <= 140)
        {
            //Bクラス
            result[0] = "B";
            result[1] = "dou";
            result[2] = "opendou";
            return result;
        }
        else if (random >= 141 && random <= 340)
        {
            //Cクラス
            result[0] = "C";
            result[1] = "yellow";
            result[2] = "openyellow";
            return result;
        }
        else if (random >= 341 && random <= 640)
        {
            //Dクラス
            result[0] = "D";
            result[1] = "blue";
            result[2] = "openblue";
            return result;
        }
        else if (random >= 641 && random <= 840)
        {
            //Eクラス
            result[0] = "E";
            result[1] = "green";
            result[2] = "opengreen";
            return result;
        }
        else if (random >= 841 && random <= 940)
        {
            //Fクラス
            result[0] = "F";
            result[1] = "green";
            result[2] = "opengreen";
            return result;
        }
        else
        {
            //Gクラス
            result[0] = "G";
            result[1] = "green";
            result[2] = "opengreen";
            return result;
        }
    }

}
