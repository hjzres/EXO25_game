using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    [Header("Rotation Properties")]
    public float mouseX;
    public float mouseY;
    public float mouseSpeed = 1f;
    public Vector2 clampAngles;
    private float xRotation, yRotation = 0f;
    private Camera cam;

    [Header("Movement Properties")]
    public float vertical;
    public float horizontal;
    public float moveSpeed = 1f;
    private Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * (mouseSpeed * 10f * Time.deltaTime);
        mouseY = Input.GetAxis("Mouse Y") * (mouseSpeed * 10f * Time.deltaTime);
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        
        yRotation -= mouseY;
        xRotation += mouseX;

        transform.rotation = Quaternion.Euler(0f, xRotation, 0f);
        cam.transform.rotation = Quaternion.Euler(Mathf.Clamp(yRotation, -clampAngles.x, clampAngles.x), Mathf.Clamp(xRotation, -clampAngles.y, clampAngles.y), 0f);

        moveDirection = transform.forward * vertical + transform.right * horizontal;
        rb.AddForce(moveDirection.normalized * 20f * moveSpeed, ForceMode.Force);
    }
}
