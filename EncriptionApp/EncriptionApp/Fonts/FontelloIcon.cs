using EncriptionApp.Fonts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: ExportFont("fontello_6.ttf", Alias = FontelloIcon.Font)]

namespace EncriptionApp.Fonts
{
        class FontelloIcon
        {
            public const string Lock = "\uF512";
            public const string Unlock = "\uF513";
            public const string Folder = "\uF068";
            public const string TrashBin = "\uF1F8";
            public const string Info = "\uE800";
            public const string Font = "FontIcon";
        }
    
}

