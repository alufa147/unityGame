using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class chickenController : MonoBehaviour
{
    //チキンの数
    int chickenNum = 0;

    //チキンを生成できる数
    int canCreateChicken = 1;

    //現在チキンを作っているかどうか
    bool createChickenNow = false;

    //チキンオブジェクト
    public GameObject chicken;

    //チキン生成場所
    public GameObject chickenPoint;

    GameObject[] chickenArray;


    //チキンの数を返す
    public int getChickenNum()
    {
        return chickenNum;
    }

    public void useChicken()
    {
        Destroy(chickenArray[chickenArray.Length - 1]);
        chickenArray[chickenArray.Length - 1] = null;
        chickenArray = chickenArray.Where(value => value != null).ToArray();
        chickenNum--;
    }

    public void createChickenStart()
    {
        createChickenNow = true;
        chickenArray = new GameObject[canCreateChicken];
    }

    public void createChickenEnd()
    {
        createChickenNow = false;
    }

    public bool getCreateChickenState()
    {
        return createChickenNow;
    }

    //店員が呼び出す用
    public void directiveCreateChicken()
    {
        createChicken();
    }


    //チキン生成（生成数）
    void createChicken()
    {
        
        var position = chicken.transform.position;

        for (int i = 0; i < chickenArray.Length; i++)
        {
            position = chickenPoint.transform.position;
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

            chickenArray[i] = Instantiate(chicken, position, Quaternion.identity);
            chickenArray[i].transform.SetParent(chickenPoint.transform);
        }
        chickenNum = chickenArray.Length;
    }

    public void setCanCreateChickenNum()
    {
        canCreateChicken++;
    }

    public void setSpecifyCanCreateChickenNum(int specifyNum)
    {
        canCreateChicken = specifyNum;
    }

    /// <summary>
    /// 復元
    /// </summary>
    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] savechickenArray = chickenArray;
        chickenArray = new GameObject[chickenArray.Length + 1];

        for (int i = 0; i < savechickenArray.Length; i++)
        {
            chickenArray[i] = savechickenArray[i];
        }

        // null部分に追加する
        createChicken();
    }
}
