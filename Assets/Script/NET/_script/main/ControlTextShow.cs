using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTextShow :MonoBehaviour {

    public int canShowCharNumber = 6;
    public Text text;
	// Use this for initialization
	void Start () {
        //text = this.GetComponent<Text>();
        controllTextShow();
    }
	

    public void controllTextShow()
    {
        if (text != null)
        {
            if (text.text.Length > this.canShowCharNumber)
            {
                text.text = text.text.Substring(0, this.canShowCharNumber - 1);
                text.text += "...";
            }
        }
    }
    public void controllTextShow(Text text)
    {
        if(text!=null)
        {
            if (text.text.Length > this.canShowCharNumber)
            {
                text.text = text.text.Substring(0, this.canShowCharNumber - 1);
                text.text += "...";
            }
        }
    }
}
