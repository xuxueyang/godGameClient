using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class flameSkill : basicSkill
{
    private frameSkillEffect tmpEffect;
    public flameSkill(int needEnergy,string skillName,string effectName,float skillTime,bool isCanStorage=false)
    {
        this.needEnergy = needEnergy;
        this.skillName = skillName;
        this.effectName = effectName;
        this.isCanStorage = false;
        this.skillTime = skillTime;
        this.isCanStorage = isCanStorage;
    }
    public override void willRelease(float yellowEnergy, Vector3 skillPoint)
    {
        //蓄力技能开始蓄力，如果是不可以蓄力技能，忽视
       
        if (this.isInReleaseSkill)
        {
            //根据传入的yellowEnergy开始更新特效大小。
            //需要规格化大小。           

            this.tmpEffect.willReleaseSkill(yellowEnergy);
            
        }
        else
        {
            //在controlEnergy中已经有是不是可以蓄力的判断
            //生成
            this.isInReleaseSkill = true;
            //根据能量程度不同，生成不同特效
            if (effect == null)
            {
                this.effect = Resources.Load<GameObject>("effect/" + "effect_" + this.effectName);

            }
            this.tmpEffect = GameObject.Instantiate(effect, skillPoint, Quaternion.identity).GetComponent<frameSkillEffect>();

        }

    }
    public  override void release(float yellowEnergy, Vector3 skillPoint)
    {
        if (!isInReleaseSkill)
        {
            if (effect == null)
            {
                this.effect = Resources.Load<GameObject>("effect/" + "effect_" + this.effectName);
            }
            this.tmpEffect = GameObject.Instantiate(effect, skillPoint, Quaternion.identity).GetComponent<frameSkillEffect>();
            
        }
        //TODO决定技能方向
        switch (this.direction)
        {
            case 0:
                //

                break;
            default:
                break;
        }
        //决定技能位置
        
        Vector3 des = new Vector3(skillPoint.x+this.skillRadius*0.5f, skillPoint.y, skillPoint.z+this.skillRadius * 0.5f);
        Debug.Log("1:" + skillPoint + "2:" + des);
        Debug.DrawRay(skillPoint, des,Color.green);

        this.tmpEffect.releaseSkill(yellowEnergy, des);
        isInReleaseSkill = false;
        this.tmpEffect = null;
    }

}
