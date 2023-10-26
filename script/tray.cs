using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tray : MonoBehaviour
{
    private Dictionary<string, int> item;


    //�g���[�ɃI�u�W�F�N�g�𐶐�����
    public void setItem(string foodName)
    {
        if(item == null)
        {
            item = new Dictionary<string, int>();
            item.Add(AppConst.JOB_SMALLHAMBURGER, 0);
            item.Add(AppConst.JOB_DRINK, 0);
            item.Add(AppConst.JOB_POTATO, 0);
            item.Add(AppConst.JOB_DONUT, 0);
            item.Add(AppConst.JOB_ONIONRING, 0);
            item.Add(AppConst.JOB_FRIEDCHICKEN, 0);
            item.Add(AppConst.JOB_RIB, 0);
            item.Add(AppConst.JOB_BIGFRIEDCHICKEN, 0);
        }
        item[foodName] = item[foodName] + 1;
        //foreach(string key in item.Keys)
        //{
        //    Debug.Log(key + item[key]);
            
        //}
    }

    //トレー上のアイテムを集計して文字列で返す
    public string getItem()
    {
        string result = "";

        foreach(string key in item.Keys)
        {
            result += item[key];
        }

        return result;
    }

   
}
