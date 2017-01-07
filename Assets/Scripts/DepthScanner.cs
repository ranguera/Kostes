using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthScanner : MonoBehaviour {

    [SerializeField]
    private DepthSourceManager depthManager;

    [SerializeField]
    private Renderer r;

    [SerializeField]
    private GameObject  go;

    [SerializeField]
    private GameObject parent;

    private Texture2D texture;
    private Color c;
    private Material mat;
    private GameObject[][] goArray;

	// Use this for initialization
	void Start () {
        texture = new Texture2D(512, 424);
        mat = r.material;
        goArray = new GameObject[512][];
        for (int i = 0; i < 512; i++)
        {
            goArray[i] = new GameObject[424];
        }


        for (int i = 0; i < 512; i++)
        {
            for (int j = 0; j < 424; j++)
            {
                GameObject t = (GameObject) Instantiate(go, new Vector3(i, j, 0f), Quaternion.identity);
                goArray[i][j] = t;
                t.transform.parent = parent.transform;
            }
        }
	}
	
	void Update ()
    {
        for (int i = 0; i < 424; i++)
        {
            for (int j = 0; j < 512; j++)
            {
                int f = depthManager.GetData()[512 * i + j];
                ColorFromDepth(f);
                texture.SetPixel(i, j, c);
                if( f <= 0 || f >= 3000 )
                {
                    goArray[j][i].SetActive(false);
                }
                else
                {
                    goArray[j][i].SetActive(true);
                    goArray[j][i].transform.position = new Vector3(goArray[j][i].transform.position.x, goArray[j][i].transform.position.y, (float)f/2f );
                }
            }
        }
        texture.Apply();
        mat.mainTexture = texture;
	}

    private void ColorFromDepth(int a)
    {
        float f = (float)a / 5000f;
        c.r = 1f - f;
        c.g = 1f - f;
        c.b = 1f - f;
    }
}
