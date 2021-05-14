using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UniRx;
using Game.User;

namespace Game.UI
{
    /// <summary>
    /// 持ち石表示
    /// </summary>
    public class StoneText : MonoBehaviour
    {
        /// <summary>
        /// 表示テキスト
        /// </summary>
        private Text DisplayText = null;

        void Awake()
        {
            DisplayText = GetComponent<Text>();
            UserInfo.Stone.Subscribe((Value) => DisplayText.text = string.Format("{0}石", Value))
                            .AddTo(gameObject);
        }
    }
}
