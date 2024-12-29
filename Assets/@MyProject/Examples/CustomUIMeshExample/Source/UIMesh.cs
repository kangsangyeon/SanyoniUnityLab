using UnityEngine;
using UnityEngine.UI;

public class UIMesh : MaskableGraphic
{
    public Mesh mesh = null;
    public Sprite sourceImage;
    public bool preserveAspect;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if (mesh == null) return;

        Vector3[] verts = mesh.vertices;
        Vector2[] uvs = mesh.uv;
        int[] tris = mesh.triangles;
        Vector2 meshMin = mesh.bounds.min;
        Vector2 meshSize = mesh.bounds.size;

        Rect rect = rectTransform.rect;
        if (preserveAspect)
            PreserveMeshAspectRatio(ref rect, meshSize);

        for (int i = 0; i < verts.Length; i++)
        {
            Vector2 v = verts[i];
            // object space에서 정의된 위치 v를 mesh bounds 내 0~1 사이의 값으로 매핑함.
            v.x = (v.x - meshMin.x) / meshSize.x;
            v.y = (v.y - meshMin.y) / meshSize.y;
            // 0~1 사이로 매핑된 위치값을 rect에 맞춰 보여주기 위해 위치 변환.
            //   ex: x가 1인 vertex는 rect의 맨 오른쪽에 위치하도록 위치 변환함.
            v = Vector2.Scale(v - rectTransform.pivot, rect.size);
            vh.AddVert(v, color, uvs[i]);
        }

        for (int i = 0; i < tris.Length; i += 3)
            vh.AddTriangle(tris[i], tris[i + 1], tris[i + 2]);
    }

    // source code from UnityEngine.UI.Image
    private void PreserveMeshAspectRatio(ref Rect rect, Vector2 meshSize)
    {
        var meshRatio = meshSize.x / meshSize.y;
        var rectRatio = rect.width / rect.height;

        if (meshRatio > rectRatio)
        {
            var oldHeight = rect.height;
            rect.height = rect.width * (1.0f / meshRatio);
            rect.y += (oldHeight - rect.height) * rectTransform.pivot.y;
        }
        else
        {
            var oldWidth = rect.width;
            rect.width = rect.height * meshRatio;
            rect.x += (oldWidth - rect.width) * rectTransform.pivot.x;
        }
    }
}