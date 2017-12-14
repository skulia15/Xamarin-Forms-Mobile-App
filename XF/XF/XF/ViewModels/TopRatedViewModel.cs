﻿using MovieSearch;
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
	class TopRatedViewModel : ListViewModel
	{
		
		public TopRatedViewModel(MovieController movieController)
		{
			_movieController = movieController;
			Movies = new List<Movie>();
		}

		internal override async Task GetMoviesAsync()
		{
			Movies = await _movieController.GetTopRated();
			await GetCast(Movies);
		}
	}
}
