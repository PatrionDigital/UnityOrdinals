using UnityEngine;
using System;
using System.Collections;
using System.Threading.Tasks;

public class OrdinalsManager : MonoBehaviour
{
    // Parameters
    public string walletOrdinals { get => this._wallet; }
    private string _wallet;

    [SerializeReference]
    private string networkAddressMain = "https://api.hiro.so/ordinals/v1/";

    [SerializeReference]
    JSONOrdinalHoldings m_OrdinalHoldings;

    private OrdinalsAPICaller ordinalsAPICaller;

    public delegate void GetBitcoinAccountCallback(string address);
    static GetBitcoinAccountCallback bitcoinAccountCallback;

    public delegate void GetOrdinalHoldingsCallback<TResultType>(TResultType result);
    static GetOrdinalHoldingsCallback<JSONOrdinalHoldings> ordinalHoldingsCallback;

    public static OrdinalsManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static OrdinalsManager instance;

    private void Awake()
    {
        // OrdinalsManager Singleton
        if (instance != this)
        {
            if (instance != null)
            {
                Destroy(instance.gameObject);
            }
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        // Ensure APICaller Component is attached
        if (!ordinalsAPICaller)
        {
            ordinalsAPICaller = gameObject.AddComponent<OrdinalsAPICaller>();
            ordinalsAPICaller.m_ApiEndpoint = networkAddressMain;
        }

        // Subscribe Internal Delegates
        bitcoinAccountCallback += OnBitcoinAccountCallback;
        ordinalHoldingsCallback += OnOrdinalHoldingsCallback;

        // Ensure the GameObject is always named "OrdinalsManager"
        // Otherwise ConnectOrdinals plugin will not work
        if (gameObject.name != "OrdinalsManager")
            gameObject.name = "OrdinalsManager";
    }

    private void OnDisable()
    {
        // De-subscribe Internal Delegates
        bitcoinAccountCallback -= OnBitcoinAccountCallback;
        ordinalHoldingsCallback -= OnOrdinalHoldingsCallback;
    }
    public void GetBitcoinAccount()
    {
        try
        {
            ConnectOrdinals.ConnectToBitcoinWallet();
        }
        catch (Exception e)
        {
            Debug.LogError("OrdinalsManager: Failed to get Bitcoin account: " + e.Message);
        }
    }

    // Callback method to receive the Bitcoin account address from the Wallet
    public void OnReceiveBtcAccount(string address)
    {
        Debug.Log("OrdinalsManager - Received Bitcoin account address: " + address);

        // Callback when call complete
        bitcoinAccountCallback?.Invoke(address);
    }

    // Bridge method to handle the asynchronous operation from UI
    public void CallGetOrdinalHoldings()
    {
        StartCoroutine(GetOrdinalHoldingsCoroutine());
    }

    private IEnumerator GetOrdinalHoldingsCoroutine()
    {
        yield return GetOrdinalHoldings();
    }
    public async Task GetOrdinalHoldings()
    {
        if (!string.IsNullOrEmpty(walletOrdinals))
        {
            m_OrdinalHoldings = await ordinalsAPICaller.GetOrdinalHoldings<JSONOrdinalHoldings>(walletOrdinals);
            if (m_OrdinalHoldings == null)
            {
                Debug.LogWarning("OrdinalsManager - API: GetOrdinalHoldings Failed");
            }
            else
            {
                // Callback when call complete
                ordinalHoldingsCallback?.Invoke(m_OrdinalHoldings);
            }
        }
    }

    // Internal Callbacks
    private void OnBitcoinAccountCallback(string address)
    {
        Debug.Log("Ordinals Manager Callback: BitCoin Account: " + address);
        _wallet = address;
    }
    
    private void OnOrdinalHoldingsCallback(JSONOrdinalHoldings result)
    {
        Debug.Log("Ordinals Manager Callback: Ordinal Holdings: " + result.total);
    }
}
