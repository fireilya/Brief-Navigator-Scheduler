namespace DataFlowDemo
{
    public record Task(string Name, int Target, SubTask[] Subtasks)
    {
        public string Name { get; } = Name;
        public int Target { get; } = Target;
        public SubTask[] Subtasks { get; } = Subtasks;
    }

    public record SubTask(string Name, Tool Tool) 
    {
        public string Name { get; } = Name;
        public Tool Tool { get; } = Tool;
    }
}