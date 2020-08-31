using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionsList
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static int OutOfListId<T>(this IList<T> list, int id)
    {
        if (id >= list.Count) { return 0; }
        return id;
    }

    //Does anything=??????????????????????????????????????????????????????????????
    public static void SortByName<T>(this List<T> list) where T : Object
    {
        System.Array.Sort(list.ToArray(), (a, b) => a.name.CompareTo(b.name));
    }
}