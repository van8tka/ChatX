using Android.Views;
using ChatX.Droid.Renderers;
using ChatX.View.Partials;
using Android.Views.InputMethods;
namespace ChatX.Droid
{
    public partial class MainActivity
    {
        private bool _lieAboutCurrentFocus;
        /// <summary>
        /// супер костыль который удерживает клавиатуру приотправке сообщения(нажатии кнопки отправить)
        /// но уберает клавиатуру при нажатии на элемент в списке(на сообщении)
        /// </summary>
        ///CustomEditorRenderer - элемент на котором нужно сохранять фокус
        ///ChatInputBarView.PressedBtnSend флаг ,
        ///если истина то была нажата кнопка и клавиатуру не надо убирать
        ///если ложно - то было нажатие на список и клавиатуру убираем
        ///!!! т/к при вызове base.DispatchTouchEvent(ev) происходит смена потока (наверно подкатом Task.Yeild)
        ///то значения до и после вызова base.DispatchTouchEvent(ev) могут быть различны
        public override bool DispatchTouchEvent(MotionEvent ev)
        {
            var focused = CurrentFocus;
            if (focused != null)
            {
                bool customEntryRendererFocused = focused.Parent is CustomEditorRenderer;
                if (customEntryRendererFocused && ChatInputBarView.PressedBtnSend)
                {
                    _lieAboutCurrentFocus = customEntryRendererFocused;
                    var result = base.DispatchTouchEvent(ev);
                    _lieAboutCurrentFocus = false;
                    if (!ChatInputBarView.PressedBtnSend)
                        HideKeyboard();
                    return result;
                }
            }
            ChatInputBarView.PressedBtnSend = false;
            return base.DispatchTouchEvent(ev);
        }

        public override Android.Views.View CurrentFocus
        {
            get
            {
                if (_lieAboutCurrentFocus)
                {
                    return null;
                }
                return base.CurrentFocus;
            }
        }

        private void HideKeyboard()
        {
            var currentFocus = Window.CurrentFocus;
            if (currentFocus != null)
            {
                var inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
                inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, HideSoftInputFlags.None);
            }
        }
    }
}
