using MemoryGame.ApiService;
using MemoryGame.Models;
using MemoryGame.Utils;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;

namespace MemoryGame.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IPokemonApiService _pokemonApiService;

        public Command<string> NavToMemoryGamePageCommand { get; }

        public MainPageViewModel(INavigationService navigationService,
            IPageDialogService dialogService,
            IPokemonApiService pokemonApiService)
            : base(navigationService)
        {
            Title = "Main Page";

            _navigationService = navigationService;
            _dialogService = dialogService;
            _pokemonApiService = pokemonApiService;
            NavToMemoryGamePageCommand = new Command<string>(NavToMemoryGamePage);

        }


        private async void NavToMemoryGamePage(string i)
        {
            if (int.TryParse(i, out int selection))
            {
                if (selection < 1)
                {
                    await _dialogService.DisplayAlertAsync("Alert", "Select a game level", "OK");
                }

                var navigationParams = new NavigationParameters 
                {
                    {Constants.LevelSelection, selection }
                
                };
                await _navigationService.NavigateAsync(Constants.MemoryGamePage, navigationParams);
            }
        }


        
    }
}
