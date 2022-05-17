using MemoryGame.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryGame.ViewModels
{
    public class MemoryGamePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;


        public MemoryGamePageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;

        }

        private int _levelSelection = 1;
        public int LevelSelection
        {
            get { return _levelSelection; }
            set { SetProperty(ref _levelSelection, value); }
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.LevelSelection)) 
            { 
                LevelSelection = parameters.GetValue<int>(Constants.LevelSelection);
            }


        }


    }
}
