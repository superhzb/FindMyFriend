using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestNPC : MonoBehaviour
{
    public RawImage bodyImage;
    public RawImage faceImage;
    public RawImage hairImage;
    public RawImage kitImage;

    public Texture[] bodies;
    public Texture[] faces;
    public Texture[] hairs;
    public Texture[] kits;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bodyImage.texture = bodies[Random.Range(0, bodies.Length)];
            faceImage.texture = faces[Random.Range(0, faces.Length)];
            hairImage.texture = hairs[Random.Range(0, hairs.Length)];
            kitImage.texture = kits[Random.Range(0, kits.Length)];

            //bodyImage.SetNativeSize();
            //faceImage.SetNativeSize();
            //hairImage.SetNativeSize();
            //kitImage.SetNativeSize();
        }
    }

}
