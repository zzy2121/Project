using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    public GameObject m_map;  //地面
    public GameObject m_line;
    private LineRenderer MyLine;
    public GameObject m_Btn;
    public GameObject bg_Btn;
    void Start()
    {
        MyLine = m_line.GetComponent<LineRenderer>();
        MyLine.transform.position = Vector3.zero;
        m_Btn.GetComponent<Button>().onClick.AddListener(LineInit);
        bg_Btn.GetComponent<Button>().onClick.AddListener(DrawPlane);
    }



    List<Vector3> pointV3 = new List<Vector3>();
    public Transform[] pointTransform;
    int n = 0;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.DrawLine(ray.origin, hit.point);
        }

        if (Input.GetMouseButtonDown(2))
        {
            n++;
            Vector3 v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject a = Instantiate(Resources.Load("Cube"), m_map.transform) as GameObject;
            a.transform.position = hit.point;
            pointV3.Add(a.transform.position);
            MyLine.enabled = true;
            MyLine.positionCount = n;

            MyLine.SetPosition(n - 1, new Vector3(a.transform.position.x, a.transform.position.y + 0.2f, a.transform.position.z));
            float ffff;
            for (int i = 0; i < pointV3.Count; i++)
            {
                if (pointV3.Count >= 3)
                {
                    ffff = Vector3.Distance(pointV3[0], pointV3[n - 1]);
                    if (ffff <= dic)
                    {
                        MyLine.loop = true;
                        DrawPlane();
                    }
                }
            }

        }




    }
    private float dic = 0.5f;
    public Material mat;
    private MeshRenderer mRenderer;
    private MeshFilter mFilter;
    public Material BGmaterial;
    bool ISDraw = false;

    private void DrawPlane()
    {
        CreateMesh(pointV3, m_map.transform, mat);
    }

    private GameObject CreateMesh(List<Vector3> lists, Transform area, Material mat)//todo  画出来的面是反的 
    {
        int iVertexCount = lists.Count;
        int iTrsCount = lists.Count - 2;
        //int iTimer = 0;
        if (area.transform.Find("mesh") != null)
        {
            return null;
        }
        GameObject meshObj = new GameObject("mesh");
        meshObj.transform.SetParent(area);
        MeshFilter filter = meshObj.AddComponent<MeshFilter>();
        MeshRenderer renderer = meshObj.AddComponent<MeshRenderer>();
        renderer.material = mat;

        int[] newTriangles = new int[3 * iTrsCount];
        for (int i = 0; i < iTrsCount; i++)
        {
            newTriangles[3 * i] = 0;//     
            newTriangles[3 * i + 1] = i + 1;
            newTriangles[3 * i + 2] = i + 2;
        }

        Vector3[] newVertices = new Vector3[iVertexCount];
        for (int i = 0; i < iVertexCount; i++)
        {
            var pos = new Vector3(lists[i].x, lists[i].y, lists[i].z);
            newVertices[i] = pos;
        }

        var mesh = filter.mesh;
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        meshObj.transform.position = new Vector3(meshObj.transform.position.x, meshObj.transform.position.y, meshObj.transform.position.z);
        return meshObj;
    }

/// <summary>
/// 重置
/// </summary>
    private void LineInit()
    {
        pointV3.Clear();
        if (m_map.transform.childCount > 0)
        {
            for (int i = 0; i < m_map.transform.childCount; i++)
            {
                Destroy(m_map.transform.GetChild(i).gameObject);
            }
        }
        MyLine.enabled = false;
        MyLine.positionCount = 0;
        MyLine.loop = false;

        n = 0;
        ISDraw = true;

    }
}
