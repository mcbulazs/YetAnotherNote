﻿#pragma checksum "..\..\..\..\ImportExport.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71AF561D2F71C257F586F2DC7EF885FAC5C76122"
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
    /// ImportExport
    /// </summary>
    public partial class ImportExport : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition EditorTitleBarRow;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle EditorTitleBar;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border EditorBorder;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border EditorCloseButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer ScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ImportExportTextBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\ImportExport.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/YetAnotherNote;component/importexport.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ImportExport.xaml"
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
            this.EditorTitleBarRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 2:
            this.EditorTitleBar = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 22 "..\..\..\..\ImportExport.xaml"
            this.EditorTitleBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DragImportExport);
            
            #line default
            #line hidden
            return;
            case 3:
            this.EditorBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.EditorCloseButton = ((System.Windows.Controls.Border)(target));
            return;
            case 5:
            
            #line 25 "..\..\..\..\ImportExport.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.CloseEditorWindow);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\..\ImportExport.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseEnter += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseEnter);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\..\ImportExport.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.TitleBarButtonMouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 7:
            this.ImportExportTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\ImportExport.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

