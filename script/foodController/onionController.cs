using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class onionController : MonoBehaviour
{
    //オニオンの数
    int onionNum = 0;

    //オニオンを生成できる数
    int canCreateOnion = 1;

    //現在オニオンを作っているかどうか
    bool createOnionNow = false;

    //オニオンオブジェクト
    public GameObject onion;

    //オニオン生成場所
    public GameObject onionPoint;

    GameObject[] onionArray;


    //オニオンの数を返す
    public int getOnionNum()
    {
        return onionNum;
    }

    public void useOnion()
    {
        Destroy(onionArray[onionArray.Length - 1]);
        onionArray[onionArray.Length - 1] = null;
        onionArray = onionArray.Where(value => value != null).ToArray();
        onionNum--;
    }

    public void createOnionStart()
    {
        createOnionNow = true;
        onionArray = new GameObject[canCreateOnion];
    }

    public void createOnionEnd()
    {
        createOnionNow = false;
    }

    public bool getCreateOnionState()
    {
        return createOnionNow;
    }

    //店員が呼び出す用
    public void directiveCreateOnion()
    {
        createOnion();
    }


    //オニオン生成（生成数）
    void createOnion()
    {
        
        var position = onion.transform.position;

        for (int i = 0; i < onionArray.Length; i++)
        {
            position = onionPoint.transform.position;
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

            onionArray[i] = Instantiate(onion, position, Quaternion.identity);
            onionArray[i].transform.SetParent(onionPoint.transform);
        }
        onionNum = onionArray.Length;
    }

    public void setCanCreateOnionNum()
    {
        canCreateOnion++;
    }

    public void setSpecifyCanCreateOnionNum(int specifyNum)
    {
        canCreateOnion = specifyNum;
    }

    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] saveonionArray = onionArray;
        onionArray = new GameObject[onionArray.Length + 1];

        for (int i = 0; i < saveonionArray.Length; i++)
        {
            onionArray[i] = saveonionArray[i];
        }

        // null部分に追加する
        createOnion();
    }
}
