using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sharedFunction : MonoBehaviour
{
    public static string createName(string[] nameList)
    {
        string[] lastName = { "佐藤", "鈴木", "高橋", "田中", "伊藤", "渡辺", "山本", "中村", "小林", "加藤", "吉田", "山田", "佐々木", "山口", "松本", "井上", "木村", "林", "斎藤", "清水" };
        string[] fastName = { "健太", "誠", "隆", "哲也", "豊", "邦彦", "大介", "一樹", "健司", "健一郎", "孝明", "翔", "雅彦", "浩一", "武弘", "健太郎", "和樹", "俊之", "正", "晃一" };
        string name = lastName[Random.Range(0, lastName.Length)] + "　" + fastName[Random.Range(0, fastName.Length)];
        while (checkName(name,nameList))
        {
            name = lastName[Random.Range(0, lastName.Length)] + "　" + fastName[Random.Range(0, fastName.Length)];
        }
        return name;
    }

    //生成された名前が重複しているか確認
    public static bool checkName(string name, string[] nali)
    {
        bool answer = false;
        string[] nameList = nali;
        if (nameList == null) return answer;
        foreach (string n in nameList)
        {
            if (n == name) answer = true;
        }
        return answer;
    }

    //ランクに応じてレベルの最大値を返す
    public static int lankToLevel(string lank)
    {
        if (lank == "F")
        {
            return 10;
        }
        else if (lank == "E")
        {
            return 15;
        }
        else if (lank == "D")
        {
            return 20;
        }
        else if (lank == "C")
        {
            return 25;
        } else if(lank == "B")
        {
            return 30;
        } else if(lank == "A")
        {
            return 35;
        } else if(lank == "S")
        {
            return 40;
        } else
        {
            return 5;
        }
        
        
    }

    //注文を解析し、注文内容を文章にまとめる
    public static string getInfoMessage(string[] chumon)
    {
        string[] result = new string[6];
        int cnt = 0;
        int amountOfMoney = 0;

        //注文を単語に変換し配列に格納
        for (int i = 0; i < chumon.Length; i++)
        {
            amountOfMoney += checkUpExp(chumon[i])[0];
            if (chumon[i] == AppConst.JOB_SMALLHAMBURGER)
            {
                result[cnt] = "ハンバーガー";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_DRINK)
            {
                result[cnt] = "ドリンク";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_POTATO)
            {
                result[cnt] = "ポテト";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_DONUT)
            {
                result[cnt] = "ドーナツ";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_ONIONRING)
            {
                result[cnt] = "オニオンリング";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_FRIEDCHICKEN)
            {
                result[cnt] = "フライドチキン";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_RIB)
            {
                result[cnt] = "リブ";
                cnt++;
                continue;
            }

            if (chumon[i] == AppConst.JOB_BIGFRIEDCHICKEN)
            {
                result[cnt] = "鳥の丸焼き";
                cnt++;
                continue;
            }
        }

        //単語の重複をまとめて文章化
        cnt = 0;
        string infoMessage = "";
        for (int i = 0; i < result.Length; i++)
        {
            if (result[i] == null || result[i] == "")
            {
                Debug.Log("result[" + i + "]はnullなためcontinueします。");
                continue;
            }

            Debug.Log("result[" + i + "] : " + result[i] + " の重複チェックを始めます。");
            for (int s = 0; s < result.Length; s++)
            {
                Debug.Log("result[" + i + "]とresult[" + s + "]を比較");
                if (result[i] == result[s])
                {
                    Debug.Log("重複しています。");
                    cnt++;
                    if (i != s)
                    {
                        Debug.Log("重複項目の片方を初期化します。");
                        result[s] = "";
                    }
                }

            }
            infoMessage += result[i] + " × " + cnt + "  ";
            cnt = 0;
        }
        return infoMessage + "   合計： " + amountOfMoney + " 円";
    }

    public static int[] checkUpExp(string foodName)
    {
        int amountOfMoney = 0;
        int exp = 0;
        int materialCost = 0;
        if (AppConst.JOB_SMALLHAMBURGER == foodName)
        {
            amountOfMoney += 500;
            exp += 7;
            materialCost += 50;
        }
        else if (AppConst.JOB_DRINK == foodName)
        {
            amountOfMoney += 350;
            exp += 3;
            materialCost += 20;
        }
        else if (AppConst.JOB_POTATO == foodName)
        {
            amountOfMoney += 400;
            exp += 5;
            materialCost += 40;
        }
        else if (AppConst.JOB_DONUT == foodName)
        {
            amountOfMoney += 400;
            exp += 5;
            materialCost += 50;
        }
        else if (AppConst.JOB_ONIONRING == foodName)
        {
            amountOfMoney += 400;
            exp += 5;
            materialCost += 40;
        }
        else if (AppConst.JOB_FRIEDCHICKEN == foodName)
        {
            amountOfMoney += 700;
            exp += 5;
            materialCost += 60;
        }
        else if (AppConst.JOB_RIB == foodName)
        {
            amountOfMoney += 1000;
            exp += 5;
            materialCost += 80;
        }
        else if (AppConst.JOB_BIGFRIEDCHICKEN == foodName)
        {
            amountOfMoney += 2000;
            exp += 7;
            materialCost += 500;
        }
        else if (AppConst.JOB_SALAD == foodName)
        {
            amountOfMoney += 350;
            exp += 5;
            materialCost += 40;
        }

        int[] result = { amountOfMoney, exp, materialCost };
        return result;
    }

   
}
