using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlAngle : MonoBehaviour {
        private CharacterController controller;
        [SerializeField]
        private Camera mainCamera;
        public int speed = 300;
        public float speedR = 0.01f;
        public float length = 0;
        private float lastX = 0;
        private bool isRCamera = false;
        private float angle = 0;

        // Use this for initialization
        void Start()
        {
            controller = this.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {

            controller.SimpleMove(new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed));

            if (Input.GetMouseButton(1))
            {
                if (isRCamera)
                {
                    length = Input.mousePosition.x - lastX;
                    mainCamera.transform.Rotate(new Vector3(0, length * speedR, 0));
                }
                else
                {
                    isRCamera = true;
                    lastX = Input.mousePosition.x;
                }
            }
            else
            {
                isRCamera = false;
                lastX = 0;
            }

        }
}
