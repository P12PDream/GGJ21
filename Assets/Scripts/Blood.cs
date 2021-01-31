using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Blood : MonoBehaviour
{
    private static readonly int bloodID = Shader.PropertyToID("u_BloodSplats");

    public Material snowMaterial;

    // This size needs to be same as in shader!
    // x = x position
    // y = z position
    // z = blood radius
    // w = blood scale
    [Tooltip("x = x position, y = z position, z = radius, w = scale")]
    public Vector4[] bloodPositions = new Vector4[32];

    void Update()
    {
        if (snowMaterial)
        {
            snowMaterial.SetVectorArray(bloodID, bloodPositions);
        }
    }

    public int nextBloodId = 0;

    public void SpawnBlood(Vector3 pos)
    {
        float r = Random.Range(2, 3.5f);
        float s = Random.Range(0.1f, 0.3f);
        bloodPositions[nextBloodId] = new Vector4(pos.x, pos.z, r, s);
        nextBloodId++;
        if (nextBloodId >= bloodPositions.Length)
            nextBloodId = 0;
    }
}
