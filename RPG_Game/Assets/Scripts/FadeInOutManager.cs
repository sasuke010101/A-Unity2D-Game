using UnityEngine;
using System.Collections;

public class FadeInOutManager : Singleton<FadeInOutManager> {

	protected FadeInOutManager() {}

	public Material fadeMaterial;
	private float fadeOutTime, fadeInTime;
	private Color fadeColor;

	private string navigateToLevelName = "";
	private int navigateToLevelIndex = 0;

	private bool fading = false;

	public static bool Fading
	{
		get {return Instance.fading;}
	}

	void Awake()
	{
		fadeMaterial = new Material("Shader \"Plane/No zTest\" {" +
		                             "SubShader { Pass { " +
		                             "    Blend SrcAlpha OneMinusSrcAlpha " +
		                             "    ZWrite Off Cull Off Fog { Mode Off } " +
		                             "    BindChannels {" +
		                           	 "      Bind \"color\", color }" +
		                           	 "} } }");
	}

	private IEnumerator Fade()
	{
		float t = 0.0f;
		while(t < 1.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t + Time.deltaTime /fadeOutTime);
			DrawingUtilities.DrawQuad(fadeMaterial, fadeColor, t);
		}

		if(navigateToLevelName != "")
			Application.LoadLevel(navigateToLevelName);
		else
			Application.LoadLevel(navigateToLevelIndex);

		while(t > 0.0f)
		{
			yield return new WaitForEndOfFrame();
			t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
			DrawingUtilities.DrawQuad(fadeMaterial, fadeColor, t);
		}

		fading = false;
	}

	private void StartFade(float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		fading = true;
		Instance.fadeOutTime = aFadeOutTime;
		Instance.fadeInTime = aFadeInTime;
		Instance.fadeColor = aColor;
		StopAllCoroutines();
		StartCoroutine("Fade");
	}

	public static void FadeToLevel(string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		if(Fading) return;
		Instance.navigateToLevelName = aLevelName;
		Instance.StartFade(aFadeOutTime, aFadeInTime, aColor);
	}

	public static void FadeToLevel(string aLevelName)
	{
		if(Fading) return;
		Instance.navigateToLevelName = aLevelName;
		FadeToLevel(aLevelName, 2f, 2f, Color.black);
	}

	public static void FadeToLevel(Material aFadeMaterial, string aLevelName)
	{
		Instance.fadeMaterial = aFadeMaterial;
		FadeToLevel(aLevelName);
	}

	public static void FadeToLevel(Material aFadeMaterial, string aLevelName, float aFadeOutTime, float aFadeInTime, Color aColor)
	{
		Instance.fadeMaterial = aFadeMaterial;
		FadeToLevel(aLevelName, aFadeOutTime, aFadeInTime, aColor);
	}

}

public static class DrawingUtilities
{
	public static void DrawQuad(
		Material aMaterial,
		Color aColor,
		float aAlpha)
	{
		aColor.a = aAlpha;
		aMaterial.SetPass(0);
		GL.PushMatrix();
		GL.LoadOrtho();
		GL.Begin(GL.QUADS);
		GL.Color(aColor);
		GL.Vertex3(0, 0, -1);
		GL.Vertex3(0, 1, -1);
		GL.Vertex3(1, 1, -1);
		GL.Vertex3(1, 0, -1);
		GL.End();
		GL.PopMatrix();
	}
}