﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Festival.App.ViewModels
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected ViewModelBase()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                LoadInDesignMode();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void LoadInDesignMode()
        {
        }
    }
}