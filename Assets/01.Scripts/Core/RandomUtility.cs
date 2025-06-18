using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class RandomUtility
{
    public static List<T> GetRandomsInListNotDuplicated<T>(List<T> list, int count)
    {
        List<T> listInstance = list.ToList();
        Shuffle<T>(listInstance);

        List<T> values = new List<T>();
        count = Mathf.Clamp(count, 0, list.Count + 1);
        for(int i = 0; i < count; i++) values.Add(listInstance[i]);
        return values;
    }

    public static T GetRandomInList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int j = Random.Range(0, list.Count);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}
