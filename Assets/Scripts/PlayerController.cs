using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace BNF
{
    public class PlayerController : Singleton<PlayerController>
    {
       
        [SerializeField]
        float moveSpeed;

        [SerializeField]
        float rotationSpeed;

        [SerializeField]
        float distanceFromFloor = 1f;

        [SerializeField]
        float turnSpeed = 1;

        float yaw = 0, pitch = 0;
        public float Pitch
        {
            get{ return pitch; }
        }

        float mouseSensitivity = 1;

        Vector2 moveInput;
        Vector2 aimInput;

        float pitchDirection = -1;

        float minPitch = -80;
        float maxPitch = 80;

        Vector3 currentVelocity = Vector3.zero;

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = -1; //10;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();

            Rotate();

            Move();
        }

        private void Move()
        {
            var targetDirection = transform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y));
            var targetVelocity = targetDirection.normalized * moveSpeed;

            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, turnSpeed * Time.deltaTime);

            var newPosition = transform.position + currentVelocity * Time.deltaTime;
            newPosition.y = distanceFromFloor;
            transform.position = newPosition;
        }

        private void Rotate()
        {
            yaw += aimInput.x * Time.deltaTime * rotationSpeed * mouseSensitivity;
            yaw %= 360;

            pitch += aimInput.y * Time.deltaTime * rotationSpeed * mouseSensitivity * pitchDirection;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);


            transform.eulerAngles = new Vector3(0, yaw, 0);
   
        }

        void CheckInput()
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            aimInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        }

    }
}