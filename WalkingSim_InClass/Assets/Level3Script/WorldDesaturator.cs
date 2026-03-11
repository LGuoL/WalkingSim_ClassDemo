using UnityEngine;

public class WorldDesaturator : MonoBehaviour
{
    public Renderer[] targetRenderers;
    public Material desaturatedMaterial;

    public void DesaturateWorld()
    {
        if (targetRenderers == null || desaturatedMaterial == null) return;

        foreach (var r in targetRenderers)
        {
            if (r != null)
                r.material = desaturatedMaterial;
        }
    }
}