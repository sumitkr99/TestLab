using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using SimpleJSON;

public class API
{
    // Login Request
    public static IEnumerator SignInEnumerator(string mobileNo, bool isGuest, Action SignInSuccessAction,
        Action UserNotExistAction, Action NetworkErrorAction)
    {
        var form = new WWWForm();
        form.AddField("MobileNo", mobileNo);
        form.AddField("DeviceType", "ANDROID");
        form.AddField("DeviceToken", SystemInfo.deviceUniqueIdentifier);
        form.AddField("IsPlayStore", "false");
        form.AddField("IsGuest", isGuest.ToString());


        using (var www = UnityWebRequest.Post(Constraints.userBaseURL + "Login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error 1:" + www.error);
                UserDetails.loginStatus = "Check your internet connection";
                NetworkErrorAction?.Invoke();
            }
            else
            {
                var responseText = www.downloadHandler.text;
                Debug.Log("Response=" + responseText);
                var jsonNode = JSON.Parse(responseText);
                UserDetails.loginStatus = jsonNode["Status"];
                if (UserDetails.loginStatus.Equals("OK"))
                {
                    SignInSuccessAction?.Invoke();
                }
                else
                {
                    UserNotExistAction?.Invoke();
                }
            }
        }
    }

    public static IEnumerator SignUpEnumerator(string mobileNo, Action SignUpSuccessAction,
        Action UserAlreadyExistAction, Action NetworkErrorAction)
    {
        var form = new WWWForm();
        form.AddField("MobileNo", mobileNo);
        form.AddField("DeviceType", "ANDROID"); //Application.platform.ToString());
        form.AddField("DeviceToken", SystemInfo.deviceUniqueIdentifier);
        form.AddField("IsPlayStore", "false");

        using (var www = UnityWebRequest.Post(Constraints.userBaseURL + "Signup", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error 1:" + www.error);
                NetworkErrorAction?.Invoke();
                UserDetails.signUpStatus = "Check your internet connection";
            }
            else
            {
                var responseText = www.downloadHandler.text;
                Debug.Log("Response=" + responseText);
                var jsonNode = JSON.Parse(responseText);
                UserDetails.signUpStatus = jsonNode["Status"];
                switch (UserDetails.signUpStatus)
                {
                    case "OK":
                        SignUpSuccessAction?.Invoke();
                        break;
                    case "UserExists":
                        UserAlreadyExistAction?.Invoke();
                        break;
                }
            }
        }
    }

    public static IEnumerator VerifyOtpEnumerator(string mobileNo, string otp, Action ValidOtpAction,
        Action invalidOtpAction, Action NetworkErrorAction)
    {
        var form = new WWWForm();
        form.AddField("MobileNo", mobileNo);
        form.AddField("Otp", otp);

        using (var www = UnityWebRequest.Post(Constraints.userBaseURL + "OTP", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error 1:" + www.error);
                UserDetails.otpStatus = "Check your internet connection";
                NetworkErrorAction?.Invoke();
                // UIManager.instance.loadingPanel.SetActive(false);
            }
            else
            {
                var responseText = www.downloadHandler.text;
                var jsonNode = JSON.Parse(responseText);
                Debug.Log("Response=" + responseText);
                UserDetails.otpStatus = jsonNode["Status"];
                if (UserDetails.otpStatus.Equals("OK"))
                {
                    // Debug.Log("Message:" + userDetails.otpStatus);
                    // Debug.Log(jsonNode["Result"]["UserId"]);
                    UserDetails.userId = jsonNode["Result"]["UserId"];
                    UserDetails.userName = jsonNode["Result"]["UserName"];
                    UserDetails.fullName = jsonNode["Result"]["FullName"];
                    UserDetails.mobileNo = jsonNode["Result"]["MobileNo"];
                    UserDetails.state = jsonNode["Result"]["State"];
                    UserDetails.refCode = jsonNode["Result"]["RefCode"];
                    UserDetails.age = jsonNode["Result"]["Age"];
                    UserDetails.deviceType = jsonNode["Result"]["DeviceType"];
                    UserDetails.deviceToken = jsonNode["Result"]["DeviceToken"];
                    UserDetails.token = jsonNode["Result"]["Token"];
                    UserDetails.addedOn = jsonNode["Result"]["AddedOn"];
                    UserDetails.updatedOn = jsonNode["Result"][" UpdatedOn"];
                    UserDetails.lastLogin = jsonNode["Result"]["LastLogin"];
                    UserDetails.sms = jsonNode["Result"]["SMS"];
                    UserDetails.emailer = jsonNode["Result"]["Emailer"];
                    UserDetails.isPlayStore = jsonNode["Result"]["IsPlayStore"];
                    UserDetails.isDeleted = jsonNode["Result"]["IsDeleted"];
                    Debug.Log("User Name= " + UserDetails.userName + " Full Name= " + UserDetails.fullName);
                    ValidOtpAction?.Invoke();
                }
                else
                {
                    invalidOtpAction?.Invoke();
                    Debug.Log("Message:" + UserDetails.otpStatus);
                }
            }
        }
    }

    public static IEnumerator ResendOtpEnumerator(string mobileNo, bool isGuest, Action ResendSuccessAction,
        Action ResendFailAction, Action NetworkErrorAction)
    {
        var form = new WWWForm();
        form.AddField("MobileNo", mobileNo);
        form.AddField("DeviceType", "ANDROID");
        form.AddField("DeviceToken", SystemInfo.deviceUniqueIdentifier);
        form.AddField("IsPlayStore", "false");
        form.AddField("IsGuest", isGuest.ToString());


        using (var www = UnityWebRequest.Post(Constraints.userBaseURL + "Login", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error 1:" + www.error);
                UserDetails.otpResendStatus = "Check your internet connection";
                NetworkErrorAction?.Invoke();
            }
            else
            {
                var responseText = www.downloadHandler.text;
                Debug.Log("Response=" + responseText);
                var jsonNode = JSON.Parse(responseText);
                UserDetails.otpResendStatus = jsonNode["Status"];
                if (UserDetails.otpResendStatus.Equals("OK"))
                {
                    ResendSuccessAction?.Invoke();
                }
                else
                {
                    ResendFailAction?.Invoke();
                }
            }
        }
    }

    public static IEnumerator UpdateUserDetailsEnumerator(string mobileNo, string deviceType, string deviceToken,
        string isPlayStore, string isGuest, string userId, string userName, string fullName, string emailId,
        string state, string refCode, string isSms, string isEmail, string isAge, string isTnC,
        Action updateSuccessAction, Action updateFailAction, Action networkErrorAction)
    {
        var form = new WWWForm();
        form.AddField("MobileNo", mobileNo);
        form.AddField("DeviceType", deviceType);
        form.AddField("DeviceToken", deviceToken);
        form.AddField("IsPlayStore", isPlayStore);
        form.AddField("IsGuest", isGuest);
        form.AddField("FullName", fullName);
        form.AddField("UserId", userId);
        form.AddField("UserName", userName);
        form.AddField("State", state);
        form.AddField("RefCode", refCode);
        form.AddField("Age", isAge);
        form.AddField("SMS", isSms);
        form.AddField("Emailer", isEmail);
        using (var www = UnityWebRequest.Post(Constraints.userBaseURL + "UpdateUser", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error 1:" + www.error);
                networkErrorAction?.Invoke();
            }
            else
            {
                var responseText = www.downloadHandler.text;
                var jsonNode = JSON.Parse(responseText);
                Debug.Log("Response=" + responseText);
                UserDetails.updateStatus = jsonNode["Status"];
                if (UserDetails.updateStatus.Equals("OK"))
                {
                    UserDetails.userId = jsonNode["Result"]["UserId"];
                    UserDetails.userName = jsonNode["Result"]["UserName"];
                    UserDetails.fullName = jsonNode["Result"]["FullName"];
                    UserDetails.mobileNo = jsonNode["Result"]["MobileNo"];
                    UserDetails.state = jsonNode["Result"]["State"];
                    UserDetails.refCode = jsonNode["Result"]["RefCode"];
                    UserDetails.age = jsonNode["Result"]["Age"];
                    UserDetails.deviceType = jsonNode["Result"]["DeviceType"];
                    UserDetails.deviceToken = jsonNode["Result"]["DeviceToken"];
                    UserDetails.token = jsonNode["Result"]["Token"];
                    UserDetails.addedOn = jsonNode["Result"]["AddedOn"];
                    UserDetails.updatedOn = jsonNode["Result"][" UpdatedOn"];
                    UserDetails.lastLogin = jsonNode["Result"]["LastLogin"];
                    UserDetails.sms = jsonNode["Result"]["SMS"];
                    UserDetails.emailer = jsonNode["Result"]["Emailer"];
                    UserDetails.isPlayStore = jsonNode["Result"]["IsPlayStore"];
                    UserDetails.isDeleted = jsonNode["Result"]["IsDeleted"];
                    updateSuccessAction?.Invoke();
                    Debug.Log("<color=pink>Response </color>" + responseText);
                }
                else
                {
                    updateFailAction?.Invoke();
                    Debug.Log("Error 2:" + responseText);
                }
            }
        }
    }
}


[Serializable]
public class UserInfo
{
    public string Status;
    public string ErrorMessage;
    public List<Result> results = new List<Result>();

    [Serializable]
    public class Result
    {
        public string UserId;
        public string Token;
    }


    public static UserInfo DataFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<UserInfo>(jsonString);
    }
}