using System.Text.Json.Serialization;

public class ActivityAll {
    [JsonPropertyName("id")]
    public string ID {get; set;}
    
    [JsonPropertyName("type")]
    public string Type {get; set;}

    [JsonPropertyName("actor")]
    public Actor Actor {get; set;}

    [JsonPropertyName("repo")]
    public Repository Repo {get; set;}

    [JsonPropertyName("payload")]
    public Payload Payload {get; set;}

    [JsonPropertyName("public")]
    public bool Public {get; set;}

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt {get; set;}
}

public class Actor {
    [JsonPropertyName("id")]
    public int ID {get; set;}

    [JsonPropertyName("login")]
    public string Login {get; set;}

    [JsonPropertyName("display_login")]
    public string DisplayLogin {get; set;}

    [JsonPropertyName("gravatar_id")]
    public string GravatarID {get; set;}

    [JsonPropertyName("url")]
    public string Url {get; set;}

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl {get; set;}
}

public class Repository {
    [JsonPropertyName("id")]
    public int ID {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("url")]
    public string Url {get; set;}
}

public class Payload {
    [JsonPropertyName("action")]
    public string Action {get; set;}
    
}