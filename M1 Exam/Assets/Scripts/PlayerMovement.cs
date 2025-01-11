using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public float acceleration = 2f;
    public float jumpForce = 5f;
    public float cameraDistance = 5f;
    public float cameraSensitivity = 100f;
    private bool isCursorLocked = true;

    private Rigidbody rb;
    private Transform cameraTransform;
    private Vector3 cameraOffset;
    private float pitch = 0f;
    private float yaw = 0f;
    private bool isGrounded = true;

    void Start()
    {
        SetCursorState(true);
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
        cameraOffset = new Vector3(0, 2, -cameraDistance);
    }

    void Update()
    {
        HandleCamera();
        HandleJump();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            SetCursorState(isCursorLocked);
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
        //ApplyDrag();
    }

    void SetCursorState(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDir = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDir.y = 0;
        moveDir.Normalize();

        if (moveDir != Vector3.zero)
        {
            rb.AddForce(moveDir * acceleration, ForceMode.Acceleration);
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void ApplyDrag()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            rb.velocity *= 0.3f; 
        }
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb.velocity.magnitude > 3f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void HandleCamera()
    {
        yaw += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        cameraTransform.position = transform.position + rotation * cameraOffset;
        cameraTransform.LookAt(transform.position);
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
