using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrayerPool : MonoBehaviour
{
    [SerializeField] private PrayerObject prayerPrefab;
    [SerializeField] private RectTransform container;
    [SerializeField] private RectTransform prayersContainer;
    [SerializeField] private ScrollHandler scrollHandler;
    [SerializeField] private GameObject LoadingObject;
    [SerializeField] private int prayerPoolSize;
    [SerializeField] private int prayersLoadedSize;

    [SerializeField] private List<PrayerObject> prayers = new List<PrayerObject>();
    [SerializeField] private List<PrayerData> prayersToLoad = new List<PrayerData>();

    private void Awake()
    {
        for(int i = 0; i < prayerPoolSize; i++)
        {
            PrayerObject prayer = Instantiate(prayerPrefab, prayersContainer);
            prayer.gameObject.SetActive(false);
            prayers.Add(prayer);
        }

        scrollHandler.OnScrollEnded += CreateMorePrayers;
        UpdateLayoutSize();
    }

    public void CreatePrayer(PrayerData data)
    {
        foreach (PrayerObject p in prayers)
        {
            if (p.gameObject.activeInHierarchy) continue;

            p.gameObject.SetActive(true);
            p.Initialize(data);
            prayersLoadedSize++;
            break;
        }

        prayersToLoad.Add(data);
        UpdateLayoutSize();
    }

    public void CreateMorePrayers()
    {
        StartCoroutine(PopulateMorePrayers());
    }

    private IEnumerator PopulateMorePrayers()
    {
        GameObject Loader = Instantiate(LoadingObject, prayersContainer);
        yield return new WaitForSeconds(2.0f);
        Destroy(Loader);
        scrollHandler.isEventLaunched = false;

        int index = 0;
        for (int i = prayersLoadedSize - 1; i < prayersToLoad.Count; i++)
        {
            if (index == prayerPoolSize) break;
            PrayerObject prayer = Instantiate(prayerPrefab, prayersContainer);
            prayer.gameObject.SetActive(true);
            prayer.Initialize(prayersToLoad[i]);
            prayers.Add(prayer);
            prayersLoadedSize++;
            index++;
        }

        UpdateLayoutSize();
        yield return 0;
    }

    public void ReturnPrayerToPool(PrayerObject p)
    {
        p.gameObject.SetActive(false);
    }

    private void UpdateLayoutSize()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(container);
        LayoutRebuilder.ForceRebuildLayoutImmediate(prayersContainer);
    }
}
