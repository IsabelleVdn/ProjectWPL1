// Updated by XamlIntelliSenseFileGenerator 4/03/2021 12:37:40
#pragma checksum "..\..\..\RequestForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B06671447D491397D0EFB7F910DADD201497C89C"
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
using TestingPlanner;


namespace TestingPlanner
{


    /// <summary>
    /// RequestForm
    /// </summary>
    public partial class RequestForm : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TestingPlanner;V1.0.0.0;component/requestform.xaml", System.UriKind.Relative);

#line 1 "..\..\..\RequestForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.txtRequester = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 2:
                    this.cmbxDivision = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.cmbxJobnature = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 4:
                    this.txtProjectName = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 5:
                    this.txtPartNumbers = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 6:
                    this.cmbxBatteriesinside = ((System.Windows.Controls.ComboBox)(target));
                    return;
                case 7:

#line 44 "..\..\..\RequestForm.xaml"
                    ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);

#line default
#line hidden
                    return;
                case 8:
                    this.txtResult = ((System.Windows.Controls.TextBox)(target));
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

