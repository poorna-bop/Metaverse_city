using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShowroomManager : MonoBehaviour
{
   #region private variables
   #endregion

   #region public variables
   public static CarShowroomManager Instance;
   #endregion

   #region unity callbacks
   /// <summary>
   /// Awake is called when the script instance is being loaded.
   /// </summary>
   void Awake()
   {
      Instance = this;
   }
   #endregion

   #region private methods
   #endregion

   #region public methods
   #endregion
}
