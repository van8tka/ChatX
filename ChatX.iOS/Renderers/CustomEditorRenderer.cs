using System;
using System.ComponentModel;
using ChatX.Controls;
using ChatX.iOS.Renderers;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(ExtendedEditorControl),typeof(CustomEditorRenderer))]
namespace ChatX.iOS.Renderers
{
    public class CustomEditorRenderer:EditorRenderer
    {
        private double _previousHeight = -1;
        private int _prevLines = 0;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                var custom = (ExtendedEditorControl)e.NewElement;
                Control.ScrollEnabled = !custom.IsExpandable;
                Control.Layer.CornerRadius = custom.HasRoundedCorner ? 5 : 0;
                Control.InputAccessoryView = new UIKit.UIView(CGRect.Empty);
                Control.ReloadInputViews();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var custom = (ExtendedEditorControl)Element;
            if(e.PropertyName == Editor.TextProperty.PropertyName)
            {
                if (custom.IsExpandable)
                {
                    var numLines = GetNumLines();
                    if (_prevLines > numLines || string.IsNullOrEmpty(Control.Text))
                        custom.HeightRequest = -1;
                    _prevLines = numLines;
                }
            } else if(ExtendedEditorControl.HasRoundedCornerProperty.PropertyName == e.PropertyName)
            {
                Control.Layer.CornerRadius = custom.HasRoundedCorner ? 5 : 0;
            }else if (ExtendedEditorControl.IsExpandableProperty.PropertyName == e.PropertyName)
            {
                Control.ScrollEnabled = !custom.IsExpandable;
            }else if (ExtendedEditorControl.HeightProperty.PropertyName == e.PropertyName)
            {
                if(custom.IsExpandable)
                {
                    var numLines = GetNumLines();
                    if(numLines >= 5)
                    {
                        Control.ScrollEnabled = true;
                        custom.HeightRequest = _previousHeight;
                    }
                    else
                    {
                        Control.ScrollEnabled = false;
                        _previousHeight = custom.Height;
                    }
                }
            }
        }

        private int GetNumLines()
        {
            var size = Control.Text.StringSize(Control.Font, Control.Frame.Size, UILineBreakMode.WordWrap);
           return (int)(size.Height / Control.Font.LineHeight); 
        }
    }
}
