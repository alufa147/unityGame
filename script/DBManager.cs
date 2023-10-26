using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;


public class DBManager
{

    //ユーザデータ
    [System.Serializable]
    public class userData
    {
        public string userId;
    }

    //店員パラメータ
    [System.Serializable]
    public class salesPerson
    {
        //店員名
        public string fullName;

        //写真名
        public string imgName;

        //時給
        public int hourWage;

        //歩行スピードレベル
        public int walkSpeedLevel;

        //作業スピードレベル
        public int workSpeedLevel;

        //スタミナレベル
        public int sutaminaLevel;

        //ランク
        public string lank;

    }

    //お金データ
    [System.Serializable]
    public class gameFunds
    {
        //お金
        public int funds;
    }

    //店舗アップグレードデータ
    [System.Serializable]
    public class storeUpgrade
    {
        //キー
        public int key;

        //アップグレード名
        public string upgradeName;

        //アップグレード費用
        public int upgradeAmountOfMoney;

        //アップグレード費用（ハート）
        public int upgradeAmountOfHeart;
    }

    //営業データ
    [System.Serializable]
    public class Sales
    {
        //営業名
        public string name;

        //費用
        public int amountOfMoney;

        //効果
        public int effect;
    }

    //今年の業績データ
    [System.Serializable]
    public class ProfitThisYear
    {
        public int month;
        public int profit;
    }

    //全年の業績データ
    [System.Serializable]
    public class ProfitAllYear
    {
        public int year;
        public int profit;
    }

    //レベルデータ
    [System.Serializable]
    public class Level
    {
        public int exp;
        public int level;
    }

    //メニューデータ
    [System.Serializable]
    public class Menu
    {
        public List<string> menuName = new List<string>();
    }

    //初期起動フラグデータ
    [System.Serializable]
    public class StartUp
    {
        public bool on = false;
    }

    //アップグレード後のデータファイル名
    [System.Serializable]
    public class UpgradedData{

        //フライヤーポジションx
        public float frierPositionX = -16.4f;

        //フライヤーポジションz
        public float frierPositionZ = -37.6f;

        //フライヤーステーションポジションx
        public float friedStationPositionX = -16.38f;

        //フライヤーステーションポジションz
        public float friedStationPositionZ = -35.28f;


        //フライヤー2
        public bool frier2 = false;

        //フライヤーステーション2
        public bool friedStation2 = false;

        //フライヤー2ポジションx
        public float frier2PositionX = -17.68f;

        //フライヤー2ポジションz
        public float frier2PositionZ = -39.83f;

        //フライヤーステーション2ポジションx
        public float friedStation2PositionX = -17.53f;

        //フライヤーステーション2ポジションz
        public float friedStation2PositionZ = -32.91f;

        //店舗拡張1
        public bool upgrade1 = false;

        //店舗拡張1に伴う削除オブジェクト
        public bool destroy1 = true;

        //店舗拡張1の壁
        public bool upgrade1wall = false;

        //ガチャ機能
        public bool gachaView = false;

        //雇用機能
        public bool employeeView = false;

        //解雇機能
        public bool dismissalView = false;

        //雇用最大人数
        public int employeeMax = 1;

        //店舗拡張2
        public bool upgrade2 = false;

        //店舗拡張2に伴う削除オブジェクト
        public bool destroy2 = true;

        //店舗拡張2の壁
        public bool upgrade2wall = true;

        //店舗拡張3
        public bool upgrade3 = false;

        //店舗拡張3に伴う削除オブジェクト
        public bool destroy3 = true;

        //店舗拡張3の壁
        public bool upgrade3wall = true;

        //店舗拡張4
        public bool upgrade4 = false;

        //店舗拡張5
        public bool upgrade5 = false;

        //店舗拡張6
        public bool upgrade6 = false;

        //営業機能
        public bool salesView = false;

        //ドライブスルー
        public bool driveThrow = false;

        //パテ最大生成数
        public int pateMaxNum = 3;

        //ポテト最大生成数
        public int potatoMaxNum = 3;

        //ドーナツ最大生成数
        public int donutMaxNum = 3;

        //オニオン最大生成数
        public int onionMaxNum = 3;

        //フライドチキン最大生成数
        public int chickenMaxNum = 3;

        //リブ最大生成数
        public int ribMaxNum = 3;

        //鳥の丸揚げ最大生成数
        public int bigChickenMaxNum = 3;

        //1段階右壁
        public bool wall1 = false;

        //1段階左壁
        public bool wall2 = false;

        //2段階右壁
        public bool wall3 = false;

        //2段階左壁
        public bool wall4 = false;

        //2段階右上壁
        public bool wall5 = false;

        //2段階左上壁
        public bool wall6 = false;

        //左下壁
        public bool wall7 = false;


