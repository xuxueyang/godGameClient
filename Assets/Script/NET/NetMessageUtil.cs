using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NetMessageUtil : MonoBehaviour {
    IHandler loginHandler;
	// Use this for initialization
	void Start () {
        loginHandler = GetComponent<LoginHandler>();
        //InvokeRepeating("checkMessage", 1f / 60, 1f / 60);
    }
	
	// Update is called once per frame
	void Update () {
        while (NetIO.Instance.messages.Count > 0)
        {
            SocketModel model = NetIO.Instance.messages[0];
            //开启异步
            StartCoroutine("MessageReceive", model);
            NetIO.Instance.messages.RemoveAt(0);
        }
	}

    void MessageReceive(SocketModel model)
    {

        switch (model.type)
        {
           
        }
    }
}
