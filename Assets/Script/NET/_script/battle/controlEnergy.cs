using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 控制能力面板的脚本
/// </summary>
public class controlEnergy  {
    public globalBattleControl globalBattleControl;
    /// <summary>
    /// 仅允许释放一个技能每次。利用这个静态变量进行绝对释放与否
    /// </summary>
    private  basicSkill skill;

    public  bool setSkill(basicSkill skill)
    {
        if(this.skill==null&&skill!=null)
        {
            this.skill = skill;
            return true;
        }
        return false;
    }
    
    private MonoBehaviour managerUpdate;
    public void setManagerUpdate(MonoBehaviour managerDelegate)
    {
        this.managerUpdate = managerDelegate;
    }

    private List<energy> energys;
    public float intEnergySize = 100f;
    public float recoverBlueSize = 2.0f;
    public float recoverYellowSize = 1.5f;

    private List<energy> animationList;

    /// <summary>
    /// 能量槽数目
    /// </summary>
    public int energysNum;
    /// <summary>
    /// 指向最右边能量槽的指针
    /// </summary>
    private int currentEnergy;
    private int currentYellowEnergy=0;
    private bool isInBlueState = true;

    private bool isInshine = false;
    private static controlEnergy instance;
    private bool isFromRight=true;
    private bool isInReleaseSkill = false;

    public void start()
    {
            globalBattleControl = GameObject.FindGameObjectWithTag("globalBattleControl").GetComponent<globalBattleControl>();
    }
    public void update()
    {
        

        //播放energys的动画
        //主要是纠正filleamount和blueEnergy、yellowEnergy的数值的不对头。
        //干脆每一帧都变化好了
        if (isInBlueState)
        {
            
            //如果从左更新，加蓝没问题，如果从右边开始更新，减蓝没问题。
            //从右向左更新fillAmount的渐变动画
            if(isFromRight)
            {
                for (int i = this.energysNum - 1; i >= 0; i--)
                {
                    var item = energys[i];
                    if (item.getImageFillAmount() != item.getBlueEnergy() && !animationList.Contains(item))
                    {
                        animationList.Add(item);
                    }
                }
            }
            else
            {
                foreach(var item in this.energys)
                {
                    if (item.getImageFillAmount() != item.getBlueEnergy() && !animationList.Contains(item))
                    {
                        animationList.Add(item);
                    }
                }
            }
            //只播放一个动画
            if (animationList.Count > 0)
            {
                energy item = animationList[0];
                item.showAnimatiton(isInBlueState, recoverBlueSize * Time.deltaTime);
                if (item.getImageFillAmount() == item.getBlueEnergy())
                    animationList.Remove(item);
            }
        }
        else
        {
            //从左向右更新
            if (this.energys[currentYellowEnergy].getYellowEnergy() == 1 && currentYellowEnergy < this.energysNum - 1)
                currentYellowEnergy += 1;
            this.energys[currentYellowEnergy].showAnimatiton(isInBlueState, recoverYellowSize * Time.deltaTime);
            //如果正在释放技能，就不断通知蓄力

            if (globalBattleControl.mainPlayerTeamId == 1)
            {
                globalBattleControl.team1Dic[globalBattleControl.mainPlayerId].willReleaseSkill(skill,currentYellowEnergy+energys[currentYellowEnergy].getYellowEnergy());
            }
            else
            {
                globalBattleControl.team2Dic[globalBattleControl.mainPlayerId].willReleaseSkill(skill, energys[currentYellowEnergy].getYellowEnergy());
            }
        }
    }
    

    private void updateAllEnergyState()
    {
        foreach(var item in energys)
        {
            if (this.isInBlueState)
            {
                item.changeBlue();
            }
            else
            {
                item.changeYellow();
            }
        }
    }

    private controlEnergy()
    {

    }


    public static controlEnergy GetInstance()
    {
        if (controlEnergy.instance == null)
        {
            controlEnergy.instance = new controlEnergy();
            instance.animationList = new List<energy>();
            instance.energys = new List<energy>();
            GameObject[] items =  GameObject.FindGameObjectsWithTag("energy");
            foreach(var item in instance.sortEnergy(items))
            {
                instance.energys.Add(item.Value);
            }
            instance.energysNum = instance.energys.Count;
            instance.currentEnergy = instance.energysNum - 1;
        }
        return controlEnergy.instance;
    }

    private Dictionary<float,energy> sortEnergy(GameObject[] items)
    {
        //根据posx从小到达排序加入其中
        Dictionary<float, energy> map = new Dictionary<float, energy>();
        foreach (var item in items)
        {
            float f = item.GetComponent<RectTransform>().anchoredPosition.x;
            map.Add(f, item.GetComponent<energy>());
        }
        return map.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
        
    }


    public void setEnergys(List<energy> energys)
    {
        instance.energys = energys;
        instance.energysNum = energys.Count;
        instance.currentEnergy = instance.energysNum - 1;
    }


