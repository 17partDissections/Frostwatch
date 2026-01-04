namespace Q17pD.Frostwatch
{
    public abstract class InventoryAction
    {
        public string LocalizationKey;
        public abstract void Init(Player.PlayerItemHandler PlayerIH);
        public abstract void Act();
    }
}
