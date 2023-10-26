using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class donutController : MonoBehaviour
{
    //ドーナツの数
    int donutNum = 0;

    //ドーナツを生成できる数
    int canCreateDonut = 3;

    //現在ドーナツを作っているかどうか
    bool createDonutNow = false;

    //ドーナツオブジェクト
    public GameObject donut;

    //ドーナツ生成場所
    public GameObject donutPoint;

    GameObject[] donutArray;

    //ドーナツの数を返す
    public int getDonutNum()
    {
        return donutNum;
    }

    public void useDonut()
    {
        Destroy(donutArray[donutArray.Length - 1]);
        donutArray[donutArray.Length - 1] = null;
        donutArray = donutArray.Where(value => value != null).ToArray();
        donutNum--;
    }

    public void createDonutStart()
    {
        createDonutNow = true;
        donutArray = new GameObject[canCreateDonut];
    }

    public void createDonutEnd()
    {
        createDonutNow = false;
    }

    public bool getCreateDonutState()
    {
        return createDonutNow;
    }

    //店員が呼び出す用
    public void directiveCreateDonut()
    {
        createDonut();
    }


    //ポテト生成（生成数）
    void createDonut()
    {
        
        var position = donut.transform.position;

        for (int i = 0; i < donutArray.Length; i++)
        {
            position = donutPoint.transform.position;
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

            donutArray[i] = Instantiate(donut, position, Quaternion.identity);
            donutArray[i].transform.SetParent(donutPoint.transform);
        }
        donutNum = donutArray.Length;
    }

    public void setCanCreateDonutNum()
    {
        canCreateDonut++;
    }

    public void setSpecifyCanCreateDonutNum(int specifyNum)
    {
        canCreateDonut = specifyNum;
    }

    public void restration()
    {
        // 現在生成してある配列の要素数を1増やして元に戻す。最後の要素数にはnullが入る。
        GameObject[] savedonutArray = donutArray;
        donutArray = new GameObject[donutArray.Length + 1];

        for (int i = 0; i < savedonutArray.Length; i++)
        {
            donutArray[i] = savedonutArray[i];
        }

        // null部分に追加する
        createDonut();
    }
}
