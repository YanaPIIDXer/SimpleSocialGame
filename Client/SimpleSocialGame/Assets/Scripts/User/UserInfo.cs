using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Game.User
{
    /// <summary>
    /// ユーザ情報
    /// </summary>
    public static class UserInfo
    {
        /// <summary>
        /// 名前
        /// </summary>
        public static string Name { get; set; }

        /// <summary>
        /// 石
        /// </summary>
        public static ReactiveProperty<int> Stone { get; set; }
    }
}
