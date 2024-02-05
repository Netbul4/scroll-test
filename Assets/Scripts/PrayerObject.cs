using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrayerObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI PId, PName, PDescription, PDate;

    public void Initialize(PrayerData data)
    {
        PId.text = data.id;
        PName.text = data.name;
        PDescription.text = data.description;
        PDate.text = data.created_at;
    }
    
}
