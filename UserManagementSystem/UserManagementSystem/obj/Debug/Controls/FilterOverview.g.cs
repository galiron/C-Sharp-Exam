﻿#pragma checksum "..\..\..\Controls\FilterOverview.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "33485EE74A27E709DD4313DC77537B269617A92651BBEC6620411A71E1FFDAE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
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
using UserManagementSystem.Controls;


namespace UserManagementSystem.Controls {
    
    
    /// <summary>
    /// FilterOverview
    /// </summary>
    public partial class FilterOverview : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addFilter;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TypeSelection;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Controls\FilterOverview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel filtersPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/UserManagementSystem;component/controls/filteroverview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\FilterOverview.xaml"
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
            this.addFilter = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Controls\FilterOverview.xaml"
            this.addFilter.Click += new System.Windows.RoutedEventHandler(this.AddFilter_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Controls\FilterOverview.xaml"
            this.saveButton.Click += new System.Windows.RoutedEventHandler(this.saveButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Controls\FilterOverview.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TypeSelection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 29 "..\..\..\Controls\FilterOverview.xaml"
            this.TypeSelection.DropDownClosed += new System.EventHandler(this.TypeSelection_DropDownClosed);
            
            #line default
            #line hidden
            return;
            case 5:
            this.label = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.filtersPanel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

