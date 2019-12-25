using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
 
public class CreatePostProcessVolume : MonoBehaviour
{
	private PostProcessVolume postProcessVolume;
 
	// Start is called before the first frame update
	void Start()
	{
		// Bloom効果のインスタンスの作成
		Bloom bloom = ScriptableObject.CreateInstance<Bloom>();
		bloom.enabled.Override(true);
		bloom.intensity.Override(20f);
		// ポストプロセスボリュームに反映
		postProcessVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, bloom);
	}
 
	void OnDestroy() {
		// 作成したボリュームの削除
		RuntimeUtilities.DestroyVolume(postProcessVolume, true, true);
	}
}