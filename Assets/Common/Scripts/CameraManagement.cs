using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagement : MonoBehaviour
{
    #region  private variables
    [SerializeField]
    private CinemachineFreeLook thirdPersonCamera;
    
    [SerializeField]
    private GameObject freeLookCameraObject;
    
    [SerializeField]
    private Transform mainCameraTransform;
    #endregion

    #region  public variables
   
    public static CameraManagement Instance;
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
    #endregion

    #region private methods
    #endregion

    #region public methods
    public void EnableThirdPersonCamera()
    {
        thirdPersonCamera.enabled = true;
    }
    public void DisableThirdPersonCamera()
    {
        thirdPersonCamera.enabled = false;
    }
    public void EnableFreeLookCamera()
    {
        freeLookCameraObject.SetActive(true);
    }
    public void DisableFreeLookCamera()
    {
        freeLookCameraObject.SetActive(false);
    }
    public Transform GetMainCameraTransform()
    {
        return mainCameraTransform;
    }
    #endregion
}
