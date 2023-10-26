using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitPositionController : MonoBehaviour
{
    public Transform rejiPosition;
    public Transform rejiWaitPosition1;
    public Transform rejiWaitPosition2;
    public Transform rejiWaitPosition3;
    public Transform rejiWaitPosition4;
    public Transform rejiWaitPosition5;
    public Transform rejiWaitPosition6;
    public Transform rejiWaitPosition7;
    public Transform rejiWaitPosition8;
    public Transform rejiWaitPosition9;
    public Transform rejiWaitPosition10;
    public Transform rejiWaitPosition11;
    public Transform rejiWaitPosition12;
    public Transform rejiWaitPosition13;
    public Transform rejiWaitPosition14;
    public Transform rejiWaitPosition15;
    public Transform rejiWaitPosition16;
    public Transform rejiWaitPosition17;
    public Transform rejiWaitPosition18;
    public Transform rejiWaitPosition19;
    public Transform rejiWaitPosition20;
    public Transform rejiWaitPosition21;
    public Transform rejiWaitPosition22;
    public Transform rejiWaitPosition23;
    public Transform rejiWaitPosition24;
    public Transform rejiWaitPosition25;

    public Transform waitPosition1;
    public Transform waitPosition2;
    public Transform waitPosition3;
    public Transform waitPosition4;
    public Transform waitPosition5;
    public Transform waitPosition6;
    public Transform waitPosition7;
    public Transform waitPosition8;
    public Transform waitPosition9;
    public Transform waitPosition10;
    public Transform waitPosition11;
    public Transform waitPosition12;
    public Transform waitPosition13;
    public Transform waitPosition14;
    public Transform waitPosition15;
    public Transform waitPosition16;
    public Transform waitPosition17;
    public Transform waitPosition18;
    public Transform waitPosition19;
    public Transform waitPosition20;
    public Transform waitPosition21;
    public Transform waitPosition22;
    public Transform waitPosition23;
    public Transform waitPosition24;
    public Transform waitPosition25;
    int rejiWaitNum = 0;

    public Transform waitCtrPosition;
    

    Dictionary<string, int> rejiWaitCustomer = new Dictionary<string, int>();
    Dictionary<string, int> waitCustomer = new Dictionary<string, int>();
    string[] rejiWaitKey = new string[25];
    string[] waitKey = new string[25];

    public void setRejiWaitCustomer(string customerName)
    {
        rejiWaitCustomer.Add(customerName, rejiWaitNum);
        addRejiWaitKey(customerName);
        rejiWaitNum++;
    }

    public void setWaitCustomer(string customerName)
    {
        waitCustomer.Add(customerName, getFreeWaitPosition());
        addWaitKey(customerName);
        Debug.Log("customerName: " + customerName + "  waitPosition : " + waitCustomer[customerName]);
    }

    int getFreeWaitPosition()
    {
        bool[] wait = new bool[17];
        for(int i = 0; i < waitKey.Length; i++)
        {
            if(waitKey[i] == null)
            {
                continue;
            }
            wait[waitCustomer[waitKey[i]]] = true;

        }

        int result = 0;
        for(int i = 0; i < wait.Length; i++)
        {
            if(wait[i] == false)
            {
                result = i;
                break;
            }
        }
        return result;
    }

    public Transform getWaitPosition(string customerName)
    {
        Transform[] tran = { waitPosition1, waitPosition2, waitPosition3, waitPosition4, waitPosition5, waitPosition6, waitPosition7, waitPosition8, waitPosition9, waitPosition10, waitPosition11, waitPosition12, waitPosition13, waitPosition14, waitPosition15, waitPosition16, waitPosition17 };
        return tran[waitCustomer[customerName]];
    }

    public Transform getRejiWaitPosition(string customerName)
    {
        Transform[] tran = { rejiPosition, rejiWaitPosition1, rejiWaitPosition2, rejiWaitPosition3, rejiWaitPosition4, rejiWaitPosition5, rejiWaitPosition6, rejiWaitPosition7, rejiWaitPosition8, rejiWaitPosition9, rejiWaitPosition10, rejiWaitPosition11, rejiWaitPosition12, rejiWaitPosition13, rejiWaitPosition14, rejiWaitPosition15, rejiWaitPosition16, rejiWaitPosition17 };
        return tran[rejiWaitCustomer[customerName]];
    }


    public void removeRejiWaitPosition(string customerName)
    {
        rejiWaitCustomer.Remove(customerName);
        updateRejiWaitPosition(customerName);
        rejiWaitNum--;
    }

    public void removeWaitPosition(string customerName)
    {
        waitCustomer.Remove(customerName);
        updateWaitPosition(customerName);
    }

    public int checkMyRejiPosition(string customerName)
    {
        return rejiWaitCustomer[customerName];
    }



    void addRejiWaitKey(string customerName)
    {
        for (int i = 0; i < rejiWaitKey.Length; i++)
        {
            if(rejiWaitKey[i] == null)
            {
                rejiWaitKey[i] = customerName;
                return;
            }
        }
    }

    void addWaitKey(string customerName)
    {
        Debug.Log("waitKey.Length : " + waitKey.Length);
        for (int i = 0; i < waitKey.Length; i++)
        {
            if (waitKey[i] == null)
            {
                waitKey[i] = customerName;
                return;
            }
        }
    }

    void updateRejiWaitPosition(string customerName)
    {
        for(int i = 0; i < rejiWaitKey.Length; i++)
        {
            if(rejiWaitKey[i] == customerName)
            {
                rejiWaitKey[i] = null;
                break;
            }
        }
        for(int i = 0; i < rejiWaitKey.Length; i++)
        {
            if(rejiWaitKey[i] == null)
            {
                continue;
            }
            rejiWaitCustomer[rejiWaitKey[i]] = rejiWaitCustomer[rejiWaitKey[i]]-1;
        }
    }

    void updateWaitPosition(string customerName)
    {
        for (int i = 0; i < waitKey.Length; i++)
        {
            if (waitKey[i] == customerName)
            {
                waitKey[i] = null;
                break;
            }
        }

    }

    public Transform getWaitCtrPosition()
    {
        return waitCtrPosition;
    }






}
