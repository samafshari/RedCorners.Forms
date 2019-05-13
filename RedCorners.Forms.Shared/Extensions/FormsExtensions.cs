using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RedCorners.Forms
{
    public static class FormsExtensions
    {
        public static ContentPage GetPage(this Element view)
        {
            var el = view;
            while (true)
            {
                if (el == null || el.Parent == null) return null;
                if (el.Parent is ContentPage page) return page;
                el = el.Parent;
            }
        }

        public static IEnumerable<View> GetAllChildren(this VisualElement source)
        {
            if (source != null)
            {
                if (source is ContentView contentView)
                {
                    yield return contentView.Content;
                    if (contentView.Content != null)
                    {
                        foreach (View view in contentView.Content.GetAllChildren())
                        {
                            yield return view;
                        }
                    }

                }
                else
                {
                    if (source is Layout<View> viewLayout)
                    {
                        foreach (View child in viewLayout.Children)
                        {
                            yield return child;
                            foreach (View view in child.GetAllChildren())
                                yield return view;
                        }
                    }
                    else
                    {
                        if (source is ContentPage contentPage)
                        {
                            yield return contentPage.Content;
                            foreach (View view in contentPage.Content.GetAllChildren())
                                yield return view;
                        }
                    }
                }
            }
        }
    }
}
