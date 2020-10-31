using System;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private float carSpeed = 100;
    [SerializeField]
    private float carAcceleration;
    private float carMaxSpeed;
    private float maxSteerAngle = 30;
    
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private float motorForce = 100;
    [SerializeField]
    private float brakePower = 50;

    public WheelCollider frontDriveW, frontPassengerW, rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT, rearDriverT, rearPassengerT;

    private float Brakes;
    private WheelCollider WC;

    public MeshRenderer BrakeLights;


    public void getInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontDriveW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
    }

    private void Brake()
    {
        Brakes = 0;
        if (Input.GetKey(KeyCode.DownArrow)){
            Brakes = 30000;
            BrakeLights.enabled = true;
        }
        else
        {
            BrakeLights.enabled = false;
        }
       
        rearDriverW.brakeTorque = Brakes;
        rearPassengerW.brakeTorque = Brakes;
        frontDriveW.brakeTorque = Brakes;
        frontPassengerW.brakeTorque = Brakes;
        
    }
    
    private void Accelerate()
    {
        rearDriverW.motorTorque = verticalInput * carAcceleration;
        rearPassengerW.motorTorque = verticalInput * carAcceleration;

        if (verticalInput < 0)
        {
            GetComponent<LightsController>().ReverseLight(true);
        }
        else
        {
            GetComponent<LightsController>().ReverseLight(false);

        }
        
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriveW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);

    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;
        
        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
        getInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        Brake();
        
    }


    private void CarMover()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("up");
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("down");
        }
    }
}
