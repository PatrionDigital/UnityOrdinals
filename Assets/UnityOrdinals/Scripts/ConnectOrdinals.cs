using System.Runtime.InteropServices;
using UnityEngine;

public static class ConnectOrdinals
{
#if UNITY_WEBGL || UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void connectToBtcWallet();

    public static void ConnectToBitcoinWallet()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        connectToBtcWallet();
#endif
#if UNITY_EDITOR
        // Set Dummy Address in Editor
        OrdinalsManager.Instance.OnReceiveBtcAccount("BTC WALLET ADDRESS");
#endif
    }
#endif
}
