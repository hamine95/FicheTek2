﻿#pragma checksum "..\..\..\..\View\PersonnelView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2905FD63B9D280FB127ED4679922357A05AB4FE2B13C1FD112577B63B7E199F7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FrontEnd.Converter;
using MvvmCross.Platforms.Wpf.Views;
using System;
using System.Diagnostics;
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


namespace FrontEnd.View {
    
    
    /// <summary>
    /// PersonnelView
    /// </summary>
    public partial class PersonnelView : MvvmCross.Platforms.Wpf.Views.MvxWpfView, System.Windows.Markup.IComponentConnector {
        
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
            System.Uri resourceLocater = new System.Uri("/rrgem;component/view/personnelview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\PersonnelView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            
            #line 7 "..\..\..\..\View\PersonnelView.xaml"
            ((FrontEnd.View.PersonnelView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MvxWpfView_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 293 "..\..\..\..\View\PersonnelView.xaml"
            ((System.Windows.Controls.ListView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ListView_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 312 "..\..\..\..\View\PersonnelView.xaml"
            ((System.Windows.Controls.ListView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ListView_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 331 "..\..\..\..\View\PersonnelView.xaml"
            ((System.Windows.Controls.ListView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ListView_SizeChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 482 "..\..\..\..\View\PersonnelView.xaml"
            ((System.Windows.Controls.ListView)(target)).SizeChanged += new System.Windows.SizeChangedEventHandler(this.ListView_SizeChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

