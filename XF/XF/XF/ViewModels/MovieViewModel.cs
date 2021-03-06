﻿
using MovieSearch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Controllers;
using XF.Pages;

namespace XF.ViewModels
{
    public class MovieViewModel : INotifyPropertyChanged
	{
		public ICommand SeeCastCommand { protected set; get; }
		private INavigation _navigation;
		protected CastMember _selectedCastMember;
		private Movie _movie;
		MovieController _movieController;

		public MovieViewModel(INavigation navigation, Movie movie, MovieController movieController)
		{
			this._navigation = navigation;
			this._movieController = movieController;
			_movie = movie;

			SeeCastCommand = new Command(async () =>
			{
				await _navigation.PushAsync(new CastPage(_movieController, _movie));
			});

		}

		public Movie Movie
		{
			get { return this._movie; }
			set
			{
				this._movie = value;
				OnPropertyChanged();
			}
		}

		public int? Runtime
		{
			get { return this._movie.Runtime; }
			set
			{
				this._movie.Runtime = value;
				OnPropertyChanged();
			}
		}

		public string Tagline
		{
			get { return this._movie.Tagline; }
			set
			{
				this._movie.Tagline = value;
				OnPropertyChanged();
			}
		}

		public async Task GetAdditionalInfo()
		{
			var movieInfo = await _movieController.GetMovieByIdAsync(_movie.Id);
			Movie.Genres = _movie.GetStringedGenres();
			Runtime = movieInfo.Runtime;
			Tagline = movieInfo.Tagline;
		}

		public CastMember SelectedCastMember
		{
			get => this._selectedCastMember;
			set
			{
				if (value != null)
				{
					this._selectedCastMember = value;
					this.OnPropertyChanged();
					this._navigation.PushAsync(new CastMemberPage(this._selectedCastMember, _movieController), true);
					this._selectedCastMember = null;
					this.OnPropertyChanged();
				}
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
