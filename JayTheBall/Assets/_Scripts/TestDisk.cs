/*
* ==============================
* FileName:		TestDisk
* Author:		DuanBin
* CreateTime:	8/28/2018 3:23:37 PM
* Description:		
* ==============================
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestDisk : MonoBehaviour
{
    MeshFilter MeshFilter;
    MeshRenderer MeshRenderer;

    public float height = 0.4f;
    public float radius = 1f;
    public int details = 20;

    static float EPS = 0.01f;

    void Start()
    {
        MeshFilter = transform.GetComponent<MeshFilter>();
        MeshRenderer = transform.GetComponent<MeshRenderer>();

    }
    //����һ�����������ԲƬ
    [ContextMenu("GeneratePie")]
    public void GeneratePieByRadian()
    {
        var arcs = new List<float>();
        // �����������0����յģ�1������Բ
        float r = Random.Range(0.1f, 0.9f);
        r *= 2 * Mathf.PI;

        arcs.Add(0);
        arcs.Add(r);

        
        GeneratePie(arcs);
        StartCoroutine(SequenceTest());
    }


    // ������ÿ���������������ȣ�float����ʾ��ÿ���������ж�����ǿ飬�ͺ�������һ��
    public void GeneratePie(List<float> arcs)
    {
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();

        List<Vector3> _verts = new List<Vector3>();
        List<Vector2> _uvs = new List<Vector2>();
        List<int> _tris = new List<int>();

        for (int i = 0; i < arcs.Count; i += 2)
        {
            _verts.Clear();
            _uvs.Clear();
            _tris.Clear();

            //�Ȱ����ĵ���ӽ�����List��
            _verts.Add(new Vector3(0, 0, 0));
            _verts.Add(new Vector3(0, -height, 0));

            AddArcMeshInfo(arcs[i], arcs[i + 1], _verts, _uvs, _tris);

            //�Ѷ���������������list��
            foreach (int n in _tris)
            {
                tris.Add(n + verts.Count);
            }
            verts.AddRange(_verts);
            uvs.AddRange(_uvs);
        }
        Mesh mesh = new Mesh();
        // ��дmesh
        mesh.vertices = verts.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        //���ݶ���������������Զ����������,���ߺ�����
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        MeshFilter.mesh = mesh;

    }

    void AddArcMeshInfo(float begin, float end, List<Vector3> verts, List<Vector2> uvs, List<int> tris)
    {
        // begin��end�ǿ�ʼ���ȡ���������
        // verts�����Ѿ����˶������ĵ㡢�������ĵ㣬�±�ֱ���0��1

        float eachRad = 2 * Mathf.PI / details;

        // �����Ǻ��������Բ���ܳ��ϵĶ���
        float a;
        for (a = begin; a <= end; a += eachRad)
        {
            Vector3 v = new Vector3(radius * Mathf.Sin(a), 0, radius * Mathf.Cos(a));
            verts.Add(v);
            Vector3 v2 = new Vector3(radius * Mathf.Sin(a), -height, radius * Mathf.Cos(a));
            verts.Add(v2);
        }
        if (a < end + EPS)
        {
            Vector3 v = new Vector3(radius * Mathf.Sin(end), 0, radius * Mathf.Cos(end));
            verts.Add(v);
            Vector3 v2 = new Vector3(radius * Mathf.Sin(end), -height, radius * Mathf.Cos(end));
            verts.Add(v2);
        }

        // ���涥�����
        int n = verts.Count;
        for (int i = 2; i < n - 2; i += 2)
        {
            tris.Add(i);
            tris.Add(i + 2);
            tris.Add(0);
        }

        // ���涥�����
        for (int i = 2; i < n - 2; i += 2)
        {
            tris.Add(i);
            tris.Add(i + 1);
            tris.Add(i + 2);
            tris.Add(i + 2);
            tris.Add(i + 1);
            tris.Add(i + 3);
        }

        // ��ס����ֱ�߱�
        tris.Add(2);
        tris.Add(0);
        tris.Add(1);
        tris.Add(3);
        tris.Add(2);
        tris.Add(1);
        tris.Add(n - 1);
        tris.Add(0);
        tris.Add(n - 2);
        tris.Add(1);
        tris.Add(0);
        tris.Add(n - 1);
    }

    private IEnumerator SequenceTest()
    {
        for (int i = 0; i < MeshFilter.mesh.triangles.Length; i += 3)
        {
            Debug.DrawLine(MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i]], MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i + 1]], Color.red, 100f);

            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i + 1]], MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i + 2]], Color.yellow, 100f);

            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i + 2]], MeshFilter.mesh.vertices[MeshFilter.mesh.triangles[i]], Color.blue, 100f);

            yield return new WaitForSeconds(0.2f);
        }
    }
}
