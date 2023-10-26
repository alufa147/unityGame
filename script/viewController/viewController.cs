using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using XCharts;

public class viewController : MonoBehaviour
{
    cameraController cc;
    timeController tc;
    humanObjectController hoc;
    common com;
    public GameObject orderMenu;
    public GameObject salesPersonMenu;
    public GameObject storeMenu;
    public GameObject investmentMenu;
    public GameObject analysisMenu;
    public GameObject settingMenu;
    public GameObject infoView;

    public GameObject scrollView;
    GameObject scrollViewClone;

    public GameObject statusListButton;
    public GameObject statusView;
    GameObject statusViewClone;

    public GameObject levelupView;
    GameObject levelupViewClone;

    public GameObject storeUpgradeListButton;

    public GameObject marketListButton;

    public GameObject thisYearGraph;
    GameObject thisYearGraphClone;

    public GameObject allYearGraph;
    GameObject allYearGraphClone;

    public GameObject dialog;
    GameObject dialogClone;

    public Slider levelSlider;
    public Text levelText;

    public Image infoMessageImageView;
    public Text infoMessageView;

    public GameObject orderView;
    bool orderViewBool = false;
    public Text orderText;

    public GameObject getInstructure;
    public GameObject preparationInstructure;

    // エラーメッセージ
    public Text errorMessage;

    // アップグレード用ダイアログ
    public GameObject upgradeDialog;

    


    bool clickReg = true;
    DBManager db;

    float time = 0;

    
    private void Start()
    {
        cc = GameObject.Find("Main Camera").GetComponent<cameraController>();
        tc = GameObject.Find("timeController").GetComponent<timeController>();
        hoc = GameObject.Find("humanObjectController").GetComponent<humanObjectController>();
        com = GameObject.Find("common").GetComponent<common>();
        db = new DBManager();
    }

    private void Update()
    {
        if(errorMessage.text != "")
        {
            time += Time.deltaTime;
            if(time > 2)
            {
                time = 0;
                setErrorMessage("");
            }
        }
    }


    public void setInfoMessage(string message)
    {
        infoMessageView.text = message;
    }

    public void switchButtonClick()
    {
        if(getInstructure.activeInHierarchy == true)
        {
            getInstructure.SetActive(false);
            preparationInstructure.SetActive(true);
        } else
        {
            getInstructure.SetActive(true);
            preparationInstructure.SetActive(false);
        }
    }

    //準備の指示ボタンクリック
    public void preparanceInstructureButtonClick(string jobName)
    {

        if (com.selectStaff == null) return;
        string job = "";

         if (jobName == "freezHamburg")
        {
            job = AppConst.JOB_MOVEFREEZHAMBURG;
        }
        else if (jobName == "freezPotato")
        {
            job = AppConst.JOB_MOVEFREEZPOTATO;
        }
        else if (jobName == "freezDonut")
        {
            job = AppConst.JOB_MOVEFREEZDONUT;
        }
        else if (jobName == "freezOnion")
        {
            job = AppConst.JOB_MOVEFREEZONION;
        }
        else if(jobName == "freezChicken")
        {
            job = AppConst.JOB_MOVEFREEZFRIEDCHICKEN;
        }
        else if(jobName == "freezRib")
        {
            job = AppConst.JOB_MOVEFREEZRIB;
        }
        else if(jobName == "freezBigChicken")
        {
            job = AppConst.JOB_MOVEFREEZBIGFRIEDCHICKEN;
        }

        Debug.Log("準備" + job);
        com.selectStaff.GetComponent<playercontroller>().setJobSlot(job, 1);
    }

    //指示ボタンクリック
    public void instructureButtonClick(string jobName)
    {
        if (com.selectStaff == null) return;

        string job = "";

        if(jobName == "hamburger")
        {
            job = AppConst.JOB_SMALLHAMBURGER;
        } else if(jobName == "reji")
        {
            job = AppConst.JOB_REJI;
        } else if(jobName == "drink")
        {
            job = AppConst.JOB_DRINK;
        } else if(jobName == "potato")
        {
            job = AppConst.JOB_POTATO;
        } else if(jobName == "donut")
        {
            job = AppConst.JOB_DONUT;
        } else if(jobName == "onion")
        {
            job = AppConst.JOB_ONIONRING;
        } else if(jobName == "friedChicken")
        {
            job = AppConst.JOB_FRIEDCHICKEN;
        } else if(jobName == "rib")
        {
            job = AppConst.JOB_RIB;
        } else if(jobName == "bigFriedChicken")
        {
            job = AppConst.JOB_BIGFRIEDCHICKEN;
        }

        com.selectStaff.GetComponent<playercontroller>().setJobSlot(job,0);
    }