        //冷蔵庫ポジションx
        public float reizoukoPositionX = -13.769f;

        //冷蔵庫ポジションz
        public float reizoukoPositionZ = -39.5f;

        //コンロポジションx
        public float bakePositionX = -15.86f;

        //コンロポジションz
        public float bakePositionZ = -32.06f;

        //トレー置き場2
        public bool traySelectButton = false;
        public bool trayStrage2 = false;
        public bool successButton2 = false;

        //トレー置き場3
        public bool trayStrage3 = false;
        public bool successButton3 = false;

        //駐車場
        public bool parking1 = false;
        public bool parking2 = false;
        public bool parking3 = false;
        public bool parking4 = false;
        public bool parking5 = false;
        public bool parking6 = false;
        public bool parking7 = false;
        public bool parking8 = false;
        public bool parking9 = false;
        public bool parking10 = false;
        public bool parking11 = false;
        public bool parking12 = false;
        public bool parking13 = false;
        public bool parking14 = false;

        //テラス側店舗拡張
        public bool upgradeTerrace1 = false;
        public bool upgradeTerrace2 = false;
        public bool destroyTerrace1 = true;
        public bool destroyTerrace2 = false;

        //テラス席拡張
        public bool terrace3 = false;
        public bool terrace4 = false;
        public bool terrace5 = false;
        public bool terrace6 = false;
        public bool terrace7 = false;
        public bool terrace8 = false;
        public bool terrace9 = false;
        public bool terrace10 = false;
        public bool terrace11 = false;
        public bool terrace12 = false;
        public bool terrace13 = false;

        // 指示ボタン
        public bool donutButton = false;
        public bool onionButton = false;
        public bool chickenButton = false;
        public bool ribButton = false;
        public bool bigChickenButton = false;
        public bool freezHamburgButton = false;
        public bool freezPotatoButton = false;
        public bool freezDonutButton = false;
        public bool freezOnionButton = false;
        public bool freezChickenButton = false;
        public bool freezRibButton = false;
        public bool freezBigChickenButton = false;

    }

    //レベルアップ時にアップグレードに追加したかどうかのデータ
    [System.Serializable]
    public class LevelUpUpgradeRegisterData
    {
        public bool upgrade2 = false;
        public bool upgrade3 = false;
        public bool upgrade4 = false;
        public bool upgrade5 = false;
        public bool upgrade6 = false;
        public bool driveThrow = false;
        public bool frier2 = false;
        public bool frierStation2 = false;
        public bool upgradeTerrace2 = false;
        public bool terrace5 = false;
        public bool terrace6 = false;
        public bool terrace7 = false;

        public bool terrace8 = false;
        public bool terrace9 = false;
        public bool terrace10 = false;
        public bool terrace11 = false;
        public bool terrace12 = false;
        public bool terrace13 = false;
    }

    [System.Serializable]
    public class EmployeeData
    {
        public string name;
        public string lank;
        public string resourceName;
    }

    [System.Serializable]
    public class WorldTimeData
    {
        public int year = 0;
        public int month = 0;
        public int week = 0;
        public float count = 0;
    }

    //時間を保存
    public void setWorldTime(int year, int month, int week, float count)
    {
        WorldTimeData wtd = (WorldTimeData)getData(AppConst.TIMEDATA_FILE_NAME);
        if(wtd == null)
        {
            wtd = new WorldTimeData();
        }
        wtd.year = year;
        wtd.month = month;
        wtd.week = week;
        wtd.count = count;
        binarySaveLoad.Save(AppConst.TIMEDATA_FILE_NAME,wtd);
    }

    //レベルアップ時に研究に追加したかどうかのデータ上書き
    public void setLevelUpgradeRegisterData(LevelUpUpgradeRegisterData upd)
    {
        binarySaveLoad.Save(AppConst.LEVELUPGRADE_REGISTERDATA_FILE_NAME, upd);
    }


    //雇用リストに登録
    public void setEmployeeData(string namex, string lankx, string resourceNamex)
    {
        Dictionary<string, EmployeeData> em = (Dictionary<string, EmployeeData>)getData(AppConst.EMPLOYEE_LIST_FILE_NAME);
        if(em == null)
        {
            em = new Dictionary<string, EmployeeData>();
        }
        EmployeeData e = new EmployeeData();
        e.name = namex;
        e.lank = lankx;
        e.resourceName = resourceNamex;
        em.Add(e.name, e);
        binarySaveLoad.Save(AppConst.EMPLOYEE_LIST_FILE_NAME, em);

    }

    //雇用リストから指定のデータを削除する
    public void deleteEmployeeData(EmployeeData em)
    {
        Dictionary<string, EmployeeData> emd = (Dictionary<string, EmployeeData>)getData(AppConst.EMPLOYEE_LIST_FILE_NAME);
        emd.Remove(em.name);
        binarySaveLoad.Save(AppConst.EMPLOYEE_LIST_FILE_NAME, emd);
    }

