namespace Common.Utils
{
    public sealed class UniversalUtils
    {
        public ConfigLoader ConfigLoader { get; private set; }
        public ResourcesLoader ResourcesLoader { get; private set; }
        public SortingTools SortingTools { get; private set; }

        public UniversalUtils()
        {
            ConfigLoader = new ConfigLoader();
            SortingTools = new SortingTools();
            ResourcesLoader = new ResourcesLoader(SortingTools);
        }
    }
}