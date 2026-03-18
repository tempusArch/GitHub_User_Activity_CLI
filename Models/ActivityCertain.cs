namespace fetchViaAPI.Model {
    public class ActivityCertain {
        public string Type {get; set;} = string.Empty;

        public int Count {get; set;}

        public List<string> RepoNames {get; set;} = new();
    }
}