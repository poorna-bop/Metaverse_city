using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class WebGLSignOut : MonoBehaviour
{
    public void OnSignOut()
    {
        // Clear Account
        PlayerPrefs.SetString("Account", "");
        // go to login scene
        //SceneManager.LoadScene(0);
        PaintingsManager.Instance.CloseSendTransactionPanel();
    }
}
#endif