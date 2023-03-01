using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    #region private variables
    int animState=0;
    #endregion

    #region public variables
    [HideInInspector]
    public Animator anim;
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

    // OnAnimatorMove called at each frame after amimation evaluated
    private void OnAnimatorMove()
    {
        Vector3 velocity = anim.deltaPosition;

        #region slow walk animation on stair
        // public float speed;
        // Vector3 rootMotion;
        //rootMotion = anim.deltaPosition;
        // if (GameSceneManager.isCollidedAscendingStair)
        //     currentSpeed = 0.5f;
        // else
        //     currentSpeed = speed;
        #endregion

        PlayerManager.Instance.MoveCharacterBy(velocity);
    }
   
   void Jump()
   {
        if(InputManager.jump)
        {
            anim.SetTrigger("jump");
            InputManager.jump = false;
        }


   }
    void LateUpdate()
    {

        Jump();
        if(InputManager.Instance.IsJumpKeyPress())
        {
            InputManager.jump = true;
            return;
        }
        

        if(InputManager.Instance.IsUpKeyPress())
        {
            if(InputManager.Instance.IsRunKeyPress())
            return;

            switch(AnimationState)
            {
                case 0:
                    AnimationState = 1;
                break;

                case 3:
                    AnimationState = 1;
                break; 

                case 4:
                    AnimationState = 5;
                break;
            }
        }
        // if(InputManager.Instance.IsDownKeyPress())
        // {
        //     AnimationState = 1;
        // }
        // if(InputManager.Instance.IsLeftKeyPress())
        // {
        //     AnimationState = 1;
        // }
        // if(InputManager.Instance.IsRightKeyPress())
        // {
        //     AnimationState = 1;
        // }
        if(InputManager.Instance.IsRunKeyPress())
        {
            switch(AnimationState)
            {
                case 0:
                    AnimationState = 2;
                break;

                case 1:
                    AnimationState = 4;
                    break;
                
                case 3:
                    AnimationState = 2;
                    break;
                case 5:
                    AnimationState = 4;
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
                case 2:
                    AnimationState = 3;
                break;

                case 4:
                    AnimationState = 3;
                break;

                case 5:
                    AnimationState = 0; 
                break;

            }
        }
    }
    #endregion

    #region private methods
    #endregion
}
