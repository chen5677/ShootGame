using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxSample : MonoBehaviour
{
    public Material[] mats;
    public int index;

    private void Start()
    {
        InvokeRepeating(nameof(ChangeSkybox), 2, 40);
    }

    private void ChangeSkybox()
    {
        RenderSettings.skybox = mats[index];
        index++;
        index %= mats.Length;
    }
}
