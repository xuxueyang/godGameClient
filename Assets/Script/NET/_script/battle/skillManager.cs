using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class skillManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    //管理技能的类
    public basicSkill skill;

    public Image skillImage;
    public Text skillText;


    private bool isWantToReleaseSkill = false; 

    private bool isReleaseSkill = false;

    public void SetSkill(basicSkill skill)
    {
        this.skill = skill;
        //加载图片和修改技能文本名字
        skillText.text = skill.skillName;
        skillImage.sprite = Resources.Load<Sprite>("Image/SkillImage/"+skill.skillName);
    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointDown");
        if (!this.skill.isCanStorage)
        {
            this.isWantToReleaseSkill = true;
        }
        else
        {
            isReleaseSkill = controlEnergy.GetInstance().setSkill(skill);
            if (isReleaseSkill == false)
                return;
            isReleaseSkill = controlEnergy.GetInstance().buttonDown(eventData);
        }
          this.skillImage.color = new Color(this.skillImage.color.r, this.skillImage.color.g, this.skillImage.color.b, 0.5f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {     
        if (!this.skill.isCanStorage)
        {
            if (isWantToReleaseSkill)
            {
                isReleaseSkill = controlEnergy.GetInstance().setSkill(skill);
                if (isReleaseSkill == false)
                    return;
                isReleaseSkill = controlEnergy.GetInstance().buttonDown(eventData);

            }
            else
            {
                //说明遇到了bug，1.蓄力的：导致没有按下就down，在蓄力，让其释放
                //2.不可蓄力，但是却莫名抬起。忽视
                if (isReleaseSkill)
                {
                    controlEnergy.GetInstance().buttonUp(eventData);

                }
            }
            
        }
        else
        {
            if (isReleaseSkill)
            {
                controlEnergy.GetInstance().buttonUp(eventData);
            }
        }
        isReleaseSkill = false;
        this.isWantToReleaseSkill = false;
        this.skillImage.color = new Color(this.skillImage.color.r, this.skillImage.color.g, this.skillImage.color.b, 1.0f);
    }
}
