using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadJson : MonoBehaviour
{
    [SerializeField] private PrayerPool prayerPool;

    private void Start()
    {
        string path = Application.streamingAssetsPath + "/prayers.json";
        string jsonContent = File.ReadAllText(path);

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        PrayerData[] prayerData = JsonConvert.DeserializeObject<PrayerData[]>(jsonContent, settings);

        foreach(PrayerData prayer in prayerData)
        {
            prayerPool.CreatePrayer(prayer);
        }
    }
}

[System.Serializable]
public class PrayerData
{
    public string id;
    public string name;
    public string description;
    public string created_at;
}