    //雇用view表示
    public void onEmployeeButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //scrollViewの中にデータを入れる
        Dictionary<string, DBManager.EmployeeData> em = (Dictionary<string, DBManager.EmployeeData>)db.getData(AppConst.EMPLOYEE_LIST_FILE_NAME);
        if (em == null)
        {
            successMessageDialog("求職者はいません。");
            return;
        }

            //scrollView表示
            Destroy(scrollViewClone);
        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        scrollViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        

        foreach(string key in em.Keys)
        {
            GameObject t = Instantiate(statusListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = em[key].name;

            t.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(em[key].resourceName);

            t.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(em[key].lank);

            t.GetComponent<Button>().onClick.AddListener(() => employeeStatusViewOpen(em[key]));
        }

    }

    //雇用者情報の詳細表示
    void employeeStatusViewOpen(DBManager.EmployeeData em)
    {
        Destroy(scrollViewClone);
        statusViewClone = Instantiate(statusView);
        statusViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        statusViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        statusViewClone.transform.GetChild(0).gameObject.GetComponent<Text>().text = em.name;
        statusViewClone.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(em.resourceName);
        int lankToLevelMax = sharedFunction.lankToLevel(em.lank);
        statusViewClone.transform.GetChild(2).gameObject.GetComponent<Text>().text = "　歩行： Lv. 0 / " + lankToLevelMax + "\n　作業： Lv. 0 / " + lankToLevelMax + "\nスタミナ： Lv. 0 / " + lankToLevelMax;
        statusViewClone.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(em.lank);
        GameObject employeeButton = statusViewClone.transform.GetChild(4).gameObject;
        employeeButton.SetActive(true);
        employeeButton.GetComponent<Button>().onClick.AddListener(() => {

            //雇用ボタンの処理
            //この人を雇用するのかしないのかのダイアログを表示
            employeeDialogOpen(em.name + "を雇用しますか？", em, 50000);
            
        });


    }

    /// <summary>
    /// 雇用確認ダイアログ
    /// </summary>
    /// <param name="message"></param>
    /// <param name="em"></param>
    /// <param name="cost"></param>
    void employeeDialogOpen(string message, DBManager.EmployeeData em, int cost)
    {
        // ダイアログを生成
        dialogClone = com.upgradeDialogController.instantiateUpgradeDialog();

        // メッセージ設定
        com.upgradeDialogController.setMessage(dialogClone, message);

        // moneyボタンテキスト設定
        com.upgradeDialogController.setMoneyButtonText(dialogClone, cost + "円");

        // heartボタンテキスト設定
        com.upgradeDialogController.setHeartButtonText(dialogClone, cost / AppConst.HEART_CORFFICIENT + "個");

        // moneyボタン処理
        com.upgradeDialogController.setMoneyButton(dialogClone, () => {

            //雇用最大数取得
            DBManager.UpgradedData upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);

            //現在の雇用数取得
            string[] employeeNumNow = db.getSalesPersonNameList();
            Debug.Log("現在雇用数" + employeeNumNow.Length + "  最大雇用可能数" + upd.employeeMax);

            //雇用最大数のチェック
            if (upd.employeeMax == employeeNumNow.Length)
            {
                successMessageDialog("雇用数が上限に達しています。");
                return;
            }

            //費用計算
            if (com.procon.getFund() < cost)
            {
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }
            else
            {
                com.procon.setMoney(-cost);
            }

            //店員データに追加と店員オブジェクトの生成
            db.setSalesPerson(em.name, em.resourceName, 750, 1, 1, 1, em.lank);
            hoc.instantiateSalesPersonInitial(em.name, em.resourceName, em.lank);

            successMessageDialog(em.name + "を雇用しました");

            setInfoMessage(em.name + "が今日から仲間に入りました。");

            //雇用データから削除
            db.deleteEmployeeData(em);

            //画面の初期化
            onCancelButtonClick();

        });

