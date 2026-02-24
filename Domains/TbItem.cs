using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

namespace Mediateur.Models;

public partial class TbItem
{
    public int ItemId { get; set; }
    [Required(ErrorMessage = "Item Name is required.")]
    [StringLength(100, ErrorMessage = "Item Name must be less than 100 characters.")]
    public string ItemName { get; set; } = null!;
    [Range(0, double.MaxValue, ErrorMessage = "Sales Price must be a non-negative number.")]
    public decimal SalesPrice { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "Purchase Price must be a non-negative number.")]
    public decimal PurchasePrice { get; set; }

    public int CategoryId { get; set; }

    public string? ImageName { get; set; }

    public DateTime CreatedDate { get; set; }
    [ValidateNever]
    public string CreatedBy { get; set; } = null!;
    [ValidateNever]
    public int CurrentState { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
    public string? Description { get; set; }

    public string? Gpu { get; set; }

    public string? HardDisk { get; set; }

    public int? ItemTypeId { get; set; }

    public string? Processor { get; set; }
    [Range(1, 1024, ErrorMessage = "RAM Size must be between 1 and 1024.")]
    public int? RamSize { get; set; }

    public string? ScreenReslution { get; set; }

    public string? ScreenSize { get; set; }

    public string? Weight { get; set; }

    public int? OsId { get; set; }

    [ValidateNever]
    public virtual TbCategory Category { get; set; } = null!;
    [ValidateNever]
    public virtual TbItemType? ItemType { get; set; }
    [ValidateNever]
    public virtual TbO? Os { get; set; }



    public virtual ICollection<TbItemImage> TbItemImages { get; set; } = new List<TbItemImage>();



   


}
