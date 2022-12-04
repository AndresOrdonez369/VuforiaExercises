using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Profile Data", menuName = "ScriptableObjects/Profile Data", order = 0)]
public class ProfileDataSO : ScriptableObject
{ 
    public enum URLType
    {
        Normal,
        Phone,
        Email,
        Whats
    }
    [Header("URL")]
    public string URL;

    public URLType urlType = URLType.Normal;
    
    public Sprite profileSprite;
    [Space]
    public bool useProfileText;
    public string profileText;
    [Header("Type - Email")]
    public string emailDirection;
    public string emailSubject;
    public string emailBody;
    [Header("Type - WhatsApp")] public string numberWhats;
    public string messageWhats;

    public string GetUrl()
    {
        switch (urlType)
        {
            case URLType.Normal:
                return URL;
            case URLType.Phone:
                return string.Format("tel://{0}", URL);
            case URLType.Email:
                return string.Format("mailto:{0}?subject={1}?body{2}", emailDirection,emailSubject,emailBody);
            case URLType.Whats:
                return string.Format("https://api.whatsapp.com/send?phone={0}&text={1}",numberWhats,messageWhats);
        }
        return "";
    }
}