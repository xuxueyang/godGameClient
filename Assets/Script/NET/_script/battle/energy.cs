using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energy : MonoBehaviour {

    [SerializeField]
    /// <summary>
    /// 显示图片
    /// </summary>
    private Image energyImage;

    #region 蓝量的


    /// <summary>
    /// 当前蓝量
    /// </summary>
    public float blueEnergy = 1f;
    public void changeBlue()
    {
        //表示蓝量
        if(energyImage.color!=Color.blue)
        {
            energyImage.color = Color.blue;
            this.energyImage.fillAmount = blueEnergy;
        }     
    }
    public float getBlueEnergy()
    {
        return blueEnergy;
    }
    public bool blueEnergyIsEnough(float energy)
    {
        return this.blueEnergy >= energy ? true : false;
    }
    public void addBlueEnergy(float add)
    {
        this.blueEnergy += add;
        if (blueEnergy > 1)
            blueEnergy = 1;
    }
    public void subBlueEnergy(float sub)
    {
        this.blueEnergy -= sub;
        if (blueEnergy < 0)
            blueEnergy = 0;
    }
    #endregion


    #region 蓄力的金色能量的



    /// <summary>
    /// 当前金色能量
    /// </summary>
    private float yellowEnergy = 0;


    public void changeYellow()
    {
        //表示蓄力
        if (energyImage.color != new Color(1, 195 / 255, 0))
        {
            energyImage.color = new Color(1, 195 / 255, 0);
            yellowEnergy = 0;
            this.energyImage.fillAmount = yellowEnergy;
        }
    }

    public float getYellowEnergy()
    {
        return yellowEnergy;
    }

    public bool yellowEnergyIsEnough(float energy)
    {
        return this.yellowEnergy >= energy ? true : false;
    }



    public void addYellowEnergy(float add)
    {
        this.yellowEnergy += add;
        if (yellowEnergy > 1)
            yellowEnergy = 1;
    }
    public void subYellowEnergy(float sub)
    {
        this.yellowEnergy -= sub;
        if (yellowEnergy < 0)
            yellowEnergy = 0;
    }


    #endregion
    public float getImageFillAmount()
    {
        return this.energyImage.fillAmount;
    }
    //闪烁面板
    public void changeAlpha(float alpha)
    {
        this.energyImage.color = new Color(this.energyImage.color.r, this.energyImage.color.g, this.energyImage.color.b, alpha);
    }

    //显示动态的能量变化动画
    public void showAnimatiton(bool isInBlueState,float recoverSize)
    {
        if (isInBlueState)
        {
            if(this.energyImage.fillAmount<=blueEnergy)
            {
                if (this.energyImage.fillAmount + recoverSize > blueEnergy)
                    this.energyImage.fillAmount = blueEnergy;
                else
                    this.energyImage.fillAmount += recoverSize;
            }
            else
            {
                if (this.energyImage.fillAmount - recoverSize < blueEnergy)
                    this.energyImage.fillAmount = blueEnergy;
                else
                    this.energyImage.fillAmount -= recoverSize;
            }
        }
        else
        {
            addYellowEnergy(recoverSize); ;//因为金色能量只有每次蓄力时，在update里增加
            this.energyImage.fillAmount = yellowEnergy;
        }
    }

}
