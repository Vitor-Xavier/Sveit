using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Sveit.ViewModels
{
    class VacancyRegisterViewModel : BaseViewModel
    {
        private int minAge;

        public int MinAge
        {
            get { return minAge; }
            set { minAge = value; OnPropertyChanged(); }
        }

        private int maxAge;

        public int MaxAge
        {
            get { return maxAge; }
            set { maxAge = value; OnPropertyChanged(); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        public VacancyRegisterViewModel()
        {

        }
    }
}
