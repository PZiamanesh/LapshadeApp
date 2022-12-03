﻿using _Framework.Domain;
using ShopMgmt.Domain.ProductAgg;

namespace ShopMgmt.Domain.CommentAgg;
#nullable disable

public class Comment : EntityBase<long>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Message { get; private set; }
    public bool IsConfirmed { get; private set; }
    public bool IsCanceled { get; private set; }

    public long ProductId { get; private set; }
    public Product Product { get; private set; }

    public Comment(string name, string email, string message, long productId)
    {
        Name = name;
        Email = email;
        Message = message;
        ProductId = productId;
        IsConfirmed = false;
        IsCanceled = false;
    }

    public void Confirm()
    {
        IsConfirmed = true;
    }

    public void Cancel()
    {
        IsCanceled = true;
    }
}
