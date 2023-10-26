using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class hamburgController : MonoBehaviour
{
    public GameObject freezHamburg;
    public GameObject bakedHamburg;
    public Transform freezHamburgPoint;

    int hamburgNum = 0;
    int canCreateHamburg = 3;
    bool createHamburgNow = false;

    GameObject[] hamburgArray;
    GameObject[] bakedHamburgArray;

    public bool hamburgChangeFlag = true;

    public int getHamburgNum()
    {
        return hamburgNum;
    }

    public int getHamburgCreateNum()
    {
        if(bakedHamburgArray != null)
        {
            return bakedHamburgArray.Length;
        } else
        {
            return 0;
        }
        
    }

    public int getCanCreateHamburgNum()
    {
        return canCreateHamburg;
    }

    public void useHamburg()
    {
        hamburgNum--;
        Destroy(bakedHamburgArray[bakedHamburgArray.Length - 1]);
        bakedHamburgArray[bakedHamburgArray.Length - 1] = null;
        bakedHamburgArray = bakedHamburgArray.Where(value => value != null).ToArray();
    }

    public void createHamburgStart()
    {
        createHamburgNow = true;
        hamburgChangeFlag = true;
        bakedHamburgArray = new GameObject[canCreateHamburg];
    }

    public void createHamburgEnd()
    {
        createHamburgNow = false;
    }

    public bool getCreateHamburgState()
    {
        return createHamburgNow;
    }

    //まだ焼けてないハンバーグを削除する
    public void destroyFreezHamburg()
    {
        for (int i = 0; i < hamburgArray.Length; i++){
            Destroy(hamburgArray[i]);
        }
    }

    //コンロ上の焼けてるハンバーグを削除する
    public void destroyBakeHamburg()
    {
        if (hamburgArray.Length == 0) return;
        Destroy(hamburgArray[hamburgArray.Length - 1]);
        hamburgArray[hamburgArray.Length - 1] = null;
        hamburgArray = hamburgArray.Where(value => value != null).ToArray();
    }

    public void createBakeHamburg(string mode)
    {
        GameObject hamburg;
        if (mode == "freez")
        {
            hamburg = freezHamburg;
        }
        else
        {
            hamburg = bakedHamburg;
        }
        hamburgArray = new GameObject[canCreateHamburg];
        for (int i = 0; i < hamburgArray.Length; i++)
        {
            var pos = freezHamburgPoint.transform.position;
            switch (i)
            {

                case 1:
                    pos.z += 0.2f;
                    break;

                case 2:
                    pos.z += 0.4f;
                    break;

                case 3:
                    pos.x += 0.3f;
                    break;

                case 4:
                    pos.x += 0.3f;
                    pos.z += 0.2f;
                    break;

                case 5:
                    pos.x += 0.3f;
                    pos.z += 0.4f;
                    break;

                case 6:
                    pos.x += 0.6f;
                    break;

                case 7:
                    pos.x += 0.6f;
                    pos.z += 0.2f;
                    break;

                case 8:
                    pos.x += 0.6f;
                    pos.z += 0.4f;
                    break;

            }
            Debug.Log("ハンバーグ" + hamburg.name);
            hamburgArray[i] = Instantiate(hamburg, pos, Quaternion.identity);
            hamburgArray[i].transform.SetParent(freezHamburgPoint);
        }

    }

    //完成ハンバーグを左に生成する
    public void createBakedHamburg()
    {
        var pos = freezHamburgPoint.position;
        pos.x += -0.8f;
        switch (hamburgNum)
        {
            case 1:
                pos.z += 0.2f;
                break;

            case 2:
                pos.z += 0.4f;
                break;

            case 3:
                pos.x += 0.3f;
                break;

            case 4:
                pos.x += 0.3f;
                pos.z += 0.2f;
                break;

            case 5:
                pos.x += 0.3f;
                pos.z += 0.4f;
                break;

            case 6:
                pos.x += 0.6f;
                break;

            case 7:
                pos.x += 0.6f;
                pos.z += 0.2f;
                break;

            case 8:
                pos.x += 0.6f;
                pos.z += 0.4f;
                break;
        }
        bakedHamburgArray[hamburgNum] = Instantiate(bakedHamburg, pos, Quaternion.identity);
        bakedHamburgArray[hamburgNum].transform.SetParent(freezHamburgPoint);
        hamburgNum++;
    }

    public void setCanCreateHamburgNum()
    {
        canCreateHamburg++;
    }

    public void setSpecifyCanCreateHamburgNum(int specifyNum)
    {
        canCreateHamburg = specifyNum;
    }

    /// <summary>
    /// 復元
    /// </summary>
    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] saveBakedhamburgArray = bakedHamburgArray;
        bakedHamburgArray = new GameObject[bakedHamburgArray.Length + 1];

        for(int i = 0; i < saveBakedhamburgArray.Length; i++)
        {
            bakedHamburgArray[i] = saveBakedhamburgArray[i];
        }

        // null部分にハンバーグを追加する
        createBakedHamburg();
    }

}
