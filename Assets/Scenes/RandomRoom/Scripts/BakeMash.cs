using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class BakeMash : MonoBehaviour
{
    public NavMeshSurface Surface2D;

    public void Bake() {
        Surface2D.BuildNavMesh();
    }
}
