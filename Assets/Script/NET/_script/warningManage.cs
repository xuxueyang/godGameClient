using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warningManage : MonoBehaviour {

    //使用队列管理所有的警告
    public static List<WarningModel> errors = new List<WarningModel>();
    [SerializeField]
    private WarnningWindow window;

	// Update is called once per frame
	void Update () {
        if (errors.Count > 0)
        {
            WarningModel err = errors[0];
            errors.RemoveAt(0);
            window.active(err);
        }
	}
    public static void addError(string errorMessage, float delay =-1)
    {
        errors.Add(new WarningModel(errorMessage,null,delay));
    }
    public static void addError(WarningModel errorModel)
    {
        errors.Add(errorModel);
    }

}
