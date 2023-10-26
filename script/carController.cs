using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class carController : MonoBehaviour
{
    Vector3 destroyPos = new Vector3(9.96f, 0f, -11.37f);

    public Transform parkingPos;

    public GameObject customer;

    public int number = 0;

    string state = "";

    NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        state = "parking";
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            //駐車上に車を止める
            case "parking":
                agent.SetDestination(parkingPos.position);
                if(agent.remainingDistance < 0.01f && !agent.pathPending)
                {
                    //客の人間オブジェクトを生成
                    agent.enabled = false;
                    state = "";
                    GameObject c = Instantiate(customer, transform.position,Quaternion.identity);
                    c.transform.GetChild(Random.Range(1, 11)).gameObject.SetActive(true);
                    c.name = number.ToString() + "man";
                    c.transform.position += new Vector3(0, 0, 1.2f);
                    customerController cc = c.GetComponent<customerController>();
                    cc.car = this.gameObject;
                    
                }
                break;

            //終了して削除
            case "goHome":
                agent.enabled = true;
                agent.SetDestination(destroyPos);
                if(agent.remainingDistance < 0.01f && !agent.pathPending)
                {
                    Destroy(this.gameObject);
                }

                break;
        }
    }

    public void setGoHome()
    {
        state = "goHome";
    }
}
