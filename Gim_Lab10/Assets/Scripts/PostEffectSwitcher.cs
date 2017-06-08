using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

[RequireComponent(typeof(Camera))]
public class PostEffectSwitcher : MonoBehaviour
{
    private readonly Dictionary<string, PostEffectsBase> _postEffects = new Dictionary<string, PostEffectsBase>();
    private readonly Dictionary<string, ImageEffectBase> _imageEffects = new Dictionary<string, ImageEffectBase>();

    public Text PostEffectsLabel;

    // Use this for initialization
    void Start()
    {
        RegisterPostEffect<DepthOfFieldDeprecated>();
        RegisterPostEffect<BloomOptimized>();
        RegisterPostEffect<EdgeDetection>();
        RegisterPostEffect<SunShafts>();
        RegisterPostEffect<Antialiasing>();
        RegisterPostEffect<GlobalFog>();
        RegisterImageEffect<MotionBlur>();
        UpdateLabel();
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < 10; i++)
            if(Input.GetKeyDown(KeyCode.Alpha1+i)) SwitchEffect(i);
    }

    private void RegisterPostEffect<T>() where T : PostEffectsBase
    {
        var effect = GetComponent<T>();
        if (effect != null)
            _postEffects[typeof(T).Name] = effect;
    }

    private void RegisterImageEffect<T>() where T : ImageEffectBase
    {
        var effect = GetComponent<T>();
        if (effect != null)
            _imageEffects[typeof(T).Name] = effect;
    }

    private void SwitchEffect(int effectIndex)
    {
        if (effectIndex < _postEffects.Count)
        {
            var effect = _postEffects.ElementAt(effectIndex).Value;
            effect.enabled = !effect.enabled;
            UpdateLabel();
        }
        else if (effectIndex - _postEffects.Count < _imageEffects.Count)
        {
            var effect = _imageEffects.ElementAt(effectIndex - _postEffects.Count).Value;
            effect.enabled = !effect.enabled;
            UpdateLabel();
        }
    }

    private void UpdateLabel()
    {
        var s = "";
        for(var i=0; i<_postEffects.Count; i++)
            if (_postEffects.ElementAt(i).Value.enabled) s += _postEffects.ElementAt(i).Key + " ON\n";
        for (var i = 0; i < _imageEffects.Count; i++)
            if (_imageEffects.ElementAt(i).Value.enabled) s += _imageEffects.ElementAt(i).Key + " ON\n";
        PostEffectsLabel.text = !string.IsNullOrEmpty(s)
            ? s
            : (_postEffects.Count + _imageEffects.Count == 0
                ? ""
                : "Use 1-" + (_postEffects.Count + _imageEffects.Count) + " to switch posteffects"
              );
    }
}
