namespace AuthService.UseCases
{
    public class Actor
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public Actor(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
