using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.API;
using Game.Expansion;
using Game.User;

namespace Game.UI
{
    /// <summary>
    /// エキスパンション選択
    /// </summary>
    public class ExpansionSelector : MonoBehaviour
    {
        /// <summary>
        /// エキスパンションリスト
        /// </summary>
        private ExpansionData[] Expansions = null;

        /// <summary>
        /// エキスパンション選択リスト
        /// </summary>
        [SerializeField]
        private Dropdown ExpansionDropdown = null;

        /// <summary>
        /// 引くボタン
        /// </summary>
        [SerializeField]
        private Button DrawButton = null;

        void Awake()
        {
            Expansions = ExpansionManager.Instance.GetAll();
            foreach (var Expansion in Expansions)
            {
                ExpansionDropdown.options.Add(new Dropdown.OptionData { text = Expansion.name });
            }
            ExpansionDropdown.value = 0;

            DrawButton.onClick.AddListener(OnClickDrawButton);
        }

        /// <summary>
        /// 引くボタンが押された
        /// </summary>
        private void OnClickDrawButton()
        {
            var Expansion = Expansions[ExpansionDropdown.value];
            StartCoroutine(APICall.DrawGacha(Expansion.id, (Result) =>
            {
                if (!Result.result)
                {
                    Debug.LogError("DrawGacha Failed.");
                    Debug.Log(Result.card_name);
                    return;
                }

                UserInfo.Stone.Value = Result.last_stone;
                Debug.Log(Result.card_name);
            }));
        }
    }
}
