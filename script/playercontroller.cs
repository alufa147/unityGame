using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Specialized;

public class playercontroller : MonoBehaviour
{
    //component
    NavMeshAgent agent;
    Animator ani;
    common com;

    public string fullName;
    public int skinNumber;
    public int hourWage;
    public int walkSpeedLevel;
    public int workSpeedLevel;
    public int sutaminaLevel;
    public int slotNumber;

    //プレイヤーの作業係数
    float workCoefficient = 1;

    // スピナー
    public objectMarker jobSpinner;

    // スライダー
    public objectMarker sutaminaSlider;


    //スタミナ
    float sutamina = 300;
    bool sutaminaFlag = false;

    //共有用位置
    Vector3 frierPosition;
    Vector3 friedStationPoint;
    string frierName;

    //店員の要素
    string[,] jobSlot;


    float stateTime = 0f;
    bool stateEnter = false;
    string foodName = "";


    //生成するためのオブジェクト
    GameObject completeFoodObj;

    Vector3 netInObjectPosition;
    GameObject netInObject;


    //人間がもつアイテム
    GameObject mansFreezFood;
    GameObject mansNetWithFood;
    GameObject mansFood;
    public GameObject mansPotato;
    public GameObject mansDonut;
    public GameObject mansOnion;
    public GameObject mansFriedChicken;
    public GameObject mansrib;
    public GameObject mansBigFriedChicken;
    public GameObject mansSalad;
    public GameObject mansNetInPoteto;
    public GameObject mansNetInDonut;
    public GameObject mansNetInOnion;
    public GameObject mansNetInChicken;
    public GameObject mansNetInRib;
    public GameObject mansNetInBigChicken;
    public GameObject mansClosedCup;
    public GameObject mansFreezPoteto;
    public GameObject mansFreezDonut;
    public GameObject mansFreezOnion;
    public GameObject mansFreezChicken;
    public GameObject mansFreezRib;
    public GameObject mansFreezBigChicken;
    public GameObject mansTong;
    public GameObject mansFreezHamburg;
    public GameObject mansKaeshi;
    public GameObject mansHamburger;
    public GameObject mansSmallHamburger;
    public GameObject mansMidiumHamburger;
    public GameObject mansLargeHamburger;

    //使いまわしできるオブジェクト
    GameObject resicleObject;


    public bool destroy = false;

    bool waitFlag = true;


    public Renderer renderer;
    private Color originalColor;

    private int trayNum = 0;
    private GameObject counter;

    //優先仕事が必要性は判断済みかどうか
    private bool priorityCheckFlg = false;

    //準備ジョブがセットされたか
    private bool preparation = false;

    // Start is called before the first frame update
    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        com = GameObject.FindWithTag("common").GetComponent<common>();
        jobSpinner.initialize(this.transform);
        sutaminaSlider.initialize(this.transform);

        renderer = transform.GetChild(skinNumber).GetComponent<Renderer>();
        originalColor = renderer.material.color;
        jobSlot = new string[slotNumber + 2, 2];

