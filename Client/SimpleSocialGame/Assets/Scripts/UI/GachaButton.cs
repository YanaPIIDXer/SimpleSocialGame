using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    /// <summary>
    /// ガチャシーン遷移ボタン
    /// </summary>
    public class GachaButton : MonoBehaviour
    {
        /// <summary>
        /// シーン遷移ボタン
        /// </summary>
        private Button SceneMoveButton = null;

        void Awake()
        {
            SceneMoveButton = GetComponent<Button>();
            SceneMoveButton.onClick.AddListener(() => SceneManager.LoadScene("Gacha"));
        }
    }
}
