using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    // This is the same as Xamarin.Forms.IElement
    // The reason it exists is because IElement is internal and not accessible from outside.
    public interface IElement2
    {
        Element Parent { get; set; }
    }
}
