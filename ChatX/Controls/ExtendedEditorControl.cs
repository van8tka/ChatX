using System;
using Xamarin.Forms;

namespace ChatX.Controls
{
    public class ExtendedEditorControl:Editor
    {
        public static BindableProperty HasRoundedCornerProperty = BindableProperty.Create("HasRoundedCorner", typeof(bool), typeof(ExtendedEditorControl),false);
        public static BindableProperty IsExpandableProperty = BindableProperty.Create("IsExpandable", typeof(bool), typeof(ExtendedEditorControl), false);
       
        public bool HasRoundedCorner
        {
            get => (bool)GetValue(HasRoundedCornerProperty);
            set => SetValue(HasRoundedCornerProperty, value);
        }
        public bool IsExpandable
        {
            get => (bool)GetValue(IsExpandableProperty);
            set => SetValue(IsExpandableProperty, value);
        }

        //ctor
        public ExtendedEditorControl()
        {
            TextChanged += OnTextChanged;
        }
        ~ExtendedEditorControl()
        {
            TextChanged -= OnTextChanged;
        }
        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsExpandable)
                InvalidateMeasure();
        }
    }
}
