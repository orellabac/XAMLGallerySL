//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
//using AppUIBasics.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Data;
using System.IO;
using System.Collections.ObjectModel;

namespace AppUIBasics
{

    public class Substitutions : ObservableCollection<ControlExampleSubstitution> { }

    /// <summary>
    /// Describes a textual substitution in sample content.
    /// If enabled (default), then $(Key) is replaced with the stringified value.
    /// If disabled, then $(Key) is replaced with the empty string.
    /// </summary>
    public sealed class ControlExampleSubstitution : DependencyObject
    {
        public ControlExampleSubstitution() 
        {
        }

        public static readonly DependencyProperty IsEnabledPropery = DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ControlExampleSubstitution), new PropertyMetadata(false));
        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledPropery); }
            set { SetValue(IsEnabledPropery, value); }
        }


        public string Key { get; set; }

        private object _value = null;
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
            }
        }
       

        public string ValueAsString()
        {
            if (!IsEnabled)
            {
                return string.Empty;
            }

            object value = Value;

            // For solid color brushes, use the underlying color.
            if (value is SolidColorBrush)
            {
                value = ((SolidColorBrush)value).Color;
            }

            if (value == null)
            {
                return string.Empty;
            }
            
            return value.ToString();
        }
    }

    //[ContentProperty(Name = "Example")]
    public sealed partial class ControlExample : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(ControlExample), new PropertyMetadata("Header"));
        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty ExampleProperty = DependencyProperty.Register("Example", typeof(object), typeof(ControlExample), new PropertyMetadata(null));
        public object Example
        {
            get { return GetValue(ExampleProperty); }
            set { SetValue(ExampleProperty, value); }
        }

        public static readonly DependencyProperty OptionsProperty = DependencyProperty.Register("Options", typeof(object), typeof(ControlExample), new PropertyMetadata(null));
        public object Options
        {
            get { return GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        public static readonly DependencyProperty XamlProperty = DependencyProperty.Register("Xaml", typeof(string), typeof(ControlExample), new PropertyMetadata(null));
        public string Xaml
        {
            get { return (string)GetValue(XamlProperty); }
            set { SetValue(XamlProperty, value); }
        }

        public static readonly DependencyProperty XamlSourceProperty = DependencyProperty.Register("XamlSource", typeof(object), typeof(ControlExample), new PropertyMetadata(null));
        public Uri XamlSource
        {
            get { return (Uri)GetValue(XamlSourceProperty); }
            set {
                SetValue(XamlSourceProperty, value);
                Uri dataUri = new Uri("/XamlControlsGallerySL;component/ControlPagesSampleCode/" + value, UriKind.Relative);
                try {
                    using (Stream s = Application.GetResourceStream(dataUri).Stream)
                    {
                        var reader = new StreamReader(s);
                        Xaml = reader.ReadToEnd();
                    }
                }
                catch
                {

                }
            }
        }

        public static readonly DependencyProperty CSharpProperty = DependencyProperty.Register("Xaml", typeof(string), typeof(ControlExample), new PropertyMetadata(null));
        public string CSharp
        {
            get { return (string)GetValue(CSharpProperty); }
            set { SetValue(CSharpProperty, value); }
        }

        public static readonly DependencyProperty CSharpSourceProperty = DependencyProperty.Register("CSharpSource", typeof(object), typeof(ControlExample), new PropertyMetadata(null));
        public Uri CSharpSource
        {
            get { return (Uri)GetValue(CSharpSourceProperty); }
            set {
                SetValue(CSharpSourceProperty, value);
                Uri dataUri = new Uri("/XamlControlsGallerySL;component/" + value, UriKind.Relative);
                try
                {
                    using (Stream s = Application.GetResourceStream(dataUri).Stream)
                    {
                        var reader = new StreamReader(s);
                        Xaml = reader.ReadToEnd();
                    }
                }
                catch
                {

                }

            }
        }

        public static readonly DependencyProperty SubstitutionsProperty = DependencyProperty.Register("Substitutions", typeof(Substitutions), typeof(ControlExample), new PropertyMetadata(null));
        public Substitutions Substitutions
        {
            get { return (Substitutions)GetValue(SubstitutionsProperty); }
            set { SetValue(SubstitutionsProperty, value); }
        }

        public static readonly DependencyProperty ExampleHeightProperty = DependencyProperty.Register("ExampleHeight", typeof(GridLength), typeof(ControlExample), new PropertyMetadata(new GridLength(1, GridUnitType.Star)));
        public GridLength ExampleHeight
        {
            get { return (GridLength)GetValue(ExampleHeightProperty); }
            set { SetValue(ExampleHeightProperty, value); }
        }

        public static readonly DependencyProperty WebViewHeightProperty = DependencyProperty.Register("WebViewHeight", typeof(int), typeof(ControlExample), new PropertyMetadata(400));
        public int WebViewHeight
        {
            get { return (int)GetValue(WebViewHeightProperty); }
            set { SetValue(WebViewHeightProperty, value); }
        }

        public static readonly DependencyProperty WebViewWidthProperty = DependencyProperty.Register("WebViewWidth", typeof(int), typeof(ControlExample), new PropertyMetadata(800));
        public int WebViewWidth
        {
            get { return (int)GetValue(WebViewWidthProperty); }
            set { SetValue(WebViewWidthProperty, value); }
        }

        public new static readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ControlExample), new PropertyMetadata(HorizontalAlignment.Left));
        public new HorizontalAlignment HorizontalContentAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalContentAlignmentProperty); }
            set { SetValue(HorizontalContentAlignmentProperty, value); }
        }

        public static readonly DependencyProperty MinimumUniversalAPIContractProperty = DependencyProperty.Register("MinimumUniversalAPIContract", typeof(int), typeof(ControlExample), new PropertyMetadata(null));
        public int MinimumUniversalAPIContract
        {
            get { return (int)GetValue(MinimumUniversalAPIContractProperty); }
            set { SetValue(MinimumUniversalAPIContractProperty, value); }
        }

        public ControlExample()
        {
            this.InitializeComponent();
            
            Substitutions = new Substitutions();
            //this.txtHeader.SetBinding(TextBlock.TextProperty, new Binding() { Source = this, Path=new PropertyPath("HeaderText"), Mode=BindingMode.TwoWay});
            //this.ControlPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding() { Source = this, Path = new PropertyPath("Example"), Mode=BindingMode.OneWay});
            //this.OptionsPresenter.SetBinding(ContentPresenter.ContentProperty, new Binding() { Source = this, Path = new PropertyPath("Options"), Mode = BindingMode.OneWay });

        }

        private void rootGrid_Loaded(object sender, RoutedEventArgs e)
        {
           
                foreach (ControlExampleSubstitution substitution in Substitutions)
                {
                    //substitution.IsEnabledChanged += Substitution_IsEnabledChanged;
                }
        }

        private void Substitution_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var sampleString = Xaml;

            // Trim out stray blank lines at start and end.
            sampleString = sampleString.TrimStart('\n').TrimEnd();

            // Also trim out spaces at the end of each line
            sampleString = string.Join("\n", sampleString.Split('\n').Select(s => s.TrimEnd()));

            // Perform any applicable substitutions.
            sampleString = SubstitutionPattern.Replace(sampleString, match =>
            {
                
                foreach (var substitution in Substitutions)
                {
                    if (substitution.Key == match.Groups[1].Value)
                    {
                        return substitution.ValueAsString();
                    }
                }
                throw new KeyNotFoundException(match.Groups[1].Value);
            });
            this.XamlPresenter.Text = sampleString;

        }

        private Uri GetDerivedSource(Uri rawSource)
        {
            Uri derivedSource = null;

            // Get the full path of the source string
            string concatString = "";
            ////for (int i = 2; i < rawSource.Segments.Length; i++)
            ////{
            ////    concatString += rawSource.Segments[i];
            ////}
            derivedSource = new Uri(new Uri("ms-appx:///ControlPagesSampleCode/"), concatString);

            return derivedSource;
        }

        private enum SyntaxHighlightLanguage { Xml, CSharp };

        private void XamlPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            GenerateSyntaxHighlightedContent(XamlPresenter, Xaml, XamlSource, "XML");
            var hasContent = (XamlPresenter.Text ?? "").Trim().Length > 0;
            this.XamlPresenter.Visibility = hasContent ? Visibility.Visible : Visibility.Collapsed;

        }

        private void CSharpPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            var hasContent = (CSharp ?? "").Trim().Length > 0;
            this.CSharpPresenter.Visibility = hasContent ? Visibility.Visible : Visibility.Collapsed;
        }

        private void GenerateAllSyntaxHighlightedContent()
        {
           GenerateSyntaxHighlightedContent(XamlPresenter, Xaml as string, XamlSource, "Xml");
           GenerateSyntaxHighlightedContent(CSharpPresenter, CSharp, CSharpSource, "CSharp");
        }

        private void GenerateSyntaxHighlightedContent(TextBlock presenter, string sampleString, Uri sampleUri, string highlightLanguage)
        {
            if (!string.IsNullOrEmpty(sampleString))
            {
                FormatAndRenderSampleFromString(sampleString, presenter, highlightLanguage);
            }
            else
            {
                FormatAndRenderSampleFromFile(sampleUri, presenter, highlightLanguage);
            }
        }

        private void FormatAndRenderSampleFromFile(Uri source, TextBlock presenter, string highlightLanguage)
        {
            if (source != null && source.AbsolutePath.EndsWith("txt"))
            {
                Uri derivedSource = GetDerivedSource(source);
                Uri dataUri = new Uri("/XamlControlsGallerySL;component/DataModel/ControlInfoData.json", UriKind.Relative);
                string sampleString;
                using (Stream s = Application.GetResourceStream(derivedSource).Stream)
                {
                    var reader = new StreamReader(s);
                    sampleString = reader.ReadToEnd();
                }

                FormatAndRenderSampleFromString(sampleString, presenter, highlightLanguage);
            }
            else
            {
                presenter.Visibility = Visibility.Collapsed;
            }
        }

        private static Regex SubstitutionPattern = new Regex(@"\$\(([^\)]+)\)");
        private void FormatAndRenderSampleFromString(string sampleString, TextBlock presenter, string highlightLanguage)
        {
            // Trim out stray blank lines at start and end.
            sampleString = sampleString.TrimStart('\n').TrimEnd();

            // Also trim out spaces at the end of each line
            sampleString = string.Join("\n", sampleString.Split('\n').Select(s => s.TrimEnd()));

            // Perform any applicable substitutions.
            sampleString = SubstitutionPattern.Replace(sampleString, match =>
            {
                
                foreach (var substitution in Substitutions)
                {
                    if (substitution.Key == match.Groups[1].Value)
                    {
                        return substitution.ValueAsString();
                    }
                }
                throw new KeyNotFoundException(match.Groups[1].Value);
            });
            presenter.Text = sampleString;
            //var formatter = new HtmlFormatter();
            //var html = formatter.GetHtmlString(sampleString, highlightLanguage);
            //var sampleCodeRTB = new RichTextBlock { FontFamily = new FontFamily("Consolas") };

            //var formatter = GenerateRichTextFormatter();
            //formatter.FormatRichTextBlock(sampleString, highlightLanguage, sampleCodeRTB);
            //presenter.Content = sampleCodeRTB;
        }




        private void OnValueChanged(ControlExampleSubstitution sender, object e)
        {
            GenerateAllSyntaxHighlightedContent();
        }
    }
}
