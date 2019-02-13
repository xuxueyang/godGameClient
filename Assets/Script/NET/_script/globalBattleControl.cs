using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 战斗场面的全局控制，包括发送网络请求，获取数据，初始化游戏等
/// </summary>
public class globalBattleControl : MonoBehaviour {
    public Dictionary<int, playerControl> team1Dic = new Dictionary<int, playerControl>();
    public Dictionary<int, playerControl> team2Dic = new Dictionary<int, playerControl>();
    public static int mainPlayerId = 0;
    public static int mainPlayerTeamId;


    private void Start()
    {
        //发送数据请求
        playerDTO dto = new playerDTO();
        dto.skillArray = new Dictionary<int, basicSkill>();
        dto.soupArray = new Dictionary<int, basicSoup>();
        dto.playerId = 0;
        dto.playerTeamId = 1;
        for(int i=1;i<=4;i++)
        {
            dto.skillArray.Add(i, new flameSkill(120,"火之契约", "火之契约",5.0f,true));
        }
        dto.soupArray.Add(1, new smallBlueEnergySoup(1, "小蓝瓶", 99, 500));
        dto.soupArray.Add(2, new smallBloodSoup(2, "小血瓶", 99, 250));
        initPlayerControl(dto);
        gloabManagerClass.battleControl = this;

        //初始化技能生成点
        gloabManagerClass.skillBornPointController = this.gameObject.GetComponent<skillBornPointController>();
        GameObject[] skillBornPoint = GameObject.FindGameObjectsWithTag("SkillPoint");
        foreach(var item in skillBornPoint)
        {
            gloabManagerClass.skillBornPointController.skillBornPoints.Add(new skillBornPoint(item));
        }
    }




    /// <summary>
    /// 根据初始数据DTO，初始化playerControl
    /// </summary>
    private void initPlayerControl(playerDTO dto)
    {
        //TODO设置DTO中的playerModel传递给playerControl以及id
        playerControl player = new playerControl(1, dto);
        
        //if这个id是和我方id一样的话，初始化技能面板
        //初始化技能面板
        if(dto.playerId==globalBattleControl.mainPlayerId)
        {
            mainPlayerTeamId = dto.playerTeamId;
            GameObject[] skillPanels = GameObject.FindGameObjectsWithTag("Skill");
            foreach (var item in skillPanels)
            {
                skillManager skillManager = item.GetComponent<skillManager>();
                int i = int.Parse(item.name.Substring(2));
                
                ///这里明确，技能标号从1开始
                skillManager.SetSkill(player.getPlayerModel().skillArray[i]);
            }
        }
        //初始化快捷键
        if (dto.playerId == globalBattleControl.mainPlayerId)
        {
            mainPlayerTeamId = dto.playerTeamId;
            GameObject[] skillPanels = GameObject.FindGameObjectsWithTag("ShortKey");
            foreach (var item in skillPanels)
            {
                shortKeyManager skillManager = item.GetComponent<shortKeyManager>();
                int i = int.Parse(item.name.Substring(3));

                ///这里明确，技能标号从1开始
                skillManager.SetSoup(player.getPlayerModel().soupArray[i]);
            }
        }

        if (dto.playerTeamId == 1)
            team1Dic.Add(dto.playerId, player);
        else
            team2Dic.Add(dto.playerId, player);

    }

    public void addBloodEnergy(int playerId,float energy)
    {

    }
    public void subBloodEnergy(int playerId, float energy)
    {

    }
    public void addBlueEnergy(int playerId, float energy)
    {

    }
    public void subBlueEnergy(int playerId, float energy)
    {

    }

}
