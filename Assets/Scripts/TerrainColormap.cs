using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainColormap : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    DataManager data;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ColorByHeight()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.Lerp(Color.red, Color.blue, (vertices[i].y - data.minHeight) / (data.maxHeight - data.minHeight));
        }
        
        mesh.SetColors(colors);
    }

    public void ColorBySlope()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            int a = i / DataManager.DATA_SIZE;
            int b = i % DataManager.DATA_SIZE;
            colors[i] = Color.Lerp(Color.white, Color.blue, (data.slopeData[a, b] - data.minSlope) / (data.maxSlope - data.minSlope));
        }

        mesh.SetColors(colors);
    }
}