        // レベルに応じてスライダーのサイズを変える
        sutaminaSlider.targetUi.GetComponent<RectTransform>().sizeDelta = new Vector2(sutamina / 5, 10);
    }


    // Update is called once per frame
    void Update()
    {
        //停止状態
        if (com.pose) return;

        //準備ジョブの開始許可待ち処理
        if (preparation)
        {
            stateTime += Time.deltaTime;
            if (stateTime > 2)
            {
                stateTime = 0;
                preparation = preparationJobSet(jobSlot[0, 1]);
            }
        }

        //待機状態はナビメッシュをオフ
        if (waitFlag)
        {
            agent.enabled = false;
        }

        // スタミナスライダー制御
        sliderControl();

        // スタミナ回復
        if (sutaminaFlag)
        {
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            ani.SetInteger("ID", 0);
            float baseSutamina = (300f + (700 / 30)) * sutaminaLevel;
            sutamina += Time.deltaTime * (baseSutamina / 10);
            agent.enabled = false;
            if (sutamina > baseSutamina)
            {
                sutaminaFlag = false;
                deleteJobSlot(false);
            }
            return;
        }

        string j = "";
        if (jobSlot[0, 0] != null)
        {
            j = jobSlot[0, 1];
            trayNum = int.Parse(jobSlot[0, 0]);
        }
        else
        {
            return;
        }

        //スタミナ消費
        sutamina -= Time.deltaTime;
        if (sutamina < 1)
        {
            sutaminaFlag = true;

            // 休憩をジョブに追加する
            interruptJob(AppConst.JOB_SLEEP);
        }


        //Debug.Log("j = " + j);

        //レジ
        if (j == AppConst.JOB_REJI)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkLocationUseState();
                    stateTime = 0f;
                }
            }
            else
            {
                reji();
            }
            return;
        }

        //ドリンク
        if (j == AppConst.JOB_DRINK)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkLocationUseState();
                    stateTime = 0f;
                }
            }
            else
            {
                drink();
            }
            return;
        }

        //ドリンク移動
        if (j == AppConst.JOB_MOVEDRINK)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkLocationUseState();
                    stateTime = 0f;
                }
            }
            else
            {
                moveDrink();
            }

            return;
        }


        //冷蔵庫から食べ物を取得
        if (j.Length > 9 && j.Substring(0,9) == "moveFreez" && j != AppConst.JOB_MOVEFREEZHAMBURG)
        {

            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkPriorityJobOfLocationUseState();
                    stateTime = 0f;
                }

            }
            else
            {
                freezFryFood();
            }
            return;
        }

        //揚げる
        if (j == AppConst.JOB_FRY)
        {
            fry();
            return;
        }

        //揚げ物を揚げ物ステーションを移動
        if (j == AppConst.JOB_MOVEFRIEDFOOD)
        {
            moveFriedFood();
            return;
        }

        //揚げ物をトングで取得
        if (j == AppConst.JOB_POTATO || j == AppConst.JOB_DONUT || j == AppConst.JOB_ONIONRING || j == AppConst.JOB_FRIEDCHICKEN || j == AppConst.JOB_RIB || j == AppConst.JOB_BIGFRIEDCHICKEN)
        {
            if (!priorityCheckFlg)
            {

                foodName = jobSlot[0, 1];

                //揚げ物情報をセット
                setFryFoodInfo();

                //優先仕事が必要かどうか
                getNeedPriorityJob();

                priorityCheckFlg = true;
            }
            else
            {
                if (waitFlag)
                {
                    stateTime += Time.deltaTime;
                    if (stateTime > 1f)
                    {
                        waitFlag = checkLocationUseState();
                        stateTime = 0f;
                    }
                }
                else
                {
                    getFryFood();
                }
            }
            return;
        }

        //揚げ物をトレーに移動
        if (j == AppConst.JOB_MOVEFRYFOOD)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkLocationUseState();
                    stateTime = 0f;
                }
            }
            else
            {
                moveFryFood();
            }

            return;
        }

        //冷凍庫から冷凍ハンバーグを取り出す
        if (j == AppConst.JOB_MOVEFREEZHAMBURG)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1)
                {
                    waitFlag = checkPriorityJobOfLocationUseState();
                    stateTime = 0f;
                }

            }
            else
            {
                moveFreezHamburg();
            }

            return;
        }

        //ハンバーグを焼く
        if (j == AppConst.JOB_BAKEHAMBURG)
        {
            bakeHamburg();
            return;
        }

        //ハンバーガー生成
        if (j == AppConst.JOB_SMALLHAMBURGER)
        {
            if (!priorityCheckFlg)
            {
                //優先仕事が必要であれば割り込み
                getNeedPriorityJob();
                priorityCheckFlg = true;
            }
            else
            {
                if (waitFlag)
                {
                    stateTime += Time.deltaTime;
                    if (stateTime > 1f)
                    {
                        waitFlag = checkLocationUseState();
                        stateTime = 0f;
                    }
                }
                else
                {
                    getHamburger();
                }

            }

            return;
        }

        //ハンバーガーをトレーに移動
        if (j == AppConst.JOB_MOVEHAMBURGER)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1f)
                {
                    waitFlag = checkLocationUseState();
                    stateTime = 0f;
                }
            }
            else
            {
                moveHamburger();
            }

            return;
        }

        // 納品
        if (j == AppConst.JOB_MOVETRAY)
        {
            if (waitFlag)
            {
                stateTime += Time.deltaTime;
                if (stateTime > 1)
                {
                    stateTime = 0;
                    waitFlag = checkLocationUseState();
                }
            }
            else
            {
                if (counter == null)
                {
                    stateTime += Time.deltaTime;
                    if (stateTime > 1)
                    {
                        stateTime = 0;
                        counter = com.counterController.getFreeCounterPosition();
                    }
                }
                else
                {
                    moveTray();
                }
            }
            return;
        }



    }

    //仕事：レジ
    void reji()
    {
        //レジに行く
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.rejiPosition.position);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                stateEnter = true;
                transform.position += new Vector3(0.4f, 0, 0);
                //レジの方向を向く
                look(90);
            }
        }
        else
        {

            //レジアニメーション
            ani.SetInteger("ID", 1);
            stateTime += Time.deltaTime;
            updateGage(stateTime, 10f * workCoefficient);
            if (stateTime > 10f * workCoefficient)
            {

                //客から注文を取る
                com.chumoncontroller.setOrder();
                clear();
                com.locacon.setRejiFalse();
                deleteJobSlot(false);
                ani.SetInteger("ID", 0);
            }
        }

    }

    //仕事:ドリンク
    void drink()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.drinkPosition.position);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                stateEnter = true;
                resicleObject = Instantiate(com.karaCupObject);
                stateEnter = true;
                ani.SetInteger("ID", 2);
                look(90);
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 8f * workCoefficient);
            if (stateTime > 5f * workCoefficient && resicleObject.name == "karacup(Clone)")
            {
                Destroy(resicleObject);
                resicleObject = Instantiate(com.fullCupObject);
                ani.SetInteger("ID", 8);
            }

            if (stateTime > 8 * workCoefficient && resicleObject.name == "fullcup(Clone)")
            {

                Destroy(resicleObject);
                mansClosedCup.SetActive(true);
                com.locacon.setDrinkFalse();
                clear();
                foodName = jobSlot[0, 1];
                jobSlot[0, 1] = AppConst.JOB_MOVEDRINK;
                waitFlag = checkLocationUseState();
            }
        }
    }

    //仕事：ドリンク移動
    void moveDrink()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            agent.SetDestination(com.trayController.getTrayPosition(trayNum));
            ani.SetInteger("ID", 7);
            fixedAngle();
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                agent.enabled = false;
                look(90);
                ani.SetInteger("ID", 9);
            }

        }
        else
        {
            stateTime += Time.deltaTime;
            if (stateTime > 1)
            {

                if (com.trayController.getTray(trayNum) == null)
                {
                    com.trayController.instantiateTray(trayNum);
                }

                if(!com.trayController.instantiateItem(trayNum, com.closedCupObject, foodName))
                {
                    com.vc.setErrorMessage("これ以上トレーにのせられません。");
                }
                mansClosedCup.SetActive(false);
                clear();
                setTrayFalse();
                deleteJobSlot(false);
            }
        }
    }

    //仕事：冷蔵庫から食べ物を取り出す
    void freezFryFood()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.reizoukoPosition.position);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                openDoor();
                look(180);
                stateEnter = true;
                transform.position += new Vector3(0, 0, -0.4f);
                ani.SetInteger("ID", 6);
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 3f * workCoefficient);
            if (stateTime > 3 * workCoefficient)
            {

                mansFreezFood.SetActive(true);
                jobSlot[0, 1] = AppConst.JOB_FRY;
                com.locacon.setReizoukoFalse();
                stateEnter = false;
                stateTime = 0f;
            }
        }



    }

    //仕事: 揚げる
    void fry()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            closeDoor();
            agent.SetDestination(frierPosition);
            ani.SetInteger("ID", 7);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                resicleObject = Instantiate(netInObject);
                resicleObject.transform.SetParent(GameObject.FindWithTag(frierName).transform);
                resicleObject.transform.localPosition = netInObjectPosition;
                mansFreezFood.SetActive(false);
                agent.enabled = false;
                look(270);
                ani.SetInteger("ID", 9);
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 5f);
            if (stateTime > 5 + workCoefficient * workCoefficient)
            {

                jobSlot[0, 1] = AppConst.JOB_MOVEFRIEDFOOD;
                stateEnter = false;
                stateTime = 0f;
            }
        }
    }

    //仕事：揚げたものを揚げ物ステーションへ移動
    void moveFriedFood()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            Destroy(resicleObject);
            mansNetWithFood.SetActive(true);
            agent.SetDestination(friedStationPoint);
            ani.SetInteger("ID", 7);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                agent.enabled = false;
                look(270);
                ani.SetInteger("ID", 9);

            }
        }
        else
        {
            stateTime += Time.deltaTime;
            if (stateTime > 1)
            {
                mansNetWithFood.SetActive(false);
                if (foodName == AppConst.JOB_POTATO)
                {
                    com.potatoController.directiveCreatePotato();
                    com.potatoController.createPotatoEnd();
                    com.locacon.setPotatoFrierFalse();
                }
                else if (foodName == AppConst.JOB_DONUT)
                {
                    com.donucon.directiveCreateDonut();
                    com.donucon.createDonutEnd();
                    com.locacon.setDonutFrierFalse();
                }
                else if (foodName == AppConst.JOB_ONIONRING)
                {
                    com.onicon.directiveCreateOnion();
                    com.onicon.createOnionEnd();
                    com.locacon.setOnionFrierFalse();
                }
                else if (foodName == AppConst.JOB_FRIEDCHICKEN)
                {
                    com.chiccon.directiveCreateChicken();
                    com.chiccon.createChickenEnd();
                    com.locacon.setFriedChickenFrierFalse();
                }
                else if (foodName == AppConst.JOB_RIB)
                {
                    com.ribcon.directiveCreateRib();
                    com.ribcon.createRibEnd();
                    com.locacon.setRibFrierFalse();
                }
                else if (foodName == AppConst.JOB_BIGFRIEDCHICKEN)
                {
                    com.bchicon.directiveCreateBigChicken();
                    com.bchicon.createBigChickenEnd();
                    com.locacon.setBigFriedChickenFrierFalse();
                }

                deleteJobSlot(true);
                stateEnter = false;
                stateTime = 0f;
            }
        }
    }

    //仕事：揚げ物をトングで取得
    void getFryFood()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(friedStationPoint);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                ani.SetInteger("ID", 11);
                mansTong.SetActive(true);
                look(270);
                stateEnter = true;
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 5f);
            if (stateTime > 5 * workCoefficient) { 

                useAndSetObject();
                mansTong.SetActive(false);
                mansFood.SetActive(true);
                clear();
                if (foodName == AppConst.JOB_POTATO)
                {
                    com.locacon.setPotatoFriedStationFalse();
                }
                else if (foodName == AppConst.JOB_DONUT)
                {
                    com.locacon.setDonutFriedStationFalse();
                }
                else if (foodName == AppConst.JOB_ONIONRING)
                {
                    com.locacon.setOnionFriedStationFalse();
                }
                else if (foodName == AppConst.JOB_FRIEDCHICKEN)
                {
                    com.locacon.setFriedChickenFriedStationFalse();
                }
                else if (foodName == AppConst.JOB_RIB)
                {
                    com.locacon.setRibFriedStationFalse();
                }
                else if (foodName == AppConst.JOB_BIGFRIEDCHICKEN)
                {
                    com.locacon.setBigFriedChickenFriedStationFalse();
                }

                jobSlot[0, 1] = AppConst.JOB_MOVEFRYFOOD;
                priorityCheckFlg = false;
            }
        }

    }

    //仕事：揚げ物をトレーに移動
    void moveFryFood()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            agent.SetDestination(com.trayController.getTrayPosition(trayNum));
            ani.SetInteger("ID", 7);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                agent.enabled = false;
                look(90);
                ani.SetInteger("ID", 9);
            }
        }
        else
        {

            stateTime += Time.deltaTime;
            if (stateTime > 1)
            {
                if (com.trayController.getTray(trayNum) == null)
                {
                    com.trayController.instantiateTray(trayNum);
                }
                mansFood.SetActive(false);
                if(!com.trayController.instantiateItem(trayNum, completeFoodObj, foodName))
                {
                    com.vc.setErrorMessage("これ以上トレーにのせられません。");
                    // 復元
                    if (foodName == AppConst.JOB_POTATO)
                    {
                        com.potatoController.restration();
                    }
                    else if (foodName == AppConst.JOB_DONUT)
                    {
                        com.donucon.restration();
                    }
                    else if (foodName == AppConst.JOB_ONIONRING)
                    {
                        com.onicon.restration();
                    }
                    else if (foodName == AppConst.JOB_FRIEDCHICKEN)
                    {
                        com.chiccon.restration();
                    }
                    else if (foodName == AppConst.JOB_RIB)
                    {
                        com.ribcon.restration();
                    }
                    else if (foodName == AppConst.JOB_BIGFRIEDCHICKEN)
                    {
                        com.bchicon.restration();
                    }
                }
                clear();
                setTrayFalse();
                deleteJobSlot(false);
            }
        }
    }

    //仕事：冷凍ハンバーグを冷凍庫から取り出す
    void moveFreezHamburg()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.reizoukoPosition.position);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                openDoor();
                transform.position += new Vector3(0, 0, -0.4f);
                look(180);
                ani.SetInteger("ID", 6);
                stateEnter = true;
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 3f * workCoefficient);
            if (stateTime > 3 * workCoefficient)
            {

                stateEnter = false;
                stateTime = 0f;
                mansFreezHamburg.SetActive(true);
                jobSlot[0, 1] = AppConst.JOB_BAKEHAMBURG;
                com.locacon.setReizoukoFalse();
            }
        }
    }

    //仕事：ハンバーグを焼く
    void bakeHamburg()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            closeDoor();
            agent.SetDestination(com.bakePosition.transform.position);
            ani.SetInteger("ID", 7);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                agent.enabled = false;
                com.hamcon.createBakeHamburg("freez");
                ani.SetInteger("ID", 0);
                look(0);
                mansKaeshi.SetActive(true);
                mansFreezHamburg.SetActive(false);
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            if (stateTime < 10 * workCoefficient)
            {
                updateGage(stateTime, 10 * workCoefficient);
            }
            else
            {
                updateGage(stateTime - 10, 15 * workCoefficient - 10);
            }
            if (stateTime > 8 * workCoefficient && com.hamcon.hamburgChangeFlag)
            {
                //生焼けハンバーグを焼けハンバーグと入れかえ
                com.hamcon.destroyFreezHamburg();
                com.hamcon.createBakeHamburg("bake");
                com.hamcon.hamburgChangeFlag = false;
            }
            if (stateTime > 10 * workCoefficient)
            {
                ani.SetInteger("ID", 4);
            }

            //焼けハンバーグをグリルから移動
            if (stateTime > 15 * workCoefficient)
            {
                stateTime = 10 * workCoefficient;
                com.hamcon.destroyBakeHamburg();
                com.hamcon.createBakedHamburg();

                if (com.hamcon.getHamburgNum() == com.hamcon.getHamburgCreateNum())
                {
                    //  店員のカエシを非表示
                    mansKaeshi.SetActive(false);

                    //ハンバーグ焼き行程が終了したことを設定
                    com.hamcon.createHamburgEnd();

                    //優先仕事が終了したことを設定
                    deleteJobSlot(true);

                    stateEnter = false;
                    stateTime = 0f;

                    ani.SetInteger("ID", 0);

                }
            }
        }
    }

    //仕事：ハンバーガー生成
    void getHamburger()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.bakePosition.position);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                mansTong.SetActive(true);
                look(0);
                ani.SetInteger("ID", 11);
                stateEnter = true;
            }
        }
        else
        {
            stateTime += Time.deltaTime;
            updateGage(stateTime, 5 * workCoefficient);
            if (stateTime > 5 * workCoefficient)
            {
                com.hamcon.useHamburg();
                mansTong.SetActive(false);
                mansHamburger.SetActive(true);
                clear();
                foodName = jobSlot[0, 1];
                jobSlot[0, 1] = AppConst.JOB_MOVEHAMBURGER;
                com.locacon.setBakeFalse();
                waitFlag = checkLocationUseState();
                priorityCheckFlg = false;
            }
        }

    }

    //仕事：ハンバーガーをトレーに移動
    void moveHamburger()
    {
        if (!stateEnter)
        {
            agent.enabled = true;
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            agent.SetDestination(com.trayController.getTrayPosition(trayNum));
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                agent.enabled = false;
                look(90);
                ani.SetInteger("ID", 9);
                stateEnter = true;
            }
        }
        else
        {
            stateTime += Time.deltaTime;

            if (stateTime > 1)
            {
                if (com.trayController.getTray(trayNum) == null)
                {
                    com.trayController.instantiateTray(trayNum);
                }
                if(!com.trayController.instantiateItem(trayNum, com.hamburger, foodName))
                {
                    com.vc.setErrorMessage("これ以上トレーにのせられません。");

                    // 復元
                    com.hamcon.restration();
                }
                mansHamburger.SetActive(false);
                clear();
                setTrayFalse();
                deleteJobSlot(false);
            }
        }

    }

    /// <summary>
    /// 
    /// 納品
    /// </summary>
    void moveTray()
    {
        agent.enabled = true;
        if (!stateEnter)
        {
            ani.SetInteger("ID", 0);
            ani.SetFloat("PlayerSpeed", agent.velocity.magnitude);
            // トレー置き場まで移動
            agent.SetDestination(com.trayController.getTrayPosition(trayNum));
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                stateEnter = true;
                look(90);

                // トレーを取得
                if (com.trayController.getTray(trayNum) == null) com.trayController.instantiateTray(trayNum);
                com.trayController.getTray(trayNum).transform.SetParent(transform);
                com.trayController.getTray(trayNum).transform.localPosition = new Vector3(-0.11f, 1.23f, 0.453f);
                com.trayController.unSuccessTray(trayNum);
                ani.SetInteger("ID", 12);

            }
        }
        else
        {
            Vector3 counterPosition = counter.transform.position;
            counterPosition.x -= 0.5f;
            agent.SetDestination(counterPosition);
            if (agent.remainingDistance <= 0.01f && !agent.pathPending)
            {
                GameObject t = transform.GetChild(transform.childCount-1).gameObject;
                t.transform.SetParent(counter.transform);
                t.transform.localPosition = new Vector3(0, 1.021f, 0);

                clear();
                agent.enabled = false;
                counter = null;
                ani.SetInteger("ID", 0);
                setTrayFalse();
                deleteJobSlot(false);
            }
        }

    }

    //方向転換
    void look(float ang)
    {
        jobSpinner.targetUi.SetActive(false);
        Vector3 angle = transform.eulerAngles;
        angle.y = ang;
        transform.eulerAngles = angle;
        jobSpinner.targetUi.SetActive(true);
    }

    //ジョブに応じてuseを使い分ける
    void useAndSetObject()
    {
        string job = jobSlot[0, 1];
        if (job == AppConst.JOB_POTATO)
        {
            com.potatoController.usePotato();
            mansFood = mansPotato;
            completeFoodObj = com.boxInPotato;

        }
        else if (job == AppConst.JOB_DONUT)
        {
            com.donucon.useDonut();
            mansFood = mansDonut;
            completeFoodObj = com.donut;
        }
        else if (job == AppConst.JOB_ONIONRING)
        {
            com.onicon.useOnion();
            mansFood = mansOnion;
            completeFoodObj = com.onion;
        }
        else if (job == AppConst.JOB_FRIEDCHICKEN)
        {
            com.chiccon.useChicken();
            mansFood = mansFriedChicken;
            completeFoodObj = com.friedChicken;
        }
        else if (job == AppConst.JOB_RIB)
        {
            com.ribcon.useRib();
            mansFood = mansrib;
            completeFoodObj = com.rib;
        }
        else if (job == AppConst.JOB_BIGFRIEDCHICKEN)
        {
            com.bchicon.useBigChicken();
            mansFood = mansBigFriedChicken;
            completeFoodObj = com.bigFriedChicken;
        }
    }



    void fixedAngle()
    {
        var direction = mansClosedCup.transform.position;
        direction.x = 180;
        direction.z = 0;
        var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        mansClosedCup.transform.rotation = Quaternion.Lerp(mansClosedCup.transform.rotation, lookRotation, 0.1f);
    }



    //揚げ物系ジョブの位置を設定
    void setFryFoodInfo()
    {
        if (jobSlot[0, 1] == AppConst.JOB_DONUT)
        {
            frierPosition = com.frier1Position.position + new Vector3(0, 0, -0.621f);
            friedStationPoint = com.friedStation1Position.position + new Vector3(0, 0, -0.621f);
            frierName = "frier1";
        }
        else if (jobSlot[0, 1] == AppConst.JOB_POTATO)
        {
            frierPosition = com.frier1Position.position;
            friedStationPoint = com.friedStation1Position.position;
            frierName = "frier1";

        }
        else if (jobSlot[0, 1] == AppConst.JOB_ONIONRING)
        {
            frierPosition = com.frier1Position.position + new Vector3(0, 0, -1.242f);
            friedStationPoint = com.friedStation1Position.position + new Vector3(0, 0, 0.621f);
            frierName = "frier1";
        }
        else if (jobSlot[0, 1] == AppConst.JOB_FRIEDCHICKEN)
        {
            frierPosition = com.frier2Position.position + new Vector3(0, 0, -0.621f);
            friedStationPoint = com.friedStation2Position.position;

            frierName = "frier2";
        }
        else if (jobSlot[0, 1] == AppConst.JOB_RIB)
        {
            frierPosition = com.frier2Position.position;
            friedStationPoint = com.friedStation2Position.position + new Vector3(0, 0, 0.621f);
            frierName = "frier2";

        }
        else if (jobSlot[0, 1] == AppConst.JOB_BIGFRIEDCHICKEN)
        {
            frierPosition = com.frier2Position.position + new Vector3(0, 0, -1.242f);
            friedStationPoint = com.friedStation2Position.position + new Vector3(0, 0, -0.621f);
            frierName = "frier2";
        }
    }

    //トレー名に応じてトレーの使用をオフにする
    void setTrayFalse()
    {
        //Debug.Log("trayNumberは" + trayNumber + "です");
        if (trayNum == 1)
        {
            com.locacon.setTrayFalse();
            return;
        }

        if (trayNum == 2)
        {
            com.locacon.setTray2False();
            return;
        }

        if (trayNum == 3)
        {
            com.locacon.setTray3False();
            return;
        }
    }



    void openDoor()
    {
        Transform tran = com.door.transform;
        Vector3 angle = tran.eulerAngles;
        angle.y = 120.0f;
        tran.eulerAngles = angle;
    }

    void closeDoor()
    {
        Transform tran = com.door.transform;
        Vector3 angle = tran.eulerAngles;
        angle.y = 0f;
        tran.eulerAngles = angle;
    }



    //仕事終わりの初期化
    void clear()
    {
        waitFlag = true;
        stateEnter = false;
        stateTime = 0f;

    }

    //レベルアップ時の反映処理
    public void levelup()
    {
        //歩行レベルの反映
        agent.acceleration = 8 + 17 / 30 * walkSpeedLevel;
        agent.angularSpeed = 120 + 230 / 30 * walkSpeedLevel;
        agent.speed = 1 + 3 / 30 * walkSpeedLevel;
        ani.SetFloat("walkSpeed", 1f + 1.5f / 30f * walkSpeedLevel);

        //作業レベルの反映
        ani.SetFloat("workSpeed", 1f + 1.5f / 30f * workSpeedLevel);
        workCoefficient = 1f - 0.5f / 30 * workSpeedLevel;

        //スタミナレベル
        sutamina = 300f + 700 / 30 * sutaminaLevel;

        // レベルに応じてスライダーのサイズを変える
        sutaminaSlider.targetUi.GetComponent<RectTransform>().sizeDelta = new Vector2(sutamina / 5, 10);

    }

    //自身のUIゲージを更新
    void updateGage(float now, float end)
    {
        if (now >= end)
        {
            jobSpinner.targetUi.transform.GetChild(0).GetComponent<Image>().fillAmount = 0f;
        }
        else
        {
            jobSpinner.targetUi.transform.GetChild(0).GetComponent<Image>().fillAmount = now / end;
        }

    }

    //店員がクリックされたとき
    private void OnMouseDown()
    {

        if (renderer.material.color == originalColor)
        {
            renderer.material.color = Color.yellow;
            com.selectStaff = gameObject;
            com.salesPersonController.selectStaff(name);
        }
        else
        {
            renderer.material.color = originalColor;
            com.selectStaff = null;
            com.salesPersonController.selectStaff(name);
        }

    }

    //view側で店員が選択された時用
    public void viewSelect()
    {
        OnMouseDown();
    }

    /// <summary>
    /// 色をもとに戻す
    /// </summary>
    public void setRenderColor(string salesPersonName)
    {
        renderer.material.color = originalColor;

        if(com.selectStaff.name != salesPersonName)
        {
            com.selectStaff = null;
        }
    }

    //仕事に応じた場所の使用を確認し、使用中でなければ処理をして結果を返す
    bool checkLocationUseState()
    {
        Debug.Log("場所の使用状況の確認をします。job = " + jobSlot[0, 1]);

        //レジの場所確認
        if (jobSlot[0, 1] == AppConst.JOB_REJI && !com.locacon.getRejiBool())
        {
            com.locacon.setRejiTrue();
            return false;
        }


        //トレーの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_TRAY || jobSlot[0, 1] == AppConst.JOB_MOVEDRINK || jobSlot[0, 1] == AppConst.JOB_MOVEFRYFOOD || jobSlot[0, 1] == AppConst.JOB_MOVEHAMBURGER || jobSlot[0, 1] == AppConst.JOB_MOVETRAY)
        {
            if (trayNum == 1 && !com.locacon.getTrayBool())
            {
                com.locacon.setTrayTrue();
                return false;
            }
            if (trayNum == 2 && !com.locacon.getTray2Bool())
            {
                com.locacon.setTray2True();
                return false;
            }
            if (trayNum == 3 && !com.locacon.getTray3Bool())
            {
                com.locacon.setTray3True();
                return false;
            }
            return true;
        }


        //ハンバーグ生成場所の確認
        if (jobSlot[0, 1] == AppConst.JOB_SMALLHAMBURGER)
        {
            if (!com.locacon.getBakeBool())
            {
                Debug.Log("bakeは空き状態なので使用中にセットします。");
                com.locacon.setBakeTrue();
                return false;
            }
            return true;
        }

        //ドリンク生成場所の確認
        if (jobSlot[0, 1] == AppConst.JOB_DRINK && !com.locacon.getDrinkBool())
        {
            com.locacon.setDrinkTrue();
            return false;
        }

        //ポテトの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_POTATO && !com.locacon.getPotatoFriedStationBool())
        {
            com.locacon.setPotatoFriedStationTrue();
            return false;
        }

        //ドーナツの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_DONUT && !com.locacon.getDonutFriedStationBool())
        {
            com.locacon.setDonutFriedStationTrue();
            return false;
        }

        //オニオンの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_ONIONRING && !com.locacon.getOnionFriedStationBool())
        {
            com.locacon.setOnionFriedStationTrue();
            return false;
        }

        //フライドチキンの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_FRIEDCHICKEN && !com.locacon.getFriedChickenFriedStationBool())
        {
            com.locacon.setFriedChickenFriedStationTrue();
            return false;
        }

        //リブの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_RIB && !com.locacon.getRibFriedStationBool())
        {
            com.locacon.setRibFriedStationTrue();
            return false;
        }

        //ビッグフライドチキンの生成場所確認
        if (jobSlot[0, 1] == AppConst.JOB_BIGFRIEDCHICKEN && !com.locacon.getBigFriedChickenFriedStationBool())
        {
            com.locacon.setBigFriedChickenFriedStationTrue();
            return false;
        }

        //場所が取れなかったときfalse
        return true;
    }

    //仕事に応じて優先仕事が必要かどうか判断しあれば、優先仕事を返す
    public void getNeedPriorityJob()
    {
        Debug.Log("優先仕事が必要かどうかの判断に入りました。仕事 = " + jobSlot[0, 0]);
        //ハンバーガー
        if (jobSlot[0, 1] == AppConst.JOB_SMALLHAMBURGER)
        {
            if (com.hamcon.getHamburgNum() == 0 && !com.hamcon.getCreateHamburgState())
            {
                interruptJob(AppConst.JOB_MOVEFREEZHAMBURG);
                com.hamcon.createHamburgStart();
                Debug.Log("moveFreezHamburgに入りました。");
                Debug.Log("ハンバーグ生成の優先仕事が必要です。");
                return;
            }
        }

        //ポテト
        if (jobSlot[0, 1] == AppConst.JOB_POTATO && com.potatoController.getPotatoNum() == 0 && !com.potatoController.getCreatePotatoState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezPotato");

            //店員にドーナツの冷凍食品をセット
            mansFreezFood = mansFreezPoteto;

            //店員にネットオブジェクトのセット
            mansNetWithFood = mansNetInPoteto;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInPoteto;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(-0.671f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.potatoController.createPotatoStart();

            Debug.Log("getFreezPotatoに入りました。");
            return;
        }

        //ドーナツ
        if (jobSlot[0, 1] == AppConst.JOB_DONUT && com.donucon.getDonutNum() == 0 && !com.donucon.getCreateDonutState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezFryFood");

            //店員にポテトの冷凍食品をセット
            mansFreezFood = mansFreezDonut;

            mansNetWithFood = mansNetInDonut;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInDonut;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(0f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.donucon.createDonutStart();

            Debug.Log("getFreezDonutに入りました。");
            return;
        }

        //オニオン
        if (jobSlot[0, 1] == AppConst.JOB_ONIONRING && com.onicon.getOnionNum() == 0 && !com.onicon.getCreateOnionState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezFryFood");

            //店員にポテトの冷凍食品をセット
            mansFreezFood = mansFreezOnion;

            mansNetWithFood = mansNetInOnion;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInOnion;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(0.671f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.onicon.createOnionStart();

            Debug.Log("getFreezOnionに入りました。");
            return;
        }

        //フライドチキン
        if (jobSlot[0, 1] == AppConst.JOB_FRIEDCHICKEN && com.chiccon.getChickenNum() == 0 && !com.chiccon.getCreateChickenState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezFryFood");

            //店員にポテトの冷凍食品をセット
            mansFreezFood = mansFreezChicken;

            mansNetWithFood = mansNetInChicken;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInChicken;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(0f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.chiccon.createChickenStart();

            Debug.Log("getFreezChickenに入りました。");
            return;
        }

        //リブ
        if (jobSlot[0, 1] == AppConst.JOB_RIB && com.ribcon.getRibNum() == 0 && !com.ribcon.getCreateRibState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezFryFood");

            //店員にポテトの冷凍食品をセット
            mansFreezFood = mansFreezRib;

            mansNetWithFood = mansNetInRib;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInRib;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(-0.671f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.ribcon.createRibStart();

            Debug.Log("getFreezRibに入りました。");
            return;
        }

        //ビッグフライドチキン
        if (jobSlot[0, 1] == AppConst.JOB_BIGFRIEDCHICKEN && com.bchicon.getBigChickenNum() == 0 && !com.bchicon.getCreateBigChickenState())
        {
            //優先度高いジョブのセット
            interruptJob("moveFreezFryFood");

            //店員にポテトの冷凍食品をセット
            mansFreezFood = mansFreezBigChicken;

            mansNetWithFood = mansNetInBigChicken;

            //ネットオブジェクトにネットに入ったポテトのオブジェクトを入れる
            netInObject = com.netInBigChicken;

            //ネットオブジェクトの位置を設定
            netInObjectPosition = new Vector3(0.671f, 1.116f, 0.439f);

            //potatoControllerに作成を知らせる。
            com.bchicon.createBigChickenStart();

            Debug.Log("getFreezBigChickenに入りました。");
            return;
        }

        Debug.Log("優先仕事は不要の結果になりました。");
    }


    //優先仕事に応じた場所の使用を確認し、使用中でなければ処理をして結果を返す
    bool checkPriorityJobOfLocationUseState()
    {
        Debug.Log("優先仕事：" + jobSlot[0, 1] + "を開始するために場所の使用状況を確認します。");

        //ハンバーグ
        if (jobSlot[0, 1] == AppConst.JOB_MOVEFREEZHAMBURG && !com.locacon.getReizoukoBool() && !com.locacon.getBakeBool())
        {
            Debug.Log("場所は空いているので使用中に設定しました。");
            com.locacon.setReizoukoTrue();
            com.locacon.setBakeTrue();
            return false;
        }

        //ポテト
        if (foodName == AppConst.JOB_POTATO)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getPotatoFrierBool() && !com.locacon.getPotatoFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setPotatoFrierTrue();
                com.locacon.setPotatoFriedStationTrue();
                return false;
            }

            return true;
        }

        //ドーナツ
        if (foodName == AppConst.JOB_DONUT)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getDonutFrierBool() && !com.locacon.getDonutFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setDonutFrierTrue();
                com.locacon.setDonutFriedStationTrue();
                return false;
            }
            return true;
        }

        //オニオン
        if (foodName == AppConst.JOB_ONIONRING)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getOnionFrierBool() && !com.locacon.getOnionFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setOnionFrierTrue();
                com.locacon.setOnionFriedStationTrue();
                return false;
            }
            return true;
        }

        //フライドチキン
        if (foodName == AppConst.JOB_FRIEDCHICKEN)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getFriedChickenFrierBool() && !com.locacon.getFriedChickenFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setFriedChickenFrierTrue();
                com.locacon.setFriedChickenFriedStationTrue();
                return false;
            }
            return true;
        }

        //リブ
        if (foodName == AppConst.JOB_RIB)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getRibFrierBool() && !com.locacon.getRibFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setRibFrierTrue();
                com.locacon.setRibFriedStationTrue();
                return false;
            }
            return true;
        }

        //bigフライドチキン
        if (foodName == AppConst.JOB_BIGFRIEDCHICKEN)
        {
            if (!com.locacon.getReizoukoBool() && !com.locacon.getBigFriedChickenFrierBool() && !com.locacon.getBigFriedChickenFriedStationBool())
            {
                com.locacon.setReizoukoTrue();
                com.locacon.setBigFriedChickenFrierTrue();
                com.locacon.setBigFriedChickenFriedStationTrue();
                return false;
            }
            return true;
        }

        Debug.Log("場所は使用中のため待機状態に設定します。");
        return true;

    }


    /// <summary>
    ///  jobセット
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="mode"></param>
    /// <param name="trayNum"></param>
    public bool setJobSlot(string jobName, int mode, string trayNum = "")
    {

        for (int i = 0; i < slotNumber; i++)
        {

            if(!checkJobExecute(jobName))
            {
                // エラーメッセージを表示
                com.vc.setErrorMessage("生産設備が不足しています。");
                return false;
            }

            if (jobSlot[i, 0] == null)
            {
                //get系のジョブ
                if (mode == 0)
                {
                    jobSlot[i, 0] = com.trayController.getSelectedTray().ToString();
                    com.trayController.setTrayItemImage(int.Parse(jobSlot[i, 0]), jobName);


                }
                else if (mode == 1)
                {

                    // 準備系のジョブ
                    jobSlot[i, 0] = "0";
                    preparation = preparationJobSet(jobName);
                }
                else if (mode == 2)
                {
                    //納品
                    jobSlot[i, 0] = trayNum;

                    //場所チェック
                    waitFlag = checkLocationUseState();
                }

                jobSlot[i, 1] = jobName;
                com.salesPersonController.inputJobImage(name, jobName, i);
                setSpinnerImage();
                return true;
            }
        }

        return false;
    }



    //jobの割り込み
    private void interruptJob(string job)
    {
        getLog();
        for (int i = slotNumber; i > 0; i--)
        {
            jobSlot[i, 0] = jobSlot[i - 1, 0];
            jobSlot[i, 1] = jobSlot[i - 1, 1];

        }
        jobSlot[0, 1] = job;
        com.salesPersonController.interruptJobImage(name, job);
        setSpinnerImage();
        Debug.Log("jobSlot割り込み");
        getLog();

    }

    /// <summary>
    /// jobSlotから作業したジョブを削除する
    /// </summary>
    private void deleteJobSlot(bool preparation)
    {
        // 準備作業の時はトレーを確定しない
        if (!preparation)
        {
            // トレー上のアイテムを確定する
            com.trayController.setTrayItemImageConfirm(trayNum, foodName);
        }
       
        for (int i = 0; i + 1 < slotNumber + 1; i++)
        {
            jobSlot[i, 0] = jobSlot[i + 1, 0];
            jobSlot[i, 1] = jobSlot[i + 1, 1];
            jobSlot[i + 1, 0] = null;
            jobSlot[i + 1, 1] = null;
        }
        com.salesPersonController.deleteJobImage(name);
        setSpinnerImage();
        Debug.Log("jobSlot削除");
        getLog();

    }

    //スピナー上の画像を設定
    void setSpinnerImage()
    {
        Image i = jobSpinner.targetUi.transform.GetChild(1).GetComponent<Image>();
        i.sprite = com.salesPersonController.getSpinnerImage(name);

        if (i.sprite == null)
        {
            i.gameObject.SetActive(false);
        }
        else
        {
            i.gameObject.SetActive(true);
        }
    }


    //自身の色を変更する
    public void setRenderColor()
    {
        renderer.material.color = originalColor;
    }

    void getLog()
    {
        Debug.Log("jobSlot[0,1] : " + jobSlot[0, 1]);
        Debug.Log("jobSlot[1,1] : " + jobSlot[1, 1]);
    }

    // 準備作業の開始合図
    bool preparationJobSet(string jobName)
    {
        if (jobName == AppConst.JOB_MOVEFREEZHAMBURG && !com.hamcon.getCreateHamburgState())
        {
            com.hamcon.createHamburgStart();
            return false;
        }
        else if (jobName == AppConst.JOB_MOVEFREEZPOTATO && !com.potatoController.getCreatePotatoState())
        {
            com.potatoController.createPotatoStart();
            return false;
        }
        else if (jobName == AppConst.JOB_MOVEFREEZDONUT && !com.donucon.getCreateDonutState())
        {
            com.donucon.createDonutStart();
            return false;
        }
        else if (jobName == AppConst.JOB_MOVEFREEZONION && !com.onicon.getCreateOnionState())
        {
            com.onicon.createOnionStart();
        }
        else if (jobName == AppConst.JOB_MOVEFREEZFRIEDCHICKEN && !com.chiccon.getCreateChickenState())
        {
            com.chiccon.getCreateChickenState();
            return false;
        }
        else if (jobName == AppConst.JOB_MOVEFREEZRIB && !com.ribcon.getCreateRibState())
        {
            com.ribcon.createRibStart();
            return false;
        }
        else if (jobName == AppConst.JOB_BIGFRIEDCHICKEN && !com.bchicon.getCreateBigChickenState())
        {
            com.bchicon.createBigChickenStart();
            return false;
        }

        return true;
    }

    // スタミナスライダー制御
    private void sliderControl()
    {
        // スタミナ最大値の取得
        float maxSutamina = 300 + 700 / 30 * sutaminaLevel;

        sutaminaSlider.targetUi.GetComponent<Slider>().value = sutamina / maxSutamina;

    }

    // 指示されたジョブが実行可能か確認
    private bool checkJobExecute(string jobName)
    {
        if (jobName == AppConst.JOB_FRIEDCHICKEN || 
            jobName == AppConst.JOB_RIB || 
            jobName == AppConst.JOB_BIGFRIEDCHICKEN || 
            jobName == AppConst.JOB_MOVEFREEZFRIEDCHICKEN || 
            jobName == AppConst.JOB_MOVEFREEZRIB || 
            jobName == AppConst.JOB_MOVEFREEZBIGFRIEDCHICKEN)

        {

            if(!com.friedStation2.activeInHierarchy || !com.frier2.activeInHierarchy)
            {
                return false;
            }
        }

        return true;
    }



}
