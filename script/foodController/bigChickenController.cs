using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class bigChickenController : MonoBehaviour
{
    //ビッグチキンの数
    int bigChickenNum = 0;

    //ビッグチキンを生成できる数
    int canCreateBigChicken = 1;

    //現在ビッグチキンを作っているかどうか
    bool createBigChickenNow = false;

    //ビッグチキンオブジェクト
    public GameObject bigChicken;

    //ビッグチキン生成場所
    public GameObject bigChickenPoint;

    GameObject[] bigChickenArray;


    //ビッグチキンの数を返す
    public int getBigChickenNum()
    {
        return bigChickenNum;
    }

    public void useBigChicken()
    {
        Destroy(bigChickenArray[bigChickenArray.Length - 1]);
        bigChickenArray[bigChickenArray.Length - 1] = null;
        bigChickenArray = bigChickenArray.Where(value => value != null).ToArray();
        bigChickenNum--;
    }

    public void createBigChickenStart()
    {
        createBigChickenNow = true;
        bigChickenArray = new GameObject[canCreateBigChicken];
    }

    public void createBigChickenEnd()
    {
        createBigChickenNow = false;
    }

    public bool getCreateBigChickenState()
    {
        return createBigChickenNow;
    }

    //店員が呼び出す用
    public void directiveCreateBigChicken()
    {
        createBigChicken();
    }


    //ビッグチキン生成（生成数）
    void createBigChicken()
    {
        
        var position = bigChicken.transform.position;

        for (int i = 0; i < bigChickenArray.Length; i++)
        {
            position = bigChickenPoint.transform.position;
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

            bigChickenArray[i] = Instantiate(bigChicken, position, Quaternion.identity);
            bigChickenArray[i].transform.SetParent(bigChickenPoint.transform);
        }
        bigChickenNum = bigChickenArray.Length;
    }

    public void setCanCreateBigChickenNum()
    {
        canCreateBigChicken++;
    }

    public void setSpecifyCanCreateBigChickenNum(int specifyNum)
    {
        canCreateBigChicken = specifyNum;
    }

    /// <summary>
    /// 復元
    /// </summary>
    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] savebigChickenArray = bigChickenArray;
        bigChickenArray = new GameObject[bigChickenArray.Length + 1];

        for (int i = 0; i < savebigChickenArray.Length; i++)
        {
            bigChickenArray[i] = savebigChickenArray[i];
        }

        // null部分に追加する
        createBigChicken();
    }
}
