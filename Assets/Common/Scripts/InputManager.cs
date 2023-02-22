using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region  private variables
    #endregion

    #region  public variables
    public static InputManager Instance;
    public static float vertical;
    public static float horizontal;
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

    
    // Update is called once per frame
    void LateUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    public bool IsKeyPressed()
    {
        bool isPressed = false;
        isPressed = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));

        if(isPressed)
            UIManager.Instance.OffTutorial();
        return isPressed;
    }
    #endregion
}
