﻿#pragma checksum "..\..\..\..\Confirm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4D231FD869556E4D8AB2A21FF2252348FC544A5D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using YetAnotherNote;


namespace YetAnotherNote {
    
    
    /// <summary>
    /// Confirm
    /// </summary>
    public partial class Confirm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Confirm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Title;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\Confirm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApplyButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Confirm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NO;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/YetAnotherNote;component/confirm.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Confirm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.ApplyButton = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\..\Confirm.xaml"
            this.ApplyButton.Click += new System.Windows.RoutedEventHandler(this.ApplyButtonClick);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\Confirm.xaml"
            this.ApplyButton.GotFocus += new System.Windows.RoutedEventHandler(this.ApplyButton_GotFocus);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\Confirm.xaml"
            this.ApplyButton.LostFocus += new System.Windows.RoutedEventHandler(this.ApplyButton_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NO = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\..\Confirm.xaml"
            this.NO.Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\Confirm.xaml"
            this.NO.GotFocus += new System.Windows.RoutedEventHandler(this.ApplyButton_GotFocus);
            
            #line default
            #line hidden
            
            #line 18 "..\..\..\..\Confirm.xaml"
            this.NO.LostFocus += new System.Windows.RoutedEventHandler(this.ApplyButton_LostFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
