using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Utility.Extensions
{
    public static class MeshArrayExtensions
    {
        public static Mesh Combine(this MeshFilter[] meshes)
        {
            CombineInstance[] combine = new CombineInstance[meshes.Length];

            for (int i = 0; i < combine.Length; i++)
            {
                combine[i].mesh = meshes[i].sharedMesh;
                combine[i].transform = meshes[i].transform.localToWorldMatrix;
            }

            Mesh mesh = new Mesh();
            mesh.indexFormat = IndexFormat.UInt32;
            mesh.CombineMeshes(combine);

            return mesh;
        }
    }
}