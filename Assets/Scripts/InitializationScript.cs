using UnityEngine;
using UnityEngine.XR.Management;

public class InitializationScript : MonoBehaviour
{
    void Awake()
    {
        Debug.Log(OVRManager.eyeTextureFormat);
        OVRManager.eyeTextureFormat = OVRManager.EyeTextureFormat.R11G11B10_FP;
        Debug.Log(OVRManager.eyeTextureFormat);
    }
}
