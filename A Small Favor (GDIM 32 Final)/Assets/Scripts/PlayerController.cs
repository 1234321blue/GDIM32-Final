using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 200f;
    public float interactRange = 3f;
    public Transform holdPoint;
    public Transform largeHoldPoint;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI npcInteractText;
    public TextMeshProUGUI dropText;

    private GameObject heldItem;

    public Transform playerCamera;

    private Rigidbody rb;
    private float xRotation = 0f;
    private float smoothMouseX;
    private float smoothMouseY;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();

        CheckForInteractable();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
        /*else if (Input.GetKeyDown(KeyCode.E)&&heldItem!=null)
        {
            heldItem.GetComponent<Item>().Use();
        }*/

        if (heldItem != null)
        {
            dropText.gameObject.SetActive(true);
        }
        else
        {
            dropText.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        smoothMouseX = Mathf.Lerp(smoothMouseX, Input.GetAxis("Mouse X"), 10f * Time.deltaTime);
        smoothMouseY = Mathf.Lerp(smoothMouseY, Input.GetAxis("Mouse Y"), 10f * Time.deltaTime);

        float mouseX = smoothMouseX * mouseSensitivity * Time.deltaTime;
        float mouseY = smoothMouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 90f);

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
            if (hit.collider.CompareTag("item") || hit.collider.CompareTag("largeitem"))
            {
                heldItem = hit.collider.gameObject;

                Item item = heldItem.GetComponent<Item>();
                item.held = true;

                Rigidbody itemRb = heldItem.GetComponent<Rigidbody>();
                if (itemRb != null)
                {
                    itemRb.isKinematic = true;
                }

            // choose correct hold point
                if (heldItem.CompareTag("largeitem"))
                {
                    heldItem.transform.SetParent(largeHoldPoint);
                }
                else
                {
                    heldItem.transform.SetParent(holdPoint);
                }

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

        Item item = heldItem.GetComponent<Item>();
        item.held=false;

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

    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange))
        {
            if (hit.collider.CompareTag("item") || hit.collider.CompareTag("largeitem") && heldItem == null)
            {
                interactText.gameObject.SetActive(true);
                return;
            }
            /*if (hit.collider.CompareTag("npc"))
            {
                npcInteractText.gameObject.SetActive(true);
                return;
            }*/
        }

        interactText.gameObject.SetActive(false);
        //npcInteractText.gameObject.SetActive(false);
    }
}