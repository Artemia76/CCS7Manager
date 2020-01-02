// Decompiled with JetBrains decompiler
// Type: HD1CManager.Properties.Resources
// Assembly: HD1CManager, Version=1.2.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8C1DA119-4A1D-4204-9594-C375FB624612
// Assembly location: D:\Program Files (x86)\HK3A\CSV File Generator for DMR Contacts - By HK3A\HD1CManager.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HD1CManager.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (HD1CManager.Properties.Resources.resourceMan == null)
          HD1CManager.Properties.Resources.resourceMan = new ResourceManager("HD1CManager.Properties.Resources", typeof (HD1CManager.Properties.Resources).Assembly);
        return HD1CManager.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return HD1CManager.Properties.Resources.resourceCulture;
      }
      set
      {
        HD1CManager.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap btn_donate_paypal
    {
      get
      {
        return (Bitmap) HD1CManager.Properties.Resources.ResourceManager.GetObject(nameof (btn_donate_paypal), HD1CManager.Properties.Resources.resourceCulture);
      }
    }
  }
}
