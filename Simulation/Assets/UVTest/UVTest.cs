using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf == null)
        {
            mf = gameObject.AddComponent<MeshFilter>();
        }
        mf.mesh = CreateMesh();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (mr == null)
        {
            mr = gameObject.AddComponent<MeshRenderer>();
        }

        mr.material = Resources.Load<Material>("uvtest");
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    // Update is called once per frame
    void Update()
    {

    }

    Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();

        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0) };

        mesh.triangles = new int[] {0, 3, 1,
                                    1, 3, 2};

        mesh.uv = new Vector2[] { new Vector2(1, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1) };
        return mesh;
    }
}