    //アップグレードデータの上書き
    public void setUpgradeData(UpgradedData upd)
    {
        binarySaveLoad.Save(AppConst.UPGRADED_DATA_FILE_NAME, upd);
    }

    //初回起動フラグの保存
    public void setStartupFlag()
    {
        StartUp s = new StartUp();
        s.on = true;
        binarySaveLoad.Save(AppConst.FIRST_STARTFLAG_FILE_NAME, s);
    }

    
    

    //メニューの保存
    public void setMenu(string menuName)
    {
        Menu m = (Menu)getData(AppConst.MENU_FILE_NAME);
        if(m == null)
        {
            m = new Menu();
            m.menuName = new List<string>();
        } else
        {
            //重複確認
            foreach (string mn in m.menuName)
            {
                if (mn == menuName)
                {
                    return;
                }
            }
        }

        m.menuName.Add(menuName);
        binarySaveLoad.Save(AppConst.MENU_FILE_NAME, m);

    }

    //経験値保存
    public void setExp(int exp,int lev)
    {
        Level level = (Level)getData(AppConst.LEVEL_FILE_NAME);
        if(level == null)
        {
            level = new Level();
        }
        level.exp = exp;
        level.level = lev;
        binarySaveLoad.Save(AppConst.LEVEL_FILE_NAME, level);
    }


    //データをロードして返す。
    public object getData(string filename)
    {
        object obj;
        binarySaveLoad.Load(filename, out obj);
        return obj;
    }

    //全年の業績データ登録
    public void setProfitAllYear(int year, int profit)
    {
        Dictionary<int, ProfitAllYear> pay = (Dictionary<int, ProfitAllYear>)getData(AppConst.PROFIT_ALL_YEAR_FILE_NAME);
        //データがなければ作成
        if (pay == null)
        {
            pay = new Dictionary<int, ProfitAllYear>();
        }
        else
        {
            //重複があれば終了
            foreach (int p in pay.Keys)
            {
                if (p == year)
                {
                    return;
                }
            }
        }


        ProfitAllYear instans = new ProfitAllYear();
        instans.year = year;
        instans.profit = profit;
        pay.Add(year, instans);
        binarySaveLoad.Save(AppConst.PROFIT_ALL_YEAR_FILE_NAME, pay);
    }

    //今年の業績データ登録
    public void setProfitThisYear(int month, int profit)
    {
        Dictionary<int, ProfitThisYear> pty = (Dictionary<int, ProfitThisYear>)getData(AppConst.PROFIT_THIS_YEAR_FILE_NAME);
        //データがなければ作成
        if(pty == null)
        {
            pty = new Dictionary<int, ProfitThisYear>();
        } else
        {
            //重複があれば終了
            foreach(int p in pty.Keys)
            {
                if(p == month)
                {
                    return;
                }
            }
        }


        ProfitThisYear instans = new ProfitThisYear();
        instans.month = month;
        instans.profit = profit;
        pty.Add(month, instans);
        binarySaveLoad.Save(AppConst.PROFIT_THIS_YEAR_FILE_NAME, pty);

    }

    //今年の業績データの全削除
    public void deleteProfitThisYear()
    {
        binarySaveLoad.Delete(AppConst.PROFIT_THIS_YEAR_FILE_NAME);
    }


    //営業リスト新規登録
    public void setSalesListData(string name, int amountOfMoney, int effect)
    {
        Dictionary<string, Sales> sl = (Dictionary<string, Sales>)getData(AppConst.SALES_LIST_FILE_NAME);
        if(sl == null)
        {
            sl = new Dictionary<string, Sales>();
        }
        Sales s = new Sales();
        s.name = name;
        s.amountOfMoney = amountOfMoney;
        s.effect = effect;
        sl.Add(s.name, s);
        binarySaveLoad.Save(AppConst.SALES_LIST_FILE_NAME, sl);
    }


    //店舗アップグレード新規登録
    public void setStoreUpgradeData(string updateName, int updateAmontOfMoney, int updateAmountOfHeart)
    {
        Dictionary<int, storeUpgrade> s = (Dictionary<int, storeUpgrade>)getData(AppConst.STORE_UPGRADE_FILE_NAME);
        if(s == null)
        {
            //初回中の初回はファイルを作成
            s = new Dictionary<int, storeUpgrade>();
        }
        storeUpgrade su = new storeUpgrade();
        su.upgradeName = updateName;
        su.upgradeAmountOfMoney = updateAmontOfMoney;
        su.upgradeAmountOfHeart = updateAmountOfHeart;

        //キーの付与
        if(s.Count != 0)
        {
            su.key = s.Count + 1;

            while(s.ContainsKey(su.key))
            {
                su.key = su.key + 1;
            }

        } else
        {
            su.key = 0;
        }

        s.Add(su.key, su);

        binarySaveLoad.Save(AppConst.STORE_UPGRADE_FILE_NAME, s);
    }

