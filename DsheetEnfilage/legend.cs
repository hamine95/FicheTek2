using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BackEnd2;
using BackEnd2.Model;

namespace DSheetEnfilage
{
    public class legend:Control
    {
        
        public static readonly DependencyProperty LegendListProperty = DependencyProperty.Register(
            nameof(LegendList), typeof(ObservableCollection<Composition>), typeof(legend), new PropertyMetadata(null));

        public ObservableCollection<Composition> LegendList
        {
            get { return (ObservableCollection<Composition>)GetValue(LegendListProperty); }
            set { SetValue(LegendListProperty, value); }
        }



        public static readonly DependencyProperty SelectedComposantProperty = DependencyProperty.Register(
            nameof(SelectedComposant), typeof(Composition), typeof(legend), new PropertyMetadata(default(Composition),onSetSelectedComposant));

        public Composition SelectedComposant
        {
            get { return (Composition)GetValue(SelectedComposantProperty); }
            set { SetValue(SelectedComposantProperty, value); }
        }

        public static void onSetSelectedComposant(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var mycontrol = (legend)obj;
            mycontrol.SetSelectedComposant((Composition)args.OldValue);
        }


        public static readonly DependencyProperty isPrintProperty = DependencyProperty.Register(
            nameof(isPrint), typeof(bool), typeof(legend), new PropertyMetadata(default(bool)));

        public bool isPrint
        {
            get { return (bool)GetValue(isPrintProperty); }
            set { SetValue(isPrintProperty, value); }
        }
        
        public void SetSelectedComposant(Composition PrevComp)
        {
            if (PrevComp != null)
                PrevComp.Highlight = false;
            if (SelectedComposant != null)
                SelectedComposant.Highlight = true;
            NotifySelectedComposantEvent.Invoke(SelectedComposant);
        }

        public  delegate void NotifySelectedComposantDelegate(Composition SelectedComp);


        public   NotifySelectedComposantDelegate NotifySelectedComposantEvent;
        

        public legend()
        {
            
        }
    }
}