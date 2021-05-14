using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Expansion
{
    [Serializable]
    public struct ExpansionData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id;

        /// <summary>
        /// 名前
        /// </summary>
        public string name;

        /// <summary>
        /// コスト
        /// </summary>
        public int cost;
    }

    /// <summary>
    /// エキスパンション管理
    /// </summary>
    public class ExpansionManager
    {
        /// <summary>
        /// エキスパンションリスト
        /// </summary>
        private List<ExpansionData> Expansions = new List<ExpansionData>();

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="Data">データ</param>
        public void Add(ExpansionData Data)
        {
            Expansions.Add(Data);
        }

        /// <summary>
        /// 全取得
        /// </summary>
        /// <returns>全てのデータ</returns>
        public ExpansionData[] GetAll()
        {
            return Expansions.ToArray();
        }

        #region Singleton
        public static ExpansionManager Instance { get { return _Instance; } }
        private static ExpansionManager _Instance = new ExpansionManager();
        #endregion
    }

}
