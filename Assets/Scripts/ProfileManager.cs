using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [Header("Data Object")] public QuickDataScriptableObject data;
    public UserDetails userDetails;

    // public GameplayDataManager gameplayDataManager;
    // public GameplaySetingManager gameplaySetingManager;
    [Header("Panels & Objects")] public RectTransform accountPanel;
    public RectTransform profilePanel;
    public RectTransform settingsPanel;
    public RectTransform kycPanel;

    [Header("Account Panel")] public Button profileButton;
    public Button accountBackButton;

    [Header("Profile Panel")] public Button kycButton;
    public Button profileBackButton;

    [Header("Settings Panel")] public Button settingsButton;
    public Button settingsBackButton;
    public Button notificationsButton;
    public GameObject onNotificationsText;
    public GameObject offNotificationsText;
    public Button marketingAlertsButton;
    public GameObject onMarketingText;
    public GameObject offMarketingText;
    public Sprite onSprite;
    public Sprite offSprite;
    private int _notificationClickCount;
    private int _marketingAlertsClickCount;

    [Header("KYC Panel")] public Button uploadKYCButton;
    public Button kycBackButton;
    public Button uploadPanButton;
    public Button uploadAadharButton;
    public Button uploadAadharFrontButton;
    public Button uploadAadharBackButton;


    // Start is called before the first frame update
    private void Start()
    {
        userDetails = new UserDetails();
        uploadAadharFrontButton.gameObject.SetActive(false);
        uploadAadharBackButton.gameObject.SetActive(false);

        profileButton.onClick.AddListener(delegate { ActivePanel(profilePanel, true); });
        settingsButton.onClick.AddListener(delegate { ActivePanel(settingsPanel, true); });
        kycButton.onClick.AddListener(delegate { ActivePanel(kycPanel, true); });

        accountBackButton.onClick.AddListener(delegate { ActivePanel(accountPanel, false); });
        profileBackButton.onClick.AddListener(delegate { ActivePanel(profilePanel, false); });
        settingsBackButton.onClick.AddListener(delegate { ActivePanel(settingsPanel, false); });
        kycBackButton.onClick.AddListener(OnKYCBack);

        uploadAadharButton.onClick.AddListener(OnUploadAadhar);
        uploadPanButton.onClick.AddListener(OnUploadPAN);

        notificationsButton.onClick.AddListener(OnNotificationAlerts);
        marketingAlertsButton.onClick.AddListener(OnMarketingAlerts);
    }

    private void OnUploadPAN()
    {
    }

    private void OnUploadAadhar()
    {
        uploadAadharFrontButton.gameObject.SetActive(true);
        uploadAadharBackButton.gameObject.SetActive(true);
    }

    private void ResetKYCPanel()
    {
        uploadAadharFrontButton.gameObject.SetActive(false);
        uploadAadharBackButton.gameObject.SetActive(false);
    }

    private void OnKYCBack()
    {
        ActivePanel(kycPanel, false);
        ResetKYCPanel();
    }

    private void OnNotificationAlerts()
    {
        userDetails.isSMSAllowed = !userDetails.isSMSAllowed;
        notificationsButton.GetComponent<Image>().sprite = userDetails.isSMSAllowed ? onSprite : offSprite;
        onNotificationsText.SetActive(userDetails.isSMSAllowed);
        offNotificationsText.SetActive(!userDetails.isSMSAllowed);
    }

    private void OnMarketingAlerts()
    {
        userDetails.isEmailAllowed = !userDetails.isEmailAllowed;
        marketingAlertsButton.GetComponent<Image>().sprite = userDetails.isEmailAllowed ? onSprite : offSprite;
        onMarketingText.SetActive(userDetails.isEmailAllowed);
        offMarketingText.SetActive(!userDetails.isEmailAllowed);
    }

    private void ActivePanel(RectTransform panel, bool status)
    {
        panel.DOAnchorPos(status ? data.inPos : data.outPos, data.duration);
    }

    private IEnumerator ActivePanelAfterWait(RectTransform panel, bool status, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        panel.DOAnchorPos(status ? data.inPos : data.outPos, data.duration);
    }
}