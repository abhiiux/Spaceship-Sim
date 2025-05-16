using System;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float thrustFactor;
    [SerializeField] private TMP_Text thrustValue;
    private float thrustSpeed;
    private Vector2 direction;
    [SerializeField] private float rotationSpeed;
    private PlayerInput playerInput;

    [Header(" Turn On is You want to Debug")]
    [SerializeField] private bool isLog;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        Thrust();
        ShipControl();
    }
    
    private void ShipControl()
    {
        direction = playerInput.actions["Move"].ReadValue<Vector2>();
        Logger("direction is : "+direction);

        float pitch = -direction.x * rotationSpeed * Time.deltaTime;
        float roll = -direction.y * rotationSpeed * Time.deltaTime;

        transform.Rotate(roll,0,pitch,Space.Self);
    }
    private void Thrust()
    {
        thrustSpeed += thrustFactor * playerInput.actions["ThrustSpeed"].ReadValue<float>();
        Logger("thruster value :"+thrustSpeed);
        thrustSpeed = Mathf.Clamp(thrustSpeed,-10f,50f);

        transform.position += transform.forward * thrustSpeed * Time.deltaTime;

        int thrustUI = (int)thrustSpeed;
        thrustValue.text = $"{thrustUI * 2}";
    }
    private void Logger(string message)
    {
        if(isLog)
        {
            Debug.Log(message);
        }
    }
}
