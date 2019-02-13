using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class controlBloodEnergyUI : MonoBehaviour {
    private static int intBloodSize = 1000;

    private float animationSize = 0.2f;
    private float currentBlood = intBloodSize;
    public Image bloodImage;
    public Text text;
    public bool isFromTop = true;

    public void addBlood(float energy)
    {
        Debug.Log("addBlood");
        if (energy <= 1)
        {
            energy *= intBloodSize ;
        }
        currentBlood += energy;
        if (currentBlood > intBloodSize)
            currentBlood = intBloodSize;
        //bloodImage.fillAmount = currentBlood/intBloodSize;
        this.text.text = currentBlood / intBloodSize * 100 + "%";
    }
    public void subBlood(float energy)
    {

        if (energy <= 1)
        {
            energy *= intBloodSize;
        }
        currentBlood -= energy;
        if (currentBlood < 0)
            currentBlood = 0;
        //bloodImage.fillAmount = currentBlood / intBloodSize;
        this.text.text = currentBlood / intBloodSize * 100 + "%";
    }
    private void Update()
    {
        //动画更新血量UI
        float tmp = bloodImage.fillAmount - currentBlood / intBloodSize;
        if (tmp > 0)
        {
            bloodImage.fillAmount -= Mathf.Min(tmp, this.animationSize) * Time.deltaTime;
        }
        else if (tmp < 0)
        {
            bloodImage.fillAmount -= Mathf.Min(tmp, -this.animationSize) * Time.deltaTime;
        }
       
    }
    private void Start()
    {
        gloabManagerClass.bloodShowUI = this;
    }
}
