using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}
