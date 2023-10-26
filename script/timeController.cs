using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeController : MonoBehaviour
{
    public int year = 1;
    public int month = 1;
    public int week = 1;
    float count = 0f;

    common com;

    private void Start()
    {
        com = GameObject.FindWithTag("common").GetComponent<common>();
        DBManager.WorldTimeData wtd = (DBManager.WorldTimeData)new DBManager().getData(AppConst.TIMEDATA_FILE_NAME);
        if(wtd != null)
        {
            year = wtd.year;
            month = wtd.month;
            week = wtd.week;
            count = wtd.count;
        }
        com.vc.updateDateView(year, month, week);
    }


    private void Update()
    {
        //60フレームなので1秒単位
        count += Time.deltaTime;

        //1週間たったらweek++
        if(count > 150)
        {
            count = 0;
            week++;

            //1カ月たったら売上データを保存
            if (week == 5)
            {
                com.procon.changeMonth(month);

                // オーダー数をリセット
                com.orderViewController.resetOrderCount();

                week = 1;
                month++;

                //1年たったらその年の売上データを保存
                if (month == 13)
                {
                    com.procon.changeYear(year);
                    month = 1;
                    year++;
                }
            }
            com.vc.updateDateView(year, month, week);
            saveWorldTime();
        }



    }

    void saveWorldTime()
    {
        new DBManager().setWorldTime(year,month,week,count);
    }

    //ゲーム世界の進行を止める
    public void stop()
    {
        //ゲーム世界の進行を停止する
        Time.timeScale = 0;
        com.pose = true;
    }

    //ゲーム世界の進行させる
    public void start()
    {
        Time.timeScale = 1;
        com.pose = false;
    }
}
