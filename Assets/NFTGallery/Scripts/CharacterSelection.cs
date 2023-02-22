using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    #region public variables
    #endregion

    #region private variables
    #endregion

    #region unity callbacks
    void OnEnable() 
    {
        Time.timeScale = 0;
        PlayerManager.Instance.OffPlayerMove();
    }
    #endregion

    #region public methods
    public void SelectClint()
    {
       PlayerManager.Instance.SelectClint();
    }
    public void SelectMetaboy()
    {
        PlayerManager.Instance.SelectMetaboy();
    }
    public void SelectRelyna()
    {
        PlayerManager.Instance.SelectRelyna();
    }
    public void OnCloseSelectionWindow()
    {
        Time.timeScale = 1;
        PlayerManager.Instance.OnPlayerMove();
        gameObject.SetActive(false);
    }
    #endregion

    #region private methods
    #endregion
}
