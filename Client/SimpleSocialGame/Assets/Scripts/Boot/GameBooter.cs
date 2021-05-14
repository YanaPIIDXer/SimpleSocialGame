using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.API;
using Game.AssetBundle;

namespace Game.Boot
{
    /// <summary>
    /// ゲーム起動管理
    /// </summary>
    public class GameBooter : MonoBehaviour
    {
        void Awake()
        {
            StartCoroutine(APICall.Expansions(OnGetExpansionList));
        }

        /// <summary>
        /// エキスパンションリストを落としてきた
        /// </summary>
        /// <param name="Expansions">エキスパンションリスト</param>
        private void OnGetExpansionList(string[] Expansions)
        {
            if (Expansions == null)
            {
                Debug.LogError("Get Expansion List Failed.");
                return;
            }

            foreach (var Expansion in Expansions)
            {
                Debug.Log(Expansion);
                DownloadExpansion(Expansion, () =>
                {
                    Debug.Log("Download Failed...");
                    return;
                });
                Debug.Log("Download Success!");
            }
        }

        /// <summary>
        /// エキスパンションをダウンロード
        /// </summary>
        /// <param name="Name">エキスパンション名</param>
        /// <param name="OnFail">失敗コールバック</param>
        private void DownloadExpansion(string Name, Action OnFail)
        {
            StartCoroutine(DownloadExpansionBundle(Name, (bSuccess) =>
            {
                if (!bSuccess)
                {
                    OnFail?.Invoke();
                }
            }));
        }

        /// <summary>
        /// エキスパンションをダウンロード
        /// </summary>
        /// <param name="Name">エキスパンション名</param>
        private IEnumerator DownloadExpansionBundle(string Name, Action<bool> Callback)
        {
            yield return AssetBundleManager.Instance.Download(Name, (bSuccess) =>
            {
                if (!bSuccess)
                {
                    Callback?.Invoke(false);
                }
            });
            Callback?.Invoke(true);
        }
    }
}
