using Bogus;
using budaevaler.Interview.Task2;

var closedList = new LinkedClosedList<string>(GetNouns(10));
closedList.HeadReached += (s, e) => Console.WriteLine($"Head reached! Head: {e}");
while (!Console.KeyAvailable)
{
    await Task.Delay(1000);
    Console.WriteLine($"Previous: {closedList.Previous}, Current: {closedList.Current}, Next: {closedList.Next}");
    closedList.MoveBack();
}

static IEnumerable<string> GetNouns(int count)
{
    var faker = new Faker();
    return Enumerable.Range(1, count)
        .Select(i => faker.Hacker.Noun());
}
