using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1_MeinHundNanga.ViewModels;

public partial class MainWindowViewModel : ObservableObject, IComparable
{
    public MainWindowViewModel()
    {
        Name = "SI.CA.leerString";
        Anmerkung = "SI.CA.leerString";
        SelectedStatusDerUebernahme = new ClsStatusDerUebernahme();
        _lstStatusDerUebernahme = [new ClsStatusDerUebernahme() { EnumStatus = StatusDerUebernahme.Unbekannt, StrCaption = "Unbekannt" }, new ClsStatusDerUebernahme() { EnumStatus = StatusDerUebernahme.NichtEingereicht, StrCaption = "N. e." }, new ClsStatusDerUebernahme() { EnumStatus = StatusDerUebernahme.Eingereicht, StrCaption = "Eingereicht" }, new ClsStatusDerUebernahme() { EnumStatus = StatusDerUebernahme.Uebernommen, StrCaption = "Übernommen" }];
    }

    [ObservableProperty]
    private string _name;
    public EntryFoundSource FoundSource { get; set; }
    public Auftragsart Ort { get; set; }

    [ObservableProperty]
    private string _anmerkung;

    [ObservableProperty]
    private ObservableCollection<ClsMenuEntryKividOrGEOgraf>? _kivids;

    [ObservableProperty]
    private ObservableCollection<ClsMenuEntryKividOrGEOgraf>? _gEOgrafs;

    [ObservableProperty]
    private ClsMenuEntryFolder? _folder;   // hier wird sowieso nur ein Eintrag sein.

    [ObservableProperty]
    private ClsStatusDerUebernahme _selectedStatusDerUebernahme;

    [ObservableProperty]
    private ObservableCollection<ClsStatusDerUebernahme> _lstStatusDerUebernahme;



    public int CompareTo(object? obj)
    {
        if (obj == null)
        {
            return -1;
        }
        else
        {
            MainWindowViewModel a = this;
            MainWindowViewModel b = (MainWindowViewModel)obj;
            return string.Compare(a.Name, b.Name);
        }
    }
}
