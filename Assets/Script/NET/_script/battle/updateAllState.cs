using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateAllState : MonoBehaviour {

    private void Start()
    {
        controlEnergy.GetInstance().start();
        controlEnergy.GetInstance().setManagerUpdate(this);
       
    }
    private void Update()
    {
        controlEnergy.GetInstance().update();
    }
}
