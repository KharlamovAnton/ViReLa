﻿#pragma checksum "..\..\..\Student\Student.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1FEB02896745631B1E99E01BB9FE89961637E2F7817FC400AA06C56557F8940C"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using ViReLa_Client.Student;


namespace ViReLa_Client.Student {
    
    
    /// <summary>
    /// Student
    /// </summary>
    public partial class Student : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\Student\Student.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ViReLa_Client.Student.Student windowsStudent;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Student\Student.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView twTree;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Student\Student.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btAdd;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Student\Student.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView twTreeTree;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Student\Student.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser wbVisible;
        
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
            System.Uri resourceLocater = new System.Uri("/ViReLa_Client;component/student/student.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Student\Student.xaml"
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
            this.windowsStudent = ((ViReLa_Client.Student.Student)(target));
            
            #line 8 "..\..\..\Student\Student.xaml"
            this.windowsStudent.Closed += new System.EventHandler(this.windowsStudent_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.twTree = ((System.Windows.Controls.TreeView)(target));
            
            #line 11 "..\..\..\Student\Student.xaml"
            this.twTree.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.twTree_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btAdd = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Student\Student.xaml"
            this.btAdd.Click += new System.Windows.RoutedEventHandler(this.btAdd_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.twTreeTree = ((System.Windows.Controls.TreeView)(target));
            
            #line 13 "..\..\..\Student\Student.xaml"
            this.twTreeTree.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.twTreeTree_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.wbVisible = ((System.Windows.Controls.WebBrowser)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

