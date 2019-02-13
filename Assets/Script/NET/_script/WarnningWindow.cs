using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WarnningWindow : MonoBehaviour {


    [SerializeField]
    private Text text;

    WarningResult result;//回调

    public void active(WarningModel value)
    {
        GetComponent<errorPanel>().showPanel();
        text.text = value.value;
        this.result = value.result;
        if (value.delay > 0)
        {
            Invoke("close", value.delay);
        }
        gameObject.SetActive(true);
    }
    public void close()
    {
        if (IsInvoking("close"))
        {
            CancelInvoke("close");
        }
        GetComponent<errorPanel>().hidePanel();
        if (result != null)
        {
            result();
        }
    }
}
