﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HonooLanguageLocalisationConverter.ViewModels
{
    public sealed partial class SectionEntry : ObservableObject
    {
        #region Members

        [ObservableProperty]
        public string _name;

        private readonly MainWindowViewModel _mainWindowViewModel;
        public ObservableCollection<TranslationEntry> TranslationEntries { get; } = [];

        #endregion Members

        public SectionEntry(string name, MainWindowViewModel mainWindowViewModel)
        {
            this.Name = name;
            _mainWindowViewModel = mainWindowViewModel;
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _mainWindowViewModel.Modified = true;
        }
    }
}