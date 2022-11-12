﻿namespace ShopMgmt.Application.Contract.ProductPicture;
#nullable disable

public record ProductPictureViewModel
{
    public long Id { get; set; }
    public string Product { get; set; }
    public string Picture { get; set; }
    public string CreationDate { get; set; }
    public long ProductId { get; set; }
}