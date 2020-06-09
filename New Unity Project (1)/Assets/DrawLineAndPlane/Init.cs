using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Init : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer MyLine;
    public GameObject m_line;
    public GameObject m_map;


    void Start()
    {
        MyLine = m_line.GetComponent<LineRenderer>();

        this.GetComponent<Button>().onClick.AddListener(LineInit);
    }

    private void LineInit()
    {
        if (m_map.transform.childCount > 0)
        {
            for (int i = 0; i < m_map.transform.childCount; i++)
            {
                Destroy(m_map.transform.GetChild(i).gameObject);
            }
        }
        MyLine.enabled = false;
        MyLine.positionCount = 0;
    
    }
}
