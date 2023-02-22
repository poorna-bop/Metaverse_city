using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public static string CitySceneName = "CityScene";
    public static string CarShowroomSceneName = "CarShowroom";
    public static string NFTGalleryceneName = "NFTGalleryScene";
    public static string FashionStoreSceneName = "StoreScene";

    public static string FashionStoreEnterTag = "StoreDoor";
    public static string CarShowroomEnterTag = "";
    public static string NFTGalleryEnterTag = "NFTGalleryPoint";



    // Position and Rotation Points of player on doors of each scene
    public static Vector3 cityScenePointOnFashionStoreGate = new Vector3(-137.66f,-45.762f,-91.604f);
    public static Quaternion citySceneRotationPointOnFashionStoreGate = Quaternion.Euler(0,0,0);

    public static Vector3 cityScenePointOnNFTGalleryGate = new Vector3(-152.418f,-45.985f,-63.007f); 
    public static Quaternion citySceneRotationPointOnNFTGalleryGate = Quaternion.Euler(0,180,0);


    // Initial Position and Rotation Points of player on each scene
    public static Vector3 cityScenePlayerInitialPoint = new Vector3(-110f,-46.278f,-82.6f);
    public static Quaternion cityScenePlayerInitialRotationPoint = Quaternion.Euler(0,-90,0);

    public static Vector3 fashionStorePlayerInitialPoint = new Vector3(8.4f,0,10f);
    public static Quaternion fashionStorePlayerInitialRotationPoint = Quaternion.Euler(0,180,0);

    public static Vector3 nftGalleryPlayerInitialPoint = new Vector3(-1f,0,20f);
    public static Quaternion nftGalleryPlayerInitialRotationPoint = Quaternion.Euler(0,180,0);
}
