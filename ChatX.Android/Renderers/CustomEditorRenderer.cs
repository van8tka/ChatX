using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using ChatX.Controls;
using ChatX.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(ExtendedEditorControl),typeof(CustomEditorRenderer))]
namespace ChatX.Droid.Renderers
{
    public class CustomEditorRenderer:EditorRenderer
    {
        private bool _initial = true;
        private Drawable _originalBackground;

        public CustomEditorRenderer(Context context):base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
        {
            base.OnElementChanged(e);
            if(Control!=null)
            {
                if(_initial)
                {
                    _originalBackground = Control.Background;
                    _initial = false;
                }
                Control.SetMaxLines(5);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if(ExtendedEditorControl.HasRoundedCornerProperty.PropertyName == e.PropertyName)
            {
                if (((ExtendedEditorControl)Element).HasRoundedCorner)
                    ApplyBorder();
                else
                    Control.Background = _originalBackground;
            }
        }

       

        private void ApplyBorder()
        {
            var gradiendDraw= new GradientDrawable();
            gradiendDraw.SetCornerRadius(10);
            gradiendDraw.SetStroke(2, Color.Black.ToAndroid());
            Control.Background = gradiendDraw;
        }
    }
}
