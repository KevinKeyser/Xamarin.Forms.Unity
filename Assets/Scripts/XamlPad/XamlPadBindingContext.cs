using UnityEngine;

using System.Windows.Input;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using JetBrains.Annotations;

using Xamarin.Forms;

namespace XamlPad
{
    public class XamlPadBindingContext : INotifyPropertyChanged
    {
        public XamlPadBindingContext()
        {
            InitializePropertyValues();
        }

        /// <summary>
        /// プロパティ値を初期化する。
        /// BindingContext に設定後に呼び出す。
        /// </summary>
        public void InitializePropertyValues()
        {
            xamlSource =
                "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n<Grid\n  xmlns=\"http://xamarin.com/schemas/2014/forms\"\n  xmlns:x=\"http://schemas.microsoft.com/winfx/2009/xaml\">\n</Grid>";

            fontSizeSelectedIndex = 3;
            fontSize = Int32.Parse(FontSizeList[fontSizeSelectedIndex]);
            autoParse = false;
            CompileCommand = new Command(() =>
            {
                CompileResult = autoParse ? "Success!" : "Failed!";
            });
        }

        public ICommand CompileCommand { get; private set; }

        private bool autoParse;

        public bool AutoParse
        {
            get => autoParse;
            set
            {
                if (Equals(autoParse, value))
                {
                    return;
                }

                autoParse = value;
                OnPropertyChanged();
            }
        }

        private int fontSizeSelectedIndex;

        public int FontSizeSelectedIndex
        {
            get => fontSizeSelectedIndex;
            set
            {
                if (Equals(fontSizeSelectedIndex, value))
                {
                    return;
                }

                fontSizeSelectedIndex = value;

                FontSize = Int32.Parse(FontSizeList[fontSizeSelectedIndex]);
                OnPropertyChanged();
            }
        }

        private double fontSize;

        public double FontSize
        {
            get => fontSize;
            set
            {
                if (Equals(fontSize, value))
                {
                    return;
                }

                fontSize = value;
                OnPropertyChanged();
            }
        }

        public string[] FontSizeList { get; } = new string[] { "12", "14", "16", "18", "20", "22" };

        private string xamlSource;

        public string XamlSource
        {
            get => xamlSource;
            set
            {
                if (Equals(xamlSource, value))
                {
                    return;
                }

                xamlSource = value;
                OnPropertyChanged();
            }
        }

        private string compileResult;

        public string CompileResult
        {
            get => compileResult;
            set
            {
                if (Equals(compileResult, value))
                {
                    return;
                }

                compileResult = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}