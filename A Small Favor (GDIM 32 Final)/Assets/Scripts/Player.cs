using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _speed;
    [SerializeField] float _rotationSpeed;
    void Update()
    {
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
        {
           _playerTransform.Translate(Vector3.forward*_speed*Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
        {
           _playerTransform.Rotate(Vector3.up*_rotationSpeed*Time.deltaTime*-1); 
        }
        if (Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
        {
           _playerTransform.Translate(Vector3.back*_speed*Time.deltaTime); 
        }
        if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
        {
           _playerTransform.Rotate(Vector3.up*_rotationSpeed*Time.deltaTime); 
        }
    }
}
