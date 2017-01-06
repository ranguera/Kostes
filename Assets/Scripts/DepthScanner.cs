using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthScanner : MonoBehaviour {

    [SerializeField]
    private DepthSourceManager depthManager;

    [SerializeField]
    private Renderer r;

    private Texture2D texture;
    private Color c;
    private Material mat;

	// Use this for initialization
	void Start () {
        texture = new Texture2D(512, 424);
        mat = r.material;
	}
	
	void Update ()
    {
        for (int i = 0; i < 424; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                ColorFromDepth(depthManager.GetData()[512*i + j]);
                texture.SetPixel(i, j, c);
            }
        }
        texture.Apply();
        mat.mainTexture = texture;
	}

    private void ColorFromDepth(int a)
    {
        float f = (float)a / 8000f;
        c.r = f;
        c.g = f;
        c.b = f;
    }
}
