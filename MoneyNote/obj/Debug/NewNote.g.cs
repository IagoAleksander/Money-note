﻿

#pragma checksum "C:\Users\Iago Aleksander\Documents\Visual Studio 2015\Projects\MoneyNote\MoneyNote\NewNote.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "301685F2D891076C99E0FC2BABA628C4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneyNote
{
    partial class NewNote : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 56 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.Controls.DatePicker)(target)).DateChanged += this.Date2_DateChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 68 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.Controls.TimePicker)(target)).TimeChanged += this.Time_TimeChanged;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 127 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.Controls.ToggleSwitch)(target)).Toggled += this.ToggleSwitch_Toggled;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 165 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.ok_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 176 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Cancel_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 119 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.Value_GotFocus;
                 #line default
                 #line hidden
                #line 120 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.Value_LostFocus;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 91 "..\..\NewNote.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).KeyUp += this.Description_KeyUp;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


