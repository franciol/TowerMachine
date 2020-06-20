using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{
    [SerializeField] [Range(1f,20f)] float gridsize = 10f;

    TextMesh textMesh;

    void Update()
    {
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x / gridsize) * gridsize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridsize) * gridsize;

        transform.position = new Vector3(snapPos.x, 0f, snapPos.z);

        textMesh = textMesh.GetComponentInChildren<TextMesh>();
        textMesh.text = snapPos.x / gridsize + "," + snapPos.z / gridsize;
    }
}
