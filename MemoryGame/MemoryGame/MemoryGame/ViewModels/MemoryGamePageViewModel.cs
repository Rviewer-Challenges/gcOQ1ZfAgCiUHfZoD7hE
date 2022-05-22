using MemoryGame.ApiService;
using MemoryGame.Helpers;
using MemoryGame.Models;
using MemoryGame.Utils;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MemoryGame.ViewModels
{
    public class MemoryGamePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPokemonApiService _pokemonApiService;

        public ObservableCollection<Pokemon> ObPokemon { get; set; }
        public List<Pokemon> ListPokemon;

        private int MaxNum;
        private int MaxNumOfCards;
        public int LevelSelection;

        public MemoryGamePageViewModel(INavigationService navigationService, IPokemonApiService pokemonApiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pokemonApiService = pokemonApiService;

            ObPokemon = new ObservableCollection<Pokemon>();
        }

        //private ObservableCollection<Pokemon> _obpokemons;

        //public ObservableCollection<Pokemon> OBPokemons
        //{
        //    get { return _obpokemons; }
        //    set { _obpokemons = value; }
        //}

        private Pokemon _pokemon;
        public Pokemon pokemon
        {
            get { return _pokemon; }
            set { SetProperty(ref _pokemon, value); }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _urlimg ;
        public string UrlImg
        {
            get { return _urlimg; }
            set { SetProperty(ref _urlimg, value); }
        }

        private string _image;
        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }



        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.LevelSelection))
            {
                LevelSelection = parameters.GetValue<int>(Constants.LevelSelection);
            }

            MaxNum = (LevelSelection == 1) ? 100
                : (LevelSelection == 2) ? 200
                : (LevelSelection == 3) ? 300 : 100;

            MaxNumOfCards = (LevelSelection == 1) ? 8
                : (LevelSelection == 2) ? 12
                : (LevelSelection == 3) ? 15 : 8;


            UrlImg = "https://cdn.pixabay.com/photo/2016/08/15/00/50/pokeball-1594373_960_720.png";

            await LoadPokemons(MaxNum);

        }

        private async Task LoadPokemons(int cardLimit)
        {
            ListPokemon = new List<Pokemon>();

            var pokeList = await _pokemonApiService.GetPokemonList<PokemonList>(cardLimit);

            //add pokelist to local list
            ListPokemon.AddRange(pokeList.Pokemons);
            //shuffle complet list
            ListPokemon.Shuffle();
            //select max num of cards depens on level
            var cardlimit = ListPokemon.GetRange(0, MaxNumOfCards);
            //duplicate cardlimit
            var duplicatedLst = cardlimit.ToList();

            cardlimit.AddRange(duplicatedLst);

            cardlimit.ForEach(p => ObPokemon.Add(p)); ;
            
        }




    }
}
