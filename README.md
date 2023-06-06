# UNITY ORDINALS

## Overview

UNITY Ordinals is a Unity Template project for creating WebGL applications that can be token-gated by Ordinals (BitCoin NFTs)

Unity Ordinals can access the Hiro wallet browser plugin to retrieve the user's wallet.

It can then retrieve a list of Ordinals stored in that wallet from the Hiro public Ordinals API.

### Contents

- [Installation](#installation)
- [Demo Scene and Basic Setup](#demo-scene-and-basic-setup)
- [Public Functions](#public-functions)
- [Callbacks](#callbacks)
- [Requirements](#requirements)
- [Changelist](#changelist)
- [Contact](#contact)
- [License](#license)
- [Credits](#credits)

## Installation

Currently Unity Ordinals can be cloned from GitHub to setup a new project.

We will provide a Unity `.asset` package in the future.

## Demo Scene and Basic Setup

A demo scene is located at `Assets/Scenes/Test_Ordinals.unity`

To use Unity Ordinals in another scene, simply add `Assets/UnityOrdinals/Prefabs/OrdinalsManager.prefab` to your scene hierarchy.

**WARNING**
_Changing the prefab's name will cause the linkage to the JavaScript plugin to break._

_The component will rename itself if the name has been changed, but best practice is to leave it named `OrdinalsManager`_

### First Build

- Ensure your project is set to build for WebGL in `File -> Build Settings`
- Click the `Player Settings...` button (or `Edit -> Project Settings...`) and select the `Player` tab on the left
  - Under `Resolution and Presentation` in the `WebGL Template` subsection, ensure `Better2020` is selected.
- In `Build Settings` click `Build And Run` and select the output folder (create one called `Builds` if needed)
- Use your browser's Developer Tools `F12` to view the console to see output.



## Public Functions

Unity Ordinals exposes the following functions:

`GetBitcoinAccount()`

- Opens the wallet (Hiro) browser plugin and prompts the user to connect to the webpage.
- Callback to `OnReceiveBtcAccount(string adddress)` from the ConnectOrdinals JavaScript plugin that handles the wallet request.

`CallGetOrdinalHoldings()`

- Bridge method for use with Unity UI and NGUI buttons and other UI functions.
- Uses `APICaller` to retrieve the first 20 ordinals in the given user's wallet.
- Uses the wallet set in `walletOrdinals`

`GetOrdinalHoldings()`

- Asynchronous Task, can be used in code to retrieve the first 20 ordinals in the given user's wallet.
- Uses the wallet set in `walletOrdinals`

## Callbacks

Use these to get receive notifications in your Components when the connected functions return their results.

`OnBitcoinAccountCallback`

- Uses Unity's C# delegate framework.
- Called when the wallet returns a valid BitCoin address.
- Passes the user's address as parameter.

`OnOrdinalHoldingsCallback`

- Uses Unity's C# delegate framework.
- Called when the Hiro API returns a JSON object of ordinals (or empty if the wallet has no ordinals.)
- Passes the JSON object `JSONOrdinalHoldings` as parameter

### Example delegate use

```C#
// In your Component's Awake() method add the following
private void Awake()
{
  myComponentAccountCallback += OnBitcoinAccountCallback;
  myComponentHoldingsCallback == OnOrdinalHoldingsCallback;
}

// Define your callbacks
private void myComponentAccountCallback(string address)
{
  Debug.Log("My Component Callback: BitCoin Account: " + address);
}
    
private void myComponentHoldingsCallback(JSONOrdinalHoldings result)
{
  Debug.Log("My Component Callback: Ordinal Holdings: " + result.total);
} 

// Cleanup: de-register the callbacks
private void OnDisable()
{
  myComponentAccountCallback -= OnBitcoinAccountCallback;
  myComponentHoldingsCallback -= OnOrdinalHoldingsCallback;
}
```

## Requirements

Currently Unity Ordinals is only compatible with [Hiro Wallet](https://wallet.hiro.so/)

Unity Ordinals has been tested with Unity 2021 LTS (2021.3.26f1)

## Changelist

- 0.1.1
  - Retrieve Bitcoin (Taproot) address from Hiro wallet
  - Retrieve list of Ordinals (up to 20) from given wallet

## Contact

For more information about Unity Ordinals, contact the author

- [Patrion@MetaBoy.run](mailto:Patrion@MetaBoy.run) / [@PatrionDigital](https://twitter.com/@PatrionDigital)

## License

Unity Ordinals is licensed under the LGPL 3.0

For more information, see the LICENSE file in the repository.

## Credits

- Unity Ordinals uses the [better-unity-webgl-template](https://github.com/greggman/better-unity-webgl-template)
