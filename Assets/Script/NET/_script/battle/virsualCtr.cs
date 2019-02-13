using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class virsualCtr : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
    public ThirdPersonUserControl control;
    public Image smallImage;

    private Vector3 smallImageInitPos;
    private Vector3 smallImageInitScreenPos;
    private Vector2 moveV;
    private float vLenght;

    private float r;
    private bool isDown = false;

    private void Start()
    {
        this.smallImageInitPos = this.smallImage.transform.position;
        this.smallImageInitScreenPos = Camera.main.WorldToScreenPoint(this.smallImageInitPos);
        this.smallImageInitScreenPos.z = 0;
        float width = this.GetComponent<RectTransform>().sizeDelta.x;
        float height = this.GetComponent<RectTransform>().sizeDelta.y;
        r = (float)Math.Sqrt(width * width+height * height)/4;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        this.isDown = false;
        this.smallImage.transform.position = this.smallImageInitPos;
    }
    private void Update()
    {
        if (isDown)
        {
            //如果触摸移动了
            Vector2 tmp = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
            if (tmp.sqrMagnitude >= 0.05)
            {
                Debug.Log("111");
                setChange();
            }

            //更新控制杆位置以及行动
            if (vLenght > r)
            {
                vLenght = r;
                control.setHAndV(-vLenght * moveV.x/r, -vLenght * moveV.y/r);
            }

            else if (vLenght < 0.5 * r)
            {
                control.setHAndV(-vLenght * moveV.x * 0.5f/r, -vLenght * moveV.y * 0.5f/r);
            }
            else
            {
                control.setHAndV(-vLenght * moveV.x/r, -vLenght * moveV.y/r);
            }
            this.smallImage.transform.position = this.smallImageInitPos + new Vector3(-vLenght * moveV.x, -vLenght * moveV.y, 0);

        }
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        //获取鼠标位置
        this.isDown = true;
        setChange();

    }
    private void setChange()
    {
        Vector3 ScreenPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        Vector3 GUIPos = new Vector3(ScreenPos.x, ScreenPos.y, 0);
        moveV = new Vector2(GUIPos.x - this.smallImageInitScreenPos.x, GUIPos.y - this.smallImageInitScreenPos.y);
        vLenght = (float)Math.Sqrt(moveV.sqrMagnitude);
        moveV = moveV.normalized;
    }



}
