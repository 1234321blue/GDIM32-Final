using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 200f;
    public float interactRange = 3f;
    public Transform holdPoint;

    private GameObject heldItem;

    public Transform playerCamera;

    private Rigidbody rb;
    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        moveDirection.Normalize();

        rb.velocity = new Vector3(
            moveDirection.x * moveSpeed,
            rb.velocity.y,
            moveDirection.z * moveSpeed
        );
    }
    
    void TryPickup()
    {
        if (heldItem != null) return;

        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("item"))
            {
                heldItem = hit.collider.gameObject;

            Rigidbody itemRb = heldItem.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.isKinematic = true;
            }

            heldItem.transform.SetParent(holdPoint);
            heldItem.transform.localPosition = Vector3.zero;
            heldItem.transform.localRotation = Quaternion.identity;

            Collider col = heldItem.GetComponent<Collider>();
            if (col != null)
                col.enabled = false;
            }
        }
    }   

    void DropItem()
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);

        Collider col = heldItem.GetComponent<Collider>();
        if  (col != null)
            col.enabled = true;

        Rigidbody itemRb = heldItem.GetComponent<Rigidbody>();
        if (itemRb != null)
        {
            itemRb.isKinematic = false;
            itemRb.AddForce(playerCamera.forward * 2f, ForceMode.Impulse);
        }

        heldItem = null;
    }
}