    //店舗アップグレードデータの削除
    public void deleteStoreUpgradeData(storeUpgrade su)
    {
        Dictionary<int, storeUpgrade> s = (Dictionary<int, storeUpgrade>)getData(AppConst.STORE_UPGRADE_FILE_NAME);
        s.Remove(su.key);
        binarySaveLoad.Save(AppConst.STORE_UPGRADE_FILE_NAME, s);
    }

    //資金データ保存
    public void setFunds(int amountOfMoney)
    {
        gameFunds fund;
        binarySaveLoad.Load(AppConst.FUNDS_FILE_NAME, out fund);

        //初回中の初回
        if(fund == null)
        {
            gameFunds gf = new gameFunds();
            gf.funds = amountOfMoney;
            binarySaveLoad.Save(AppConst.FUNDS_FILE_NAME, gf);
            return;
        }
        fund.funds = fund.funds + amountOfMoney;
        //初回以降
        binarySaveLoad.Save(AppConst.FUNDS_FILE_NAME, fund);
    }

    //現在の資金を返す
    public int getAllFunds()
    {
        gameFunds fund;
        binarySaveLoad.Load(AppConst.FUNDS_FILE_NAME, out fund);
        if(fund == null)
        {
            return 0;
        }
        return fund.funds;
    }

    //店員登録
    public void setSalesPerson(string name, string imgName,int hourWage, int walkSpeedLevel, int workSpeedLevel, int sutaminaLevel, string lank)
    {
        //すでに登録されているか調べる
        if (existRegisterSalesPerson(name))
        {
            Debug.Log("すでに登録されています。");
        }

        salesPerson s = new salesPerson();
        s.fullName = name;
        s.imgName = imgName;
        s.hourWage = hourWage;
        s.walkSpeedLevel = walkSpeedLevel;
        s.workSpeedLevel = workSpeedLevel;
        s.sutaminaLevel = sutaminaLevel;
        s.lank = lank;
        Dictionary<string, salesPerson> data = (Dictionary<string, salesPerson>)getData(AppConst.SALES_PERSON_FILE_NAME);
        if(data == null)
        {
            data = new Dictionary<string, salesPerson>();
        }
        data.Add(s.fullName, s);
        if(binarySaveLoad.Save(AppConst.SALES_PERSON_FILE_NAME, data) == binarySaveLoad.eResult.Success)
        {
            Debug.Log("データの登録が完了しました。：　" + name);
        } else
        {
            Debug.Log("データの登録に失敗しました。：　" + name);
        }
        
    }
    //すでに登録されているかを調べる
    bool existRegisterSalesPerson(string salesPersonName)
    {
        Dictionary<string, salesPerson> data = (Dictionary<string, salesPerson>)getData(AppConst.SALES_PERSON_FILE_NAME);
        if (data == null) return false;
        foreach(string str in data.Keys)
        {
            if(str == salesPersonName)
            {
                return true;
            } 
        }
        return false;
    }

    //店員パラメータ上書き用
    public void updateSalesPerson(salesPerson salesP)
    {
        Dictionary<string, salesPerson> data = (Dictionary<string, salesPerson>)getData(AppConst.SALES_PERSON_FILE_NAME);
        data[salesP.fullName] = salesP;
        binarySaveLoad.Save(AppConst.SALES_PERSON_FILE_NAME, data);
    }

    //DBから店員名リストを取得
    public string[] getSalesPersonNameList()
    {
        Dictionary<string, salesPerson> data = (Dictionary<string, salesPerson>)getData(AppConst.SALES_PERSON_FILE_NAME);
        if (data == null) return null;

        string[] nameList = new string[data.Count];
        int cnt = 0;
        foreach (string name in data.Keys)
        {
            nameList[cnt] = name;
            cnt++;
        }
        return nameList;
    }

    //指定の店員を削除
    public void deleteSalesPeson(salesPerson salesP)
    {
        Dictionary<string, salesPerson> data = (Dictionary<string, salesPerson>)getData(AppConst.SALES_PERSON_FILE_NAME);
        data.Remove(salesP.fullName);
        binarySaveLoad.Save(AppConst.SALES_PERSON_FILE_NAME, data);
    }

    //ユーザIDを登録
    public void setUserId(string userId)
    {
        userData u = new userData();
        u.userId = userId;
        binarySaveLoad.Save(AppConst.USERDATA_FILE_NAME, u);
    }

    //ユーザIDを返す
    public string getUserId()
    {
        userData userId = (userData)getData(AppConst.USERDATA_FILE_NAME);
        if(userId == null)
        {
            return "";
        }
        return userId.userId;

    }





}
