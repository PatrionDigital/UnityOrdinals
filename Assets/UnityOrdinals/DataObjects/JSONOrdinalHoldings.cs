using System;

[Serializable]
public class JSONOrdinalHoldings
{
    public int limit;
    public int offset;
    public int total;
    public JSONOrdinalHoldingsResults[] results;
}

[Serializable]
public class JSONOrdinalHoldingsResults
{
    public string id;
    public int number;
    public string address;
    public string genesis_address;
    public int genesis_block_height;
    public string genesis_block_hash;
    public string genesis_tx_id;
    public string genesis_fee;
    public int genesis_timestamp;
    public string tx_id;
    public string location;
    public string output;
    public string value;
    public string offset;
    public string sat_ordinal;
    public string sat_rarity;
    public int sat_coinbase_height;
    public string mime_type;
    public string content_type;
    public int content_length;
    public int timestamp;    
}