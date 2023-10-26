using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chumonController : MonoBehaviour

{
    private common com;

    private Dictionary<string, customerController> orderList = new Dictionary<string, customerController>();

    private customerController rejiCustomer;

    float stateTime = 0f;

    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
    }

    private void Update()
    {
        stateTime += Time.deltaTime;
        if(stateTime > 1)
        {
            stateTime = 0f;
            inquiryOredrResult();
        }
    }

    //納品物と注文を照合し、合致する客を呼び出す
    private void inquiryOredrResult()
    {

        for (int i = 0; i < 7; i++)
        {
            //カウンター上のトレーを取得
            GameObject tray = com.counterController.getCounterOnTray(i);

            //トレーが存在すれば、中身を取得
            if (tray != null && tray.transform.childCount > 0)
            {
                tray = tray.transform.GetChild(0).gameObject;
            }
            else
            {
                //存在しなければ次のトレーを取得
                continue;
            }

            
            foreach (string c in orderList.Keys)
            {
                Debug.Log(orderList[c].getMyOrder());
                if (tray.GetComponent<tray>().getItem() == orderList[c].getMyOrder())
                {
                    orderList[c].setTray(tray, i);
                    orderList.Remove(c);
                    com.counterController.setUsePosition(i);
                    return;
                }
            }


        }
    }

    //注文をセット
    public void setOrder()
    {
        if(rejiCustomer != null)
        {
            orderList[rejiCustomer.name] = rejiCustomer;
            rejiCustomer.getChumon();
            rejiCustomer = null;
        }
        
    }

    //レジ前の客をセット
    public void setRejiCustomer(customerController g)
    {
        rejiCustomer = g;
    }

}
