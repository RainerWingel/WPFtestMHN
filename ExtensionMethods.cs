using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga;

public static class ExtensionMethods
{
    public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable      // Source: https://stackoverflow.com/a/16344936/2925705  ("How do I sort an observable collection?")
    {
        List<T> sorted = [.. collection.OrderBy(x => x)];
        for (int i = 0; i < sorted.Count; i++)
            collection.Move(collection.IndexOf(sorted[i]), i);
    }

    public static void SortDescending<T>(this ObservableCollection<T> collection) where T : IComparable
    {
        List<T> sorted = [.. collection.OrderByDescending(x => x)];
        for (int i = 0; i < sorted.Count; i++)
            collection.Move(collection.IndexOf(sorted[i]), i);
    }
}
