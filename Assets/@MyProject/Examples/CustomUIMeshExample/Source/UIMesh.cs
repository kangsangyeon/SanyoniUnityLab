using UnityEngine;
using UnityEngine.UI;

public class UIMesh : MaskableGraphic
{
    public Mesh Mesh = null;
    public Sprite SourceImage;
    public bool PreserveAspect;

    protected override void OnPopulateMesh(VertexHelper _vh)
    {
        _vh.Clear();
        if (Mesh == null) return;

        Vector3[] _verts = Mesh.vertices;
        Vector2[] _uvs = Mesh.uv;
        int[] _tris = Mesh.triangles;
        Vector2 _meshMin = Mesh.bounds.min;
        Vector2 _meshSize = Mesh.bounds.size;

        Rect _rect = rectTransform.rect;
        if (PreserveAspect)
            PreserveMeshAspectRatio(ref _rect, _meshSize);

        for (int i = 0; i < _verts.Length; i++)
        {
            Vector2 v = _verts[i];
            // object space에서 정의된 위치 v를 mesh bounds 내 0~1 사이의 값으로 매핑함.
            v.x = (v.x - _meshMin.x) / _meshSize.x;
            v.y = (v.y - _meshMin.y) / _meshSize.y;
            // 0~1 사이로 매핑된 위치값을 rect에 맞춰 보여주기 위해 위치 변환.
            //   ex: x가 1인 vertex는 rect의 맨 오른쪽에 위치하도록 위치 변환함.
            v = Vector2.Scale(v - rectTransform.pivot, _rect.size);
            _vh.AddVert(v, color, _uvs[i]);
        }

        for (int i = 0; i < _tris.Length; i += 3)
            _vh.AddTriangle(_tris[i], _tris[i + 1], _tris[i + 2]);
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