using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCharacterControl : MonoBehaviour {
    public GameObject playerModel;

    // Use this for initialization
    void Start () {
        //人物控制：上下左右wsad移动(委托给character control）。e锁定周围角色。鼠标左键选择目标，右键转换视野。
        GameObject.Instantiate(playerModel, this.transform.position, Quaternion.identity, this.transform);
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
