﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        /// ログイン
        /// </summary>
        /// <param name="Name">名前</param>
        /// <param name="Callback">コールバック</param>
        public static IEnumerator Login(string Name, Action<LoginResult> Callback)
        {
            string URL = BaseURL + "login";
            using (var Req = UnityWebRequest.Post(URL, "POST"))
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
    }
}