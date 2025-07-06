using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices; // for DllImport
using AOT;
using System;

namespace WebGLSupport
{
    class WebGLInputMobilePlugin
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        public static extern int WebGLInputMobileRegister(Action<int> OnTouchEnd);

        [DllImport("__Internal")]
        public static extern void WebGLInputMobileOnFocusOut(int id, Action<int> OnFocusOut);
#else
        /// <summary>
        /// ID を割り振り
        /// </summary>
        /// <returns></returns>
        public static int WebGLInputMobileRegister(Action<int> OnTouchEnd) { return 0; }

        public static void WebGLInputMobileOnFocusOut(int id, Action<int> OnFocusOut) {}
#endif
    }

    public class WebGLInputMobile : MonoBehaviour, IPointerDownHandler
    {
        static Dictionary<int, WebGLInputMobile> instances = new Dictionary<int, WebGLInputMobile>();

        int id = -1;

        private void Awake()
        {
#if !(UNITY_WEBGL && !UNITY_EDITOR)
            // WebGL 以外、更新メソッドは動作しないようにします
            enabled = false;
#endif
        }

        /// <summary>
        /// 押されたら、touchend イベントを登録する
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"OnPointerDown: {id}");
            if (id != -1)
            {
                Debug.LogError($"OnPointerDown-skip: {id}");
                return;
            }
            id = WebGLInputMobilePlugin.WebGLInputMobileRegister(OnTouchEnd);
            Debug.Log($"WebGLInputMobileRegister: {id}");
            instances[id] = this;
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        static void OnTouchEnd(int id)
        {
            Debug.Log($"OnTouchEnd: {id}");
            var @this = instances[id];
            try
            {
                @this.GetComponent<WebGLInput>().OnSelect();
            }
            catch (Exception ex)
            {
                Debug.LogError("OnSelect-failed");
                Debug.LogException(ex);
            }
            Debug.Log($"OnSelect-done: {id}");

            try
            {
                Debug.Log($"RegisterOnFocusOut: {@this.isActiveAndEnabled}");
                @this.StartCoroutine(RegisterOnFocusOut(id));
            }
            catch (Exception ex)
            {
                Debug.LogError("RegisterOnFocusOut-failed");
                Debug.LogException(ex);
            }
            Debug.Log($"RegisterOnFocusOut-done: {id}");
        }

        static IEnumerator RegisterOnFocusOut(int id)
        {
            Debug.Log("RegisterOnFocusOut-begin1");
            yield return null;  // wait one frame.
            Debug.Log("RegisterOnFocusOut-begin2");
            WebGLInputMobilePlugin.WebGLInputMobileOnFocusOut(id, OnFocusOut);
            Debug.Log("RegisterOnFocusOut-begin3");
        }

        [MonoPInvokeCallback(typeof(Action<int>))]
        static void OnFocusOut(int id)
        {
            var @this = instances[id];
            @this.StartCoroutine(ExecFocusOut(id));
        }

        static IEnumerator ExecFocusOut(int id)
        {
            yield return null;  // wait one frame.
            var @this = instances[id];
            @this.GetComponent<WebGLInput>().DeactivateInputField();
            // release
            @this.id = -1;
            instances.Remove(id);
        }
    }
}

