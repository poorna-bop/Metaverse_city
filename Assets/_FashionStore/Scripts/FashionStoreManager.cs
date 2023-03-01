using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FashionStoreManager : MonoBehaviour
{
   #region private variables
   #endregion

   #region public variables
   public static FashionStoreManager Instance;
   #endregion

   #region unity callbacks
   private void Awake() 
   {
        Instance = this;
   }
   private void Start() 
   {
        UIManager.Instance.CloseGoToPanel();
   }
   #endregion

   #region private methods
   #endregion

   #region public methods
   #endregion
}
