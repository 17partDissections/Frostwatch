namespace Q17pD.Frostwatch.Inventory
{
    public abstract class InventoryAction
    {
        public string LocalizationKey;
        public bool IsCustomConditionSatisfied = true;
        public abstract void Init(Player.Player player, AudioHandler audioHandler);
        public abstract void Act();
    }
}
