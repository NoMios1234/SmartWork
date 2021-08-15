﻿using System.ComponentModel.DataAnnotations;

namespace SmartWork.Core.ViewModels.SubscribeDetailViewModel
{
    public class SubscribeDetailViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Subscribe name")]
        public string SubscribeName { get; set; }

        [Display(Name = "Subscribe price")]
        public uint SubscribePrice { get; set; }

        [Display(Name = "Subscribe description")]
        public string SubscribeDescription { get; set; }

        [Display(Name = "Office id")]
        public int OfficeId { get; set; }
    }
}
