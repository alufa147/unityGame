using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class profitController : MonoBehaviour
{
    private int allFund = 0;
    private int thisYearFund = 0;
    private int thisMonthFund = 0;
    private int materialCost = 0;
    DBManager db;
    common com;

    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
        db = new DBManager();
        allFund = db.getAllFunds();
        com.vc.updateMoneyView(allFund);
        Dictionary<int,DBManager.ProfitThisYear> pty = (Dictionary<int, DBManager.ProfitThisYear>)db.getData(AppConst.PROFIT_THIS_YEAR_FILE_NAME);
        if (pty != null)
        {
            int[] p = new int[pty.Count];
            int cnt = 0;
            foreach(int i in pty.Keys)
            {
                p[cnt] = i;
                cnt++;
            }
            thisMonthFund = pty[getLatest(p)].profit;
        }
        Dictionary<int, DBManager.ProfitAllYear> pay = (Dictionary<int, DBManager.ProfitAllYear>)db.getData(AppConst.PROFIT_ALL_YEAR_FILE_NAME);
        if(pay != null)
        {
            int[] p = new int[pay.Count];
            int cnt = 0;
            foreach (int i in pay.Keys)
            {
                p[cnt] = i;
                cnt++;
            }
            thisYearFund = pty[getLatest(p)].profit;
        }

    }

    //単純に持ち金の増減
    public void setMoney(int fund)
    {
        allFund = allFund + fund;
        db.setFunds(fund);
        com.vc.updateMoneyView(allFund);
    }

    //売上
    public void setFund(int fund)
    {
        allFund = allFund + fund;
        thisMonthFund = thisMonthFund + fund;
        thisYearFund = thisYearFund + fund;
        db.setFunds(fund);
        com.vc.updateMoneyView(allFund);
    }

    public void setMaterialCost(int cost)
    {
        materialCost += cost;
    }

    public int getFund()
    {
        return allFund;
    }

    //月変わり
    public void changeMonth(int oldMonth)
    {
        db.setProfitThisYear(oldMonth, thisMonthFund);
        thisMonthFund = 0;

        //人件費
        allFund += -com.hoc.getHourWage();

        //アナウンス
        viewController vc = GameObject.FindWithTag("viewController").GetComponent<viewController>();
        vc.setInfoMessage("人件費： " + com.hoc.getHourWage() + " 円　　材料費: " + materialCost + " 円　　合計： " + (com.hoc.getHourWage() + materialCost));

        //材料費
        allFund += -materialCost;
        materialCost = 0;

        
    }

    //年変わり
    public void changeYear(int oldYear)
    {
        db.setProfitAllYear(oldYear, thisYearFund);
        thisYearFund = 0;
    }


    //最新の業績を取得
    int getLatest(int[] p)
    {

        //最新を取得
        int latestNum = 0;
        for(int i = 0; i < p.Length; i++)
        {
            if(p[i] > latestNum)
            {
                latestNum = p[i]; 
            }
        }
        return latestNum;
    }
}
