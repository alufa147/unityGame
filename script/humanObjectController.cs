using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanObjectController : MonoBehaviour
{
    public GameObject tenin;

    public GameObject customer;

    DBManager db;
    common com;

    GameObject[] customers = new GameObject[17];
    GameObject[] walkCustomers = new GameObject[8];

    GameObject[] tenins = new GameObject[5];

    public GameObject car1;
    public GameObject car2;
    public GameObject car3;
    public GameObject car4;
    public GameObject car5;
    public GameObject car6;
    public GameObject car7;
    public GameObject car8;
    public GameObject car9;


    int customerNum = 0;

    int wcinterval = 0;
    int ccinterval = 0;
    float wcTime = 0;
    float ccTime = 0;
    float effectTime = 0;


    private void Start()
    {
        db = new DBManager();
        com = GameObject.FindWithTag("common").GetComponent<common>();
        instantiateSalesPerson();
        instantiateCustomerControl(0);

        //店員の指示viewを表示する
        com.salesPersonController.setView(tenins);
    }

    private void Update()
    {
        //歩行客の生成タイミングの設定
        if (wcinterval == 0)
        {
            wcinterval = Random.Range(com.instantiateWalkCustomerMinInterval, com.instantiateWalkCustomerMaxInterval);
        }

        //車客の生成タイミングの設定
        if(ccinterval == 0)
        {
            ccinterval = Random.Range(com.instantiateCarCustomerMinInterval, com.instantiateCarCustomerMaxInterval);
        }

        wcTime += Time.deltaTime;
        ccTime += Time.deltaTime;

        if(wcinterval < wcTime)
        {
            Debug.Log("生成：歩行客の生成タイミング : " + wcinterval);
            instantiateCustomerControl(1);
            wcTime = 0f;
            wcinterval = 0;
        }

        if(ccinterval < ccTime)
        {
            Debug.Log("生成：くるま客の生成タイミング : " + wcinterval);
            instantiateCustomerControl(0);
            ccTime = 0f;
            ccinterval = 0;
        }

        if(com.instantiateCarCustomerMinInterval != 60)
        {
            effectTime += Time.deltaTime;
            if(effectTime > 300)
            {
                effectTime = 0;
                com.instantiateCarCustomerMinInterval = 60;
                com.instantiateWalkCustomerMinInterval = 60;
            }
        }

    }

    //データから店員を生成する
    public void instantiateSalesPerson()
    {
        db = new DBManager();
        Dictionary<string, DBManager.salesPerson> data = (Dictionary<string, DBManager.salesPerson>)db.getData(AppConst.SALES_PERSON_FILE_NAME);
        if (data == null)
        {
            Debug.Log("店員オブジェクトは最低一つ必要です。生成を開始します。");

            //オブジェクトの生成とパラメータセット
            tenins[0] = Instantiate(tenin);
            tenins[0].transform.GetChild(1).gameObject.SetActive(true);
            playercontroller pc = tenins[0].GetComponent<playercontroller>();
            pc.name = sharedFunction.createName(db.getSalesPersonNameList());
            pc.walkSpeedLevel = 1;
            pc.workSpeedLevel = 1;
            pc.sutaminaLevel = 1;
            pc.skinNumber = 1;
            pc.hourWage = 700;
            pc.slotNumber = 1;

            //データベースに登録
            db.setSalesPerson(pc.name, "man2", 700, 1, 1, 1, "G");
            return;
        }

        Debug.Log("登録されいてるデータから店員オブジェクトを生成します。");
        foreach (string name in data.Keys)
        {
            for(int i = 0; i < tenins.Length; i++)
            {
                if(tenins[i] == null)
                {
                    Debug.Log(name + "生成します");
                    tenins[i] = instantiateSalesPerson(data[name]);
                    break;
                }
            }          
            
        }
    }

    //初期ﾊﾟﾗﾒｰﾀで店員を生成
    public void instantiateSalesPersonInitial(string name, string objectName, string lank)
    {
        for(int i = 0; i < tenins.Length; i++)
        {
            if(tenins[i] == null)
            {
                tenins[i] = instantiateSalesPersonInitialParam(name, objectName, lank);
                return;
            }
        }

        // 店員viewを生成
        com.salesPersonController.setView(tenins);
    }

    //指定したパラメータで店員を生成
    GameObject instantiateSalesPerson(DBManager.salesPerson p)
    {
        GameObject ten = Instantiate(tenin);
        int skinNumber = resourceNameOfHuman(p.imgName);
        ten.transform.GetChild(skinNumber).gameObject.SetActive(true);
        playercontroller pc = ten.GetComponent<playercontroller>();
        pc.fullName = p.fullName;
        pc.walkSpeedLevel = p.walkSpeedLevel;
        pc.workSpeedLevel = p.workSpeedLevel;
        pc.sutaminaLevel = p.sutaminaLevel;
        pc.hourWage = p.hourWage;
        pc.skinNumber = skinNumber;
        pc.slotNumber = getSlotNum(p.lank);
        pc.name = p.fullName;

        return ten;
    }

    //初期パラメータの店員を生成
    public GameObject instantiateSalesPersonInitialParam(string name, string objectName, string lank)
    {
        GameObject ten = Instantiate(tenin);
        int skinNumber = resourceNameOfHuman(objectName);
        ten.transform.GetChild(skinNumber).gameObject.SetActive(true);
        playercontroller pc = ten.GetComponent<playercontroller>();
        pc.fullName = name;
        pc.walkSpeedLevel = 1;
        pc.workSpeedLevel = 1;
        pc.sutaminaLevel = 1;
        pc.hourWage = 700;
        pc.skinNumber = skinNumber;
        pc.slotNumber = getSlotNum(lank);
        pc.name = name;

        return ten;
    }

    //指定オブジェクトの削除
    public void destroySalesPerson(DBManager.salesPerson salesP)
    {
        for(int i = 0; i < tenins.Length; i++)
        {
            if(tenins[i] != null)
            {
                if (tenins[i].GetComponent<playercontroller>().fullName == salesP.fullName)
                {
                    tenins[i].GetComponent<playercontroller>().destroy = true;
                }
            }
            
        }
    }

    //イメージ名に紐づいたオブジェクト番号を返す
    public int resourceNameOfHuman(string resourceName)
    {
        if (resourceName == "woman1")
        {
            return 1;
        }
        else if (resourceName == "man2")
        {
            return 2;
        }
        else if (resourceName == "woman2")
        {
            return 3;
        }
        else if (resourceName == "man1")
        {
            return 4;
        }
        else return 5;
    }

    public void instantiateCustomerControl(int mode)
    {
        //レベルに応じた客生成最大数の制御
        //レベルの取得
        DBManager.Level level = (DBManager.Level)db.getData(AppConst.LEVEL_FILE_NAME);
        
        //レベル5以下は客数５まで
        if(level.level < 5 && getCustomerCount() > 5)
        {
            return;
        }

        //レベルに応じて増えていく
        if(level.level > 5 && level.level > getCustomerCount())
        {
            return;
        }

        if(mode == 0)
        {
            for (int i = 0; i < customers.Length; i++)
            {
                if (customers[i] == null)
                {
                    //有効な駐車場を探す
                    GameObject[] par = { com.defaultParking1, com.defaultParking2, com.defaultParking3, com.parking1, com.parking2, com.parking3, com.parking4, com.parking5, com.parking6, com.parking7, com.parking8, com.parking9, com.parking10, com.parking11, com.parking12, com.parking13, com.parking14 };
                    if (par[i].activeInHierarchy)
                    {
                        customers[i] = instantiateCustomer(par[i].transform.GetChild(0).transform);
                    }
                    return;
                }
            }
        } else
        {
            for (int i = 0; i < walkCustomers.Length; i++)
            {
                if(walkCustomers[i] == null)
                {
                    walkCustomers[i] = Instantiate(customer, new Vector3(9.96f, 0f, -11.37f), Quaternion.identity);
                    walkCustomers[i].transform.GetChild(Random.Range(1, 11)).gameObject.SetActive(true);
                    walkCustomers[i].name = customerNum.ToString();
                    customerNum++;
                    return;
                }
            }
        }
        
    }

    //現在の客数を取得
    int getCustomerCount()
    {
        int cnt = 0;
        for(int i = 0; i < customers.Length; i++)
        {
            if(customers[i] != null)
            {
                cnt++;
            }
        }

        for(int i = 0; i < walkCustomers.Length; i++)
        {
            if(walkCustomers[i] != null)
            {
                cnt++;
            }
        }

        return cnt;
    }

    GameObject instantiateCustomer(Transform parkingPos)
    {
        //車が生成されるポジションとりあえず１つ
        Vector3 instantiatePosition = new Vector3(9.86f, 0f, -50.08f);
        GameObject[] car = { car1, car2, car3, car4, car5, car6, car7, car8, car9 };
        GameObject c = Instantiate(car[Random.Range(0, 9)], instantiatePosition, Quaternion.identity);
        carController cc = c.GetComponent<carController>();
        cc.parkingPos = parkingPos;
        cc.number = customerNum;
        cc.customer = customer;
        customerNum++;
        return c;
    }

    //全店員の時給額を返却
    public int getHourWage()
    {
        int hourWage = 0;
        for(int i = 0; i < tenins.Length; i++)
        {
            if(tenins[i] != null)
            {
                hourWage += tenins[i].GetComponent<playercontroller>().hourWage;
            }
        }
        return hourWage;
    }

    //全店員の情報を渡す
    public GameObject[] getAllSalesPersonData()
    {
        return tenins;
    }

    //指定の店員のレベルを反映
    public void salesPLevelup(string name)
    {
        for(int i = 0; i < tenins.Length; i++)
        {
            if(tenins[i].name == name)
            {
                tenins[i].GetComponent<playercontroller>().levelup();
                return;
            }
        }
    }

    //ランクに応じたスロット数
    int getSlotNum(string lank)
    {
        if(lank == "S")
        {
            return 5;
        } else if(lank == "A")
        {
            return 4;
        } else if(lank == "B")
        {
            return 3;
        } else if(lank == "C")
        {
            return 3;
        } else if(lank == "D")
        {
            return 2;
        } else if(lank == "E")
        {
            return 2;
        } else if(lank == "F")
        {
            return 1;
        } else if(lank == "G")
        {
            return 1;
        }
        return 1;
    }

    /// <summary>
    /// 店員の色をセットする
    /// </summary>
    /// <param name="salesPersonName"></param>
    public void salesPersonSetSelect(string salesPersonName)
    {
        foreach(GameObject g in tenins)
        {
            if (g == null)
            {
                continue;
            }

            if (salesPersonName != g.name)
            {
                g.GetComponent<playercontroller>().setRenderColor();
                continue;
            }

            g.GetComponent<playercontroller>().viewSelect();
        }
    }
}
