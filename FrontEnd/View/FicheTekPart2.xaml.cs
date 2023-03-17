using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BackEnd2.Model;
using BackEnd2.ViewModel;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Layout;
using MvvmCross.Binding.BindingContext;
using MvvmCross.ViewModels;

namespace FrontEnd.View
{
    /// <summary>
    ///     Interaction logic for FicheTekPart2.xaml
    /// </summary>
    public partial class FicheTekPart2 : UserControl, INotifyPropertyChanged

    {
        private MvxObservableCollection<Composition> _SchCompList;

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

        
        public MvxObservableCollection<Composition> SchCompList
        {
            get => _SchCompList;
            set
            {
                _SchCompList = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TrameDragger_DragFinished(object sender, System.Windows.Input.MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetTramePosition(df.X,df.Y);
        }


        private void Rep1_OnDragFinished(object sender, MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetRep1(df.X,df.Y);
        }

        private void Rep2_OnDragFinished(object sender, MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetRep2(df.X,df.Y);
        }
        private void Rep3_OnDragFinished(object sender, MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetRep3(df.X,df.Y);
        }
        private void Rep4_OnDragFinished(object sender, MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetRep4(df.X,df.Y);
        }
        private void Rep5_OnDragFinished(object sender, MouseEventArgs e)
        {
            FicheTechniqueViewModel ft = this.DataContext as FicheTechniqueViewModel;
            var df = (MouseDragElementBehavior)sender;
            ft.SetRep5(df.X,df.Y);
        }
    }
}