using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class QuickPingPong : MonoBehaviour
{
    float m_time = 0.5f;

    Transform m_trans;

    // Use this for initialization
    private void Start()
    {
        m_trans = transform;
        MyPingPong(m_trans.localScale, m_trans.localScale + Vector3.one * 0.1f);
    }

    private void MyPingPong(Vector3 from, Vector3 to)
    {
        m_trans.DOScale(to, m_time).OnComplete(() => MyPingPong(to, from));
    }
}