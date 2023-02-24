using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    #region  private variables

    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private GameObject clint;
    [SerializeField]
    private GameObject metaboy;
    [SerializeField]
    private GameObject relyna;


    private bool move=true;

    [SerializeField]
    private Animator playerAnimator;
    private int animState;
    
    private float currentAngleVelocity;
    private float currentAngle;
    private Vector3 playerVelocity = Vector3.zero;
    
    #endregion


    #region  public variables
    public static PlayerManager Instance;
    public float maxSpeed = 1.6f;
    public float duration = 1;
    public float gravityValue = -9.81f;
    public float rotationSmoothTime = 0.15f;
    public int AnimationState
    {
        get
        {
            return animState;
        }
        set
        {
            animState = value;
            playerAnimator.SetInteger("animationState", value);
        }
    }
    #endregion


    #region  unity Callbacks
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == Constants.FashionStoreEnterTag)
        {
            GameManager.Instance.OnStoreEnter();
        }
        if(other.gameObject.tag == Constants.NFTGalleryEnterTag)
        {
            GameManager.Instance.OnNFTGalleryEnter();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CalculatePlayerMovement();
        EvaluatePlayerAnimation();
    }
    #endregion


    #region private methods
 
    void CalculatePlayerMovement()
    {
        // player should be on surface
        if (playerVelocity.y < 0)
            playerVelocity.y = 0f;
        
        //get direction with respect to input axis
        Vector3 direction = new Vector3(InputManager.horizontal, 0f, InputManager.vertical).normalized;
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + CameraManagement.Instance.GetMainCameraTransform().eulerAngles.y;
            currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
            transform.localRotation = Quaternion.Euler(0, currentAngle, 0);
        } 

        //To make player grounded
        playerVelocity.y += gravityValue;
        MoveCharacterBy(playerVelocity*Time.deltaTime);
    }
    void EvaluatePlayerAnimation()
    {
        if (InputManager.Instance.IsKeyPressed())
        {
            switch (AnimationState)
            {
                case 0:
                    AnimationState = 1;
                    break;
            }

        }

        if (!InputManager.Instance.IsKeyPressed())
        {
            switch (AnimationState)
            {
                case 1:
                    AnimationState = 0;
                    break;

            }
        }
    }

    IEnumerator changePosition(Vector3 _position, Quaternion _rotation)
    {   
        move = false;
        characterController.enabled = false;
        CameraManagement.Instance.DisableFreeLookCamera();
        transform.position = _position;
        transform.rotation = _rotation;
        yield return null;
        CameraManagement.Instance.EnableFreeLookCamera();
        characterController.enabled = true;
        move = true;
        //APIManager.Instance.OffLoading();
    }
    
    #endregion

    #region public methods
    public void MoveCharacterBy(Vector3 velocity)
    {
        if(characterController.enabled)
        characterController.Move(velocity);
    }
    public void OffPlayerMove()
    {
        move = false;
    }
    public void OnPlayerMove()
    {
        move = true;
    }
    public bool isPlayerMoving()
    {
        return move;
    }
    
    public void SetDirectPlayerPosition(Vector3 _position, Quaternion _rotation, bool direct = false)
    {
        transform.position = _position;
        transform.rotation = _rotation;
    }
    public void SetPlayerPosition(Vector3 _position, Quaternion _rotation)
    {
        StartCoroutine(changePosition(_position,_rotation));
    }
    
    public void SelectClint()
    {
        clint.SetActive(true);
        metaboy.SetActive(false);
        relyna.SetActive(false);
        playerAnimator = clint.GetComponent<Animator>();
    }
    public void SelectMetaboy()
    {
        clint.SetActive(false);
        metaboy.SetActive(true);
        relyna.SetActive(false);
        playerAnimator = metaboy.GetComponent<Animator>();
    }
    public void SelectRelyna()
    {
        clint.SetActive(false);
        metaboy.SetActive(false);
        relyna.SetActive(true);
        playerAnimator = relyna.GetComponent<Animator>();
    }

    #endregion
}
