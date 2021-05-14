using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.API;
using Game.AssetBundle;
using UnityEngine.SceneManagement;
using Game.Expansion;

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
        private void OnGetExpansionList(ExpansionData[] Expansions)
        {
            if (Expansions == null)
            {
                Debug.LogError("Get Expansion List Failed.");
                return;
            }

            foreach (var Expansion in Expansions)
            {
                ExpansionManager.Instance.Add(Expansion);
                Debug.Log(Expansion.name);
                DownloadExpansion(Expansion.name, () =>
                {
                    Debug.Log("Download Failed...");
                    return;
                });
                Debug.Log("Download Success!");
                SceneManager.LoadScene("Title");
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
