namespace EventSystem
{
    public static class Events
    {
        public static readonly EventActions <bool> OnPlayerInRange = new EventActions<bool>();
        public static readonly EventActions <int> OnPlayerGetHealth = new EventActions<int>();
        public static readonly EventActions <int> OnPlayerGetDamage = new EventActions<int>();
        public static readonly EventActions OnDisplayPlayerHealth = new EventActions();
        public static readonly EventActions OnBulletCountUp = new EventActions();
    }
}