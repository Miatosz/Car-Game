using UnityEngine;
using UnityEngine.Networking;

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
    private bool driveWay;
    private float motorForce = 100;
    [SerializeField]
    //private float brakePower = 50;

    private bool isBraking = false;
    //public bool isLocalPlayer;
    
    public WheelCollider frontDriveW, frontPassengerW, rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT, rearDriverT, rearPassengerT;

    private float Brakes = 0f;
    private WheelCollider WC;
    

    public MeshRenderer BrakeLights;
    public LightsController lightController;


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
        if (GetSpeed() > 0 && Input.GetKeyDown(KeyCode.DownArrow)){
            Brakes = 30000;
            GetComponent<LightsController>().BrakeLight(true);
            isBraking = true;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && GetSpeed() < 0)
        {
            Brakes = 30000;
            GetComponent<LightsController>().BrakeLight(true);
            isBraking = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            GetComponent<LightsController>().BrakeLight(false);
            isBraking = false;
            Brakes = 0;
        }
       
        rearDriverW.brakeTorque = Brakes;
        rearPassengerW.brakeTorque = Brakes;
        frontDriveW.brakeTorque = Brakes;
        frontPassengerW.brakeTorque = Brakes;
        
    }

    private void Handbrake()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Brakes = 30000;
            isBraking = true;

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBraking = false;
            Brakes = 0;
        }
        
        rearDriverW.brakeTorque = Brakes;
        rearPassengerW.brakeTorque = Brakes;
    }

    private void Accelerate()
    {
        if (!isBraking)
        {
            rearDriverW.motorTorque = verticalInput * carAcceleration;
            rearPassengerW.motorTorque = verticalInput * carAcceleration; 
        }

        if (GetSpeed() < 0 && Input.GetKey(KeyCode.DownArrow))
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

    private float GetSpeed()
    {
        return Mathf.RoundToInt(GetComponentInChildren<WheelCollider>().rpm);
    }

    private void FixedUpdate()
    {
        
        // if (!isLocalPlayer)
        // {
        //     return;
        // }
        getInput();
        Steer();
        Brake();
        Handbrake();
        Accelerate();
        UpdateWheelPoses();
    }

}
