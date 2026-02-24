using System;
using System.Collections.Generic;

namespace Mediateur.Models;

public partial class TbPages
{
  
    public int PageId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string? ImageName { get; set; } 

    public string? MetaKeyWork { get; set; } 

    public string? MetaDescription { get; set; } 

    public int CurrentState { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }
}
