using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PotatoController : MonoBehaviour
{
    //ポテトの数
    int potatoNum = 0;

    //ポテトを生成できる数
    int canCreatePotato = 1;

    //現在ポテトを作っているかどうか
    bool createPotatoNow = false;

    //ポテトオブジェクト
    public GameObject potato;

    //ポテト生成場所
    public GameObject potatoPoint;

    GameObject[] potatoArray;

    //ポテトの数を返す
    public int getPotatoNum()
    {
        return potatoNum;
    }

    public void usePotato()
    {
        Destroy(potatoArray[potatoArray.Length-1]);
        potatoArray[potatoArray.Length - 1] = null;
        potatoArray = potatoArray.Where(value => value != null).ToArray();
        potatoNum--;
    }

    public void createPotatoStart()
    {
        createPotatoNow = true;
        potatoArray = new GameObject[canCreatePotato];
    }
    
    public void createPotatoEnd()
    {
        createPotatoNow = false;
    }

    public bool getCreatePotatoState()
    {
        return createPotatoNow;
    }

    //店員が呼び出す用
    public void directiveCreatePotato()
    {
        createPotato();
    }


    //ポテト生成（生成数）
    void createPotato()
    {
        var position = potato.transform.position;

        for (int i = 0; i < potatoArray.Length; i++)
        {
            position = potatoPoint.transform.position;
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

            potatoArray[i] = Instantiate(potato, position, Quaternion.identity);
            potatoArray[i].transform.SetParent(potatoPoint.transform);
        }
        potatoNum = potatoArray.Length;
    }

    public void setCanCreatePotatoNum()
    {
        canCreatePotato++;
    }

    public void setSpecifyCanCreatePotatoNum(int specifyNum)
    {
        canCreatePotato = specifyNum;
    }


    /// <summary>
    /// 復元
    /// </summary>
    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] savePotatoArray = potatoArray;
        potatoArray = new GameObject[potatoArray.Length + 1];

        for (int i = 0; i < savePotatoArray.Length; i++)
        {
            potatoArray[i] = savePotatoArray[i];
        }

        // null部分に追加する
        createPotato();
    }

}
