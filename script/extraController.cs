using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class extraController : MonoBehaviour
{
    float time = 0f;
    float instantiateTime = 0;

    public GameObject walker;
    public Transform walkStartPosition;
    public Transform walkEndPosition;

    public GameObject car;
    public Transform carStartPosition;
    public Transform carEndPosition;

    //歩行者
    GameObject walker1;
    GameObject walker2;
    GameObject walker3;


    //車
    GameObject car1;
    GameObject car2;

    // Start is called before the first frame update
    void Start()
    {
        instantiateTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime;
        //ランダムな時間でランダムなオブジェクトを生成
        if(instantiateTime < time)
        {
            int r = Random.Range(0, 2);
            if(r == 0)
            {
                walkerInstantiateController();
            } else
            {
                carInstantiateController();
            }
            time = 0;
            instantiateTime = Random.Range(5,15);
        }
    }

    

    //歩行者生成コントロール
    void walkerInstantiateController()
    {
        if (walker1 == null)
        {
            walker1 = instantiateWalker();
        } else if(walker2 == null)
        {
            walker2 = instantiateWalker();
        } else if(walker3 == null)
        {
            walker3 = instantiateWalker();
        }

    }

    //車生成コントロール
    void carInstantiateController()
    {
        if(car1 == null)
        {
            car1 = instantiateCar();
        } else if(car2 == null)
        {
            car2 = instantiateCar();
        }
    }

    //歩行者の生成
    GameObject instantiateWalker()
    {
        GameObject g = Instantiate(walker, walkStartPosition);
        g.GetComponent<NavMeshAgent>().SetDestination(walkEndPosition.position);
        g.GetComponent<Animator>().SetInteger("ID", 1);
        return g;
    }

    //歩行者の生成
    GameObject instantiateCar()
    {
        GameObject g = Instantiate(car, carStartPosition);
        g.GetComponent<NavMeshAgent>().SetDestination(carEndPosition.position);
        return g;
    }

}
