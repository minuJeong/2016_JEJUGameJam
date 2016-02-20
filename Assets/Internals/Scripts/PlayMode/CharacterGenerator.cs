using UnityEngine;
using System.Collections;

public class CharacterGenerator : MonoBehaviour
{
	public AnimationClip Clip_Idle;

	public void Generate ()
	{
		SaveModel model = SavedLoader.Instance.SelectedModel;

		if (null == model)
		{
			return;
		}

		// Head
		Texture2D HeadTex = new Texture2D (PreviewPixelControl.X_COUNT, PreviewPixelControl.Y_COUNT);
		HeadTex.LoadImage (model.HeadData.TextureData);

		HeadJointDef HeadDef = new HeadJointDef ();
		HeadDef.Neck = model.HeadData.JointDef.Neck;

		// Body
		Texture2D BodyTex = new Texture2D (PreviewPixelControl.X_COUNT, PreviewPixelControl.Y_COUNT);
		BodyTex.LoadImage (model.BodyData.TextureData);

		BodyJointDef BodyDef = new BodyJointDef ();
		BodyDef.LLegPelvis = model.BodyData.JointDef.LLegPelvis;
		BodyDef.RLegPelvis = model.BodyData.JointDef.RLegPelvis;
		BodyDef.Neck = model.BodyData.JointDef.Neck;

		// RLeg
		Texture2D RLegTex = new Texture2D (PreviewPixelControl.X_COUNT, PreviewPixelControl.Y_COUNT);
		RLegTex.LoadImage (model.RLegData.TextureData);

		RLegJointDef RLegDef = new RLegJointDef ();
		RLegDef.Pelvis = model.RLegData.JointDef.Pelvis;

		// LLeg
		Texture2D LLegTex = new Texture2D (PreviewPixelControl.X_COUNT, PreviewPixelControl.Y_COUNT);
		LLegTex.LoadImage (model.LLegData.TextureData);

		RLegJointDef LLegDef = new RLegJointDef ();
		LLegDef.Pelvis = model.LLegData.JointDef.Pelvis;


		// Generate and Instantiate to GameObject
		GameObject Container = new GameObject (model.SaveName);
		Container.transform.localScale = Vector3.one * 10.0F;


		// Body
		GameObject Body = new GameObject ("Body", typeof(SpriteRenderer));
		Body.GetComponent<SpriteRenderer> ().sprite = 
			Sprite.Create (BodyTex,
			new Rect (0, 0, BodyTex.width, BodyTex.height),
			new Vector2 (0.5F, 0.5F),
			100.0F, 1, SpriteMeshType.FullRect, Vector4.zero);
		Body.transform.SetParent (Container.transform);
		Body.transform.localScale = Vector3.one;

		float ppu = Body.GetComponent<SpriteRenderer> ().sprite.pixelsPerUnit;
		const float pixelCount = PreviewPixelControl.X_COUNT;


		// Head
		GameObject Head = new GameObject ("Head", typeof(SpriteRenderer));
		Head.GetComponent<SpriteRenderer> ().sprite =
			Sprite.Create (HeadTex,
			new Rect (0, 0, HeadTex.width, HeadTex.height),
			new Vector2 (HeadDef.Neck [0], HeadDef.Neck [1]),
			100.0F, 1, SpriteMeshType.FullRect, Vector4.zero);
		Head.transform.SetParent (Body.transform);
		Head.transform.localPosition = new Vector2 (BodyDef.Neck [0] - 0.5F, BodyDef.Neck [1] - 0.5F) * (pixelCount / ppu);
		Head.transform.localScale = Vector3.one;

		// RLeg
		GameObject RLeg = new GameObject ("RLeg", typeof(SpriteRenderer));
		RLeg.GetComponent<SpriteRenderer> ().sprite =
			Sprite.Create (RLegTex,
			new Rect (0, 0, RLegTex.width, RLegTex.height),
			new Vector2 (RLegDef.Pelvis [0], RLegDef.Pelvis [1]),
			100.0F, 1, SpriteMeshType.FullRect, Vector4.zero);
		RLeg.transform.SetParent (Body.transform);
		RLeg.transform.localPosition = new Vector2 (BodyDef.RLegPelvis [0] - 0.5F, BodyDef.RLegPelvis [1] - 0.5F) * (pixelCount / ppu);
		RLeg.transform.localScale = Vector3.one;

		// LLeg
		GameObject LLeg = new GameObject ("LLeg", typeof(SpriteRenderer));
		LLeg.GetComponent<SpriteRenderer> ().sprite =
			Sprite.Create (LLegTex,
			new Rect (0, 0, LLegTex.width, LLegTex.height),
			new Vector2 (LLegDef.Pelvis [0], LLegDef.Pelvis [1]),
			100.0F, 1, SpriteMeshType.FullRect, Vector4.zero);
		LLeg.transform.SetParent (Body.transform);
		LLeg.transform.localPosition = new Vector2 (BodyDef.LLegPelvis [0] - 0.5F, BodyDef.LLegPelvis [1] - 0.5F) * (pixelCount / ppu);
		LLeg.transform.localScale = Vector3.one;


		Container.AddComponent<Animation> ();
		Container.GetComponent<Animation> ().AddClip (Clip_Idle, "idle");
		Container.GetComponent<Animation> ().Play ("idle", UnityEngine.PlayMode.StopAll);
	}
}
