﻿using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class LocationVM
    {
        public List<Restaurant> Restaurants { get; set; }
        public CustomUser customUser { get; set; }
    }
}
