using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
public class OverridePostProcess : MonoBehaviour
{
	// [SerializeField] private PostProcessVolume postProcessVolume;
	private PostProcessVolume postProcessVolume;
	private PostProcessProfile postProcessProfile;
 
	// Start is called before the first frame update
	void Start()
	{
		postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
		
		postProcessProfile = postProcessVolume.profile;	// このボリュームのみ適用
		// postProcessProfile = postProcessVolume.sharedProfile;	// 同じプロファイルを使用した他のボリュームにも適用 : 何やらError が出てしまうのでNG.
 
		Bloom bloom = postProcessProfile.GetSetting<Bloom>();
		bloom.enabled.Override(true);
		bloom.intensity.Override(20f);
 
		// MotionBlurエフェクトの追加
		MotionBlur motionBlur = postProcessProfile.AddSettings<MotionBlur>();
		motionBlur.enabled.Override(true);
		motionBlur.shutterAngle.Override(360f);
 
		// エフェクトが存在するかどうか？
		bool hasVignetteEffect = postProcessProfile.HasSettings<Vignette>();
		Debug.Log("hasVignetteEffect: " + hasVignetteEffect);
 
		// エフェクトがあるかどうかの判断と取得を同時に行う
		Grain grain;
		bool hasGrainEffect = postProcessProfile.TryGetSettings<Grain>(out grain);
 
		if(hasGrainEffect) {
			grain.enabled.Override(true);
			grain.intensity.Override(1f);
		}
		
		// エフェクトを削除
		postProcessProfile.RemoveSettings<Vignette>();
	}
 
	void OnDestroy() {
		// 作成したプロファイルの削除
		RuntimeUtilities.DestroyProfile(postProcessProfile, true);
	}
}
