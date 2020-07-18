using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
}

[System.Serializable]
public class UserDetails
{
    public static string loginStatus;
    public static string signUpStatus;
    public static string otpStatus;
    public static string otpResendStatus;
    public static string updateStatus;

    public static string userId;
    public static string userName;
    public static string fullName;
    public static string mobileNo;
    public static string state;
    public static string refCode;
    public static string age;
    public static string deviceType;
    public static string deviceToken;
    public static string token;
    public static string addedOn;
    public static string updatedOn;
    public static string lastLogin;
    public static string sms;
    public static string emailer;
    public static string isPlayStore;
    public static string isDeleted;

    public bool isSMSAllowed;
    public bool isEmailAllowed;
    public bool isAgeDeclared;
    public bool isTCDeclared;
}