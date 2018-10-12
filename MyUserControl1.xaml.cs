using newSoundBoard.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace newSoundBoard
{
    public sealed partial class MyUserControl1 : UserControl
    {
        public Sound this_sound { get { return this.DataContext as Sound; } }
        public MyUserControl1()
        {
            this.InitializeComponent();
            this.DataContextChanged += (s, e) =>Bindings.Update();
        }
    }
}
