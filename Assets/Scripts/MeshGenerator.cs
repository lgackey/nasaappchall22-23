using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshGenerator : MonoBehaviour
{
    public GameObject dataLoaderObject;

    DataManager data;
    Vector3[] vertices;
    int[] triangles;
    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        data = dataLoaderObject.GetComponent<DataManager>();
        data.ReadCSV();

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[DataManager.DATA_SIZE * DataManager.DATA_SIZE];
        for (int i = 0; i < DataManager.DATA_SIZE; i++)
        {
            for (int j = 0; j < DataManager.DATA_SIZE; j++)
            {
                vertices[i * DataManager.DATA_SIZE + j] = data.GetCartesian(i, j);
            }
        }

        triangles = new int[6 * vertices.Length];
        for (int ti = 0, vi = 0, y = 0; y < DataManager.DATA_SIZE - 1; y++, vi++)
        {
            for (int x = 0; x < DataManager.DATA_SIZE - 1; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + DataManager.DATA_SIZE;
                triangles[ti + 5] = vi + DataManager.DATA_SIZE + 1;
            }
        }

    }

    void UpdateMesh()
    {
        mesh.Clear();

        mesh.indexFormat = IndexFormat.UInt32;
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    /*void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < 1277 * 1277; i += 1000)
        {
            Gizmos.DrawSphere(vertices[i], 10f);
        }
    }*/
}
