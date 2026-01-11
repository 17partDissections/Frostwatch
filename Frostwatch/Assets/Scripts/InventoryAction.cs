namespace Q17pD.Frostwatch
{
    public abstract class InventoryAction
    {
        public string LocalizationKey;
        public abstract void Init(Player.Player player);
        public abstract void Act();
    }
}
