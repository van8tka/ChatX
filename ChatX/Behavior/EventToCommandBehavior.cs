﻿using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace ChatX.Behavior
{
    public class EventToCommandBehavior:BehaviorBase<Element>
    {
        private Delegate _eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior), null);

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        public IValueConverter Converter
        {
            get => (IValueConverter)GetValue(InputConverterProperty);
            set => SetValue(InputConverterProperty, value);
        }

        protected override void OnAttachedTo(Element bindable)
        {
            base.OnAttachedTo(bindable);
            RegisterEvent(EventName);
        }
        protected override void OnDetachingFrom(Element bindable)
        {
            DeregisterEvent(EventName);
            base.OnDetachingFrom(bindable);           
        }
        private void RegisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return;
            var eventInfo = GetEventInfoByName(eventName);
            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
        }

        private EventInfo GetEventInfoByName(string eventName)
        {
            var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(eventName);
            if (eventInfo == null)
                throw new ArgumentException($"EventToCommandBehavior: can't register the '{EventName}' event");
            return eventInfo;
        }

        private void DeregisterEvent(string eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName) || _eventHandler == null)
                return;
            var eventInfo = GetEventInfoByName(eventName);
            eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);
            _eventHandler = null;
        }

        private void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
                return;
            object resolvedParameter;
            if (CommandParameter != null)
                resolvedParameter = CommandParameter;
            else if (Converter != null)
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            else
                resolvedParameter = eventArgs;
            if (Command.CanExecute(resolvedParameter))
                Command.Execute(resolvedParameter);
        }


        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject != null)
            {
                behavior.DeregisterEvent((string)oldValue);
                behavior.RegisterEvent((string)newValue);
            }
        }
    }
}