        // ハートボタン処理
        com.upgradeDialogController.setHeartButton(dialogClone, () =>
        {

            //雇用最大数取得
            DBManager.UpgradedData upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);

            //現在の雇用数取得
            string[] employeeNumNow = db.getSalesPersonNameList();
            Debug.Log("現在雇用数" + employeeNumNow.Length + "  最大雇用可能数" + upd.employeeMax);

            //雇用最大数のチェック
            if (upd.employeeMax == employeeNumNow.Length)
            {
                successMessageDialog("雇用数が上限に達しています。");
                return;
            }

            //費用計算
            if (com.heartController.getHeart() < cost / AppConst.HEART_CORFFICIENT)
            {
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }
            else
            {
                com.heartController.setHeart(-(cost / AppConst.HEART_CORFFICIENT));
            }

            //店員データに追加と店員オブジェクトの生成
            db.setSalesPerson(em.name, em.resourceName, 750, 1, 1, 1, em.lank);
            hoc.instantiateSalesPersonInitial(em.name, em.resourceName, em.lank);

            successMessageDialog(em.name + "を雇用しました");

            setInfoMessage(em.name + "が今日から仲間に入りました。");

            //雇用データから削除
            db.deleteEmployeeData(em);

            //画面の初期化
            onCancelButtonClick();

        });
        
        // キャンセルボタンの処理
        com.upgradeDialogController.setCancelButton(dialogClone, () => { Destroy(dialogClone); });
    }


    //スライダーに値をセット
    public void setSliderValue(int exp, int level, int maxValue)
    {
        levelSlider.value = exp;
        levelSlider.maxValue = maxValue;
        levelText.text = level.ToString();
    }

    //レベルアップダイアログ
    public void openLevelUpDialog()
    {
        GameObject d = Instantiate(dialog);
        d.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        d.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        d.transform.GetChild(2).gameObject.SetActive(false);
        d.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => {
            Destroy(d);
            onCancelButtonClick();
            
        });
        d.transform.GetChild(4).gameObject.SetActive(true);
    }


    //ぷらポリ
    public void onPolicyButtonClick()
    {
        successMessageDialog("ぷらポリ内容の記載。必要であれば、スクロールビューを配置");
    }

    //セーブボタン
    public void onSaveButtonClick()
    {
        //セーブが完了しましたダイアログの表示
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //セーブ処理記述↓

        successMessageDialog("セーブが完了しました。");
        setInfoMessage("セーブが完了しました。");
    }


    //全年のグラフ表示
    public void onProfitAllYearButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        allYearGraphClone = Instantiate(allYearGraph);
        allYearGraphClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        allYearGraphClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        BarChart bc = allYearGraphClone.GetComponent<BarChart>();
        Dictionary<int, DBManager.ProfitAllYear> pay = (Dictionary<int, DBManager.ProfitAllYear>)db.getData(AppConst.PROFIT_ALL_YEAR_FILE_NAME);
        if(pay == null)
        {
            Debug.Log("null");
            return;
        }
        
        foreach (int key in pay.Keys)
        {
            bc.xAxis0.AddData(key.ToString());
            bc.AddData(0, key, pay[key].profit);
        }
    }

    //今年のグラフ表示
    public void onProfitThisYearButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");
        thisYearGraphClone = Instantiate(thisYearGraph);
        thisYearGraphClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        thisYearGraphClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        BarChart bc = thisYearGraphClone.GetComponent<BarChart>();
        Dictionary<int, DBManager.ProfitThisYear> pty = (Dictionary<int, DBManager.ProfitThisYear>)db.getData(AppConst.PROFIT_THIS_YEAR_FILE_NAME);
        if(pty == null)
        {
            return;
        }
        foreach(int key in pty.Keys)
        {
            bc.AddData(0, key, pty[key].profit);
        }
    }

    //いい報告の時のダイアログ
    public void successMessageDialog(string message)
    {
        if (dialogClone != null)
        {
            Destroy(dialogClone);
        }
        
        GameObject d = Instantiate(dialog);
        d.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        d.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        d.transform.GetChild(1).gameObject.GetComponent<Text>().text = message;
        d.transform.GetChild(2).gameObject.SetActive(false);
        d.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => {
            Destroy(d);
            onCancelButtonClick();
        });

    }
    

    //営業ボタンクリック

    public void onSalesButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        scrollViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        Dictionary<string, DBManager.Sales> sales = (Dictionary<string, DBManager.Sales>)db.getData(AppConst.SALES_LIST_FILE_NAME);
        foreach (string key in sales.Keys)
        {
            GameObject t = Instantiate(storeUpgradeListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = sales[key].name;

            t.GetComponent<Button>().onClick.AddListener(() => salesExplanationDialogOpen(sales[key]));
        }
    }

    //営業についての説明ダイアログ
    void salesExplanationDialogOpen(DBManager.Sales sales)
    {
        dialogClone = Instantiate(dialog);
        dialogClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        dialogClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        dialogClone.transform.GetChild(1).gameObject.GetComponent<Text>().text = sales.name + "を利用して集客アップを図ります。\n 費用："+ sales.amountOfMoney + "円";
        dialogClone.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            //お金の支払い
            if (sales.amountOfMoney > com.procon.getFund())
            {
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }

            com.procon.setMoney(-sales.amountOfMoney);

            //効果を反映
            com.instantiateCarCustomerMinInterval = sales.effect;

            Destroy(dialogClone);
        });
        dialogClone.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(dialogClone));

    }

    /// <summary>
    /// 店舗アップグレードボタンクリック
    /// </summary>
    public void onStoreUpgradeButtonClick()
    {
        //Debug.Log("店舗アップグレードボタンが押されました");
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        //scrollViewの中にリストを表示
        Dictionary<int, DBManager.storeUpgrade> sug = (Dictionary<int, DBManager.storeUpgrade>)db.getData(AppConst.STORE_UPGRADE_FILE_NAME);

        //特にデータがなければダイアログの表示
        if(sug == null || sug.Count == 0)
        {
            successMessageDialog("アップグレードはありません。");
            return;
        }

        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        scrollViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        foreach (int key in sug.Keys)
        {
            
            GameObject t = Instantiate(storeUpgradeListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = sug[key].upgradeName;

            t.GetComponent<Button>().onClick.AddListener(() => storeUpgradeExplanationDialogOpen(sug[key]));
        }
    }

    /// <summary>
    /// アップグレードダイアログ表示
    /// </summary>
    /// <param name="sug"></param>
    void storeUpgradeExplanationDialogOpen(DBManager.storeUpgrade sug)
    {
        // ダイアログ生成
        dialogClone = com.upgradeDialogController.instantiateUpgradeDialog();

        // メッセージ設定
        com.upgradeDialogController.setMessage(dialogClone, "アップグレード : " + sug.upgradeName + " をします。よろしいですか？");

        // moneyボタンのテキストを設定
        com.upgradeDialogController.setMoneyButtonText(dialogClone, sug.upgradeAmountOfMoney + "円");

        // heartボタンのテキストを設定
        com.upgradeDialogController.setHeartButtonText(dialogClone, sug.upgradeAmountOfHeart + "個");

        // moneyボタン処理
        com.upgradeDialogController.setMoneyButton(dialogClone, (() =>
        {
            //お金の支払い
            if (sug.upgradeAmountOfMoney > com.procon.getFund())
            {
                Destroy(dialogClone);
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }

            com.procon.setMoney(-sug.upgradeAmountOfMoney);

            //アップグレードリストから削除
            db.deleteStoreUpgradeData(sug);

            //infoMessage
            setInfoMessage(sug.upgradeName + "をアップグレードしました。");

            //ダイアログ削除
            Destroy(dialogClone);

            //storeUpgradeListViewを更新して再表示
            Destroy(scrollViewClone);
            onStoreUpgradeButtonClick();

            GameObject.FindWithTag("updateFunctionController").GetComponent<updateFunctionController>().upgradeFunction(sug.upgradeName);

            successMessageDialog(sug.upgradeName + "のアップグレードが完了しました。");

        }));

        // heartボタン処理
        com.upgradeDialogController.setHeartButton(dialogClone, () => {

            // ハート計算
            if (com.heartController.getHeart() > sug.upgradeAmountOfHeart)
            {
                Destroy(dialogClone);
                shortageFundsDialogOpen(AppConst.NOHEART_MESSAGE);
                return;
            }

            com.heartController.setHeart(-sug.upgradeAmountOfHeart);

            //アップグレードリストから削除
            db.deleteStoreUpgradeData(sug);

            //infoMessage
            setInfoMessage("アップグレード : " + sug.upgradeName + " を実行しました。");

            //ダイアログ削除
            Destroy(dialogClone);

            //storeUpgradeListViewを更新して再表示
            Destroy(scrollViewClone);
            onStoreUpgradeButtonClick();

            GameObject.FindWithTag("updateFunctionController").GetComponent<updateFunctionController>().upgradeFunction(sug.upgradeName);

            successMessageDialog(sug.upgradeName + "のアップグレードが完了しました。");
        });

        // キャンセルボタン処理
        com.upgradeDialogController.setCancelButton(dialogClone, () => { Destroy(dialogClone); });

    }

    //レベルアップボタンクリック
    public void onSalesPersonLevelupButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        //scrollViewを表示
        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        RectTransform rect = scrollViewClone.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);

        //scrollViewの中にリストを表示
        Dictionary<string, DBManager.salesPerson> salesPerson = (Dictionary<string, DBManager.salesPerson>)db.getData(AppConst.SALES_PERSON_FILE_NAME);
        foreach (string key in salesPerson.Keys)
        {
            GameObject t = Instantiate(statusListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = salesPerson[key].fullName;

            //店員画像設定
            t.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].imgName);

            //ランク設定
            t.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].lank);

            t.GetComponent<Button>().onClick.AddListener(() => salesPersonLevelupViewOpen(salesPerson[key]));
        }
    }

    void salesPersonLevelupViewOpen(DBManager.salesPerson salesP)
    {
        Destroy(scrollViewClone);
        levelupViewClone = Instantiate(levelupView);
        levelupViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        levelupViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        levelupViewClone.transform.GetChild(0).gameObject.GetComponent<Text>().text = salesP.fullName;
        levelupViewClone.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.imgName);
        int lankToLevelMax = sharedFunction.lankToLevel(salesP.lank);
        levelupViewClone.transform.GetChild(2).gameObject.GetComponent<Text>().text = "　歩行： Lv. " + salesP.walkSpeedLevel + " / " + lankToLevelMax + "\n　作業： Lv. " + salesP.workSpeedLevel + " / " + lankToLevelMax + "\nスタミナ： Lv. " + salesP.sutaminaLevel + " / " + lankToLevelMax;
        levelupViewClone.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => levelupDialogOpen(salesP.fullName + "の歩行レベルをあげます。時給が50円上昇します。","walk",salesP, salesP.walkSpeedLevel*5000));
        levelupViewClone.transform.GetChild(4).gameObject.GetComponent<Button>().onClick.AddListener(() => levelupDialogOpen(salesP.fullName + "の作業レベルをあげます。時給が50円上昇します。", "work", salesP, salesP.workSpeedLevel * 5000));
        levelupViewClone.transform.GetChild(5).gameObject.GetComponent<Button>().onClick.AddListener(() => levelupDialogOpen(salesP.fullName + "のスタミナレベルをあげます。時給が50円上昇します。", "sutamina", salesP, salesP.sutaminaLevel * 5000));
        levelupViewClone.transform.GetChild(6).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.lank);
        if (salesP.walkSpeedLevel >= lankToLevelMax)
        {
            levelupViewClone.transform.GetChild(3).gameObject.SetActive(false);
        }
        if (salesP.workSpeedLevel >= lankToLevelMax)
        {
            levelupViewClone.transform.GetChild(4).gameObject.SetActive(false);
        }
        if (salesP.sutaminaLevel >= lankToLevelMax)
        {
            levelupViewClone.transform.GetChild(5).gameObject.SetActive(false);
        }
    }

    //レベルアップ確認ダイアログ
    void levelupDialogOpen(string message, string parametaType, DBManager.salesPerson salesP, int cost)
    {
        // ダイアログ生成
        dialogClone = com.upgradeDialogController.instantiateUpgradeDialog();

        // メッセージ設定
        com.upgradeDialogController.setMessage(dialogClone, message);

        // moneyボタンのテキスト設定
        com.upgradeDialogController.setMoneyButtonText(dialogClone, cost + "円");

        // heartボタンのテキスト設定
        com.upgradeDialogController.setHeartButtonText(dialogClone, cost / AppConst.HEART_CORFFICIENT + "個");

        // moneyボタンの処理
        com.upgradeDialogController.setMoneyButton(dialogClone, () => {

            //資金不足の場合は処理しない。
            if (com.procon.getFund() < cost)
            {
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }

            //店員パラメータ登録
            if (parametaType == "walk")
            {
                salesP.walkSpeedLevel = salesP.walkSpeedLevel + 1;
            }
            else if (parametaType == "work")
            {
                salesP.workSpeedLevel = salesP.workSpeedLevel + 1;
            }
            else if (parametaType == "sutamina")
            {
                salesP.sutaminaLevel = salesP.sutaminaLevel + 1;
            }

            //店員の時給50UP
            salesP.hourWage = salesP.hourWage + 50;

            //レベルアップの反映
            com.hoc.salesPLevelup(salesP.fullName);

            //データ登録
            db.updateSalesPerson(salesP);

            //資金から費用分差し引き
            com.procon.setMoney(-cost);

            //現在のダイアログを削除
            Destroy(dialogClone);

            //現在の店員ステータスを削除
            Destroy(levelupViewClone);

            //再度同じ店員のlevelUpViewを表示
            salesPersonLevelupViewOpen(salesP);

            //infoMessage
            setInfoMessage(salesP.fullName + "をレベルアップしました。");
        });

        com.upgradeDialogController.setHeartButton(dialogClone, () => {

            //資金不足の場合は処理しない。
            if (com.heartController.getHeart() < cost / AppConst.HEART_CORFFICIENT)
            {
                shortageFundsDialogOpen(AppConst.NOMONEY_MESSAGE);
                return;
            }

            //店員パラメータ登録
            if (parametaType == "walk")
            {
                salesP.walkSpeedLevel = salesP.walkSpeedLevel + 1;
            }
            else if (parametaType == "work")
            {
                salesP.workSpeedLevel = salesP.workSpeedLevel + 1;
            }
            else if (parametaType == "sutamina")
            {
                salesP.sutaminaLevel = salesP.sutaminaLevel + 1;
            }

            //店員の時給50UP
            salesP.hourWage = salesP.hourWage + 50;

            //レベルアップの反映
            com.hoc.salesPLevelup(salesP.fullName);

            //データ登録
            db.updateSalesPerson(salesP);

            //資金から費用分差し引き
            com.heartController.setHeart(-(cost / AppConst.HEART_CORFFICIENT));

            //現在のダイアログを削除
            Destroy(dialogClone);

            //現在の店員ステータスを削除
            Destroy(levelupViewClone);

            //再度同じ店員のlevelUpViewを表示
            salesPersonLevelupViewOpen(salesP);

            //infoMessage
            setInfoMessage(salesP.fullName + "をレベルアップしました。");
        });

        // キャンセルボタンの処理
        com.upgradeDialogController.setCancelButton(dialogClone, () => { Destroy(dialogClone); });
    }

    /// <summary>
    /// 不足ダイアログ
    /// </summary>
    /// <param name="message"></param>
    void shortageFundsDialogOpen(string message)
    {
        //初期化
        Destroy(dialogClone);
        dialogClone = com.dialogController.instantiateDialog();
        com.dialogController.setDialogMessage(dialogClone, message);
        com.dialogController.setOkButton(dialogClone, () => { Destroy(dialogClone); });
        com.dialogController.setCancelButtonHidden(dialogClone);
    }

    //店員一覧ボタン押下
    public void onSalesPersonListButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        //scrollViewを表示
        Destroy(scrollViewClone);
        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        RectTransform rect = scrollViewClone.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);

        //scrollViewの中にリストを表示
        Dictionary<string, DBManager.salesPerson> salesPerson = (Dictionary<string, DBManager.salesPerson>)db.getData(AppConst.SALES_PERSON_FILE_NAME);
        foreach(string key in salesPerson.Keys)
        {
            Debug.Log(salesPerson[key].fullName);
            //リストを生成してscrollViewに格納
            GameObject t = Instantiate(statusListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            //名前設定
            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = salesPerson[key].fullName;

            //店員画像設定
            t.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].imgName);

            //ランク設定
            t.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].lank);

            t.GetComponent<Button>().onClick.AddListener(() => salesPersonStatusViewOpen(salesPerson[key]));
        }
        
    }
    
    //店員一覧から店員をクリックしstatusViewを表示
    void salesPersonStatusViewOpen(DBManager.salesPerson salesP)
    {
        Destroy(scrollViewClone);
        statusViewClone = Instantiate(statusView);
        statusViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        statusViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        statusViewClone.transform.GetChild(0).gameObject.GetComponent<Text>().text = salesP.fullName;
        statusViewClone.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.imgName);
        int lankToLevelMax = sharedFunction.lankToLevel(salesP.lank);
        statusViewClone.transform.GetChild(2).gameObject.GetComponent<Text>().text = "　歩行： Lv. " + salesP.walkSpeedLevel +" / " +lankToLevelMax +"\n　作業： Lv. " + salesP.workSpeedLevel + " / " + lankToLevelMax +"\nスタミナ： Lv. " + salesP.sutaminaLevel + " / " + lankToLevelMax;
        statusViewClone.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.lank);
    }

    //解雇viewを表示
     void dismissalViewOpen(DBManager.salesPerson salesP)
    {
        Destroy(scrollViewClone);
        statusViewClone = Instantiate(statusView);
        statusViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        statusViewClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        statusViewClone.transform.GetChild(0).gameObject.GetComponent<Text>().text = salesP.fullName;
        statusViewClone.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.imgName);
        int lankToLevelMax = sharedFunction.lankToLevel(salesP.lank);
        statusViewClone.transform.GetChild(2).gameObject.GetComponent<Text>().text = "　歩行： Lv. " + salesP.walkSpeedLevel + " / " + lankToLevelMax + "\n　作業： Lv. " + salesP.workSpeedLevel + " / " + lankToLevelMax + "\nスタミナ： Lv. " + salesP.sutaminaLevel + " / " + lankToLevelMax;
        statusViewClone.transform.GetChild(3).gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(salesP.lank);
        GameObject dismissalButton = statusViewClone.transform.GetChild(5).gameObject;
        dismissalButton.SetActive(true);
        dismissalButton.GetComponent<Button>().onClick.AddListener(() => {
            //解雇処理するかダイアログ表示
            dismissalDialogOpen(salesP);
        });
    }

    //解雇ボタン押下
    public void onDismissalButtonClick()
    {
        //1回目押下時のみ処理
        if (!clickReg) return;
        clickReg = false;

        //menu非表示
        otherObjectHidden("cancel");

        //scrollViewを表示
        scrollViewClone = Instantiate(scrollView);
        scrollViewClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        RectTransform rect = scrollViewClone.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(0, 0);

        //scrollViewの中にリストを表示
        Dictionary<string, DBManager.salesPerson> salesPerson = (Dictionary<string, DBManager.salesPerson>)db.getData(AppConst.SALES_PERSON_FILE_NAME);
        foreach (string key in salesPerson.Keys)
        {
            //リストを生成してscrollViewに格納
            GameObject t = Instantiate(statusListButton);
            t.transform.SetParent(GameObject.FindWithTag("content").transform);

            //名前設定
            GameObject text = t.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = salesPerson[key].fullName;

            //店員画像設定
            t.transform.GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].imgName);

            //ランク設定
            t.transform.GetChild(2).GetComponent<Image>().sprite = Resources.Load<Sprite>(salesPerson[key].lank);

            t.GetComponent<Button>().onClick.AddListener(() => dismissalViewOpen(salesPerson[key]));
        }

    }

    //解雇確認ダイアログ
    void dismissalDialogOpen(DBManager.salesPerson salesP)
    {
        dialogClone = Instantiate(dialog);
        dialogClone.transform.SetParent(GameObject.FindWithTag("canvas").transform);
        dialogClone.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        dialogClone.transform.GetChild(1).gameObject.GetComponent<Text>().text = salesP.fullName + "を解雇しますか？";
        dialogClone.transform.GetChild(3).gameObject.GetComponent<Button>().onClick.AddListener(() => {

            //データベースから削除
            db.deleteSalesPeson(salesP);

            //オブジェクトの削除
            hoc.destroySalesPerson(salesP);

            //infoMessage
            setInfoMessage(salesP.fullName + "を解雇しました。");

            //画面更新
            onCancelButtonClick();

        });
        dialogClone.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(dialogClone));
    }

    //オーダーメニューボタン押下
    public void onOrderMenuButtonClick()
    {
        if (orderViewBool)
        {
            orderView.SetActive(false);
            orderViewBool = false;
        } else
        {
            cc.cameraOff();
            orderView.SetActive(true);
            orderViewBool = true;
        }
    }


    //メニューの店員ボタン押下
    public void onSalesPersonMenuButtonClick()
    {
        cc.cameraOff();
        //ゲーム停止
        stopGame();
        salesPersonMenu.SetActive(true);
        otherObjectHidden("salesPerson");
    }

    public void onStoreMenuButtonClick()
    {
        cc.cameraOff();
        //ゲーム停止
        stopGame();
        storeMenu.SetActive(true);
        otherObjectHidden("store");
    }

    public void onInvestmentMenuButtonClick()
    {
        cc.cameraOff();
        //ゲーム停止
        stopGame();
        cc.cameraOff();
        investmentMenu.SetActive(true);
        otherObjectHidden("investment");
    }

    public void onAnalysisMenuButtonClick()
    {
        cc.cameraOff();
        //ゲーム停止
        stopGame();
        cc.cameraOff();
        analysisMenu.SetActive(true);
        otherObjectHidden("analysis");
    } 

    public void onSettingMenuButtonClick()
    {
        cc.cameraOff();
        //ゲーム停止
        stopGame();
        cc.cameraOff();
        settingMenu.SetActive(true);
        otherObjectHidden("setting");
    }

    void otherObjectHidden(string objName)
    {
        int objNum = 0;
        if (objName == "salesPerson") objNum = 0;
        if (objName == "store") objNum = 1;
        if (objName == "investment") objNum = 2;
        if (objName == "analysis") objNum = 3;
        if (objName == "setting") objNum = 4;
        if (objName == "cancel") objNum = 5;
        GameObject[] obj = { salesPersonMenu, storeMenu, investmentMenu, analysisMenu, settingMenu };
        for(int i = 0; i < obj.Length; i++)
        {
            if(i == objNum)
            {
                continue;
            }
            obj[i].SetActive(false);
        }
    }

    //UIボタン以外の部分を押下
    public void onCancelButtonClick()
    {
        Debug.Log("キャンセルボタンがおされました　。");
        clickReg = true;

        //ボタンを初期表示にする
        otherObjectHidden("cancel");

        //scrollView削除
        if(scrollViewClone != null) Destroy(scrollViewClone);


        //statusView削除
        if(statusViewClone != null) Destroy(statusViewClone);


        //levelUpViewを削除
        if(levelupView != null) Destroy(levelupViewClone);


        //dialogを削除
        if(dialogClone) Destroy(dialogClone);


        //グラフの削除
        if(thisYearGraph != null) Destroy(thisYearGraphClone);

        if(allYearGraph != null) Destroy(allYearGraphClone);

        cc.cameraOn();

        //ゲーム進行
        startGame();
    }

    //UI操作時はゲーム進行を停止する
    void stopGame()
    {
        tc.stop();
    }

    //UI操作時以外はゲームを進行する
    void startGame()
    {
        tc.start();
    }

    //お金viewの更新
    public void updateMoneyView(int fund)
    {

        infoView.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = fund.ToString() + "円";
    }

    //ハートviewの更新
    public void updateHeartView(int heart)
    {
        Debug.Log("これからハート数を表示設定します。 heart : " + heart);
        infoView.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = "×" + heart;
    }

    //日付の更新
    public void updateDateView(int year, int month, int week)
    {
        infoView.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = year + "年　" + month + "月　" + week + "週";
    }

    //注文viewの更新
    public void updateOrderView(string[][] order)
    {
        int cnt = 1;
        orderText.text = "";
        foreach (string[] od in order)
        {
            
            orderText.text += "\n" + cnt + ".: " + sharedFunction.getInfoMessage(od);
            cnt++;
        }
    }

    public void setErrorMessage(string message)
    {
        errorMessage.text = message;
    }

        
}
