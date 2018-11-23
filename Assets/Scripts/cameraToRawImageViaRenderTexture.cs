using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//code to see if can find rendertexture
//wow render textures are messy dont understand why cant put them on objects would like to avoid them entirely if can
//im useing here special resource folder and calling from that
//will work now need to hook up where it receives from too
//the string will be given to thisplayersetting

public class cameraToRawImageViaRenderTexture : MonoBehaviour {

private RenderTexture rt;
private RawImage ri;
private GameObject aCam;
	// Use this for initialization
	void Start () {

	ri = GetComponent<RawImage>();
	aCam = GameObject.Find("aCam");
	rt = Resources.Load<RenderTexture>("BLURIGHTRENDERTEXTURE");
	aCam.GetComponent<Camera>().targetTexture = rt;
	ri.texture = rt;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
