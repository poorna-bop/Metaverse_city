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
    public Transform currentCamera;
    public Animator playerAnimator;

    #endregion

    #region unity callbacks
    void OnTriggerExit(Collider other)
    {
        // if(other.transform.gameObject.name == "AscendingStart")
        // {
        //     GameSceneManager.isCollidedAscendingStair = true;
        //     playerAnimator.SetFloat("stair",1);
        // }
        
        // if(other.transform.gameObject.name == "AscendingOver")
        // {
        //     GameSceneManager.isCollidedAscendingStair = false;
        //     playerAnimator.SetFloat("stair",0);
        // }

        // if(other.transform.gameObject.name == "AscendingStart1")
        // {
        //     GameSceneManager.isCollidedAscendingStair = false;
        //     playerAnimator.SetFloat("stair",0);
        // }
        
        // if(other.transform.gameObject.name == "AscendingOver1")
        // {
        //     GameSceneManager.isCollidedAscendingStair = true;
        //     playerAnimator.SetFloat("stair",-1);
        // }
    }
    
    void LateUpdate()
    {
        if (playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + currentCamera.eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        }

        if (IsKeyPressed())
        {
            speed = Mathf.SmoothDamp(speed, maxSpeed, ref currentSpeed, duration);
        }
        if(!IsKeyPressed())
        {
            speed = Mathf.SmoothDamp(speed, 0, ref currentSpeed, duration);
        }

        //controller.Move( transform.forward * Time.deltaTime * speed);
        // playerAnimator.SetFloat("animationSpeed",speed);
        // playerAnimator.SetFloat("verticle",vertical);
        
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
    bool IsKeyPressed()
    {
        bool isPressed = false;
        isPressed = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && move;

        return isPressed;
    }
    #endregion

    #region public methods
    #endregion


}
