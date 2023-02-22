using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{

    #region private variables
    [SerializeField]
    private CharacterController controller;
    private float currentAngle = 180;
    private float currentAngleVelocity;
    [SerializeField] 
    private float rotationSmoothTime = 0.15f;
    private RaycastHit hit;
    private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public bool move=true;
    public float speed;
    [SerializeField]
    private Transform groundCheckPoint;
    #endregion

    #region public variables
    public float duration = 1;
    public float maxSpeed = 1.6f;


    #endregion

    #region unity callbacks
    void Start() 
    {
        
    }
    
    void LateUpdate()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 direction = new Vector3(InputManager.horizontal, 0f, InputManager.vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraManagement.Instance.GetMainCameraTransform().eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }

        if (InputManager.Instance.IsKeyPressed() && move)
        {
            speed = Mathf.SmoothDamp(speed, maxSpeed, ref currentSpeed, duration);
        }
        if(!(InputManager.Instance.IsKeyPressed() && move))
        {
            speed = Mathf.SmoothDamp(speed, 0, ref currentSpeed, duration);
        }

        #region to adjust speed over stairs
        //controller.Move( transform.forward * Time.deltaTime * speed);
        // playerAnimator.SetFloat("animationSpeed",speed);
        // playerAnimator.SetFloat("verticle",vertical);
        #endregion
        
        //To make player grounded
        playerVelocity.y += gravityValue;
        controller.Move(playerVelocity*Time.deltaTime);

    }
    float currentSpeed;
    #endregion

    #region private methods
    
    private bool IsGrounded()
    {
        float floorDistanceFromFoot = controller.stepOffset;

        RaycastHit hit;
        if (Physics.Raycast(groundCheckPoint.position, Vector3.down, out hit, floorDistanceFromFoot) || controller.isGrounded)
        {
            Debug.DrawRay(groundCheckPoint.position, Vector3.down * floorDistanceFromFoot, Color.green);
            return true;
        }
        return false;
    }
    
    #endregion

    #region public methods
    #endregion


}
