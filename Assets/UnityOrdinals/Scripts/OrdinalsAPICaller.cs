using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class OrdinalsAPICaller : MonoBehaviour
{
    public string m_ApiEndpoint;

    // Non-Fungible Tokens
    public async Task<TResultType> GetOrdinalHoldings<TResultType>(string _address)
    {
        string _json;
        var _url = string.Format("{0}inscriptions?address={1}", m_ApiEndpoint, _address);
        Debug.Log(_url);

        using var _www = UnityWebRequest.Get(_url);
        _www.SetRequestHeader("Content-Type", "application/json");
        var _operation = _www.SendWebRequest();

        while(!_operation.isDone)
        {
            await Task.Yield();
        }

        if (_www.result == UnityWebRequest.Result.Success)
        {
            _json = _www.downloadHandler.text;
            var _result = JsonUtility.FromJson<TResultType>(_json);
            return _result;
        } else { return default; }
    }
}
