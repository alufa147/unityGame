using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class customerController : MonoBehaviour
{
    //客の車オブジェクト
    public GameObject car;

    terraceController teracon;

    public string customerName = "";
    Vector3 endPosition;
    chumonController cc;
    State currentState = State.wait;
    bool stateEnter = false;
    waitPositionController wpc;
    NavMeshAgent agent;
    Animator ani;
    bool firstSetWaitCustomer = false;
    string[] menu;
    private int exp;

    DBManager db;
    levelController lc;
    private int amountOfMoney = 0;
    private int materialCost = 0;

    float stateTime = 0f;
    common com;

    GameObject takeoutObject;

    bool takeout;
    GameObject terrace;

    private string myOrder = "";
    private GameObject myTray;

    float waitTime = 0;

    private int trayNum = 0;

    public objectMarker customerComent;

    int orderNum = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        customerComent.initialize(this.transform);
        com = GameObject.FindWithTag("common").GetComponent<common>();
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        cc = GameObject.Find("chumonController").GetComponent<chumonController>();
        currentState = State.gotoWaitCtr;
        wpc = GameObject.Find("waitPositionController").GetComponent<waitPositionController>();
        db = new DBManager();
        teracon = GameObject.FindWithTag("terraceController").GetComponent<terraceController>();
        DBManager.Menu m = (DBManager.Menu)db.getData(AppConst.MENU_FILE_NAME);
        if (m == null)
        {
            //初期メニューの設定
            db.setMenu(AppConst.JOB_SMALLHAMBURGER);
            db.setMenu(AppConst.JOB_POTATO);
            db.setMenu(AppConst.JOB_DRINK);
            m = new DBManager.Menu();
            m.menuName.Add(AppConst.JOB_SMALLHAMBURGER);
            m.menuName.Add(AppConst.JOB_POTATO);
            m.menuName.Add(AppConst.JOB_DRINK);
        }
        menu = m.menuName.ToArray();
        
        lc = GameObject.FindWithTag("levelController").GetComponent<levelController>();
        endPosition = this.transform.position;

        //takeoutするかしないか
        if (Random.Range(0, 3) < 0)
        {
            takeout = true;
        }
        else
        {
            takeout = false;
        }

        //持ち帰り用のオブジェクト取得
        takeoutObject = transform.GetChild(11).gameObject;
    }

    enum State
    {
        gotoWaitCtr,
        chumon,
        wait,
        eat,
        goHome,
        gotoCounter
    }

    void changeState(State newState)
    {
        ani.SetInteger("ID", 0);
        currentState = newState;
        stateEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (com.pose) return;
        if (ani != null)
        {
            if (!stateEnter)
            {
                float speed = agent.velocity.magnitude;
                ani.SetFloat("PlayerSpeed", speed);
            }

        }


        switch (currentState)
        {
            case State.gotoWaitCtr:
                agent.SetDestination(wpc.getWaitCtrPosition().position);
                if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                {
                    //Debug.Log("客：waitCtrPositionに到着しました。");
                    wpc.setRejiWaitCustomer(name);
                    currentState = State.chumon;
                }
                break;

            case State.chumon:
                if (!stateEnter)
                {
                    agent.SetDestination(wpc.getRejiWaitPosition(name).position);
                    if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                    {
                        
                        
                        //Debug.Log("客：rejiPositionに到着しました。");
                        if (wpc.checkMyRejiPosition(name) == 0)
                        {
                            stateEnter = true;
                            agent.enabled = false;
                            look(270);
                        }
                        if (!firstSetWaitCustomer)
                        {
                            firstSetWaitCustomer = true;
                        }
                    }
                }
                if (stateEnter)
                {

                    cc.setRejiCustomer(GetComponent<customerController>());
                }
                break;

            case State.wait:
                if (!stateEnter)
                {
                    agent.SetDestination(wpc.getWaitPosition(name).position);
                    if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                    {
                        stateEnter = true;
                        agent.enabled = false;
                    }
                }
                else
                {
                    stateTime += Time.deltaTime;
                    if (waitTime < stateTime)
                    {
                        if (myTray != null)
                        {
                            agent.enabled = true;
                            changeState(State.gotoCounter);
                            waitTime = Random.Range(1, 10) / 5;
                            stateTime = 0f;
                        }
                        
                    }
                }


                break;

            case State.gotoCounter:
                if (!stateEnter)
                {
                    Vector3 trayPosition = myTray.transform.position;
                    trayPosition.x += 1.5f;
                    agent.SetDestination(trayPosition);
                    if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                    {
                        // オーダーviewから削除する
                        com.orderViewController.removeScrollContents(name);

                        // コメントをhidden
                        customerComent.targetUi.SetActive(false);

                        look(270);
                        ani.SetInteger("ID", 12);
                        if (takeout)
                        {
                            Destroy(myTray);
                            takeoutObject.SetActive(true);
                            stateEnter = true;
                        }
                        else
                        {
                            changeState(State.eat);
                            myTray.transform.SetParent(transform);
                            myTray.transform.localPosition = new Vector3(-0.11f, 1.23f, 0.48f);
                            terrace = teracon.getFreeChair();
                            getOrderNum();
                        }

                        com.counterController.setFreePosition(trayNum);
                        wpc.removeWaitPosition(name);
                        com.procon.setFund(amountOfMoney);
                        com.procon.setMaterialCost(materialCost);
                        lc.setExp(exp);
                    }
                } else
                {
                    agent.SetDestination(endPosition);
                    if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                    {
                        if (car != null)
                        {
                            Debug.Log("車まで到着");
                            car.GetComponent<carController>().setGoHome();
                        }

                        Destroy(this.gameObject);
                    }
                }
                
                break;

            case State.eat:
                
                if (terrace == null)
                {
                    stateTime += Time.deltaTime;
                    if(stateTime > 2)
                    {
                        stateTime = 0;
                        terrace = teracon.getFreeChair();
                    }
                    return;
                }
                if (!stateEnter)
                {
                    ani.SetInteger("ID", 12);
                    agent.SetDestination(terrace.transform.position);
                    if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                    {
                        agent.enabled = false;
                        ani.SetInteger("ID", 13);
                        stateEnter = true;
                        sittingTuning(terrace.transform.eulerAngles.y);
                        look(terrace.transform.eulerAngles.y);
                        myTray.transform.localPosition = new Vector3(-0.11f, 1.23f, 0.2f);
                        myTray.transform.SetParent(transform.parent);
                        Vector3 vec = myTray.transform.position;
                        vec.y = 0.887f;
                        myTray.transform.position = vec;
                    }
                } else
                {
                    stateTime += Time.deltaTime;
                    if(stateTime > orderNum * 60f)
                    {
                        changeState(State.goHome);
                        teracon.setChairStatus(terrace);
                        agent.enabled = true;
                        Destroy(myTray);
                        look(90);
                    }
                }
                
                break;

            case State.goHome:

                agent.SetDestination(endPosition);
                if (agent.remainingDistance <= 0.01f && !agent.pathPending)
                {
                    if (car != null)
                    {
                        Debug.Log("車まで到着");
                        car.GetComponent<carController>().setGoHome();
                    }

                    Destroy(this.gameObject);
                }
                break;

        }
    }


    //注文を生成
    void createChumon()
    {
        DBManager.UpgradedData upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);

        //注文を入れる配列
        string[] chumon = new string[6];

        //int elementCount = Random.Range(2, 5);
        int elementCount = Random.Range(1, com.canOrderNum+1);
        int i = 0;

        Dictionary<string, int> orderCount = new Dictionary<string, int>();
        orderCount.Add(AppConst.JOB_SMALLHAMBURGER, 0);
        orderCount.Add(AppConst.JOB_DRINK, 0);
        orderCount.Add(AppConst.JOB_POTATO, 0);
        orderCount.Add(AppConst.JOB_DONUT, 0);
        orderCount.Add(AppConst.JOB_ONIONRING, 0);
        orderCount.Add(AppConst.JOB_FRIEDCHICKEN, 0);
        orderCount.Add(AppConst.JOB_RIB, 0);
        orderCount.Add(AppConst.JOB_BIGFRIEDCHICKEN, 0);

        while (i < elementCount)
        {
            
            string m = menu[Random.Range(0, menu.Length)];
            Debug.Log(m + " menu.length" + menu.Length);

            if (m == AppConst.JOB_FRIEDCHICKEN || m == AppConst.JOB_RIB || m == AppConst.JOB_BIGFRIEDCHICKEN)
            {
                if (!upd.frier2 || !upd.friedStation2)
                {
                    continue;
                }
            }

            orderCount[m] = orderCount[m] + 1;
            chumon[i] = m;
            int[] checkUp = sharedFunction.checkUpExp(chumon[i]);
            amountOfMoney += checkUp[0];
            exp += checkUp[1];
            materialCost += checkUp[2];
            i++;
        }
        viewController vw = GameObject.FindWithTag("viewController").GetComponent<viewController>();
        vw.setInfoMessage(sharedFunction.getInfoMessage(chumon));

        //照合キーの作成
        foreach(string key in orderCount.Keys)
        {
            myOrder += orderCount[key];
        }

        //コメントの生成
        setComent(chumon);

        // 注文viewに追加
        com.orderViewController.addScrollContents(name, chumon);
    }

    //注文を渡す
    public string getChumon()
    {
        wpc.removeRejiWaitPosition(name);
        wpc.setWaitCustomer(name);
        changeState(State.wait);
        agent.enabled = true;
        createChumon();
        return myOrder;
    }

    //方向転換
    void look(float ang)
    {
        Vector3 angle = transform.eulerAngles;
        angle.y = ang;
        transform.eulerAngles = angle;
    }

    //座った時の位置微調整
    void sittingTuning(float vector)
    {
        Vector3 nowTransform = terrace.transform.position;
        const float n = 0.43f;
        //Debug.Log("微調整します。vector = " + vector);
        if(vector <= 10f)
        {
            nowTransform.z += n;
            //Debug.Log("微調整1" + nowTransform);
            //Debug.Log("微調整します。" + nowTransform + " + " + n);
        } else if(vector <= 100f)
        {
            nowTransform.x += n;
            //Debug.Log("微調整2" + nowTransform);
        } else if(vector <= 190f)
        {
            nowTransform.z += -n;
            //Debug.Log("微調整3" + nowTransform);
        } else if(vector <= 280f)
        {
            nowTransform.x += -n;
            //Debug.Log("微調整4" + nowTransform);
        }

        //Debug.Log(terrace.transform.position);

        transform.position = nowTransform;
    }



    //トレーセット
    public void setTray(GameObject tray, int num)
    {
        myTray = tray;
        trayNum = num;
    }

    //orderを渡す
    public string getMyOrder()
    {
        return myOrder;
    }

    //comentを設定する
    private void setComent(string[] order)
    {
        customerComent.targetUi.SetActive(true);
        Debug.Log(order);
        int cnt = 0;
        for(int i = 0; i < order.Length; i++)
        {
            if (order[i] == null) continue;
            Image im = customerComent.targetUi.transform.GetChild(i).GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + order[i]);
            im.gameObject.SetActive(true);
            cnt++;
        }
        setComentSize(cnt);
    }

    //要素数に応じてコメントサイズを調整する
    private void setComentSize(int size)
    {
        Debug.Log(size);
        customerComent.targetUi.GetComponent<RectTransform>().sizeDelta += new Vector2(46 * (size-1), 0);
    }

    /// <summary>
    /// オーダー数を返す
    /// </summary>
    /// <returns>オーダー数</returns>
    public int getOrderNum()
    {

        // オーダーを一文字ずつ切り出す
        char[] orderArr = myOrder.ToCharArray();

        foreach(char order in orderArr)
        {
            //Debug.Log(" orderNum : " + orderNum + "  order : " + order);
            orderNum += (int)System.Char.GetNumericValue(order);
        }

        //Debug.Log("orderNum " + orderNum);
        return orderNum;
    }
    
}
