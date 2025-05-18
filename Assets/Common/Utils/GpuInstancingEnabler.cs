using UnityEngine;

namespace Common.Utils
{
  [RequireComponent(typeof(MeshRenderer))]
  public class GpuInstancingEnabler : MonoBehaviour
  {
    private void Awake()
    {
      var materialPropertyBlock = new MaterialPropertyBlock();
      var meshRenderer = GetComponent<MeshRenderer>();
      meshRenderer?.SetPropertyBlock(materialPropertyBlock);
    }
  }
}