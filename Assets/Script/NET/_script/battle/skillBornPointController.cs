using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillBornPointController : MonoBehaviour {

    public List<skillBornPoint> skillBornPoints = new List<skillBornPoint>();
    private void Start()
    {
        gloabManagerClass.skillBornPointController = this;
    }
}
