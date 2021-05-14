using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.API
{
    /// <summary>
    /// ログイン結果
    /// </summary>
    public struct LoginResult
    {
        /// <summary>
        /// 成功か？
        /// </summary>
        public bool IsSuccess;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string message;

        /// <summary>
        /// 石
        /// </summary>
        public string stone;
    }
}
