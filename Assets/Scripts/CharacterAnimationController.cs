using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region private variables
    #endregion

    #region public variables
    [HideInInspector]
    public Animator anim;
    public CharacterController controller;
    public float speed;
    Vector3 rootMotion;
    [Range(0, 1f)]
    public float distanceToGround;
    public LayerMask layermask;
    int animState=0;
    public int AnimationState
    {
        get
        {
            return animState;
        }
        set
        {
            animState = value;
            anim.SetInteger("animationState", value);
        }
    }
    #endregion

    #region unity callbacks
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnAnimatorMove()
    {
        float currentSpeed = speed;
        Vector3 velocity = anim.deltaPosition;
        //rootMotion = anim.deltaPosition;
        // if (GameSceneManager.isCollidedAscendingStair)
        //     currentSpeed = 0.5f;
        // else
        //     currentSpeed = speed;
        if (controller.enabled)
            controller.Move(velocity);
    }
   
    void LateUpdate()
    {
        if (IsKeyPressed())
        {
            switch (AnimationState)
            {
                case 0:
                    AnimationState = 1;
                    break;
            }

        }

        if (!IsKeyPressed())
        {
            switch (AnimationState)
            {
                case 1:
                    AnimationState = 0;
                    break;

            }
        }
    }
    #endregion

    #region private methods
    bool IsKeyPressed()
    {
        bool isPressed = false;
        isPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) ||
                    Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow);

        return isPressed;
    }
    #endregion
}
