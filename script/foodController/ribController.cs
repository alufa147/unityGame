using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ribController : MonoBehaviour
{
    //リブの数
    int ribNum = 0;

    //リブを生成できる数
    int canCreateRib = 1;

    //現在リブを作っているかどうか
    bool createRibNow = false;

    //リブオブジェクト
    public GameObject rib;

    //リブ生成場所
    public GameObject ribPoint;

    GameObject[] ribArray;


    //リブの数を返す
    public int getRibNum()
    {
        return ribNum;
    }

    public void useRib()
    {
        Destroy(ribArray[ribArray.Length - 1]);
        ribArray[ribArray.Length - 1] = null;
        ribArray = ribArray.Where(value => value != null).ToArray();
        ribNum--;
    }

    public void createRibStart()
    {
        createRibNow = true;
        ribArray = new GameObject[canCreateRib];
    }

    public void createRibEnd()
    {
        createRibNow = false;
    }

    public bool getCreateRibState()
    {
        return createRibNow;
    }

    //店員が呼び出す用
    public void directiveCreateRib()
    {
        createRib();
    }


    //リブ生成（生成数）
    void createRib()
    {
        
        var position = rib.transform.position;

        for (int i = 0; i < ribArray.Length; i++)
        {
            position = ribPoint.transform.position;
            switch (i)
            {
                case 1:
                    position.x += -0.2f;
                    break;

                case 2:
                    position.x += -0.4f;
                    break;

                case 3:
                    position.z += 0.2f;
                    break;

                case 4:
                    position.z += 0.2f;
                    position.x += -0.2f;
                    break;

                case 5:
                    position.z += 0.2f;
                    position.x += -0.4f;
                    break;

                case 6:
                    position.z += 0.4f;
                    break;

                case 7:
                    position.z += 0.4f;
                    position.x += -0.2f;
                    break;

                case 8:
                    position.z += 0.4f;
                    position.x += -0.4f;
                    break;
            }

            ribArray[i] = Instantiate(rib, position, Quaternion.identity);
            ribArray[i].transform.SetParent(ribPoint.transform);
        }
        ribNum = ribArray.Length;
    }

    public void setCanCreateRibNum()
    {
        canCreateRib++;
    }

    public void setSpecifyCanCreateRibNum(int specifyNum)
    {
        canCreateRib = specifyNum;
    }

    /// <summary>
    /// 復元
    /// </summary>
    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] saveribArray = ribArray;
        ribArray = new GameObject[ribArray.Length + 1];

        for (int i = 0; i < saveribArray.Length; i++)
        {
            ribArray[i] = saveribArray[i];
        }

        // null部分に追加する
        createRib();
    }
}
