using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.User;
using Game.API;

namespace Game.UI
{
    /// <summary>
    /// ログインフォーム
    /// </summary>
    public class LoginForm : MonoBehaviour
    {
        /// <summary>
        /// 名前入力欄
        /// </summary>
        [SerializeField]
        private InputField NameInput = null;

        /// <summary>
        /// ログインボタン
        /// </summary>
        [SerializeField]
        private Button LoginButton = null;

        void Awake()
        {
            LoginButton.onClick.AddListener(OnLoginButtonClick);
        }

        /// <summary>
        /// ログインボタンが押された
        /// </summary>
        private void OnLoginButtonClick()
        {
            string Name = NameInput.text;
            StartCoroutine(APICall.Login(Name, (Result) =>
            {
                if (!Result.IsSuccess)
                {
                    Debug.Log("ログイン失敗");
                    return;
                }

                UserInfo.Name = Name;
                UserInfo.Stone.Value = Result.stone;
            }));
        }
    }
}
