using fetchViaAPI.Utilities;
using System.Text.Json;
using System.Net.Http;
using System.Diagnostics;
using fetchViaAPI.Model;
using System.Web;

DisplayWelcomeMessage();
List<string> commands = [];

while (true) {
    Utility.PrintCommandMessage("Enter command: ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(input)) {
        RestartMessage();
        continue;
    }

    commands = Utility.ParseInput(input);

    if (commands[0] == "exit")
        break;

    if (commands.Count != 1 && (commands[0] != "exit")) {
        RestartMessage();
        continue;
    }

    if (string.IsNullOrEmpty(commands[0])) {
        Utility.PrintErrorMessage(@"Username is required. Please type <username> to get started.");
        continue;
    }

    HttpClient c = new HttpClient();
    c.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

    var zenbu = await Fetching(commands[0], c);
    var resultRisuto = new List<ActivityCertain>();

    if (zenbu.Count != 0) {
        var groups = zenbu.GroupBy(n => n.Type);

        foreach (var i in groups) {
            var result = new ActivityCertain();
            result.Type = i.Key;
            result.RepoNames = i.Select(n => n.Repo.Name).Distinct().ToList();
            result.Count = result.RepoNames.Count;
            resultRisuto.Add(result);
        }

        Console.WriteLine("\nOutput: \n");

        foreach (var i in resultRisuto) {
            switch(i.Type) {
                case "CreateEvent":
                    Console.WriteLine($"Created {i.Count} new repositories called {NameListToString(i.RepoNames)}");
                    break;

                case "PushEvent":
                    Console.WriteLine($"Pushed {i.Count} new changes in repositories {NameListToString(i.RepoNames)}");
                    break;

                case "PullRequestEvent":
                    Console.WriteLine($"Opened {i.Count} new pull requestes in repositories {NameListToString(i.RepoNames)}");
                    break;

                case "IssueCommentEvent":
                    Console.WriteLine($"Added {i.Count} new comments in repositories {NameListToString(i.RepoNames)}");
                    break;

                case "IssuesEvent":
                    Console.WriteLine($"Opened {i.Count} new issues in repositories {NameListToString(i.RepoNames)}");
                    break; 

                case "WatchEvent":
                    Console.WriteLine($"Starred {i.Count} {NameListToString(i.RepoNames)}");
                    break;

                default:
                    break;
            }
        }
    } else {
        Utility.PrintInfoMessage("None activities found for this user. Please try with some other users.");
        continue;
    }

}

static void DisplayWelcomeMessage() {
    Utility.PrintInfoMessage("Hi, welcome to fetchActivityViaAPI.");
    Utility.PrintInfoMessage(@"Please type <username> to get started.");
}

static void RestartMessage() {
    Utility.PrintErrorMessage(@"Wrong command. Please type <username> to get started.");
}

static async Task<List<ActivityAll>> Fetching(string m, HttpClient c) {
    try {
        var nameOnUrl = HttpUtility.UrlEncode(m);
        var url = $"https://api.github.com/users/{nameOnUrl}/events";

        var response = await c.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var naiyou = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<List<ActivityAll>>(naiyou);

        return result ?? [];

    } catch (Exception e) {
        Utility.PrintErrorMessage("Failed at fetching github activities.");
        throw;
    }
}

static string NameListToString(List<string> risuto) {
    if (risuto.Count == 0)
        return string.Empty;

    if (risuto.Count == 1)
        return risuto.FirstOrDefault() ?? string.Empty;

    if (risuto.Count > 1) {
        string result = string.Empty;

        for (int i = 0; i < risuto.Count - 1; i++)
            result += risuto[i] + ", ";

        result += risuto[risuto.Count - 1];

        return result;
    }

    return string.Empty;
}