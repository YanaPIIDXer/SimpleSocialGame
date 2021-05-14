using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.API
{
    /// <summary>
    /// ガチャを引いた結果
    /// </summary>
    public struct DrawGachaResult
    {
        /// <summary>
        /// 成功か？
        /// </summary>
        public bool result;

        /// <summary>
        /// メッセージ
        /// </summary>
        public string card_name;

        /// <summary>
        /// 石
        /// </summary>
        public int last_stone;
    }
}
