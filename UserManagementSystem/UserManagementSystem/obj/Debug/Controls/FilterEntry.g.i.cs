﻿#pragma checksum "..\..\..\Controls\FilterEntry.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A2EFB713E580A8B8F28C645476A78C830C7F605D6AAE7939B5131E36F599E0DC"
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
    /// FilterEntry
    /// </summary>
    public partial class FilterEntry : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Controls\FilterEntry.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PropertyNameComboBox;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Controls\FilterEntry.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComparatorComboBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Controls\FilterEntry.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PropertyValueTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/UserManagementSystem;component/controls/filterentry.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Controls\FilterEntry.xaml"
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
            this.PropertyNameComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\Controls\FilterEntry.xaml"
            this.PropertyNameComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.enableComparator);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ComparatorComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.PropertyValueTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\Controls\FilterEntry.xaml"
            this.PropertyValueTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.PropertyValue_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

