using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour
{
    DBManager db;
    DBManager.UpgradedData upd;
    common com;

    private void Start()
    {
        db = new DBManager();
        DBManager.Level level = (DBManager.Level)db.getData(AppConst.LEVEL_FILE_NAME);
        com = GameObject.FindWithTag("common").GetComponent<common>();
        if(level == null)
        {
            //初期レベルの設定
            db.setExp(0, 1);
            com.vc.setSliderValue(0, 1, goalExp(1));
        } else
        {
            com.vc.setSliderValue(level.exp, level.level, goalExp(level.level));
        }

        // 注文可能数設定
        orderNumCalc(level.level);
        
        
    }


    /// <summary>
    /// レベルに応じて市場に機能を解禁していく。レベルアップ時に呼ばれる
    /// </summary>
    /// <param name="level"></param>
    public void examinationLevelToFunc(int level)
    {


        upd = (DBManager.UpgradedData)db.getData(AppConst.UPGRADED_DATA_FILE_NAME);
        DBManager.LevelUpUpgradeRegisterData lurd = (DBManager.LevelUpUpgradeRegisterData)db.getData(AppConst.LEVELUPGRADE_REGISTERDATA_FILE_NAME);


        if(lurd == null)
        {
            lurd = new DBManager.LevelUpUpgradeRegisterData();
        }
        switch (level)
        {
            case 2:
                
                break;
                
            case 3:
                dbAccess("ガチャ", 2000);
                dbAccess("雇用最大人数+1", 2000);
                dbAccess("雇用", 2000);
                break;

            case 4:
                //フライヤー
                dbAccess("パティ作成数+1", 3000);
                dbAccess("チーズバーガー", 3000);
                dbAccess("席拡張1",3000);
                break;

            case 5:
                dbAccess("パティ作成数+1", 6000);
                dbAccess("駐車場1", 6000);
               
                break;

            case 6:
                dbAccess("ポテト作成数+1", 5000);
                dbAccess("席拡張2", 3000);
                break;

            case 7:
                //中級ハンバーガー
                dbAccess("店舗拡張1", 10000);
                dbAccess("パティ作成数+1", 4000);
                dbAccess("駐車場2", 4000);
                dbAccess("飲食席拡張1", 10000);
                break;

            case 8:
                dbAccess("オニオンリング", 10000);
                dbAccess("雇用最大人数+1", 11000);
                dbAccess("パティ準備作業", 10000);
                break;

            case 9:
                //店舗拡張
                dbAccess("トレー置き場2", 20000);
                dbAccess("パティ作成数+1", 4000);
                break;

            case 10:
                //雇用
                
                dbAccess("ポテト作成数+1", 18000);
                dbAccess("オニオンリング作成数+1", 18000);
                break;

            case 11:
                //雇用最大人数+1
                dbAccess("雇用最大人数+1", 25000);
                dbAccess("パティ作成数+1", 15000);
                dbAccess("駐車場3", 14000);
                dbAccess("ポテト準備作業", 20000);
                break;

            case 12:
                dbAccess("ドーナツ", 30000);
                dbAccess("ポテト作成数+1", 16500);
                dbAccess("オニオンリング作成数+1", 16500);
                break;

            case 13:
                //フライドチキン
                dbAccess("フライドチキン", 50000);
                dbAccess("パティ作成数+1", 22000);
                break;

            case 14:
                
                dbAccess("ポテト作成数+1", 20000);
                dbAccess("オニオンリング作成数+1", 20000);
                dbAccess("ドーナツ作成数+1", 20000);
                dbAccess("駐車場4", 21000);
                dbAccess("ドーナツ準備作業", 20000);
                break;
                
            case 15:
                //雇用最大人数+1
                
                dbAccess("パティ作成数+1", 20000);
                dbAccess("フライドチキン作成数+1", 20000);
                dbAccess("鳥の丸揚げ", 50000);
                break;

            case 16:
                //営業機能
                dbAccess("オニオンリング準備作業", 20000);
                dbAccess("営業", 10000);
                dbAccess("リブ", 50000);
                dbAccess("ポテト作成数+1", 13000);
                dbAccess("オニオンリング作成数+1", 13000);
                dbAccess("ドーナツ作成数+1", 13000);

                break;

            case 17:
                dbAccess("トレー置き場3", 50000);
                dbAccess("チラシ営業", 10000);
                dbAccess("パティ作成数+1", 15000);
                dbAccess("フライドチキン作成数+1", 15000);
                dbAccess("鳥の丸揚げ作成数+1", 10000);
                dbAccess("駐車場5", 10000);

                break;

            case 18:
                //高級ハンバーガー
                dbAccess("ビッグハンバーガー", 40000);
                dbAccess("ポテト作成数+1", 20000);
                dbAccess("オニオンリング作成数+1", 21000);
                dbAccess("ドーナツ作成数+1", 21000);
                dbAccess("リブ作成数+1", 21000);

                break;

            case 19: 
                //
                //dbAccess("サラダ", 100000);
                dbAccess("パティ作成数+1", 40000);
                dbAccess("フライドチキン作成数+1", 40000);
                dbAccess("鳥の丸揚げ作成数+1", 55000);
                dbAccess("フライドチキン準備作業", 40000);
                break;

            case 20: 
                dbAccess("ポテト作成数+1", 30000);
                dbAccess("オニオンリング作成数+1", 30000);
                dbAccess("ドーナツ作成数+1", 29000);
                dbAccess("リブ作成数+1", 29000);
                dbAccess("駐車場6", 29000);

                break;

            case 21:
                
                dbAccess("ラジオ営業", 50000);
                dbAccess("フライドチキン作成数+1", 60000);
                dbAccess("鳥の丸揚げ作成数+1", 61000);

                break;

            case 22:

                dbAccess("リブ準備作業", 40000);
                dbAccess("ポテト作成数+1", 40000);
                dbAccess("オニオンリング作成数+1", 50000);
                dbAccess("ドーナツ作成数+1", 40000);
                dbAccess("リブ作成数+1", 50000);


                break;

            case 23:
                //テレビ営業
                dbAccess("駐車場7", 50000);
                dbAccess("テレビ営業", 50000);
                dbAccess("フライドチキン作成数+1", 50000);
                dbAccess("鳥の丸揚げ作成数+1", 50000);


                break;

            case 24:
                
                dbAccess("ポテト作成数+1", 50000);
                dbAccess("オニオンリング作成数+1", 50000);
                dbAccess("ドーナツ作成数+1", 50000);
                dbAccess("リブ作成数+1", 50000);

                break;

            case 25:
                //キャッシュレス決済
                //dbAccess("キャッシュレス決済", 400000);
                dbAccess("フライドチキン作成数+1", 110000);
                dbAccess("鳥の丸揚げ作成数+1", 110000);
                dbAccess("ビッグフライドチキン準備作業", 100000);

                break;

            case 26:
                dbAccess("駐車場8", 50000);
                dbAccess("オニオンリング作成数+1", 50000);
                dbAccess("ドーナツ作成数+1", 60000);
                dbAccess("リブ作成数+1", 60000);

                break;

            case 27:
                dbAccess("フライドチキン作成数+1", 110000);
                dbAccess("鳥の丸揚げ作成数+1", 110000);

                break;


            case 28:
                dbAccess("ドーナツ作成数+1", 115000);
                dbAccess("リブ作成数+1", 115000);

                break;

            case 29:
                dbAccess("駐車場9", 70000);
                dbAccess("フライドチキン作成数+1", 80000);
                dbAccess("鳥の丸揚げ作成数+1", 80000);

                break;

            case 30:
                
                dbAccess("ドーナツ作成数+1", 100000);
                dbAccess("リブ作成数+1", 100000);

                break;

            case 31:
                dbAccess("駐車場10", 100000);
                dbAccess("フライドチキン作成数+1", 100000);
                dbAccess("鳥の丸揚げ作成数+1", 100000);

                break;

            case 32:
                dbAccess("リブ作成数+1", 225000);

                break;

            case 33:
                dbAccess("鳥の丸揚げ作成数+1", 250000);

                break;

            case 34:
                dbAccess("リブ作成数+1", 250000);
                dbAccess("駐車場11", 250000);
                break;

            case 37:
                dbAccess("駐車場12", 250000);
                break;

            case 40:
                dbAccess("駐車場13", 250000);
                break;

            case 43:
                dbAccess("駐車場14", 250000);
                break;

        }

        //レベルと別条件が含まれるアップグレード
        if(level >= 8 && !lurd.upgrade2 && upd.upgrade1 && !upd.upgrade2)
        {
            //店舗拡張2
            dbAccess("店舗拡張2", 50000);
            lurd.upgrade2 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if(level >= 13 && !lurd.upgrade3 && upd.upgrade2 && !upd.upgrade3)
        {
            //店舗拡張3
            dbAccess("店舗拡張3", 50000);
            lurd.upgrade3 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if(level >= 17 && !lurd.upgrade4 && upd.upgrade3 && !upd.upgrade4)
        {
            //店舗拡張4
            dbAccess("店舗拡張4", 50000);
            dbAccess("コンロ", 25000);
            lurd.upgrade4 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if(level >= 22 && !lurd.upgrade5 && upd.upgrade4 && !upd.upgrade5)
        {
            //店舗拡張5
            dbAccess("店舗拡張5", 80000);
            lurd.upgrade5 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if(level >= 27 && !lurd.upgrade6 && upd.upgrade5 && !upd.upgrade6)
        {
            //店舗拡張6
            dbAccess("店舗拡張6", 120000);
            lurd.upgrade6 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

       // if(level >= 30 && !lurd.driveThrow && upd.upgrade6 && !upd.driveThrow)
       // {
       //     //ドライブスルー
       //     dbAccess("ドライブスルー", 600000);
       //     lurd.driveThrow = true;
       //     db.setLevelUpgradeRegisterData(lurd);
       // }

        if(level >= 10 && !lurd.frier2 && upd.upgrade4 && !upd.frier2 && lurd.upgrade2)
        {
            //フライヤー2
            dbAccess("フライヤー2", 30000);
            lurd.frier2 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if(level >= 11 && !lurd.frierStation2 && upd.upgrade4 && !upd.friedStation2 && lurd.upgrade2)
        {
            //フライヤーステーション2
            dbAccess("フライヤーステーション2", 30000);
            lurd.frierStation2 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        //飲食席拡張
        if(level >= 15 && !lurd.upgradeTerrace2 && upd.upgradeTerrace1)
        {
            dbAccess("飲食席拡張2", 40000);
            lurd.upgradeTerrace2 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        //席拡張
        if(level >= 9 && !lurd.terrace5 && upd.upgradeTerrace1)
        {
            dbAccess("席拡張3", 10000);
            lurd.terrace5 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 11 && !lurd.terrace6 && upd.upgradeTerrace1)
        {
            dbAccess("席拡張4", 20000);
            lurd.terrace6 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 13 && !lurd.terrace7 && upd.upgradeTerrace1)
        {
            dbAccess("席拡張5", 30000);
            lurd.terrace7 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 15 && !lurd.terrace8 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張6", 40000);
            lurd.terrace8 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 17 && !lurd.terrace9 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張7", 50000);
            lurd.terrace9 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 19 && !lurd.terrace10 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張8", 60000);
            lurd.terrace10 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 21 && !lurd.terrace11 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張9", 70000);
            lurd.terrace11 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 23 && !lurd.terrace12 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張10", 80000);
            lurd.terrace12 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        if (level >= 25 && !lurd.terrace13 && upd.upgradeTerrace2)
        {
            dbAccess("席拡張11", 90000);
            lurd.terrace13 = true;
            db.setLevelUpgradeRegisterData(lurd);
        }

        // 注文可能数設定
        orderNumCalc(level);
    }

    /// <summary>
    /// データベースアクセス
    /// </summary>
    /// <param name="updateName"></param>
    /// <param name="updateAmountOfMoney"></param>
    public void dbAccess(string updateName, int updateAmountOfMoney)
    {
        //Debug.Log("アップデートデータをDBに登録します。updateName: " + updateName);
        db.setStoreUpgradeData(updateName, updateAmountOfMoney, updateAmountOfMoney/2000);
        com.vc.successMessageDialog(updateName + "がアップデートできます。");
    }

    /// <summary>
    /// レベルに応じた目標経験値
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int goalExp(int level)
    {
        return level*25;
    }

    /// <summary>
    /// レベルに応じた注文可能数を計算
    /// </summary>
    /// <param name="level"></param>
    private void orderNumCalc(int level)
    {
        if (level > 3) com.canOrderNum = 3;
        if (level > 6) com.canOrderNum = 4;
        if (level > 9) com.canOrderNum = 5;
        if (level > 10) com.canOrderNum = 6;
    }


    /// <summary>
    /// 経験値の蓄積とレベルアップ処理
    /// </summary>
    /// <param name="exp"></param>
    public void setExp(int exp)
    {
        
        //現在のレベルを取得
        DBManager.Level level = (DBManager.Level)db.getData(AppConst.LEVEL_FILE_NAME);

        //現在の経験値を取得
        int NowExp = level.exp;

        //現在のレベルから次のレベルまでの経験値を取得
        int nextLevelExp = goalExp(level.level);

        //現在の経験値にexpを足してレベルアップするかどうか判断
        if(NowExp+exp < nextLevelExp)
        {

            //レベルアップしなければ経験値のみ保存し、viewを更新
            db.setExp(NowExp+exp, level.level);
            com.vc.setSliderValue(NowExp + exp, level.level, goalExp(level.level));


        } else
        {

            //レベルアップ時はレベルに応じた機能の解禁と、レベルアップダイアログの表示、DBManagerにレベルと経験値を保存しviewを更新
            db.setExp(exp, level.level + 1);
            com.vc.setSliderValue(exp, level.level + 1, goalExp(level.level + 1));
            
            examinationLevelToFunc(level.level + 1);
            com.vc.openLevelUpDialog();

        }


    }



    
    


}
