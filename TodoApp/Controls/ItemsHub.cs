using System.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TodoApp.Controls
{
    /// <summary>
    /// A Hub Control with bindable ItemTemplate and ItemsSource.
    /// </summary>
    public class ItemsHub : Hub
    {
        #region ItemTemplate Dependency Property

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemTemplateChanged));

        private static void ItemTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsHub hub = d as ItemsHub;
            if (hub != null)
            {
                DataTemplate template = e.NewValue as DataTemplate;
                if (template != null)
                {
                    // Apply template
                    foreach (var section in hub.Sections)
                    {
                        section.ContentTemplate = template;
                    }
                }
            }
        }

        #endregion

        #region ItemHeaderTemplate Dependency Property

        public DataTemplate ItemHeaderTemplate
        {
            get { return (DataTemplate)GetValue(ItemHeaderTemplateProperty); }
            set { SetValue(ItemHeaderTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemHeaderTemplateProperty =
            DependencyProperty.Register("ItemHeaderTemplate", typeof(DataTemplate), typeof(ItemsHub), new PropertyMetadata(null, ItemHeaderTemplateChanged));

        private static void ItemHeaderTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsHub hub = d as ItemsHub;
            if (hub != null)
            {
                DataTemplate template = e.NewValue as DataTemplate;
                if (template != null)
                {
                    // Apply template
                    foreach (var section in hub.Sections)
                    {
                        section.HeaderTemplate = template;
                    }
                }
            }
        }

        #endregion

        #region ItemsSource Dependency Property

        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ItemsHub), new PropertyMetadata(null, ItemsSourceChanged));

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ItemsHub hub = d as ItemsHub;
            if (hub != null)
            {
                IList items = e.NewValue as IList;
                if (items != null)
                {
                    hub.Sections.Clear();
                    foreach (var item in items)
                    {
                        HubSection section = new HubSection();
                        section.DataContext = item;
                        DataTemplate template = hub.ItemTemplate;
                        section.ContentTemplate = template;
                        DataTemplate headerTemplate = hub.ItemHeaderTemplate;
                        section.HeaderTemplate = headerTemplate;
                        hub.Sections.Add(section);
                    }
                }
            }
        }

        #endregion
    }
}
