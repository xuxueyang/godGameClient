using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl {
    public int playerId;


    private playerModel model = new playerModel();
    /// <summary>
    /// 设置人物的模型数据类
    /// </summary>
    /// <param name="model"></param>
    public void setPlayerModel(playerModel model)
    {
        this.model = model;
    }
    public void setPlayerModel(playerDTO model)
    {
        
        this.model.skillArray = new Dictionary<int, basicSkill>();
        foreach (var key in model.skillArray.Keys)
        {
            this.model.skillArray.Add(key, model.skillArray[key]);
        }
        this.model.soupArray = new Dictionary<int, basicSoup>();
        foreach (var key in model.soupArray.Keys)
        {
            this.model.soupArray.Add(key, model.soupArray[key]);
        }
    }
    public playerModel getPlayerModel()
    {
        return model;
    }


    public playerControl(int id,playerModel model)
    {
      
        this.model = model;
        this.playerId = id;
    }
    public playerControl(int id, playerDTO model)
    {
        
        this.model.skillArray = new Dictionary<int, basicSkill>();
        foreach(var key in model.skillArray.Keys)
        {
            this.model.skillArray.Add(key, model.skillArray[key]);
        }
        this.model.soupArray = new Dictionary<int, basicSoup>();
        foreach (var key in model.soupArray.Keys)
        {
            this.model.soupArray.Add(key, model.soupArray[key]);
        }

        this.playerId = id;
    }


    private GameObject player;

    public void releaseSkill(basicSkill skill, float yellowEnergy)
    {
        skill.release(yellowEnergy,gloabManagerClass.skillBornPointController.skillBornPoints[0].point.transform.position);
    }
    public void willReleaseSkill(basicSkill skill, float yellowEnergy)
    {
        skill.willRelease(yellowEnergy, gloabManagerClass.skillBornPointController.skillBornPoints[0].point.transform.position);
    }
}
