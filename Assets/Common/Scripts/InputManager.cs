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
    public static bool jump;
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
        if(IsRunKeyPress())
            vertical = 1; 
    }
    #endregion

    #region private methods
    #endregion

    #region public methods
    public bool IsKeyPressed()
    {
        bool isPressed = false;
        isPressed = ( Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));

        if(isPressed)
            UIManager.Instance.OffTutorial();
        return isPressed;
    }

    public bool IsUpKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        return isPressed;
    }

    public bool IsDownKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        return isPressed;
    }
    public bool IsLeftKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow);

        return isPressed;
    }

    public bool IsRightKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.RightArrow);

        return isPressed;
    }

    public bool IsRunKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.E);

        return isPressed;
    }

    public bool IsJumpKeyPress()
    {
        bool isPressed = false;
        isPressed = Input.GetKeyDown(KeyCode.Space);
    
        return isPressed;
    }
    
    #endregion
}
