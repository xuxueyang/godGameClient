using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

   public class smallBlueEnergySoup:basicSoup
    {
    private int recoverBlueEnergyNum = 500;
        public smallBlueEnergySoup(int soupId,string soupName,int soupNum,int recoverBlueEnergyNum, string soupEffect="")
        {
            this.soupId = soupId;
            this.soupName = soupName;
            this.soupEffect = soupEffect;
            this.soupNum = soupNum;
            this.recoverBlueEnergyNum = recoverBlueEnergyNum;
        }
    public override void useSoup()
    {
        this.soupNum -= 1;
        //TODO产生物品的效果。
        controlEnergy.GetInstance().addBlueEnergy(recoverBlueEnergyNum);
        //反馈给玩家
        //TODO,这里应该发送网络数据，通过网络，让globalBattleControl来决定和调用这个回复效果
        //TODO,或者，在这里实现自己的UI，而别人那里，再由网络接受
    }
}
