﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "357F3C58E9CAB4FBC465E6B1C5F3BE58B19C364B38C8A341A56E3D2161D83181"
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
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition Presets;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition PresetInfo;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle TitleBar;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MainWindowCloseButton;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MainWindowMinimizeButton;
        
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
            System.Uri resourceLocater = new System.Uri("/YetAnotherNote;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            this.Presets = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 2:
            this.PresetInfo = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 3:
            this.TitleBar = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 27 "..\..\MainWindow.xaml"
            this.TitleBar.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DragMainWindow);
            
            #line default
            #line hidden
            return;
            case 4:
            this.MainWindowCloseButton = ((System.Windows.Controls.TextBlock)(target));
            
            #line 30 "..\..\MainWindow.xaml"
            this.MainWindowCloseButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CloseMainWindow);
            
            #line default
            #line hidden
            
            #line 30 "..\..\MainWindow.xaml"
            this.MainWindowCloseButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseEnter);
            
            #line default
            #line hidden
            
            #line 30 "..\..\MainWindow.xaml"
            this.MainWindowCloseButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MainWindowMinimizeButton = ((System.Windows.Controls.TextBlock)(target));
            
            #line 36 "..\..\MainWindow.xaml"
            this.MainWindowMinimizeButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.MinimizeMainWindow);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            this.MainWindowMinimizeButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseEnter);
            
            #line default
            #line hidden
            
            #line 36 "..\..\MainWindow.xaml"
            this.MainWindowMinimizeButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

