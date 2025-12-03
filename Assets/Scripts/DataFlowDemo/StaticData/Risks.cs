namespace DataFlowDemo
{
    public record Risk(string Name, string Neutralizer)
    {
        public string Name { get; } = Name;
        public string Neutralizer { get; } = Neutralizer;
    }
}
