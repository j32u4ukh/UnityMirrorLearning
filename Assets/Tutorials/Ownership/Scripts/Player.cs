using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorials.Ownership
{
    public class Player : NetworkBehaviour
    {
        [SerializeField]
        private Vector3 movement = Vector3.zero;

        [Client]
        private void Update()
        {
            // 需勾選 Network Transform 的 Client Authority(之後由 Sync Direction 取代)
            // 檢查當前玩家是否擁有此物件，避免改到其他玩家
            if (!isOwned)
            {
                return;
            }

            if (!Input.GetKeyDown(KeyCode.Space))
            {
                return;
            }

            // 向伺服器請求移動
            CmdMove();
        }

        // Call server to excute this function.
        [Command]
        private void CmdMove()
        {
            // Validate logic here. 判斷是否邏輯是否正確
            // 若正確，將該玩家的移動通知所有玩家
            RpcMove();
        }

        // Call this from the server, and it runs on clients.
        [ClientRpc]
        private void RpcMove()
        {
            transform.Translate(movement);
        }
    }
}

