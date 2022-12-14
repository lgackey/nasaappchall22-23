using System;
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
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            this.ColorByHeight();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            this.ColorBySlope();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.ColorByElevationAngle();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            this.ColorByAzimuthAngle();
        }
    }

    public void ColorByHeight()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            colors[i] = Color.Lerp(Color.blue, Color.red, (vertices[i].y - data.minHeight) / (data.maxHeight - data.minHeight));
        }
        
        mesh.SetColors(colors);
    }

    public void ColorBySlope()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Color orange = new Color(1, (float)0.5, 0, 1);
        Color32[] colors = new Color32[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            int a = i / DataManager.DATA_SIZE;
            int b = i % DataManager.DATA_SIZE;
            colors[i] = Color.Lerp(Color.green, orange, (data.slopeData[a, b] - data.minSlope) / (data.maxSlope - data.minSlope));
        }

        mesh.SetColors(colors);
    }

    public void ColorByElevationAngle()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            int a = i / DataManager.DATA_SIZE;
            int b = i % DataManager.DATA_SIZE;
            colors[i] = Color.Lerp(Color.white, Color.black, (data.elevationData[a, b] - data.minElAngle) / (data.maxElAngle - data.minElAngle));
        }
        mesh.SetColors(colors);
    }

    public void ColorByAzimuthAngle()
    {
        data = GetComponent<DataManager>();
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        Color32[] colors = new Color32[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            int a = i / DataManager.DATA_SIZE;
            int b = i % DataManager.DATA_SIZE;
            colors[i] = Color.Lerp(Color.cyan, Color.magenta, (data.azimuthData[a, b] - data.minAzAngle) / (data.maxAzAngle - data.minAzAngle));
        }
        mesh.SetColors(colors);     
    }
}