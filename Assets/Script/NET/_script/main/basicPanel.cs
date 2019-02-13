using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class basicPanel : MonoBehaviour {
    public Image mask;
    public GameObject panel;
    public float animationSize = 0.3f;

    public virtual void showPanel()
    {

        this.gameObject.SetActive(true);
        StartCoroutine(show());
    }
    IEnumerator show()
    {

        //将面板浮现出来，修改透明度
        Image image = this.panel.GetComponent<Image>();
        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + animationSize);
            yield return new WaitForSeconds(0.5f);
        }

        yield return true;
    }

    public virtual void hidePanel()
    {
        StartCoroutine(hide());
        this.gameObject.SetActive(false);
    }
    IEnumerator hide()
    {
        //将面板隐藏，修改透明度
        Image image = this.panel.GetComponent<Image>();
        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - animationSize);
            yield return new WaitForSeconds(0.5f);
        }

        yield return true;
    }

}
