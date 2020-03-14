using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HostelQuality
{
    Cleanliness = 0,
    Comfort,
    ValueForMoney,
    Facilities,
    //---------- to były obiektywne staty
    Luxurious, // 100 - ąę, 0 - hipi
    Wildness // 100 party all night, 0 expaci czytają książki
    // --------- to były style hostelu
}

public class HostelQualities
{
    Dictionary<HostelQuality, int> qualities;

    public HostelQualities()
    {
        qualities = new Dictionary<HostelQuality, int>();
        int qualitiesCount = System.Enum.GetNames(typeof(HostelQuality)).Length;

        for (int i = 0; i < qualitiesCount; i++)
        {
            qualities.Add((HostelQuality)i, 50);
        }
    }

    public int this[HostelQuality q]
    {
        get { return Mathf.Clamp(qualities[q], 0, 100); }
        set { qualities[q] = value; }
    }

    public void ModifyQuality(HostelQuality q, int factor)
    {
        qualities[q] += factor;
    }

    public int GetQuality(HostelQuality q)
    {
        return qualities[q];
    }

    public float GetQualityFloat(HostelQuality q)
    {
        return Mathf.Clamp01(qualities[q] * 0.01f);
    }

    public void ApplyItemProperties(ItemProperty[] properties)
    {
        if (properties == null)
            return;

        for (int i = 0; i < properties.Length; i++)
        {
            qualities[properties[i].Quality] += properties[i].Factor;
        }
    }

    public void LogAllQualities()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach (var q in qualities)
        {
            sb.AppendLine($"{ q.Key.ToString() }: { q.Value }");
        }

        Debug.Log(sb.ToString());
    }
}
