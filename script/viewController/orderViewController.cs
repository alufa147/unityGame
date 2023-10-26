using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orderViewController : MonoBehaviour
{

    // スクロールコンテンツ
    public GameObject scrollContents;

    // オーダーview
    public GameObject orderView;

    // オーダー構築用
    private Dictionary<string, string[]> orderSettingDic = new Dictionary<string, string[]>();

    // オーダー削除用
    private Dictionary<string, int> orderRemoveDic = new Dictionary<string, int>();

    // オーダー番号
    private int count = 1;

    // スクロールにviewを追加する
    public void addScrollContents(string customerName, string[] order)
    {

        // オーダーview生成
        GameObject view = Instantiate(orderView);

        // Noを設定
        view.transform.GetChild(0).GetComponent<Text>().text = (count).ToString();
        count++;

        // 画像を設定
        for(int i = 0; i < order.Length; i++)
        {
            if (order[i] == null) continue;

            Image im = view.transform.GetChild(i + 1).GetComponent<Image>();
            im.sprite = Resources.Load<Sprite>("Shapes2D Sprites/" + order[i]);
            im.gameObject.SetActive(true);
        }

        // スクロールコンテンツに追加
        view.transform.SetParent(scrollContents.transform);

        // オーダー構築用に追加
        orderSettingDic.Add(customerName, order);

        // オーダー削除用に追加
        orderRemoveDic.Add(customerName, scrollContents.transform.childCount-1);
    }

    // スクロールコンテンツから削除する
    public void removeScrollContents(string customerName)
    {
        // オーダー構築用から削除
        orderSettingDic.Remove(name);

        // スクロールコンテンツから削除
        Destroy(scrollContents.transform.GetChild(orderRemoveDic[customerName]).gameObject);

        int deleteNum = orderRemoveDic[customerName];

        // オーダー削除用から削除
        orderRemoveDic.Remove(customerName);

        string[] keyArr = new string[orderRemoveDic.Count];
        int cnt = 0;
        foreach(string key in orderRemoveDic.Keys)
        {
            //Debug.Log(key + " value : " + orderRemoveDic[key]);
            keyArr[cnt] = key;
            cnt++;
        }

        // 削除した分を繰り下げる
        foreach (string key in keyArr)
        {
            //Debug.Log(key + " value : " + orderRemoveDic[key]);
            if (orderRemoveDic[key] > deleteNum)
            {

                //Debug.Log(orderRemoveDic[key] + " > " + deleteNum);
                orderRemoveDic[key] = (orderRemoveDic[key] - 1);
                //Debug.Log("置換OK");
            }
            
        }

    }
    /// <summary>
    ///  オーダー番号リセット
    /// </summary>
    public void resetOrderCount()
    {
        count = 1;
    }
}
