﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DSheetEnfilage
{
    public class CanvasElement : INotifyPropertyChanged
    {
        private int _Left;

        private string _Text;
        private int _Top;

        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                NotifyPropertyChanged();
            }
        }

        public int Left
        {
            get => _Left;
            set
            {
                _Left = value;
                NotifyPropertyChanged();
            }
        }

        public int Top
        {
            get => _Top;
            set
            {
                _Top = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}