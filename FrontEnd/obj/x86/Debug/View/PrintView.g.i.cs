﻿#pragma checksum "..\..\..\..\View\PrintView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "68A995CDF4A142B2AFFA3F6DA51FA52E398A39737AABC2E07AB3D850E366C350"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DSheetEnfilage;
using FrontEnd.Converter;
using FrontEnd.CustomClass;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace FrontEnd.View {
    
    
    /// <summary>
    /// PrintView
    /// </summary>
    public partial class PrintView : MvvmCross.Platforms.Wpf.Views.MvxWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 114 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPrinterSelection;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox NumPageActivator;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.IntegerUpDown NumPage;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Xpanel;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Ypanel;
        
        #line default
        #line hidden
        
        
        #line 176 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DocumentViewer DocViewer;
        
        #line default
        #line hidden
        
        
        #line 1805 "..\..\..\..\View\PrintView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DSheetEnfilage.SpaceFreeGrid SpaceX;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/rrgem;component/view/printview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PrintView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\..\..\View\PrintView.xaml"
            ((FrontEnd.View.PrintView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.PrintView_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 93 "..\..\..\..\View\PrintView.xaml"
            ((System.Windows.Controls.Grid)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Grid_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cmbPrinterSelection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 114 "..\..\..\..\View\PrintView.xaml"
            this.cmbPrinterSelection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CmbPrinterSelection_OnSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.NumPageActivator = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 5:
            this.NumPage = ((Xceed.Wpf.Toolkit.IntegerUpDown)(target));
            return;
            case 6:
            this.Xpanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.Ypanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.DocViewer = ((System.Windows.Controls.DocumentViewer)(target));
            
            #line 174 "..\..\..\..\View\PrintView.xaml"
            this.DocViewer.Loaded += new System.Windows.RoutedEventHandler(this.PreviewD_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 178 "..\..\..\..\View\PrintView.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Print_Executed);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 180 "..\..\..\..\View\PrintView.xaml"
            ((System.Windows.Documents.FixedDocument)(target)).Loaded += new System.Windows.RoutedEventHandler(this.FixedDocument_Loaded);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SpaceX = ((DSheetEnfilage.SpaceFreeGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

