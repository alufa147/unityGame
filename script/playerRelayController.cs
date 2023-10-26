using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class playerRelayController : MonoBehaviour
{
    common com;
    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
    }

    //trayControllerから呼び出される。
    //各店員の状態を見て納品状態に遷移できるやつに渡す
    public void setSuccessMode(GameObject tray)
    {
        GameObject[] tenins = com.hoc.getAllSalesPersonData();

        for(int i = 0; i < tenins.Length; i++)
        {
            if (tenins[i] != null)
            {

            }
        }
    }

}
