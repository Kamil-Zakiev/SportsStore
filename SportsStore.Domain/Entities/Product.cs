﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public virtual long Id { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public virtual string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public virtual string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public virtual decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public virtual string Category { get; set; }

        public virtual byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual string ImageMimeType { get; set; }
    }
}