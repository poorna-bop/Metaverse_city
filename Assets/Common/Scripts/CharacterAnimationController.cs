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
   
    void LateUpdate()
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
    #endregion

    #region private methods
    #endregion
}
