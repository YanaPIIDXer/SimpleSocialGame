using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.AssetBundle
{
    /// <summary>
    /// AssetBundle管理
    /// </summary>
    public class AssetBundleManager
    {
        /// <summary>
        /// ベースのURL
        /// </summary>
        private static string BaseURL
        {
            get
            {
                var URL = "https://simple-social-game.s3.ap-northeast-3.amazonaws.com/";
#if !UNITY_EDITOR && UNITY_IOS
                URL += "iOS/";
#else
                URL += "Windows/";
#endif
                return URL;
            }
        }

        /// <summary>
        /// AssetBundleのDictionary
        /// </summary>
        private Dictionary<string, UnityEngine.AssetBundle> BundleDic = new Dictionary<string, UnityEngine.AssetBundle>();

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="BundleName">AssetBundle名</param>
        /// <returns>AssetBundle</returns>
        public UnityEngine.AssetBundle Get(string BundleName)
        {
            if (!BundleDic.ContainsKey(BundleName)) { return null; }
            return BundleDic[BundleName];
        }

        /// <summary>
        /// ダウンロード
        /// </summary>
        /// <param name="BundleName">AssetBundle名</param>
        /// <param name="Callback">コールバック</param>
        /// <returns></returns>
        public IEnumerator Download(string BundleName, Action<bool> Callback)
        {
            if (BundleDic.ContainsKey(BundleName))
            {
                Callback?.Invoke(true);
                yield break;
            }

            var URL = BaseURL + BundleName;
            using (var Request = UnityWebRequestAssetBundle.GetAssetBundle(URL))
            {
                yield return Request.SendWebRequest();

                if (Request.isHttpError || Request.isNetworkError)
                {
                    Callback?.Invoke(false);
                    yield break;
                }

                var Handle = Request.downloadHandler as DownloadHandlerAssetBundle;
                var Bundle = Handle.assetBundle;
                BundleDic.Add(BundleName, Bundle);
                OnSuccess?.Invoke(true);
            }
        }

        #region Singleton
        public static AssetBundleManager Instance { get { return _Instance; } }
        private static AssetBundleManager _Instance = new AssetBundleManager();
        #endregion
    }
}
