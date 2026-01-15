namespace Q17pD.Frostwatch
{
    public abstract class InventoryAction
    {
        public string LocalizationKey;
        public abstract void Init(Player.Player player, AudioHandler audioHandler);
        public abstract void Act();
    }
}