    public  bool buttonDown(PointerEventData eventData)
    {

        // 准备释放技能
        bool r = true;
        //先判断技能能量够不够
        r = isEnough(skill.needEnergy/ instance.intEnergySize, false);
        if (r)
        {
            //内含动画
            subReleaseSkillEnergy(skill.needEnergy / instance.intEnergySize, false);
            //如果可以蓄力，那么开始开始蓄力
          
            if(this.skill.isCanStorage)
            {
                //同时通知技能，准备释放技能
                this.isInReleaseSkill = true;
                currentYellowEnergy = 0;
                isInBlueState = false;
                updateAllEnergyState();
            }
            else
            {
                //闪烁能量槽作为动画
                if (!isInshine)
                {
                    managerUpdate.StartCoroutine(shine(1f, 0.8f));
                }
                //立刻释放技能，不需要抬起反馈
                if (globalBattleControl.mainPlayerTeamId == 1)
                {
                    globalBattleControl.team1Dic[globalBattleControl.mainPlayerId].releaseSkill(skill, 0);
                }
                else
                {
                    globalBattleControl.team2Dic[globalBattleControl.mainPlayerId].releaseSkill(skill, 0);
                }
                skill = null;
            }
        }
        else
        {
            //能量不够，闪烁面板,同时取消技能绑定！
            if (!isInshine)
            {
                managerUpdate.StartCoroutine(shine(2f, 0.2f));
            }
            this.skill = null;
            
        }
        return r;
    }
    private IEnumerator shine(float time,float alpha)
    {
        while (time > 0)
        {
            foreach(var item in this.energys)
            {
                item.changeAlpha(alpha);
            }
            yield return new WaitForSeconds(0.25f);
            foreach (var item in this.energys)
            {
                item.changeAlpha(1);
            }
            yield return new WaitForSeconds(0.25f);
            time -= 0.5f;
        }
        isInshine = false;
    }

    public  void buttonUp(PointerEventData eventData)
    {
        if (skill.isCanStorage)//保险期间再确认一下
        {
            //真正释放技能..需要获取，金色能量数
            float yellowEnergy = currentYellowEnergy + energys[currentYellowEnergy].getYellowEnergy();
            if (globalBattleControl.mainPlayerTeamId == 1)
            {
                globalBattleControl.team1Dic[globalBattleControl.mainPlayerId].releaseSkill(skill, yellowEnergy);
            }
            else
            {
                globalBattleControl.team2Dic[globalBattleControl.mainPlayerId].releaseSkill(skill, yellowEnergy);
            }
            skill = null;
            isInReleaseSkill = false;
            isInBlueState = true;
            updateAllEnergyState();
            currentYellowEnergy = 0;
        }
    }


    public void addBlueEnergy(float energy)
    {
        this.isFromRight = false;
        if(energy>1)
        {
            energy /= this.intEnergySize;
        }
        
        while (energy > 0)
        {
            
            if (this.energys[currentEnergy].getBlueEnergy() + energy > 1 && currentEnergy < energysNum - 1)
            {
                this.energys[currentEnergy].addBlueEnergy(energy);
                energy -= 1 - this.energys[currentEnergy].getBlueEnergy();
                currentEnergy += 1;
            }
            else
            {
                this.energys[currentEnergy].addBlueEnergy(energy);
                energy = 0;
            }
        }
    }
    private void subAllEnergy(float energy,bool isYellowEnergy)
    {
        this.isFromRight = true;
        if(!isEnough(energy, isYellowEnergy))
        {
            energy item;
            //删除所有技能的蓝量
            for(int i= currentEnergy; i>=0;i--)
            {
                item = this.energys[i];
                if (isYellowEnergy)
                    item.subYellowEnergy(energy);
                else
                    item.subBlueEnergy(energy);
            }
            currentEnergy = 0;
        }
        else
        {
            energy item;

            //根据energy删除技能能量
            while (energy > 0)
            {
                item = this.energys[currentEnergy];
                if (isYellowEnergy)
                {
                    energy -= item.getYellowEnergy();
                    item.subYellowEnergy(energy+ item.getYellowEnergy());
                    if (item.getYellowEnergy() == 0)
                    {
                        currentEnergy -= 1;
                    }
                }
                else
                {
                    energy -= item.getBlueEnergy();
                    item.subBlueEnergy(energy + item.getBlueEnergy());
                    if (item.getBlueEnergy() == 0)
                    {
                        currentEnergy -= 1;
                    }
                        
                }

            }

        }

    }
    private bool subReleaseSkillEnergy(float energy, bool isYellowEnergy)
    {
        if (!isEnough(energy, isYellowEnergy))
            return false;
        subAllEnergy(energy, isYellowEnergy);
        return true;

    }
    private bool isEnough(float energy,bool isYellowEnergy)
    {
        if (isYellowEnergy)
            return currentEnergy + this.energys[currentEnergy].getYellowEnergy() >= energy ? true : false;
        else
        {
            return currentEnergy + this.energys[currentEnergy].getBlueEnergy() >= energy ? true : false;
        } 
    }


}
