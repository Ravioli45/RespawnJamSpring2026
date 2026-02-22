using Godot;
using System;

public partial class Shop : Control
{
    [Signal]
    public delegate void ShopClosedEventHandler();

    [Export]
    private AnimatedSprite2D ShopSprite;

    [Export]
    private Container ShopItemContainer;
    [Export]
    private PackedScene ShopItem;

    public void SpawnShopItems()
    {
        for (int i = 0; i < 3; i++)
        {
            BuffIcon item = ShopItem.Instantiate<BuffIcon>();
            item.BuffIconClicked += OnItemPurchase;
            ShopItemContainer.AddChild(item);
        }
    }

    public void CloseShop()
    {
        foreach (Node child in ShopItemContainer.GetChildren())
        {
            if (child is BuffIcon b)
            {
                b.BuffIconClicked -= OnItemPurchase;
            }
            child.QueueFree();
        }
        Visible = false;

        EmitSignalShopClosed();
    }

    public void OnVisibilityChanged()
    {
        if (Visible)
        {
            ShopSprite.Play("Open");
        }
    }

    public void OnOpenAnimationFinish()
    {
        SpawnShopItems();
    }

    public void OnItemPurchase()
    {
        CloseShop();
    }
}
