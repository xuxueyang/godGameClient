using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class smallBloodSoup : basicSoup
{
    public int recoverBloodEnergyNum = 500;
    public smallBloodSoup(int soupId, string soupName, int soupNum, int recoverBloodEnergyNum, string soupEffect = "")
    {
        this.soupId = soupId;
        this.soupName = soupName;
        this.soupEffect = soupEffect;
        this.soupNum = soupNum;
        this.recoverBloodEnergyNum = recoverBloodEnergyNum;
    }
    public override void useSoup()
    {
        this.soupNum -= 1;
        //TODO产生物品的效果。
        gloabManagerClass.bloodShowUI.addBlood(this.recoverBloodEnergyNum);
        //controlEnergy.GetInstance().addBlueEnergy(recoverBlueEnergyNum);

    }
}
