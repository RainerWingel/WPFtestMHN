using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1_MeinHundNanga.ViewModels;

public partial class AuftragsstarterViewModel : ObservableObject
{
    public AuftragsstarterViewModel()
    {
        StrSearchString = string.Empty;
        // Initialize the collection here
        _ocListOfFolders = []; 
    }

    [ObservableProperty]
    // Make sure the backing field is initialized
    private ObservableCollection<MainWindowViewModel> _ocListOfFolders; 

    [ObservableProperty]
    private MainWindowViewModel? _selectedFolder;

    [ObservableProperty]
    private string _strSearchString;

    public ConcurrentBag<ClsMenuEntry>? Hilfsliste { get; set; }


    [RelayCommand]
    private void Refresh()
    {
        //if (!string.IsNullOrWhiteSpace(SI.VMauftragsstarterliste.PublicStrSearchString))
        //{
        //    OcAuftragsstarterLetzteEintraege.Add(SI.VMauftragsstarterliste.PublicStrSearchString);
        //}
        ClearAllData();
        RunSearch();
    }

    public void ClearAllData()
    {
        SelectedFolder = null;
    }

    public void RunSearch()
    {
        StrSearchString = "Dummysearchstring";

        // Don't set OcListOfFolders to null if the search string is empty, 
        // just clear it if it's not null.
        if (StrSearchString.Length == 0)
        {
            OcListOfFolders?.Clear(); 
            return;
        }

        if (StrSearchString.Length > 100)
        {
            System.Diagnostics.Debug.WriteLine("The search string ist too long");
            return;
        }

        Hilfsliste = [];

        // Fill ClsMenuEntries with dummy data:
        ClsMenuEntry clsMenuEntry1 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024\24-0051\24-0051.PAR", EntryOrt = Auftragsart.Archiv, EntryType = TypeOfFile.GEOgrafFile };

        ClsMenuEntry clsMenuEntry2 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024\24-0051\24-0051.KIT", EntryOrt = Auftragsart.Archiv, EntryType = TypeOfFile.KIVIDfile };

        ClsMenuEntry clsMenuEntry3 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024\24-0051\24-0051 - Kopie.KIT", EntryOrt = Auftragsart.Archiv, EntryType = TypeOfFile.KIVIDfile };

        ClsMenuEntry clsMenuEntry4 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024\24-0051\24-0051 - Kopie (2).KIT", EntryOrt = Auftragsart.Archiv, EntryType = TypeOfFile.KIVIDfile };

        ClsMenuEntry clsMenuEntry5 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\GeoArchiv\2024\24-0051", EntryOrt = Auftragsart.Archiv, EntryType = TypeOfFile.Folder };

        ClsMenuEntry clsMenuEntry6 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\Auftraege", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\Auftraege\24-0051_U\24-0051.PAR", EntryOrt = Auftragsart.Auftraege, EntryType = TypeOfFile.GEOgrafFile };

