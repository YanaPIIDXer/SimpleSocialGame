using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.API;

namespace Game.Boot
{
    /// <summary>
    /// ゲーム起動管理
    /// </summary>
    public class GameBooter : MonoBehaviour
    {
        void Awake()
        {
            StartCoroutine(APICall.Expansions((Expansions) =>
            {
                if (Expansions == null)
                {
                    Debug.LogError("Get Expansion List Failed.");
                    return;
                }

                foreach (var Expansion in Expansions)
                {
                    Debug.Log(Expansion);
                }
            }));
        }
    }
}
