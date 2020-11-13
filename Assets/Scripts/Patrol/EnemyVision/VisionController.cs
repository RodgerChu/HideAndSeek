using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VisionController : MonoBehaviour
{
    [SerializeField]
    private uint _numberOfRays = 10;

    [SerializeField]
    private float _visionRange = 2f;

    [SerializeField]
    private float _fov = 45f;

    [SerializeField]
    private MeshFilter _meshFilter;

    [SerializeField]
    private LayerMask _rayTargetLayerMask;

    [SerializeField]
    private LayerMask _playerLayerMask;

    [SerializeField]
    private UnityEvent _onPlayerSpotted;

    [SerializeField]
    private Color _fovColor;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh;

        var eulerRotationCurrent = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotationCurrent.x, eulerRotationCurrent.y, eulerRotationCurrent.z);
    }

    // Update is called once per frame
    void Update()
    {
        DrawFOV();
    }

    private void DrawFOV()
    {
        var origin = Vector3.zero;
        var anglesPerStep = _fov / _numberOfRays;
        var currentCastAngle = -_fov / 2 + 90;


        Vector3[] verticles = new Vector3[_numberOfRays + 1 + 1];
        Vector2[] uv = new Vector2[verticles.Length];
        int[] triangles = new int[_numberOfRays * 3];

        verticles[0] = origin;

        var triangleIndex = 0;
        var vertexIndex = 1;

        for (int i = 0; i < _numberOfRays; i++)
        {
            var meshCastDirection = AngleToVector3(currentCastAngle);
            var rayCastDirection = AngleToVector3(currentCastAngle - transform.parent.rotation.eulerAngles.y);

            Vector3 vertexPos;
            RaycastHit hitInfo;
            var hit = Physics.Raycast(transform.position, rayCastDirection, out hitInfo, _visionRange, _rayTargetLayerMask + _playerLayerMask);
            if (hit)
            {
                if (hitInfo.collider.gameObject.name == "Player")
                {
                    vertexPos = origin + meshCastDirection * _visionRange;
                    _onPlayerSpotted?.Invoke();
                }
                else
                {
                    var distanceToHit = hitInfo.distance;
                    vertexPos = origin + meshCastDirection * distanceToHit;
                }
            }
            else
            {
                vertexPos = origin + meshCastDirection * _visionRange;
            }

            verticles[vertexIndex] = vertexPos;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            currentCastAngle += anglesPerStep;
        }

        _mesh.vertices = verticles;
        _mesh.triangles = triangles;
        _mesh.uv = uv;

        var colors = new Color[verticles.Length];

        for (int i = 0; i < colors.Length; i++)
            colors[i] = _fovColor;

        _mesh.colors = colors;
    }

    ///<summary>
    ///angle must be in range 0 -> 360
    ///</summary>
    private Vector3 AngleToVector3(float angle)
    {
        float angleRad = angle * Mathf.PI / 180f;
        return new Vector3(Mathf.Cos(angleRad), 0, Mathf.Sin(angleRad));
    }
}
