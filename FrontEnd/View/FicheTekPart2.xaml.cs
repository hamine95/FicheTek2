using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using BackEnd2.Model;
using DSheetEnfilage;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for FicheTekPart2.xaml
    /// </summary>
    public partial class FicheTekPart2 : UserControl, INotifyPropertyChanged

    {



    private MvxObservableCollection<Composition> _SchCompList;

    public MvxObservableCollection<Composition> SchCompList
    {
        get => _SchCompList;
        set
        {
            _SchCompList = value;
            NotifyPropertyChanged();
            

        }
    }

    public FicheTekPart2()
    {
        InitializeComponent();
        // SchCompList = new MvxObservableCollection<Composition>();
        // Composition comp1 = new Composition();
        // comp1.GetComposant = new Composant();
        // comp1.GetComposant.Name = "Chaine";
        // SchCompList.Add(comp1);
        // EnfilageSch.ListComposant = SchCompList;

    }
    public event PropertyChangedEventHandler PropertyChanged;

    
    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }

    
    
   
    }
}