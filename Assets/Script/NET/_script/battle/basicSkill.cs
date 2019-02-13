using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicSkill {

    public int skillId;
    public string skillName;
    public int  needEnergy;
    public string effectName;
    public bool isCanStorage = false;
    public float skillTime ;
    public float skillRadius = 10;
    public int direction = 0;

    //释放技能
    protected bool isInReleaseSkill = false;
    protected GameObject effect;
    public virtual void release(float yellowEnergy, Vector3 skillPoint)
    {
        //释放技能
        Debug.Log("basicSkill+release");
    }
    public virtual void willRelease(float yellowEnergy, Vector3 skillPoint)
    {
        //准备释放。主要用于可以蓄力的技能
        Debug.Log("basicSkill+willRelease");
    }
}
