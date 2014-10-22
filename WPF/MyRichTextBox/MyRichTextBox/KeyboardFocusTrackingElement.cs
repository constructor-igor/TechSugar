using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace MyRichTextBox
{
    /// <summary>
    /// KeyboardFocusTrackingElement
    /// </summary>
    /// <typeparam name="ELEMENT">Custom input element</typeparam>
    internal class KeyboardFocusTrackingElement<ELEMENT>
        where ELEMENT:IInputElement
    {
        #region Initialization

        public KeyboardFocusTrackingElement(FrameworkElement currentElement)
            :this(currentElement, true)
        {
        }

        public KeyboardFocusTrackingElement(FrameworkElement currentElement, Boolean bindingToRoot)
            :this(currentElement, true, null)
        {
        }

        public KeyboardFocusTrackingElement(
            FrameworkElement currentElement,
            Boolean bindingToRoot,
            IEnumerable<FrameworkElement> hiddenElements)
        {
            if (currentElement == null)
                throw new ArgumentNullException("currentElement");

            this._hiddenElements = hiddenElements;
            this.HasBinded = false;

            Setup(bindingToRoot, currentElement, false);

            this._keyboardFocusChanged +=
                new KeyboardFocusChangedEventHandler(this.RootElementFocusChanged);
            this._currentElement.Unloaded +=
                (this._currentElementUnloadedEventHandler = new RoutedEventHandler(this.CurrentElementUnloaded));
            this._currentElement.LayoutUpdated +=
                (this._currentElementLayoutUpdatedEventHandler = new EventHandler(this.CurrentElementLayoutUpdated));
        }
        
        #endregion // Initialization

        #region Properties and events

        public Boolean HasBinded { get; private set; }

        public ELEMENT FocusedElement { get; private set; }

        public Boolean BindingToRoot
        {
            get
            {
                return this._bindingToRoot;
            }
            set
            {
                if (value != this.BindingToRoot)
                    Setup(value, null, true);
            }
        }

        public FrameworkElement CurrentElement
        {
            get
            {
                return this._currentElement;
            }
            set
            {
                if (value != this._currentElement)
                    Setup(this.BindingToRoot, value, true);
            }
        }

        public IEnumerable<FrameworkElement> HiddenElements
        {
            get
            {
                return this._hiddenElements;
            }
            set
            {
                if (value == this._hiddenElements)
                    return;
                
                Boolean mustRebind = this.HasBinded && !this.BindingToRoot;
                
                if (mustRebind)
                    this.Unbind();
                
                this._hiddenElements = value;
                
                if (mustRebind)
                    this.Bind();
            }
        }

        public event KeyboardFocusChangedEventHandler KeyboardFocusChanged;

        #endregion // Properties and events

        #region Public methods

        public void Bind()
        {
            if (this.HasBinded)
                this.Unbind();

            this.HasBinded = true;
            DependencyObject bindedELement = this.BindedElement;

            if (bindedELement != null)
            {
                Keyboard.AddGotKeyboardFocusHandler(bindedELement,
                    this._keyboardFocusChanged);
                Keyboard.AddLostKeyboardFocusHandler(bindedELement,
                    this._keyboardFocusChanged);

                if (this._hiddenElements != null && !this.BindingToRoot)
                {
                    foreach (var hiddenElement in this._hiddenElements)
                    {
                        Keyboard.AddGotKeyboardFocusHandler(hiddenElement,
                            this._keyboardFocusChanged);
                        Keyboard.AddLostKeyboardFocusHandler(hiddenElement,
                            this._keyboardFocusChanged);
                    }
                }

                this.UpdateFocusElement(Keyboard.FocusedElement, true);
            }
        }

        public void Unbind()
        {
            if (!this.HasBinded)
                return;

            this.HasBinded = false;
            DependencyObject bindedElement = this.BindedElement;

            if (bindedElement != null)
            {
                Keyboard.RemoveGotKeyboardFocusHandler(bindedElement,
                    this._keyboardFocusChanged);
                Keyboard.RemoveLostKeyboardFocusHandler(bindedElement,
                    this._keyboardFocusChanged);

                if (this._hiddenElements != null && !this.BindingToRoot)
                {
                    foreach (var hiddenElement in this._hiddenElements)
                    {
                        Keyboard.RemoveGotKeyboardFocusHandler(hiddenElement,
                            this._keyboardFocusChanged);
                        Keyboard.RemoveLostKeyboardFocusHandler(hiddenElement,
                            this._keyboardFocusChanged);
                    }
                }

                this.UpdateFocusElement(null, true);
            }
        }

        #endregion // Public methods

        #region Event handlers

        private void RootElementFocusChanged(Object sender, KeyboardFocusChangedEventArgs e)
        {
            if (!IsHiddenElement(e.NewFocus))
                UpdateFocusElement(e.NewFocus, false);
        }

        private void CurrentElementLayoutUpdated(Object sender, EventArgs e)
        {
            if (!this.BindingToRoot)
                return;

            DependencyObject currentRootElement = FindRootElement(this._currentElement);
            if (currentRootElement != this._rootElement)
            {
                Boolean mustRebind = this.HasBinded;

                if (mustRebind)
                    this.Unbind();
                
                this._rootElement = currentRootElement;

                if (mustRebind)
                    this.Bind();
            }
        }

        private void CurrentElementUnloaded(Object sender, RoutedEventArgs e)
        {
            this._currentElement.LayoutUpdated -= this._currentElementLayoutUpdatedEventHandler;
            this._currentElement.Unloaded -= this._currentElementUnloadedEventHandler;

            this.Unbind();
        }

        #endregion // Event handlers

        #region Private methods

        private Boolean IsHiddenElement(IInputElement element)
        {
            IEnumerable<FrameworkElement> hiddenElements = this.HiddenElements;

            if (element is FrameworkElement)
            {
                FrameworkElement currentElement = (FrameworkElement)element;

                if (currentElement.Parent == null) // i.e. ListBoxItem
                    return true;

                if (HasAscendantElementOf<ToolBarOverflowPanel>(currentElement))
                    return true;

                if (hiddenElements != null)
                {
                    foreach (FrameworkElement hiddenElement in hiddenElements)
                    {
                        if (IsDescendantElement(hiddenElement, currentElement))
                            return true;
                    }
                }

                return false;
            }

            return element is DependencyObject;
        }

        private Boolean ValidateElement(IInputElement element)
        {
            if (this.BindingToRoot)
                return element is ELEMENT;
            else
                return element == this.BindedElement;
        }

        private void Setup(
            Boolean bindingToRoot,
            FrameworkElement currentElement,
            Boolean mustRebind)
        {
            mustRebind &= this.HasBinded;

            if (mustRebind)
                this.Unbind();

            if (currentElement != null)
                this._currentElement = currentElement;

            if (bindingToRoot)
            {
                this._rootElement = FindRootElement(this._currentElement);
            }
            else
            {
                if (this._currentElement is ELEMENT)
                    this._rootElement = null;
                else
                    throw new ArgumentException("bindingToRoot");
            }

            this._bindingToRoot = bindingToRoot;
            this.FocusedElement = default(ELEMENT);

            if (mustRebind)
                this.Bind();
        }

        private void UpdateFocusElement(IInputElement newFocus, Boolean forcedRaiseEvent)
        {
            Boolean whetherRaiseFocusChangedEvnet;
            IInputElement oldFocus = (IInputElement)this.FocusedElement;

            if (ValidateElement(newFocus))
            {
                whetherRaiseFocusChangedEvnet =
                    !Object.Equals(newFocus, (Object)this.FocusedElement);
                if (whetherRaiseFocusChangedEvnet)
                    this.FocusedElement = (ELEMENT)newFocus;
            }
            else
            {
                whetherRaiseFocusChangedEvnet = oldFocus != null;
                this.FocusedElement = default(ELEMENT);
                newFocus = null;
            }

            if (whetherRaiseFocusChangedEvnet || forcedRaiseEvent)
                OnFocusElementChangedEvent(oldFocus, newFocus);
        }

        private void OnFocusElementChangedEvent(IInputElement oldFocus, IInputElement newFocus)
        {
            KeyboardFocusChangedEventHandler handler = this.KeyboardFocusChanged;
            if (handler != null)
                handler(this, new KeyboardFocusChangedEventArgs(
                    Keyboard.PrimaryDevice, 0, oldFocus, newFocus));
        }

        private static DependencyObject FindRootElement(DependencyObject current)
        {
            DependencyObject lastItem = current;
            while ((current = VisualTreeHelper.GetParent(current)) != null)
            {
                lastItem = current;
            }

            return lastItem;
        }

        private static Boolean IsDescendantElement(DependencyObject parent, DependencyObject current)
        {
            if (parent == current)
                return true;

            for (int childIndex = 0; childIndex < VisualTreeHelper.GetChildrenCount(parent); childIndex++)
            {
                if (IsDescendantElement(VisualTreeHelper.GetChild(parent, childIndex), current))
                    return true;
            }

            return false;
        }

        private static Boolean HasAscendantElementOf<T>(DependencyObject current)
        {
            do
            {
                if (current is T)
                    return true;
            } while ((current = VisualTreeHelper.GetParent(current)) != null);

            return false;
        }

        #endregion // Private methods

        #region Private properties and fields

        private DependencyObject BindedElement
        {
            get
            {
                return this.BindingToRoot
                    ? this._rootElement
                    : this._currentElement;
            }
        }

        private FrameworkElement _currentElement;
        private DependencyObject _rootElement;
        private Boolean _bindingToRoot;
        private IEnumerable<FrameworkElement> _hiddenElements;

        private readonly RoutedEventHandler _currentElementUnloadedEventHandler;
        private readonly EventHandler _currentElementLayoutUpdatedEventHandler;
        private readonly KeyboardFocusChangedEventHandler _keyboardFocusChanged;

        #endregion Private properties and fields
    }
}
