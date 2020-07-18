using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SoundOnClick : MonoBehaviour
{
    public GameObject soundObject;

    public delegate void myDelegate(params object[] parameters);

    public myDelegate _myDelegate;

    // Start is called before the first frame update
    private void Start()
    {
        _myDelegate = OnDelegate;
        SimpleMethod(_myDelegate, "SUMIT");
    }

    // Update is called once per frame
    private void Update()
    {
        // var buttonDown = Input.GetMouseButtonDown(0);
        var currentSelection = EventSystem.current.currentSelectedGameObject;
        if (currentSelection && currentSelection.GetComponent<IPointerDownHandler>() != null &&
            !currentSelection.CompareTag("SpecificButtons") &&
            Input.GetMouseButtonDown(0))
        {
            Instantiate(soundObject);
        }
    }

    private void OnDelegate(params object[] parameters)
    {
        print("Call Delegate and " + parameters[0]);
    }

    private void SimpleMethod(myDelegate method, string str)
    {
        Debug.Log("I'm about to call a method");
        method(str);
    }
}