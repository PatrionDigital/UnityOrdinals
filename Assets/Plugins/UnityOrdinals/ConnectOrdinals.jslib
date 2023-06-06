var ConnectOrdinals = {
    connectToBtcWallet: function() {
        window.StacksProvider.request("getAddresses").then(function(res){
            // TapRoot Wallet is in second slot
            window.BtcAccount = res.result.addresses[1];
            
            // Call the callback function in Unity
            unityGame.SendMessage("OrdinalsManager", "OnReceiveBtcAccount", window.BtcAccount.address);
        });
    }
}
mergeInto(LibraryManager.library, ConnectOrdinals);