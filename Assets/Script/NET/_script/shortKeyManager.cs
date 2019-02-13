using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class shortKeyManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public Image image;
    public Text text;
    private basicSoup soup;
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
    public void userSoue()
    {
        soup.useSoup();
        //修改文本
        this.text.text = soup.soupNum.ToString();
    }
    public void SetSoup(basicSoup soup)
    {
        this.soup = soup;
        image.sprite = Resources.Load<Sprite>("Image/SoupImage/" + soup.soupName);
        this.text.text = soup.soupNum.ToString();
    }
}
