using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer)), ExecuteInEditMode]
public class UIMesh : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public Sprite sprite;

    void SetData()
    {
        CanvasRenderer cr = GetComponent<CanvasRenderer>();
        cr.SetMesh(mesh);
        if (!material) 
            return;
        cr.materialCount = 1;
        if (sprite)
            material.mainTexture = sprite.texture;
        cr.SetMaterial(material, 0);
    }
    
    void OnEnable()
    {
        SetData();
    }

    void OnValidate()
    {
        SetData();
    }
}
