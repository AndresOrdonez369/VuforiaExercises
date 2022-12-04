using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";
   
    [Header("Meta Data")] 
    [SerializeField] private int _indexMetaData;
    [SerializeField] private string _tittleMetaData;
    [SerializeField] private string _descriptionMetaData;
    public ImageTargetBehaviour ImageTargetTemplate;

    [Header("UI")] 
    [SerializeField] private UnityEngine.UI.Image _scanningImg;
    [SerializeField] private Color _scanningColorTrue;
    [SerializeField] private Color _scanningColorFalse;

    [Space] 
    [SerializeField] private TextMeshProUGUI _tittleTxt;
    [SerializeField] private TextMeshProUGUI _descriptionTxt;
    [Space] [SerializeField] private Button _resetBtn;
    
    
    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    private void Start()
    {
        _scanningImg.color = _scanningColorFalse;
        _tittleTxt.text = "-";
        _descriptionTxt.text = "";
        _resetBtn.gameObject.SetActive(false);
            
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;
        _scanningImg.color = mIsScanning ? _scanningColorTrue : _scanningColorFalse;
        

        if (scanning)
        {
            // Clear all known targets
        }
    }

// Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;
        Debug.LogFormat("<color=green> METADATA </color> {0}",mTargetMetadata);
        ParseMetaData(mTargetMetadata);
        
    }

    void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
            {
                // Reset Behaviour
                mCloudRecoBehaviour.enabled = true;
                mTargetMetadata = "";
            }
        }
    }

    public void ParseMetaData(string metaData)
    {
        string[] tempMetada = metaData.Split('_');
        _indexMetaData = int.Parse(tempMetada[0]);
        _tittleMetaData = tempMetada[1];
        _descriptionMetaData = tempMetada[2];
        _tittleTxt.text = _tittleMetaData;
        _descriptionTxt.text = _descriptionMetaData;
    }
}
    