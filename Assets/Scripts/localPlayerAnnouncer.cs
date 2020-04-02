using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class localPlayerAnnouncer : NetworkBehaviour
{
    #region Public.
    public static event Action<NetworkIdentity> OnLocalPlayerUpdated;
    #endregion

    #region Start/Destroy
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        OnLocalPlayerUpdated?.Invoke(base.netIdentity);
    }

    private void OnDestroy()
    {
        if (base.isLocalPlayer)
            OnLocalPlayerUpdated?.Invoke(null);
    }

    #endregion

}
