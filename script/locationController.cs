using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locationController : MonoBehaviour
{
    private bool drink = false;
    private bool bake = false;
    private bool tray = false;
    private bool tray2 = false;
    private bool tray3 = false;
    private bool reizouko = false;
    private bool reji = false;
    private bool potatoFrier = false;
    private bool potatoFriedStation = false;
    private bool donutFrier = false;
    private bool donutFriedStation = false;
    private bool onionFrier = false;
    private bool onionFriedStation = false;
    private bool friedChickenFrier = false;
    private bool friedChickenFriedStation = false;
    private bool ribFrier = false;
    private bool ribFriedStation = false;
    private bool bigFriedChickenFrier = false;
    private bool bigFriedChickenFriedStation = false;

    private bool parking1 = false;
    private bool parking2 = false;
    private bool parking3 = false;
    private bool parking4 = false;
    private bool parking5 = false;
    private bool parking6 = false;
    private bool parking7 = false;
    private bool parking8 = false;
    private bool parking9 = false;
    private bool parking10 = false;
    private bool parking11 = false;
    private bool parking12 = false;
    private bool parking13 = false;
    private bool parking14 = false;

    private bool[] terrace = new bool[13];


    public void setDrinkTrue()
    {
        drink = true;
        Debug.Log("drinkにtrueがセットされました。");
    }

    public void setDrinkFalse()
    {
        drink = false;
        Debug.Log("drinkにfalseがセットされました。");
    }

    public void setBakeTrue()
    {
        bake = true;
        Debug.Log("bakeにtrueがセットされました。");
    }

    public void setBakeFalse()
    {
        bake = false;
        Debug.Log("bakeにfalseがセットされました。");
    }

    public void setTrayTrue()
    {
        tray = true;
        Debug.Log("trayにtrueがセットされました。");
    }

    public void setTrayFalse()
    {
        tray = false;
        Debug.Log("trayにfalseがセットされました。");
    }

    public void setTray2True()
    {
        tray2 = true;
        Debug.Log("tray2にtrueがセットされました。");
    }

    public void setTray2False()
    {
        tray2 = false;
        Debug.Log("tray2にfalseがセットされました。");
    }

    public void setTray3True()
    {
        tray3 = true;
        Debug.Log("tray3にtrueがセットされました。");
    }

    public void setTray3False()
    {
        tray3 = false;
        Debug.Log("tray3にfalseがセットされました。");
    }

    public void setReizoukoTrue()
    {
        reizouko = true;
        Debug.Log("reizoukoにtrueがセットされました。");
    }

    public void setReizoukoFalse()
    {
        reizouko = false;
        Debug.Log("reizoukoにfalseがセットされました。");
    }

    public void setRejiFalse()
    {
        reji = false;
        Debug.Log("rejiにfalseがセットされました");
    }

    public void setRejiTrue()
    {
        reji = true;
        Debug.Log("rejiにtrueがセットされました。");
    }

    public void setPotatoFrierFalse()
    {
        potatoFrier = false;
        Debug.Log("potatoFrierにfalseがセットされました。");
    }

    public void setPotatoFrierTrue()
    {
        potatoFrier = true;
        Debug.Log("potatoFrierにtrueがセットされました。");
    }

    public void setPotatoFriedStationFalse()
    {
        potatoFriedStation = false;
        Debug.Log("potatoFriedStationにfalseがセットされました。");
    }

    public void setPotatoFriedStationTrue()
    {
        potatoFriedStation = true;
        Debug.Log("potatoFriedStationにtrueがセットされました。");
    }

    public void setDonutFrierFalse()
    {
        donutFrier = false;
        Debug.Log("donutFrierにfalseがセットされました。");
    }

    public void setDonutFrierTrue()
    {
        donutFrier = true;
        Debug.Log("donutFrierにtrueがセットされました。");
    }

    public void setDonutFriedStationFalse()
    {
        donutFriedStation = false;
        Debug.Log("donutFriedStationにfalseがセットされました。");
    }

    public void setDonutFriedStationTrue()
    {
        donutFriedStation = true;
        Debug.Log("donutFriedStationにtrueがセットされました。");
    }

    public void setOnionFrierFalse()
    {
        onionFrier = false;
        Debug.Log("onionFrierにfalseがセットされました。");
    }

    public void setOnionFrierTrue()
    {
        onionFrier = true;
        Debug.Log("onionFrierにtrueがセットされました。");
    }

    public void setOnionFriedStationFalse()
    {
        onionFriedStation = false;
        Debug.Log("onionFriedStationにfalseがセットされました。");
    }

    public void setOnionFriedStationTrue()
    {
        onionFriedStation = true;
        Debug.Log("onionFriedStationにtrueがセットされました。");
    }

    public void setFriedChickenFrierFalse()
    {
        friedChickenFrier = false;
        Debug.Log("friedChickenFrierにfalseがセットされました。");
    }

    public void setFriedChickenFrierTrue()
    {
        friedChickenFrier = true;
        Debug.Log("friedChickenFrierにtrueがセットされました。");
    }

    public void setFriedChickenFriedStationFalse()
    {
        friedChickenFriedStation = false;
        Debug.Log("friedChickenFriedStationにfalseがセットされました。");
    }

    public void setFriedChickenFriedStationTrue()
    {
        friedChickenFriedStation = true;
        Debug.Log("friedChickenFriedStationにtrueがセットされました。");
    }

    public void setRibFrierFalse()
    {
        ribFrier = false;
        Debug.Log("ribFrierにfalseがセットされました。");
    }

    public void setRibFrierTrue()
    {
        ribFrier = true;
        Debug.Log("ribFrierにtrueがセットされました。");
    }

    public void setRibFriedStationFalse()
    {
        ribFriedStation = false;
        Debug.Log("ribFriedStationにfalseがセットされました。");
    }

    public void setRibFriedStationTrue()
    {
        ribFriedStation = true;
        Debug.Log("ribFriedStationにtrueがセットされました。");
    }

    public void setBigFriedChickenFrierFalse()
    {
        bigFriedChickenFrier = false;
        Debug.Log("bigFriedChickenFrierにfalseがセットされました。");
    }

    public void setBigFriedChickenFrierTrue()
    {
        bigFriedChickenFrier = true;
        Debug.Log("bigFriedChickenFrierにtrueがセットされました。");
    }

    public void setBigFriedChickenFriedStationFalse()
    {
        bigFriedChickenFriedStation = false;
        Debug.Log("bigFriedChickenFriedStationにfalseがセットされました。");
    }

    public void setBigFriedChickenFriedStationTrue()
    {
        bigFriedChickenFriedStation = true;
        Debug.Log("bigFriedChickenFriedStationにtrueがセットされました。");
    }

    public void setParking1(bool b)
    {
        parking1 = b;
        Debug.Log("parking1に" + b + "がセットされました");
    }

    public void setParking2(bool b)
    {
        parking2 = b;
        Debug.Log("parking2に" + b + "がセットされました");
    }

    public void setParking3(bool b)
    {
        parking3 = b;
        Debug.Log("parking3に" + b + "がセットされました");
    }

    public void setParking4(bool b)
    {
        parking4 = b;
        Debug.Log("parking4に" + b + "がセットされました");
    }

    public void setParking5(bool b)
    {
        parking5 = b;
        Debug.Log("parking5に" + b + "がセットされました");
    }

    public void setParking6(bool b)
    {
        parking6 = b;
        Debug.Log("parking6に" + b + "がセットされました");
    }

    public void setParking7(bool b)
    {
        parking7 = b;
        Debug.Log("parking7に" + b + "がセットされました");
    }

    public void setParking8(bool b)
    {
        parking8 = b;
        Debug.Log("parking8に" + b + "がセットされました");
    }

    public void setParking9(bool b)
    {
        parking9 = b;
        Debug.Log("parking9に" + b + "がセットされました");
    }

    public void setParking10(bool b)
    {
        parking10 = b;
        Debug.Log("parking10に" + b + "がセットされました");
    }

    public void setParking11(bool b)
    {
        parking11 = b;
        Debug.Log("parking11に" + b + "がセットされました");
    }

    public void setParking12(bool b)
    {
        parking1 = b;
        Debug.Log("parking1に" + b + "がセットされました");
    }

    public void setParking13(bool b)
    {
        parking13 = b;
        Debug.Log("parking13に" + b + "がセットされました");
    }

    public void setParking14(bool b)
    {
        parking14 = b;
        Debug.Log("parking14に" + b + "がセットされました");
    }

    public void setTerrace(bool b, int num)
    {
        terrace[num] = b;
        Debug.Log("terrace" + num + 1 +"に" + b + "がセットされました");
    }


    public bool getDrinkBool()
    {
        return drink;
    }

    public bool getTrayBool()
    {
        return tray;
    }

    public bool getTray2Bool()
    {
        return tray2;
    }

    public bool getTray3Bool()
    {
        return tray3;
    }

    public bool getBakeBool()
    {
        return bake;
    }

    public bool getReizoukoBool()
    {
        return reizouko;
    }

    public bool getRejiBool()
    {
        return reji;
    }

    public bool getPotatoFrierBool()
    {
        return potatoFrier;
    }

    public bool getPotatoFriedStationBool()
    {
        return potatoFriedStation;
    }

    public bool getDonutFrierBool()
    {
        return donutFrier;
    }

    public bool getDonutFriedStationBool()
    {
        return donutFriedStation;
    }

    public bool getOnionFrierBool()
    {
        return onionFrier;
    }

    public bool getOnionFriedStationBool()
    {
        return onionFriedStation;
    }

    public bool getFriedChickenFrierBool()
    {
        return friedChickenFrier;
    }

    public bool getFriedChickenFriedStationBool()
    {
        return friedChickenFriedStation;
    }

    public bool getRibFrierBool()
    {
        return ribFrier;
    }

    public bool getRibFriedStationBool()
    {
        return ribFriedStation;
    }

    public bool getBigFriedChickenFrierBool()
    {
        return bigFriedChickenFrier;
    }

    public bool getBigFriedChickenFriedStationBool()
    {
        return bigFriedChickenFriedStation;
    }

    public bool[] getTerrace()
    {
        return terrace;
    }
}