        ClsMenuEntry clsMenuEntry7 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\Auftraege", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\Auftraege\24-0051_U\24-0051_U.KIT", EntryOrt = Auftragsart.Auftraege, EntryType = TypeOfFile.KIVIDfile };

        ClsMenuEntry clsMenuEntry8 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\Auftraege", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\Auftraege\24-0051_U\24-0051 - Kopie.KIT", EntryOrt = Auftragsart.Auftraege, EntryType = TypeOfFile.KIVIDfile };

        ClsMenuEntry clsMenuEntry9 = new() { EntryBasePath = "C:\AAOfficeTools_Testfiles\Auftraege", EntryFoundSource = EntryFoundSource.GefundenUeberNormaleSuche, EntryFullPath = "C:\AAOfficeTools_Testfiles\Auftraege\24-0051_U", EntryOrt = Auftragsart.Auftraege, EntryType = TypeOfFile.Folder };

        Hilfsliste.Add(clsMenuEntry1);
        Hilfsliste.Add(clsMenuEntry2);
        Hilfsliste.Add(clsMenuEntry3);
        Hilfsliste.Add(clsMenuEntry4);
        Hilfsliste.Add(clsMenuEntry5);
        Hilfsliste.Add(clsMenuEntry6);
        Hilfsliste.Add(clsMenuEntry7);
        Hilfsliste.Add(clsMenuEntry8);
        Hilfsliste.Add(clsMenuEntry9);

        // Remove this line: OcListOfFolders = [];

        FnkFillSelectionWindow();
    }

    private void FnkFillSelectionWindow()   // Ergebnisliste, die oben ist, füllen.
    {
        // Clear the existing collection before adding new items
        OcListOfFolders?.Clear(); 
        
        if (Hilfsliste == null) return; // Add null check for Hilfsliste

        IEnumerable<IGrouping<string, ClsMenuEntry>> dgResult = Hilfsliste.GroupBy(hs => hs.StrSortName);
        string strName, strAnmerkung, strFoundSource, strFullPath, strSearchPath;
        EntryFoundSource foundSource = EntryFoundSource.GefundenUeberNormaleSuche;
        Auftragsart auftragsart = Auftragsart.Undefined;
        ObservableCollection<ClsMenuEntryKividOrGEOgraf> theKivids;
        ObservableCollection<ClsMenuEntryKividOrGEOgraf> theGEOgrafs;
        //ClsMenuEntryFolder theFolder = new();

        foreach (IGrouping<string, ClsMenuEntry> iGroup in dgResult)
        {
            strName = iGroup.Where(s => s.EntryType == TypeOfFile.Folder).Any() ? iGroup.Where(s => s.EntryType == TypeOfFile.Folder).ElementAt(0).StrName : "(nicht gefunden)";
            strAnmerkung = string.Empty;
            strFoundSource = string.Empty;
            theKivids = [];
            theGEOgrafs = [];

            foreach (var kItem in iGroup)
            {
                strAnmerkung = kItem.EntryOrt switch
                {
                    Auftragsart.Angebote => "[ANGEBOT]",
                    Auftragsart.Archiv => "Im Archiv",
                    Auftragsart.VB_Frankfurt => "Vermessungsbüro Frankfurt",
                    Auftragsart.AbzeichnungDerFlurkarte => "Abz. der Flurkarte",
                    Auftragsart.AngeboteArchiv => "[ANGEBOT Archiv]",
                    Auftragsart.VB_Frankfurt_Angebot => "[ANGEBOT VB Frankfurt]",
                    _ => string.Empty,
                };

                strFoundSource = kItem.EntryFoundSource switch
                {
                    EntryFoundSource.GefundenUeberNormaleSuche => string.Empty,
                    EntryFoundSource.GefundenUeberBRHXTRNummer => "gefunden über B&H-Auftragsnummer",
                    EntryFoundSource.GefundenUeberFFVnummer => "gefunden über FFV-Nummer",
                    _ => string.Empty,
                };
            }

            strFullPath = iGroup.Where(s => s.EntryType == TypeOfFile.Folder).FirstOrDefault()?.EntryFullPath ?? string.Empty;
            strSearchPath = iGroup.Where(s => s.EntryType == TypeOfFile.Folder).FirstOrDefault()?.EntryBasePath ?? string.Empty;
            foundSource = iGroup.Where(s => s.EntryType == TypeOfFile.Folder).FirstOrDefault()?.EntryFoundSource ?? EntryFoundSource.GefundenUeberNormaleSuche;
            auftragsart = iGroup.Where(s => s.EntryType == TypeOfFile.Folder).FirstOrDefault()?.EntryOrt ?? Auftragsart.Undefined;
            //theFolder.FullPath = strFullPath;
            //theFolder.SearchPath = strSearchPath;
            var KividQuery = iGroup.Where(tk => tk.EntryType == TypeOfFile.KIVIDfile).ToHashSet();
            var GEOgrafQuery = iGroup.Where(tg => tg.EntryType == TypeOfFile.GEOgrafFile).ToHashSet();

            foreach (var item in KividQuery)
            {
                theKivids.Add(new ClsMenuEntryKividOrGEOgraf() { FullPath = item.EntryFullPath, SearchPath = item.EntryBasePath, DerOrt = item.EntryOrt, TheGuid = Guid.NewGuid() });
            }

            foreach (var item in GEOgrafQuery)
            {
                theGEOgrafs.Add(new ClsMenuEntryKividOrGEOgraf() { FullPath = item.EntryFullPath, SearchPath = item.EntryBasePath, DerOrt = item.EntryOrt, TheGuid = Guid.NewGuid() });
            }

            // Make sure OcListOfFolders is not null before adding
            OcListOfFolders?.Add(new() { Anmerkung = strAnmerkung, Folder = new ClsMenuEntryFolder() { FullPath = strFullPath, SearchPath = strSearchPath, TheGuid = Guid.NewGuid(), Ort = auftragsart }, FoundSource = foundSource, Name = strName, Ort = auftragsart, Kivids = theKivids, GEOgrafs = theGEOgrafs });
        }
        // Ensure OcListOfFolders is not null before sorting
        if(OcListOfFolders != null) 
        {
          OcListOfFolders!.Sort(); 
        }
    }
}
