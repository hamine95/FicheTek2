using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DataSheetDesign
{
    public class SharedSizeControl : ContentControl
    {
        private static readonly SharedSizeScope _GlobalScope = new SharedSizeScope();

        private static readonly Dictionary<object, SharedSizeScope> _SharedSizeScopes =
            new Dictionary<object, SharedSizeScope>();

        /// <summary>
        ///     Backing sore fore the IsSharedSizeScope attached property.
        /// </summary>
        public static readonly DependencyProperty IsSharedSizeScopeProperty =
            DependencyProperty.RegisterAttached("IsSharedSizeScope", typeof(bool), typeof(SharedSizeControl),
                new UIPropertyMetadata(false));

        /// <summary>
        ///     Backing store for the ShareWidthGroup dependency property.
        /// </summary>
        /// <remarks>
        ///     Using a DependencyProperty as the backing store for SharedWidthGroup.  This enables animation, styling, binding,
        ///     etc...
        /// </remarks>
        public static readonly DependencyProperty SharedWidthGroupProperty =
            DependencyProperty.Register("SharedWidthGroup", typeof(string), typeof(SharedSizeControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    SharedWidthGroup_Changed));

        /// <summary>
        ///     Backing store for the SharedHeightGroup dependency property.
        /// </summary>
        /// <remarks>
        ///     Using a DependencyProperty as the backing store for SharedHeightGroup.  This enables animation, styling, binding,
        ///     etc...
        /// </remarks>
        public static readonly DependencyProperty SharedHeightGroupProperty =
            DependencyProperty.Register("SharedHeightGroup", typeof(string), typeof(SharedSizeControl),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure,
                    SharedHeightGroup_Changed));

        /// <summary>
        ///     Gets or sets the name of the share width group
        /// </summary>
        /// <remarks>
        ///     The width of this control is sahred with the width or height of all SharedSizeControls with the same group name
        ///     If the name is empty or null the width is not shared.
        /// </remarks>
        public string SharedWidthGroup
        {
            get => (string)GetValue(SharedWidthGroupProperty);
            set => SetValue(SharedWidthGroupProperty, value);
        }


        /// <summary>
        ///     Gets or sets the name of the share height group
        /// </summary>
        /// <remarks>
        ///     The height of this control is sahred with the width or height of all SharedSizeControls with the same group name
        ///     If the name is empty or null the height is not shared.
        /// </remarks>
        public string SharedHeightGroup
        {
            get => (string)GetValue(SharedHeightGroupProperty);
            set => SetValue(SharedHeightGroupProperty, value);
        }

        /// <summary>
        ///     Setter for the ISSharedSizeScope attached property
        /// </summary>
        /// <param name="element">Element to set the property</param>
        /// <param name="value">New Value</param>
        public static void SetIsSharedSizeScope(UIElement element, bool value)
        {
            element.SetValue(IsSharedSizeScopeProperty, value);
        }

        /// <summary>
        ///     Getter for the ISSharedSizeScope attached property
        /// </summary>
        /// <param name="element">Element to get the value</param>
        /// <returns>Returns the value of the IsSharedSizeScope attached property</returns>
        public static bool GetIsSharedSizeScope(UIElement element)
        {
            return (bool)element.GetValue(IsSharedSizeScopeProperty);
        }

        private static void SharedWidthGroup_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SharedSizeControl).SharedWidthGroup_Changed(e);
        }

        private void SharedWidthGroup_Changed(DependencyPropertyChangedEventArgs e)
        {
            var oldGroup = (string)e.OldValue;
            var newGroup = (string)e.NewValue;

            var scope = GetScope();

            if (!string.IsNullOrEmpty(oldGroup)) scope.RemoveFromGroup(this, oldGroup, true);

            if (!string.IsNullOrEmpty(newGroup)) scope.AddToGroup(this, newGroup, true);
        }

        private static void SharedHeightGroup_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SharedSizeControl).SharedHeightGroup_Changed(e);
        }

        private void SharedHeightGroup_Changed(DependencyPropertyChangedEventArgs e)
        {
            var oldGroup = (string)e.OldValue;
            var newGroup = (string)e.NewValue;

            var scope = GetScope();

            if (!string.IsNullOrEmpty(oldGroup)) scope.RemoveFromGroup(this, oldGroup, false);

            if (!string.IsNullOrEmpty(newGroup)) scope.AddToGroup(this, newGroup, false);
        }

        /// <summary>
        ///     Gets the SharedSizeScope for this control
        /// </summary>
        /// <returns>Returns the SharedSizeScope for this control</returns>
        private SharedSizeScope GetScope()
        {
            // go up the logical tree to find the shared size scope
            DependencyObject element = this;
            var isScope = (bool)element.GetValue(IsSharedSizeScopeProperty);
            while (!isScope && element != null)
            {
                element = LogicalTreeHelper.GetParent(element);
                if (element != null) isScope = (bool)element.GetValue(IsSharedSizeScopeProperty);
            }

            if (isScope && element != null)
            {
                // create a new scope if it is not existing yet
                SharedSizeScope scope;
                if (!_SharedSizeScopes.TryGetValue(element, out scope))
                {
                    scope = new SharedSizeScope();
                    _SharedSizeScopes.Add(element, scope);
                }

                return scope;
            }

            // if no scope is defined use the global scope
            return _GlobalScope;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            // do normal measure
            var size = base.MeasureOverride(constraint);

            // early exit if no size is shared
            if (string.IsNullOrEmpty(SharedWidthGroup) && string.IsNullOrEmpty(SharedHeightGroup)) return size;

            // get the scope
            var scope = GetScope();

            // now get the maximum width/height of all control contents in the same group
            if (!string.IsNullOrEmpty(SharedWidthGroup)) size.Width = scope.GetMaxSizeValue(SharedWidthGroup);
            if (!string.IsNullOrEmpty(SharedHeightGroup)) size.Height = scope.GetMaxSizeValue(SharedHeightGroup);

            return size;
        }
    }

    internal class SharedSizeScope
    {
        /// <summary>
        ///     constructor
        /// </summary>
        internal SharedSizeScope()
        {
            Groups = new Dictionary<string, SharedSizeGroup>();
        }

        /// <summary>
        ///     Gets the a dictionary with all shared size groups in the scope
        /// </summary>
        internal IDictionary<string, SharedSizeGroup> Groups { get; }

        /// <summary>
        ///     Adds a control to a grpup
        /// </summary>
        /// <param name="control">Control to add</param>
        /// <param name="groupName">Name of the group</param>
        /// <param name="shareWidth">true if the width is shared, else false</param>
        internal void AddToGroup(SharedSizeControl control, string groupName, bool shareWidth)
        {
            SharedSizeGroup group;
            if (!Groups.TryGetValue(groupName, out group))
            {
                group = new SharedSizeGroup();
                Groups.Add(groupName, group);
            }

            group.AddElement(control, shareWidth);
        }

        /// <summary>
        ///     Removes a control from a group
        /// </summary>
        /// <param name="control">Control to remove</param>
        /// <param name="groupName">Name of the group</param>
        /// <param name="shareWidth">true if the width is shared, else false</param>
        internal void RemoveFromGroup(SharedSizeControl control, string groupName, bool shareWidth)
        {
            SharedSizeGroup group;
            if (Groups.TryGetValue(groupName, out group))
            {
                group.RemoveElement(control, shareWidth);
                if (group.Elements.Count == 0) Groups.Remove(groupName);
            }
        }

        /// <summary>
        ///     Gets the maximum height/width of all control contents in the group
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        internal double GetMaxSizeValue(string groupName)
        {
            SharedSizeGroup group;
            if (Groups.TryGetValue(groupName, out group)) return group.GetMaxSizeValue();
            return 0.0;
        }
    }

    /// <summary>
    ///     Class representing a shared size group
    /// </summary>
    internal class SharedSizeGroup
    {
        /// <summary>
        ///     constructor
        /// </summary>
        internal SharedSizeGroup()
        {
            Elements = new List<SharedSizeElement>();
        }

        /// <summary>
        ///     Gets the list of all elements (controls) in the group
        /// </summary>
        internal IList<SharedSizeElement> Elements { get; }

        /// <summary>
        ///     Adds an control to the group
        /// </summary>
        /// <param name="control">Control to add</param>
        /// <param name="shareWidth">true if the width is shared, else false</param>
        internal void AddElement(SharedSizeControl control, bool shareWidth)
        {
            Elements.Add(new SharedSizeElement(this, control, shareWidth));
        }

        /// <summary>
        ///     Removes an control from the group
        /// </summary>
        /// <param name="control">Control to remove</param>
        /// <param name="shareWidth">true if the width is shared, else false</param>
        internal void RemoveElement(SharedSizeControl control, bool shareWidth)
        {
            foreach (var e in Elements.ToArray())
                if (ReferenceEquals(e.Control, control) && e.ShareWidth == shareWidth)
                {
                    Elements.Remove(e);
                    return;
                }
        }

        /// <summary>
        ///     Gets the maximum width/height of the control content
        /// </summary>
        /// <returns></returns>
        internal double GetMaxSizeValue()
        {
            var maxSize = 0.0;
            foreach (var element in Elements) maxSize = Math.Max(maxSize, element.GetSize());
            return maxSize;
        }
    }

    /// <summary>
    ///     Class representing a shared size control
    /// </summary>
    internal class SharedSizeElement
    {
        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="group">Group whre this control belongs to</param>
        /// <param name="control">Control</param>
        /// <param name="shareWidth">true if the width is shared, else false</param>
        internal SharedSizeElement(SharedSizeGroup group, SharedSizeControl control, bool shareWidth)
        {
            Group = group;
            Control = control;
            ShareWidth = shareWidth;

            Control.SizeChanged += Control_SizeChanged;
        }

        /// <summary>
        ///     Gets the Group where this control belongs to
        /// </summary>
        internal SharedSizeGroup Group { get; }

        /// <summary>
        ///     Gets the control of which the size ios shared
        /// </summary>
        internal SharedSizeControl Control { get; }

        /// <summary>
        ///     Gets if the width (true) or height (false) is shared
        /// </summary>
        internal bool ShareWidth { get; }

        private void Control_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // when the size of the control changes, then all other controls in the group needs to be measured
            foreach (var element in Group.Elements)
                if (!ReferenceEquals(element.Control, this))
                    element.Control.InvalidateMeasure();
        }

        /// <summary>
        ///     Gets the width/heigth of the control content
        /// </summary>
        /// <returns></returns>
        internal double GetSize()
        {
            if (Control == null || !(Control.Content is UIElement)) return 0.0;
            if (ShareWidth)
                return (Control.Content as UIElement).DesiredSize.Width;
            return (Control.Content as UIElement).DesiredSize.Height;
        }
    }
}