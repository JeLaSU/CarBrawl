using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[ExecuteAlways]
[DisallowMultipleComponent]
[RequireComponent(typeof(PostProcessVolume))]
public class CVDFilter : MonoBehaviour
{
    enum ColorType { Normal, Protanopia, Protanomaly, Deuteranopia, Deuteranomaly, Tritanopia, Tritanomaly, Achromatopsia, Achromatomaly }

    [SerializeField] ColorType visionType = ColorType.Normal;
    ColorType currentVisionType;
    PostProcessProfile[] profiles;
    PostProcessVolume postProcessVolume;

    void Start()
    {
        currentVisionType = visionType;
        gameObject.layer = LayerMask.NameToLayer("CVDFilter");
        InitVolume();
        LoadProfiles();
        ChangeProfile();
    }

    void Update()
    {
        if (visionType != currentVisionType)
        {
            currentVisionType = visionType;
            ChangeProfile();
        }
    }

    void InitVolume()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.isGlobal = true;
    }

    public void LoadProfiles()
    {
        Object[] profileObjects = Resources.LoadAll("", typeof(PostProcessProfile));
        profiles = new PostProcessProfile[profileObjects.Length];

        for (int i = 0; i < profileObjects.Length; i++)
        {
            if (profileObjects[i].name.Contains("CVD"))
            {
                profiles[i] = (PostProcessProfile)profileObjects[i];
            }
        }
    }

    void ChangeProfile()
    {
        if (profiles.Length == 0)
        {
            Debug.LogError(string.Format("[{0}]({1}) Error: Profiles could not be loaded.\nPlease ensure that they are placed in a folder names \"Resources\" and have not been renamed", GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name));
            return;
        }
        else if (profiles.Length < 9)
        {
            Debug.LogWarning(string.Format("[{0}]({1}) Warning: Not all profiles could be loaded.\nPlease ensure that they are placed in a folder names \"Resources\" and have not been renamed", GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name));
            return;
        }
        else if (profiles.Length > 9)
        {
            Debug.LogWarning(string.Format("[{0}]({1}) Warning: Unrecognized profiles have been loaded.\nPlease ensure that there are no other post processing profiles containing the term \"CVD\"", GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name));
            return;
        }

        postProcessVolume.profile = profiles[(int)currentVisionType];
    }
}
