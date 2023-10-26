using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counterController : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject p5;
    public GameObject p6;
    public GameObject p7;

    private bool[] position = new bool[7];

    //どのカウンターのポジションが空いているかを返す
    public GameObject getFreeCounterPosition()
    {
        GameObject[] t = { p1, p2, p3, p4, p5, p6, p7 };
        for(int i = 0; i < t.Length; i++)
        {
            if (t[i].transform.childCount == 0)
            {
                return t[i];
            }
        }

        Debug.Log("例外の位置が返されています。");
        return null;
    }

    //指定のオブジェクトを返す
    public GameObject getCounterOnTray(int num)
    {
        GameObject[] g = { p1, p2, p3, p4, p5, p6, p7 };

        //配列範囲外はreturn
        if (num >= g.Length) return null;

        //ポジション使用中はreturn
        if (position[num]) return null;

        return g[num];
    }

    //指定のポジションを使用中に設定
    public void setUsePosition(int num)
    {
        position[num] = true;
    }

    public void setFreePosition(int num)
    {
        position[num] = false;
    }

    

}
