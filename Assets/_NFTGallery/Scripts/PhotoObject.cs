using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PhotoObject : MonoBehaviour
{
    #region private variables
    #endregion

    #region public variables
    public MeshRenderer photoRenderer;
    public Painting painting;
    public string imgURL;
    #endregion

    #region unity callbacks
    #endregion

    #region private methods
    private IEnumerator IGetImage(string img_url)
    {
        img_url = imgURL;
        //Debug.Log("imgUrl = " + img_url);
        if (img_url == null || img_url == "")
        {
            //Debug.Log("Empty URL");
            yield break;
        }

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(img_url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            DownloadHandlerTexture textureDownloadHandler = (DownloadHandlerTexture)www.downloadHandler;
            Texture2D texture = textureDownloadHandler.texture;

            if (texture == null)
            {
                Debug.LogException(new Exception("image not available"));
                yield break;
            }

            photoRenderer.materials[1].SetTexture(texture.name, texture);
            yield break;
        }
        else
        {
            Debug.LogException(new Exception(www.error));
        }
    }
    #endregion

    #region public methods
    public void AssignTexture()
    {
        StartCoroutine(IGetImage(imgURL));
    }
    #endregion
}
