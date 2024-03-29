﻿using BurgerKingBackEnd.Entities;

namespace BurgerKingBackEnd.ViewModels
{
    public class BurgerMenuVM
    {
        public CustomUser CustomUser { get; set; } = null!;
        public List<Order> Order { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsCourier { get; set; }
        public Courier? Courier { get; set; }

    }
}
