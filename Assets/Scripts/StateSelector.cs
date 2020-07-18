using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSelector : MonoBehaviour
{
    public Transform stateSelectorRef;

    public float distance;

// Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        distance = Vector3.Distance(transform.position, stateSelectorRef.position);
        if (distance < 10f)
        {
            UIManager.instance.stateText.text = name;
        }
    }
}