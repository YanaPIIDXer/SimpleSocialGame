using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Game.Expansion;

namespace Game.API
{
    /// <summary>
    /// APIの呼び出し
    /// </summary>
    public static class APICall
    {
        /// <summary>
        /// ベースとなるURL
        /// </summary>
        private static string BaseURL
        {
            get
            {
#if UNITY_EDITOR
                return "http://localhost/";
#else
                return "http://simple-social-game.yanap-apptest.tk/";
#endif
            }
        }

        /// <summary>
        /// エキスパンションリスト取得
        /// </summary>
        /// <param name="Callback">コールバック</param>
        public static IEnumerator Expansions(Action<ExpansionData[]> Callback)
        {
            string URL = BaseURL + "expansions";
            using (var Req = UnityWebRequest.Get(URL))
            {
                yield return Req.SendWebRequest();
                if (Req.responseCode != 200)
                {
                    Callback?.Invoke(null);
                }

                ExpansionData[] Expansions = JsonHelper.FromJson<ExpansionData>(Req.downloadHandler.text);
                Callback?.Invoke(Expansions);
            }
        }

        /// <summary>
        /// ログインリクエストパラメータ
        /// </summary>
        [Serializable]
        private struct LoginRequestParam
        {
            /// <summary>
            /// 名前
            /// </summary>
            public string name;
        }

        /// <summary>
        /// ログイン
        /// </summary>
        /// <param name="Name">名前</param>
        /// <param name="Callback">コールバック</param>
        public static IEnumerator Login(string Name, Action<LoginResult> Callback)
        {
            string URL = BaseURL + "login";
            LoginRequestParam Param = new LoginRequestParam();
            Param.name = Name;
            using (var Req = MakePostRequest<LoginRequestParam>(URL, Param))
            {
                yield return Req.SendWebRequest();
                if (Req.responseCode != 200)
                {
                    LoginResult FailResult = new LoginResult();
                    FailResult.IsSuccess = false;
                    Callback?.Invoke(FailResult);
                    yield break;
                }

                LoginResult Result = JsonUtility.FromJson<LoginResult>(Req.downloadHandler.text);
                Result.IsSuccess = true;
                Callback?.Invoke(Result);
            }
        }

        /// <summary>
        /// ガチャを引くリクエスト
        /// </summary>
        [Serializable]
        private struct DrawGachaRequest
        {
            /// <summary>
            /// ID
            /// </summary>
            public int id;
        }

        /// <summary>
        /// ガチャを引く
        /// </summary>
        /// <param name="ExpansionId">エキスパンションのID</param>
        /// <param name="Callback">コールバック</param>
        public static IEnumerator DrawGacha(int ExpansionId, Action<DrawGachaResult> Callback)
        {
            string URL = BaseURL + "expansions/draw";
            DrawGachaRequest Request = new DrawGachaRequest();
            Request.id = ExpansionId;
            using (var Req = MakePostRequest<DrawGachaRequest>(URL, Request))
            {
                yield return Req.SendWebRequest();
                if (Req.responseCode != 200)
                {
                    Debug.Log("ErrorCode:" + Req.responseCode);
                    DrawGachaResult FailResult = new DrawGachaResult();
                    FailResult.result = false;
                    Callback?.Invoke(FailResult);
                    yield break;
                };

                DrawGachaResult Result = JsonUtility.FromJson<DrawGachaResult>(Req.downloadHandler.text);
                Callback?.Invoke(Result);
            }
        }

        /// <summary>
        /// POSTリクエストを作成
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="Param">パラメータ</param>
        /// <typeparam name="T">パラメータの型</typeparam>
        /// <returns>リクエストオブジェクト</returns>
        private static UnityWebRequest MakePostRequest<T>(string URL, T Param)
        {
            var Req = UnityWebRequest.Post(URL, "POST");

            string ParamJson = JsonUtility.ToJson(Param);
            byte[] ParamData = System.Text.Encoding.UTF8.GetBytes(ParamJson);
            Req.uploadHandler = (UploadHandler)new UploadHandlerRaw(ParamData);
            Req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            Req.SetRequestHeader("Content-Type", "application/json");
            return Req;
        }
    }
}
