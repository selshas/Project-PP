using UnityEngine;
using UnityEngine.Rendering;

public class test : MonoBehaviour
{
    // Draws a Triangle, a Quad and a line
    // with different colors

    public Material mat;
    void OnEnable()
    {/*
        Camera.onPostRender = (a) =>
        {
            if (!mat)
            {
                Debug.LogError("Please Assign a material on the inspector");
                return;
            }
            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();

            GL.Begin(GL.TRIANGLES); // Triangle
            GL.Color(new Color(1, 1, 1, 1));
            GL.Vertex3(0.50f, 0.25f, 0);
            GL.Vertex3(0.25f, 0.25f, 0);
            GL.Vertex3(0.375f, 0.5f, 0);
            GL.End(); // End drawing Triangle

            GL.Begin(GL.QUADS); // Quad
            GL.Color(new Color(0.5f, 0.5f, 0.5f, 1));
            GL.Vertex3(0.5f, 0.5f, 0);
            GL.Vertex3(0.5f, 0.75f, 0);
            GL.Vertex3(0.75f, 0.75f, 0);
            GL.Vertex3(0.75f, 0.5f, 0);
            GL.End(); // End drawing quad

            GL.Begin(GL.LINES); // Line
            GL.Color(new Color(0, 0, 0, 1));
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0.75f, 0.75f, 0);
            GL.End(); // End drawing Line

            GL.PopMatrix();
        };*/
    }
}