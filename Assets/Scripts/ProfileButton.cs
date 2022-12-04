using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileButton : MonoBehaviour
{
    [Header("Profile")] [SerializeField] private ProfileDataSO profileData;
    [Header("References")] 
    [SerializeField] private TextMeshProUGUI profileTxt;
    [SerializeField] private Image profileImg;

    void Start()
    {
        profileImg.sprite = profileData.profileSprite;
        
        if (profileData.useProfileText && profileTxt != null)
            profileTxt.text = profileData.profileText;
        //Consume
    }

    public void Execute()
    {
        Application.OpenURL(profileData.GetUrl());
    }
}
