using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FreshMvvm;

namespace PlaygroundLite.ViewModels
{
    public class BaseViewModel : FreshBasePageModel
    {
        protected bool SetProperty<T>(ref T backingStore, T value,
            Action onChanged = null,
            [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            RaisePropertyChanged(propertyName);

            return true;
        }
    }
}
