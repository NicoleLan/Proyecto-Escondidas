using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviourPunCallbacks
{
    public float speed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    bool corriendo;

    private CharacterController controller;
    private Transform camTransform;
    private float verticalVelocity;
    private float xRotation = 0f;

    [Header("Head Bobbing")]
    public float bobSpeed = 8f;       // Qué tan rápido se mueve la cámara
    public float bobAmount = 0.2f;    // Qué tanto se mueve la cámara hacia arriba/abajo
    private float defaultYPos;
    private float timer = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camTransform = GetComponentInChildren<Camera>().transform;
        

        if (!photonView.IsMine)
        {
            // Si este jugador no es local, desactiva cámara y componente de control
            camTransform.gameObject.SetActive(false);
            this.enabled = false;
        }
        else
        {
            // Cursor bloqueado para jugador local
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        corriendo = Input.GetKey(KeyCode.LeftShift);
        if (!photonView.IsMine) return;
        if(corriendo){
            speed = 45f;
            bobSpeed = 12f;
        }else{
            speed= 20f;
            bobSpeed = 8f;
        }
        LookAround();
        Move();
        HeadBob();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move = move.normalized * speed;
        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }
    void HeadBob()
    {
        // Si el jugador se está moviendo y está en el suelo
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultYPos + Mathf.Sin(timer) * bobAmount;
            camTransform.localPosition = new Vector3(
                camTransform.localPosition.x,
                newY,
                camTransform.localPosition.z
            );
        }
        else
        {
            // Suavemente vuelve a la posición original cuando se detiene
            timer = 0;
            Vector3 target = new Vector3(
                camTransform.localPosition.x,
                defaultYPos,
                camTransform.localPosition.z
            );
            camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, target, Time.deltaTime * bobSpeed);
        }
    }
}