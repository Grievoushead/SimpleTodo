using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyChecklists.Infra
{
    public class NavService : INavService
    {
        private static Dictionary<string, Type> _Views = new  Dictionary<string, Type>();

        private static Frame _Frame
        {
            get { return (Frame) Window.Current.Content; }
        }

        public void Register(string viewName, Type viewType)
        {
            _Views.Add(viewName, viewType);
        }

        public void GoTo(string viewName, object parameter = null)
        {
            if (_Views.ContainsKey(viewName))
            {
                _Frame.Navigate(_Views[viewName], parameter);
            }
        }

        public void GoBack()
        {
            _Frame.GoBack();
        }
    }

    public interface INavService
    {
        void Register(string viewName, Type viewType);
        void GoTo(string viewName, object parameter = null);
        void GoBack();
    }
}